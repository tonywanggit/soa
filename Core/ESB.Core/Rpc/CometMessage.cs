using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// 通讯的消息类
    /// </summary>
    public class CometMessage
    {
        /// <summary>
        /// 和注册中心客户端通讯的类型
        /// </summary>
        public CometClientType ClientType { get; set; }

        /// <summary>
        /// 向服务器汇报客户端的版本信息
        /// ESB.Core-1.0.2014.0811
        /// NewLife.Core-1.0.2014.0811
        /// </summary>
        public String ClientVersion { get; set; }

        /// <summary>
        /// 客户端进程号
        /// </summary>
        public Int32 ProcessorID { get; set; }

        /// <summary>
        /// .NET版本
        /// </summary>
        public String DotNetFramworkVersion { get; set; }

        /// <summary>
        /// OS版本
        /// </summary>
        public String OSVersion { get; set; }

        /// <summary>
        /// 表明和注册中心通讯的意图
        /// </summary>
        public CometMessageAction Action { get; set; }

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
