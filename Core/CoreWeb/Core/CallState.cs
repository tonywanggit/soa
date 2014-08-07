using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JN.ESB.Entity;

namespace CoreWeb
{
    /// <summary>
    /// 线程调用时
    /// </summary>
    public class CallState
    {
        /// <summary>
        /// 服务绑定
        /// </summary>
        public BindingTemplate Binding { get; set; }

        /// <summary>
        /// 请求信息
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.jn.com/esb/request/20100329", ElementName = "服务请求")]
        public JN.ESB.Schemas.服务请求 Request { get; set; }

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
    }
}
