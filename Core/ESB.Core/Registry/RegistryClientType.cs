using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Registry
{
    /// <summary>
    /// 表示和注册中心通讯的客户端类型
    /// </summary>
    public enum RegistryClientType
    {
        /// <summary>
        /// 服务消费方
        /// </summary>
        Consumer,
        /// <summary>
        /// 服务提供者
        /// </summary>
        Provider,
        /// <summary>
        /// 管理平台
        /// </summary>
        Portal,
        /// <summary>
        /// 监控中心
        /// </summary>
        Monitor,
        /// <summary>
        /// 调用中心
        /// </summary>
        CallCenter,
        /// <summary>
        /// 未知类型
        /// </summary>
        AnKnown
    }
}
