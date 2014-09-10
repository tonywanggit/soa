using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ESB.Core.Configuration;
using ESB.Core.Registry;
using ESB.Core.Rpc;
using NewLife.Reflection;
using NewLife.Log;
using System.Net;
using ESB.Core.Monitor;
using NewLife.Threading;
using System.Diagnostics;

namespace ESB.Core
{
    /// <summary>
    /// ESB代理类：以单例的形式对外部提供服务
    /// </summary>
    public class ESBProxy
    {
        #region 单例模式
        private static ESBProxy m_Instance = null;

        public static ESBProxy GetInstance()
        {
            if (m_Instance != null) return m_Instance;

            //--此处可以降低第一次调用的时间：2~3秒减少到200ms左右
            HttpWebRequest.DefaultWebProxy = null;
            //HttpWebRequest.DefaultCachePolicy = null;

            //--客户端的连接数
            //--http://www.cnblogs.com/summer_adai/archive/2013/04/26/3045274.html
            ServicePointManager.DefaultConnectionLimit = 10000;

            //--创建客户端代理
            ESBProxy proxy = new ESBProxy();
            Interlocked.CompareExchange<ESBProxy>(ref m_Instance, proxy, null);

            //--初始化客户端代理
            proxy.Init();

            return m_Instance;
        }
        #endregion

        #region 配置文件
        private ConsumerConfig m_ConsumerConfig = null;
        /// <summary>
        /// 消费者配置文件
        /// </summary>
        internal ConsumerConfig ConsumerConfig
        {
            get { return m_ConsumerConfig; }
        }

        private object m_ESBConfigLock = new Object();
        private ESBConfig m_ESBConfig = null;
        /// <summary>
        /// 消费者配置文件
        /// </summary>
        internal ESBConfig ESBConfig
        {
            get { return m_ESBConfig; }
            set {
                lock (m_ESBConfigLock)
                {
                    m_ESBConfig = value;
                }
            }
        }

        /// <summary>
        /// 配置文件管理
        /// </summary>
        private ConfigurationManager m_ConfigurationManager = null;

        /// <summary>
        /// 加载配置文件：加载本地配置文件ConsumerConfig->ESBConfig
        /// </summary>
        private void LoadConfig()
        {
            m_ConfigurationManager = ConfigurationManager.GetInstance();
            m_ConsumerConfig = m_ConfigurationManager.LoadConsumerConfig();
            m_ESBConfig = m_ConfigurationManager.LoadESBConfig();

            if (m_ConsumerConfig == null)
                throw new Exception("缺少有效的消费者配置文件ConsumerConfig.xml。");

            if (m_ESBConfig == null)
                Status = ESBProxyStatus.LostESBConfig;
            else
                Status = ESBProxyStatus.Ready;
        }
        #endregion

        #region 构造函数、注册中心、队列中心
        /// <summary>
        /// 注册中心客户端
        /// </summary>
        private RegistryConsumerClient m_RegistryClient = null;
        /// <summary>
        /// 注册中心客户端
        /// </summary>
        public RegistryConsumerClient RegistryConsumerClient
        {
            get
            {
                return m_RegistryClient;
            }
        }

        /// <summary>
        /// 监控中心客户端
        /// </summary>
        private MessageQueueClient m_MessageQueueClient = null;
        /// <summary>
        /// 对内部类公开消息队列客户端
        /// </summary>
        internal MessageQueueClient MessageQueueClient
        {
            get { return m_MessageQueueClient; }
        }

        /// <summary>
        /// ESBProxy代理类的版本
        /// </summary>
        internal String Version { get; set; }

        /// <summary>
        /// ESBProxy状态枚举值
        /// </summary>
        internal enum ESBProxyStatus
        {
            /// <summary>
            /// 正在初始化
            /// </summary>
            Init,
            /// <summary>
            /// 已经就绪可以调用
            /// </summary>
            Ready,
            /// <summary>
            /// 缺少消费者配置文件
            /// </summary>
            LostConsumerConfig,
            /// <summary>
            /// 缺少ESB配置文件
            /// </summary>
            LostESBConfig
        }
        /// <summary>
        /// ESBProxy状态
        /// </summary>
        internal ESBProxyStatus Status { get; set; }

        /// <summary>
        /// ESBProxy构造函数
        /// </summary>
        private ESBProxy()
        {
        }

        /// <summary>
        /// 标识ESBProxy是否初始化过
        /// </summary>
        private Int32 m_Inited = 0;

