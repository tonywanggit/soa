using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Registry
{
    /// <summary>
    /// 和注册中心通讯的消息类
    /// </summary>
    public class RegistryMessage
    {
        /// <summary>
        /// 和注册中心客户端通讯的类型
        /// </summary>
        public RegistryClientType ClientType { get; set; }

        /// <summary>
        /// 表明和注册中心通讯的意图
        /// </summary>
        public RegistryMessageAction Action { get; set; }

        /// <summary>
        /// 消息主体内容
        /// </summary>
        public String MessageBody { get; set; }
    }
}
