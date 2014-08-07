using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core.Configuration;
using System.Net;
using System.IO;
using ESB.Core.Util;

namespace ESB.Core.Registry
{
    /// <summary>
    /// 注册中心客户端，负责和注册中心保持联系
    /// </summary>
    internal class RegistryClient
    {
        private String m_SessionId = String.Empty;
        private Int32 m_Status = 0;
        private CometClient m_CometClient = null;
        public event EventHandler<RegistryEventArgs> RegistryNotify = null;

        public RegistryClient()
        {
            m_SessionId = String.Format("RC_{0}", Guid.NewGuid().ToString());
        }

        /// <summary>
        /// 连接到注册中心
        /// </summary>
        /// <returns></returns>
        public ESBConfig ConnectTo(String registryHost)
        {
            ESBConfig esbConfig = GetESBConfig(registryHost);
            if (esbConfig == null)
                throw new Exception("无法获取到ESB配置文件");

            String cometUri = String.Format("http://{0}/RegistryCenter/RegistryAsyncCenter.ashx?sessionId={1}", registryHost, m_SessionId);
            m_CometClient = new CometClient(cometUri);
            m_CometClient.ReceiveNotify += new EventHandler<CometEventArgs>(m_CometClient_ReceiveNotify);
            m_CometClient.Connect();

            return esbConfig;
        }

        /// <summary>
        /// 当注册中心发生变化时通知到EsbProxy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_CometClient_ReceiveNotify(object sender, CometEventArgs e)
        {
            if (RegistryNotify != null)
            {
                RegistryEventArgs eventArgs = new RegistryEventArgs();
                eventArgs.ESBConfig = XmlUtil.LoadObjFromXML<ESBConfig>(e.Response);

                RegistryNotify(this, eventArgs);
            }
        }

        /// <summary>
        /// 获取到ESB的配置文件
        /// </summary>
        /// <param name="registryHost"></param>
        /// <returns></returns>
        private ESBConfig GetESBConfig(String registryHost)
        {
            String uri = String.Format("http://{0}/RegistryCenter/RegistryHandler.ashx?sessionId={1}&Action=Full", registryHost, m_SessionId);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "GET";

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            String reponse = String.Empty;
            using (Stream newstream = webResponse.GetResponseStream())
            {
                using (StreamReader srRead = new StreamReader(newstream, System.Text.Encoding.Default))
                {
                    reponse = srRead.ReadToEnd();
                    srRead.Close();
                }
            }

            return XmlUtil.LoadObjFromXML<ESBConfig>(reponse);
        }
    }

    /// <summary>
    /// 注册中心变化事件
    /// </summary>
    internal class RegistryEventArgs : EventArgs
    {
        public ESBConfig ESBConfig { get; set; }
    }
}
