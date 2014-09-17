using ESB.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Monitor
{
    /// <summary>
    /// 队列处理消息
    /// </summary>
    public class QueueMessage
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public String ServiceName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public String MethodName { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public Int32 Version { get; set; }

        /// <summary>
        /// 队列名称，如果填写则需要自行实现服务端处理程序
        /// </summary>
        public String QueueName { get; set; }

        /// <summary>
        /// 超时（单位毫秒）
        /// </summary>
        public Int32 Timeout { get; set; }

        /// <summary>
        /// 获取到路由键
        /// </summary>
        /// <returns></returns>
        public String GetRouteKey()
        {
            if (String.IsNullOrWhiteSpace(QueueName))
                return String.Format("{0}.{1}", Constant.ESB_INVOKE_QUEUE, ServiceName);
            else
                return String.Format("{0}.{1}", Constant.ESB_CUST_INVOKE_QUEUE, QueueName);
        }
    }
}
