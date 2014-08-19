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
        /// 消息编码
        /// </summary>
        public String MessageGUID { get; set; }

        private Boolean m_IsAsync = true;
        /// <summary>
        /// 是否为异步调用,默认为异步调用
        /// </summary>
        public Boolean IsAsync { get { return m_IsAsync; } set { m_IsAsync = value; } }

        /// <summary>
        /// 消息主体内容
        /// </summary>
        public String MessageBody { get; set; }
    }
}
