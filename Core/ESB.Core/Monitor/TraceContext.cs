using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Monitor
{
    /// <summary>
    /// 追踪上下文
    /// </summary>
    public class TraceContext
    {
        /// <summary>
        /// 追踪记录ID
        /// </summary>
        public String TraceID { get; set; }
        /// <summary>
        /// 调用层级
        /// </summary>
        public Int32 InvokeLevel { get; set; }
        /// <summary>
        /// 调用顺序
        /// </summary>
        public Int32 InvokeOrder { get; set; }
    }
}
