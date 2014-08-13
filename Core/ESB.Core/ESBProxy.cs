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

            ESBProxy proxy = new ESBProxy();
            Interlocked.CompareExchange<ESBProxy>(ref m_Instance, proxy, null);

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

        #region 构造函数和注册中心
        /// <summary>
        /// 注册中心客户端
        /// </summary>
        private RegistryConsumerClient m_RegistryClient = null;

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
            Status = ESBProxyStatus.Init;
            var asm = AssemblyX.Create(System.Reflection.Assembly.GetExecutingAssembly());
            XTrace.WriteLine("{0} v{1} Build {2:yyyy-MM-dd HH:mm:ss}", asm.Name, asm.FileVersion, asm.Compile);

            LoadConfig();
            m_RegistryClient = new RegistryConsumerClient(this);
            m_RegistryClient.Connect();
        }
        #endregion

        #region 对外公开的调用接口
        /// <summary>
        /// 请求响应端口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public String ReceiveRequest(String serviceName, String methodName, String message)
        {
            InitCheck();

            ESB.Core.Schema.服务请求 req = new ESB.Core.Schema.服务请求();
            req.服务名称 = serviceName;
            req.方法名称 = methodName;
            req.请求时间 = DateTime.Now;
            req.主机名称 = m_ConsumerConfig.ApplicationName;
            req.消息内容 = message;
            req.消息编码 = "";
            req.密码 = "";

            ServiceItem si = ESBConfig.Service.Find(x=>x.ServiceName == serviceName);
            if(si == null)
                throw new Exception(String.Format("请求的服务【{0}】没有注册!", serviceName));

            if(si.Binding == null || si.Binding.Count == 0)
                throw new Exception(String.Format("请求的服务【{0}】没有有效的绑定地址!", serviceName));

            //Console.WriteLine();

            Console.WriteLine("DynamicalCallWebService Start：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            String msg = EsbClient.DynamicalCallWebService(true, req, si.Binding).消息内容;
            Console.WriteLine("DynamicalCallWebService Start：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return msg;
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
