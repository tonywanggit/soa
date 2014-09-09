using ESB.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ESB.Core.Configuration
{
    /// <summary>
    /// 服务消费者配置文件
    /// </summary>
    public class ConsumerConfig
    {
        /// <summary>
        /// 注册中心配置
        /// </summary>
        public List<RegistryItem> Registry { get; set; }

        /// <summary>
        /// 服务引用配置
        /// </summary>
        public List<ReferenceItem> Reference { get; set; }

        /// <summary>
        /// 应用名称：用于计算依赖关系
        /// ESB_CallCenter 为保留字：用于表示调用中心
        /// </summary>
        public String ApplicationName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConsumerConfig()
        {
            Registry = new List<RegistryItem>();
            Reference = new List<ReferenceItem>();
            ApplicationName = String.Empty;
        }

        /// <summary>
        /// 序列化成XML格式字符串
        /// </summary>
        /// <returns></returns>
        public String ToXml()
        {
            return XmlUtil.SaveXmlFromObj<ConsumerConfig>(this);
        }
    }

    /// <summary>
    /// 服务引用配置项
    /// </summary>
    public class ReferenceItem
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public String ServiceName { get; set; }
    }
}
