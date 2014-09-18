using ESB.Core;
using ESB.Core.Monitor;
using ESB.Core.Rpc;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ESB.Core.Monitor
{
    /// <summary>
    /// 队列服务处理线程
    /// </summary>
    public class QueueThread
    {
        private Thread m_Thread;
        private String m_ServiceName;
        private RabbitMQClient m_RabbitMQ;

        /// <summary>
        /// ESB代理类对象
        /// </summary>
        private ESBProxy m_ESBProxy = ESBProxy.GetInstance();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceName"></param>
        public QueueThread(String serviceName, RabbitMQClient rabbitMQClient)
        {
            m_ServiceName = serviceName;
            m_RabbitMQ = rabbitMQClient;
            m_Thread = new Thread(x =>
            {
                ProcessInvokeQueueMessage();
            });
        }

        /// <summary>
        /// 启动线程进行操作
        /// </summary>
        public void Start()
        {
            m_Thread.Start();
            XTrace.WriteLine("线程[{0}]启动对服务[{1}]的队列操作。", m_Thread.ManagedThreadId, m_ServiceName);
        }

        /// <summary>
        /// 关闭线程进行操作
        /// </summary>
        public void Stop()
        {
            m_Thread.Abort();
            XTrace.WriteLine("线程[{0}]关闭对服务[{1}]的队列操作。", m_Thread.ManagedThreadId, m_ServiceName);
        }

        /// <summary>
        /// 处理队列调用消息
        /// </summary>
        public void ProcessInvokeQueueMessage()
        {
            //--#代表ESB专用队列
            m_RabbitMQ.ListenInvokeQueue(m_ServiceName, x =>
            {
                AdvanceInvokeParam invokeParam = new AdvanceInvokeParam();
                invokeParam.ConsumerAppName = x.ConsumerAppName;
                invokeParam.ConsumerIP = x.ConsumerIP;

                m_ESBProxy.Invoke(x.ServiceName, x.MethodName, x.Message, x.Version, invokeParam);
            });
        }
    }
}