        /// <summary>
        /// 初始化客户端代理
        /// </summary>
        private void Init()
        {
            if (m_Inited > 0 || Interlocked.CompareExchange(ref m_Inited, 1, 0) > 0) return;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //--STEP.1.记录客户端版本信息
            Status = ESBProxyStatus.Init;
            var asm = AssemblyX.Create(System.Reflection.Assembly.GetExecutingAssembly());
            Version = String.Format("{0} v{1} Build {2:yyyy-MM-dd HH:mm:ss}", asm.Name, asm.FileVersion, asm.Compile);
            XTrace.WriteLine(Version);

            //--STEP.2.加载配置文件
            LoadConfig();

            //--STEP.3.连接注册中心
            m_RegistryClient = new RegistryConsumerClient(this);
            m_RegistryClient.Connect();

            //--STEP.4.连接队列中心
            m_MessageQueueClient = new MessageQueueClient(this);
            m_MessageQueueClient.ConnectAsync();


            stopWatch.Stop();
            XTrace.WriteLine("ESBProxy Init 耗时：{0}ms。", stopWatch.ElapsedMilliseconds); ;
        }
        #endregion

        #region 对外公开的调用接口
        /// <summary>
        /// 请求响应调用
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="message">消息内容</param>
        /// <param name="version">服务版本：0代表调用默认版本</param>
        /// <returns></returns>
        public String Invoke(String serviceName, String methodName, String message, Int32 version = 0)
        {
            DateTime reqStartTime = DateTime.Now;

            ESB.Core.Schema.服务请求 req = new ESB.Core.Schema.服务请求();
            req.服务名称 = serviceName;
            req.方法名称 = methodName;
            req.请求时间 = reqStartTime;
            req.主机名称 = m_ConsumerConfig.ApplicationName;
            req.消息内容 = message;
            req.消息编码 = "";
            req.密码 = "";

            Boolean getSyncESBConfig = false;
            if (ESBConfig == null)
            {
                SyncESBConfig(serviceName);
                getSyncESBConfig = true;
            }

            if (ESBConfig == null)
                throw new Exception("无法获取到有效的配置文件");

            //--从ESBConfig中获取到服务版本信息
            ServiceItem si = this.ESBConfig.GetInvokeServiceItem(serviceName, version);
            if (si == null)
            {
                if (getSyncESBConfig)//--如果已经获取过ESBConfig文件则直接抛出异常
                {
                    m_ConfigurationManager.RemoveReference(serviceName, m_ConsumerConfig);
                    throw new Exception(String.Format("请求的服务【{0}】的【{1}】版本没有注册或者已经废弃!", serviceName, version == 0 ? "默认" : version.ToString()));
                }
                else
                {
                    SyncESBConfig(serviceName);
                    si = this.ESBConfig.GetInvokeServiceItem(serviceName, version);

                    if (si == null)
                    {
                        m_ConfigurationManager.RemoveReference(serviceName, m_ConsumerConfig);
                        throw new Exception(String.Format("请求的服务【{0}】的【{1}】版本没有注册或者已经废弃!", serviceName, version == 0 ? "默认" : version.ToString()));
                    }
                }
            }

            if(si.Binding == null || si.Binding.Count == 0)
                throw new Exception(String.Format("请求的服务【{0}】没有有效的绑定地址!", serviceName));


            //Console.WriteLine("DynamicalCallWebService 开始：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            String msg = EsbClient.DynamicalCallWebService(true, req, si.Binding, si.Version).消息内容;
            //Console.WriteLine("DynamicalCallWebService 完成：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return msg;
        }

        /// <summary>
        /// 同步服务配置
        /// </summary>
        /// <param name="serviceName"></param>
        private void SyncESBConfig(String serviceName)
        {
            //--如果没有服务,先查看引用中是否存在
            if (m_ConsumerConfig.Reference.Find(x => x.ServiceName == serviceName) == null)
            {
                m_ConsumerConfig.Reference.Add(new ReferenceItem() { ServiceName = serviceName });
                ThreadPoolX.QueueUserWorkItem(x =>
                {
                    m_ConfigurationManager.SaveConsumerConfig(m_ConsumerConfig);
                });
            }

            //--采用同步机制到注册中心获取到服务配置信息
            m_RegistryClient.SyncESBConfig();
        }

        /// <summary>
        /// 检测ESB是否达到可用状态
        /// </summary>
        private void InitCheck()
        {
            if (Status == ESBProxyStatus.Ready)
                return;

            if (Status == ESBProxyStatus.Init)
                throw new Exception("ESBProxy处于初始化状态。");

            if (Status == ESBProxyStatus.LostESBConfig)
                throw new Exception("缺少配置文件ESBConfig.xml。");

            if (Status != ESBProxyStatus.Ready)
                throw new Exception("ESBProxy没有达到可用状态");
        }
        #endregion
    }
}
