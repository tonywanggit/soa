using ESB.Core.Configuration;
using ESB.Core.Registry;
using ESB.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Registry.WindowsService
{
    /// <summary>
    /// 消息处理器
    /// </summary>
    public class MessageProcessor
    {
        private RegistryCenter m_RegistryCenter = null;

        public MessageProcessor(RegistryCenter regCenter)
        {
            m_RegistryCenter = regCenter;
        }

        /// <summary>
        /// 增对不同的客户端和消息类型进行处理
        /// </summary>
        /// <param name="regClient"></param>
        /// <param name="regMessage"></param>
        public void Process(RegistryClient regClient, RegistryMessage regMessage)
        {
            if (regMessage.ClientType == RegistryClientType.Consumer && regMessage.Action == RegistryMessageAction.Hello)
            {
                ConsumerConfig consumerConfig = XmlUtil.LoadObjFromXML<ConsumerConfig>(regMessage.MessageBody);
                ESBConfig esbConfig = GetESBConfig(consumerConfig);
                m_RegistryCenter.SendData(regClient, RegistryMessageAction.ServiceConfig, esbConfig.ToXml());
            }
        }

        /// <summary>
        /// 获取到ESBConfig
        /// </summary>
        /// <returns></returns>
        private ESBConfig GetESBConfig(ConsumerConfig consumerConfig)
        {
            ESBConfig esbConfig = new ESBConfig();
            esbConfig.Service.Add(new ServiceItem() { ServiceName = "ESB_ASHX", DirectInvokeEnabled = true, Uri = "http://esb.jn.com" });


            return esbConfig;
        }
    }
}
