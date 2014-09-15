using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// 队列调用参数
    /// </summary>
    public class QueueParam
    {
        /// <summary>
        /// 队列回调函数
        /// </summary>
        public Action<String> CallBack;
        /// <summary>
        /// 队列名称，如果填写则需要自行实现服务端处理程序
        /// </summary>
        public String QueueName { get; set; }
        /// <summary>
        /// 超时（单位毫秒）
        /// </summary>
        public Int32 Timeout { get; set; }
    }
}
