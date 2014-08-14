using ESB.Core.Configuration;
using ESB.Core.Registry;
using ESB.Core.Util;
using ESB.Core.Entity;
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
            esbConfig.Monitor.Add(new MonitorItem() { Uri = "192.168.56.2:5672:soa:123456", Load = 1, Type = "RabbitMQ" });
            //esbConfig.Monitor.Add(new MonitorItem() { Uri = "10.100.20.100:5672:admin:osroot", Load = 1, Type = "RabbitMQ" });
            esbConfig.Registry.Add(new RegistryItem() { Uri = "192.168.56.2", Load = 1 });
            esbConfig.CallCenter.Add(new CallCenterItem() { Uri = "192.168.56.2", Load = 1 });

            //esbConfig.Service.Add(new ServiceItem() { ServiceName = "ESB_ASHX", DirectInvokeEnabled = true, Uri = "http://esb.jn.com" });

            foreach (var refService in consumerConfig.Reference)
            {
                BusinessService bs = BusinessService.FindByServiceName(refService.ServiceName);
                if (bs != null)
                {
                    ServiceItem si = new ServiceItem();
                    si.ServiceName = bs.ServiceName;
                    si.Binding = bs.Binding;

                    esbConfig.Service.Add(si);
                }
            }

            return esbConfig;
        }
    }
}
