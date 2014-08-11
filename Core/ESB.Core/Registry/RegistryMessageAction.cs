using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Registry
{
    /// <summary>
    /// 表示和注册中心通讯的数据类型
    /// </summary>
    public enum RegistryMessageAction
    {
        /// <summary>
        /// 用于和注册中心第一次通讯
        /// </summary>
        Hello,
        /// <summary>
        /// 用于告诉注册中心将要断开连接
        /// </summary>
        Bye,
        /// <summary>
        /// 心跳检测
        /// </summary>
        HeartBeat,
        /// <summary>
        /// 服务配置
        /// </summary>
        ServiceConfig,
        /// <summary>
        /// 增加服务
        /// </summary>
        AddService,
        /// <summary>
        /// 增加注册中心
        /// </summary>
        AddRegistry,
        /// <summary>
        /// 增加监控中心
        /// </summary>
        AddMointor,
        /// <summary>
        /// 增加调用中心
        /// </summary>
        AddCallCenter
    }
}
