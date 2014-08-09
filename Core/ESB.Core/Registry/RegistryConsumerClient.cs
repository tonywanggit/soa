using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core.Configuration;
using System.Net;
using System.IO;
using ESB.Core.Util;
using NewLife.Threading;

namespace ESB.Core.Registry
{
    /// <summary>
    /// 注册中心客户端，负责和注册中心保持联系
    /// </summary>
    public class RegistryConsumerClient
    {
        private CometClient m_CometClient = null;
        private ESBProxy m_ESBProxy = null;
        private ConfigurationManager m_ConfigurationManager = null;

        /// <summary>
        /// 注册中心消费者客户端
        /// </summary>
        /// <param name="esbProxy"></param>
        public RegistryConsumerClient(ESBProxy esbProxy)
        {
            m_ESBProxy = esbProxy;
            m_ConfigurationManager = ConfigurationManager.GetInstance();
        }

        /// <summary>
        /// 连接到注册中心
        /// </summary>
        /// <returns></returns>
        public void Connect()
        {
            String uri = m_ESBProxy.ConsumerConfig.Registry[0].Uri;

            if(m_ESBProxy.ConsumerConfig.ApplicationName == "ESB_CallCenter")
                m_CometClient = new CometClient(uri, RegistryClientType.CallCenter);
            else
                m_CometClient = new CometClient(uri, RegistryClientType.Consumer);

            m_CometClient.OnReceiveNotify += m_CometClient_OnReceiveNotify;
            m_CometClient.Connect();
        }

        /// <summary>
        /// 接收注册中心的消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_CometClient_OnReceiveNotify(object sender, CometEventArgs e)
        {
            if (e.Type == CometEventType.ReceiveMessage)    // 接收到来自服务器的配置信息
            {
                m_ESBProxy.ESBConfig = XmlUtil.LoadObjFromXML<ESBConfig>(e.Response);
                ThreadPoolX.QueueUserWorkItem(x =>
                {
                    m_ConfigurationManager.SaveESBConfig(m_ESBProxy.ESBConfig);
                });
            }
            else if (e.Type == CometEventType.Connected)   // 当和服务器取得联系时发送消费者配置文件到服务端
            {
                m_CometClient.SendData(RegistryMessageAction.Hello, m_ESBProxy.ConsumerConfig.ToXml());
            }
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
