using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ESB.Core.Util;
using ESB.Core.Entity;

namespace ESB.Core.Configuration
{   
    /// <summary>
    /// ESB配置文件
    /// </summary>
    public class ESBConfig
    {
        /// <summary>
        /// 服务
        /// </summary>
        public List<ServiceItem> Service { get; set; }
        /// <summary>
        /// 注册中心
        /// </summary>
        public List<RegistryItem> Registry { get; set; }
        /// <summary>
        /// 调用中心
        /// </summary>
        public List<CallCenterItem> CallCenter { get; set; }
        /// <summary>
        /// 监控中心
        /// </summary>
        public List<MonitorItem> Monitor { get; set; }
        /// <summary>
        /// 队列中心
        /// </summary>
        public List<MessageQueueItem> MessageQueue { get; set; }
        /// <summary>
        /// 缓存中心
        /// </summary>
        public List<CacheItem> Cache { get; set; }

        /// <summary>
        /// 构造器
        /// </summary>
        public ESBConfig()
        {
            Service = new List<ServiceItem>();
            Registry = new List<RegistryItem>();
            CallCenter = new List<CallCenterItem>();
            Monitor = new List<MonitorItem>();
            MessageQueue = new List<MessageQueueItem>();
            Cache = new List<CacheItem>();
        }

        /// <summary>
        /// 获取到指定的服务版本
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public ServiceItem GetInvokeServiceItem(String serviceName, Int32 version)
        {
            List<ServiceItem> lstServiceItem = Service.FindAll(x => x.ServiceName == serviceName);

            if (lstServiceItem == null || lstServiceItem.Count == 0) return null;

            if (version < 1)//--所有小于1的版本调用都认为是调用默认版本
                return lstServiceItem.Find(x => x.IsDefault);
            else
                return lstServiceItem.Find(x => x.Version == version);
        }

        /// <summary>
        /// 将配置对象序列化成XML
        /// </summary>
        /// <returns></returns>
        public String ToXml()
        {
            return XmlUtil.SaveXmlFromObj<ESBConfig>(this);
        }
    }

    /// <summary>
    /// 服务配置项
    /// </summary>
    public class ServiceItem
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public String ServiceName { get; set; }

        /// <summary>
        /// 服务版本
        /// </summary>
        public Int32 Version { get; set; }

        /// <summary>
        /// 是否为默认版本
        /// </summary>
        public Boolean IsDefault { get; set; }

        private List<BindingTemplate> m_Binding = new List<BindingTemplate>();
        /// <summary>
        /// 服务的绑定地址
        /// </summary>
        public List<BindingTemplate> Binding
        {
            get { return m_Binding; }
            set { m_Binding = value; }
        }
    }

    /// <summary>
    /// 注册中心配置项
    /// </summary>
    public class RegistryItem
    {
        /// <summary>
        /// 注册中心地址
        /// </summary>
        public String Uri { get; set; }
        /// <summary>
        /// 负载：用注册中心的客户端连接数表示
        /// </summary>
        public Int32 Load { get; set; }
    }

    /// <summary>
    /// 调用中心配置项
    /// </summary>
    public class CallCenterItem
    {
        /// <summary>
        /// 调用中心地址
        /// </summary>
        public String Uri { get; set; }
        /// <summary>
        /// 负载：用调用中心的流量表示
        /// </summary>
        public Int32 Load { get; set; }
    }

    /// <summary>
    /// 监控中心配置项
    /// </summary>
    public class MonitorItem
    {
        /// <summary>
        /// 监控中心地址
        /// </summary>
        public String Uri { get; set; }
        /// <summary>
        /// 负载：用监控中心的消息队列长度进行表示
        /// </summary>
        public Int32 Load { get; set; }
    }

    /// <summary>
    /// 队列中心配置项
    /// </summary>
    public class MessageQueueItem
    {        
        /// <summary>
        /// 队列中心地址
        /// </summary>
        public String Uri { get; set; }
        /// <summary>
        /// 负载：用队列中心的消息队列长度进行表示
        /// </summary>
        public Int32 Load { get; set; }
    }

    /// <summary>
    /// 缓存中心配置项
    /// </summary>
    public class CacheItem
    {
        /// <summary>
        /// 缓存中心地址
        /// </summary>
        public String Uri { get; set; }
        /// <summary>
        /// 负载：用缓存中心的消息队列长度进行表示
        /// </summary>
        public Int32 Load { get; set; }
    }
}
