using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.TestFramework.Test
{
    /// <summary>
    /// 调用参数
    /// </summary>
    internal class ESBInvokeParam
    {
        /// <summary>
        /// 调用中心地址
        /// </summary>
        public String CallCenterUrl { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public String ServiceName { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public String MethodName { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public Int32 Version { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public String Message { get; set; }
    }
}
