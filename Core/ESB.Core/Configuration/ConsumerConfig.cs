using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// </summary>
        public String ApplicationName { get; set; }

        public ConsumerConfig()
        {
            Registry = new List<RegistryItem>();
            Reference = new List<ReferenceItem>();
            ApplicationName = String.Empty;
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
        /// <summary>
        /// 接口
        /// </summary>
        public String Interface { get; set; }
    }
}
