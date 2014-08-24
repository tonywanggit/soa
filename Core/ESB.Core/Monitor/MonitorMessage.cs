using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Monitor
{
    /// <summary>
    /// 监控中心消息类
    /// </summary>
    public class MonitorMessage
    {      
        /// <summary>
        /// 和监控中心客户端通讯的类型
        /// </summary>
        public MonitorClientType ClientType { get; set; }

        /// <summary>
        /// 表明和监控中心通讯的意图
        /// </summary>
        public MonitorMessageAction Action { get; set; }

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

    /// <summary>
    /// 和监控中心通讯的意图
    /// </summary>
    public enum MonitorMessageAction
    {
        /// <summary>
        /// 订阅监控数据
        /// </summary>
        Subscribe,
        /// <summary>
        /// 发布监控数据
        /// </summary>
        Publish
    }
}
