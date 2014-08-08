using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ESB.Core.Util;

namespace ESB.Core.Configuration
{   
    /// <summary>
    /// ESB配置文件
    /// </summary>
    public class ESBConfig
    {
        public List<ServiceItem> Service { get; set; }
        public List<RegistryItem> Registry { get; set; }
        public List<CallCenterItem> CallCenter { get; set; }
        public List<MonitorItem> Monitor { get; set; }

        public ESBConfig()
        {
            Service = new List<ServiceItem>();
            Registry = new List<RegistryItem>();
            CallCenter = new List<CallCenterItem>();
            Monitor = new List<MonitorItem>();
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
        public String ServiceName { get; set; }
        public String Uri { get; set; }

        private Boolean m_DirectInvokeEnabled = true;
        /// <summary>
        /// 是否允许消费者直接调用服务提供者
        /// </summary>
        public Boolean DirectInvokeEnabled { 
            get { return m_DirectInvokeEnabled; }
            set { m_DirectInvokeEnabled = value; }
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
}
