using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESB.Core.Entity;
using ESB.Core.Monitor;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// 线程调用时
    /// </summary>
    internal class CallState
    {
        /// <summary>
        /// 服务绑定
        /// </summary>
        public BindingTemplate Binding { get; set; }
        /// <summary>
        /// 服务配置
        /// </summary>
        public EsbView_ServiceConfig ServiceConfig { get; set; }

        /// <summary>
        /// 客户端传递的调用参数，用于覆盖默认配置
        /// </summary>
        public AdvanceInvokeParam InvokeParam { get; set; }

        /// <summary>
        /// 服务版本，用于审计
        /// </summary>
        public Int32 ServiceVersion { get; set; }

        /// <summary>
        /// 请求信息
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.jn.com/esb/request/20100329", ElementName = "服务请求")]
        public ESB.Core.Schema.服务请求 Request { get; set; }

        /// <summary>
        ///  请求开始时间
        /// </summary>
        public DateTime RequestBeginTime { get; set; }

        /// <summary>
        /// 请求结束时间
        /// </summary>
        public DateTime RequestEndTime { get; set; }

        /// <summary>
        /// 调用开始时间
        /// </summary>
        public DateTime CallBeginTime { get; set; }

        /// <summary>
        /// 调用结束时间
        /// </summary>
        public DateTime CallEndTime { get; set; }

        /// <summary>
        /// 服务开始时间
        /// </summary>
        public String ServiceBeginTime { get; set; }

        /// <summary>
        /// 服务结束时间
        /// </summary>
        public String ServiceEndTime { get; set; }

        /// <summary>
        /// 跟踪上下文
        /// </summary>
        public ESBTraceContext TraceContext { get; set; }

        /// <summary>
        /// 如果一次调用涉及到多个提供者，则多条审计日志共享同一个MessageID
        /// </summary>
        public String MessageID { get; set; }
    }
}
