using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ESB.Core.Configuration;
using ESB.Core.Registry;
using ESB.Core.Rpc;

namespace ESB.Core
{
    public class ESBProxy
    {
        private static ESBProxy m_Instance = null;

        public static ESBProxy GetInstance()
        {
            if (m_Instance != null) return m_Instance;

            ESBProxy proxy = new ESBProxy();
            Interlocked.CompareExchange<ESBProxy>(ref m_Instance, proxy, null);
            return m_Instance;
        }

        private ConsumerConfig m_ConsumerConfig = null;
        /// <summary>
        /// 消费者配置文件
        /// </summary>
        public ConsumerConfig ConsumerConfig
        {
            get { return m_ConsumerConfig; }
        }
        private ESBConfig m_ESBConfig = null;
        /// <summary>
        /// 消费者配置文件
        /// </summary>
        public ESBConfig ESBConfig
        {
            get { return m_ESBConfig; }
            set {
                lock (m_ESBConfig)
                {
                    m_ESBConfig = value;
                }
            }
        }
        /// <summary>
        /// 注册中心客户端
        /// </summary>
        private RegistryConsumerClient m_RegistryClient = null;

        /// <summary>
        /// ESBProxy构造函数
        /// </summary>
        private ESBProxy()
        {
            LoadConfig();
            m_RegistryClient = new RegistryConsumerClient(this);
            m_RegistryClient.Connect();
        }

        /// <summary>
        /// 加载配置文件：加载本地配置文件ConsumerConfig->ESBConfig
        /// </summary>
        private void LoadConfig()
        {
            m_ConsumerConfig = new ConsumerConfig();
            m_ConsumerConfig.ApplicationName = "xxx";
            m_ConsumerConfig.Registry.Add(new RegistryItem() { Uri = "192.168.156.138:5555", Load = 1 });
            m_ConsumerConfig.Reference.Add(new ReferenceItem() { ServiceName = "ESB_ASHX", Interface = "" }); 

            m_ESBConfig = new ESBConfig();
            m_ESBConfig.Registry.Add(new RegistryItem() { Uri = "", Load = 1 });
        }

        /// <summary>
        /// 请求响应端口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public String ReceiveRequest(String serviceName, String methodName, String message)
        {
            ESB.Core.Schema.服务请求 req = new ESB.Core.Schema.服务请求();
            req.服务名称 = serviceName;
            req.方法名称 = methodName;
            req.请求时间 = DateTime.Now;
            req.主机名称 = m_ConsumerConfig.ApplicationName;
            req.消息内容 = message;
            req.消息编码 = "";
            req.密码 = "";

            return EsbClient.DynamicalCallWebService(true, req).消息内容;
        }


    }
}
