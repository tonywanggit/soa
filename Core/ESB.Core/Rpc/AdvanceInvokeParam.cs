using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// 内部的调用参数
    /// </summary>
    public class AdvanceInvokeParam
    {
        /// <summary>
        /// 客户端应用名称
        /// </summary>
        internal String ConsumerAppName { get; set; }
        /// <summary>
        /// 客户端IP:队列服务调用时需要传递
        /// </summary>
        public String ConsumerIP { get; set; }

        /// <summary>
        /// 强制不用缓存: 0 -否，1-是
        /// </summary>
        public Int32 NoCache { get; set; }

        /// <summary>
        /// 是否为队列调用: 0 -否，1-是
        /// </summary>
        internal Int32 IsQueue { get; set; }
    }
}
