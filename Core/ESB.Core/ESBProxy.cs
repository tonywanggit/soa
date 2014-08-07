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
        private ESBConfig m_EsbConfig = null;

        private RegistryClient m_RegistryClient = null;

        private ESBProxy()
        {
            m_ConsumerConfig = new ConsumerConfig();
            m_RegistryClient = new RegistryClient();
            m_RegistryClient.RegistryNotify += new EventHandler<RegistryEventArgs>(m_RegistryClient_RegistryNotify);
            m_EsbConfig = m_RegistryClient.ConnectTo("localhost:8080");
        }

        /// <summary>
        /// 当注册中心发生变化时进行本地化操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_RegistryClient_RegistryNotify(object sender, RegistryEventArgs e)
        {
            m_EsbConfig = e.ESBConfig;
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
