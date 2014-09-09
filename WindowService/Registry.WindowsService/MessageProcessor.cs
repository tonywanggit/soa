using ESB.Core.Configuration;
using ESB.Core.Registry;
using ESB.Core.Util;
using ESB.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCode;
using ESB.Core.Rpc;

namespace Registry.WindowsService
{
    /// <summary>
    /// 消息处理器
    /// </summary>
    public class MessageProcessor
    {
        /// <summary>
        /// 注册中心
        /// </summary>
        private RegistryCenter m_RegistryCenter = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regCenter">注册中心</param>
        public MessageProcessor(RegistryCenter regCenter)
        {
            m_RegistryCenter = regCenter;
        }

        /// <summary>
        /// 增对不同的客户端和消息类型进行处理
        /// </summary>
        /// <param name="regClient"></param>
        /// <param name="regMessage"></param>
        public void Process(RegistryClient regClient, CometMessage regMessage)
        {
            if (regMessage.Action == CometMessageAction.Hello)
            {
                ConsumerConfig consumerConfig = XmlUtil.LoadObjFromXML<ConsumerConfig>(regMessage.MessageBody);
                regClient.ConsumerConfig = consumerConfig;

                ESBConfig esbConfig = GetESBConfig(regClient);
                m_RegistryCenter.SendData(regClient, CometMessageAction.ServiceConfig, esbConfig.ToXml(), regMessage.IsAsync);
            }
            else if (regMessage.Action == CometMessageAction.ListRegistryClient)
            {
                String message = XmlUtil.SaveXmlFromObj<List<RegistryClient>>(m_RegistryCenter.RegistryClients);
                m_RegistryCenter.SendData(regClient, CometMessageAction.ListRegistryClient, message, regMessage.IsAsync);
            }
            else if (regMessage.Action == CometMessageAction.ResendConfig)//--管理中心向每个客户端发送配置文件
            {
                foreach (var item in m_RegistryCenter.RegistryClients)
                {
                    if (item.RegistryClientType == CometClientType.Consumer
                        || item.RegistryClientType == CometClientType.CallCenter
                        || item.RegistryClientType == CometClientType.Portal)
                    {
                        ESBConfig esbConfig = GetESBConfig(item);
                        m_RegistryCenter.SendData(item, CometMessageAction.ServiceConfig, esbConfig.ToXml());
                    }
                }
            }
        }

        /// <summary>
        /// 获取到ESBConfig
        /// </summary>
        /// <returns></returns>
        private ESBConfig GetESBConfig(RegistryClient regClient)
        {
            ConsumerConfig consumerConfig = regClient.ConsumerConfig;
            ESBConfig esbConfig = new ESBConfig();
            //esbConfig.Monitor.Add(new MonitorItem() { Uri = "192.168.56.2:5672:soa:123456", Load = 1, Type = "RabbitMQ" });
            //esbConfig.Registry.Add(new RegistryItem() { Uri = "192.168.56.2", Load = 1 });
            //esbConfig.CallCenter.Add(new CallCenterItem() { Uri = "192.168.56.2", Load = 1 });

            foreach (SettingUri uri in SettingUri.FindAll())
            {
                switch (uri.UriType)
                {
                    case 0: //--注册中心
                        esbConfig.Registry.Add(new RegistryItem() { Uri = String.Format("{0}:{1}", uri.Uri, uri.Port), Load = 1 });
                        break;
                    case 1:
                        esbConfig.Monitor.Add(new MonitorItem() { Uri = String.Format("{0}:{1}:{2}:{3}", uri.Uri, uri.Port, uri.UserName, uri.PassWord), Load = 1, Type = "RabbitMQ" });
                        break;
                    case 2:
                        esbConfig.CallCenter.Add(new CallCenterItem() { Uri = uri.Uri, Load = 1 });
                        break;
                    default:
                        break;
                }
            }

            //esbConfig.Monitor.Add(new MonitorItem() { Uri = "10.100.20.100:5672:admin:osroot", Load = 1, Type = "RabbitMQ" });
            //esbConfig.Registry.Add(new RegistryItem() { Uri = "10.100.20.214", Load = 1 });
            //esbConfig.CallCenter.Add(new CallCenterItem() { Uri = "10.100.20.214", Load = 1 });

            //esbConfig.Service.Add(new ServiceItem() { ServiceName = "ESB_ASHX", DirectInvokeEnabled = true, Uri = "http://esb.jn.com" });

            if (regClient.RegistryClientType == CometClientType.Consumer || regClient.RegistryClientType == CometClientType.Portal)
            {
                foreach (var refService in consumerConfig.Reference)
                {

                    List<EsbView_ServiceVersion> lstBS = EsbView_ServiceVersion.FindAllPublish();
                    foreach(EsbView_ServiceVersion bs in lstBS.Where(x=>x.ServiceName == refService.ServiceName))
                    {
                        ServiceItem si = new ServiceItem();
                        si.ServiceName = bs.ServiceName;
                        si.Version = bs.BigVer;
                        si.Binding = bs.Binding;
                        si.IsDefault = (bs.BigVer == bs.DefaultVersion);

                        esbConfig.Service.Add(si);
                    }
                }
            }
            else if(regClient.RegistryClientType == CometClientType.CallCenter)
            {
                EntityList<EsbView_ServiceVersion> lstBS = EsbView_ServiceVersion.FindAllPublish();
                foreach (var bs in lstBS)
                {
                    ServiceItem si = new ServiceItem();
                    si.ServiceName = bs.ServiceName;
                    si.Version = bs.BigVer;
                    si.Binding = bs.Binding;
                    si.IsDefault = (bs.BigVer == bs.DefaultVersion);

                    esbConfig.Service.Add(si);
                }
            }

            return esbConfig;
        }
    }
}
