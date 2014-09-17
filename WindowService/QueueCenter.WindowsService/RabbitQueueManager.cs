using ESB.Core;
using ESB.Core.Configuration;
using ESB.Core.Monitor;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QueueCenter.WindowsService
{
    /// <summary>
    /// RabbitMQ管理器
    /// </summary>
    internal class RabbitQueueManager
    {
        /// <summary>
        /// 队列客户端
        /// </summary>
        RabbitMQClient m_RabbitMQ;

        /// <summary>
        /// 队列服务处理进程
        /// </summary>
        Thread m_ThreadQC;

        /// <summary>
        /// ESB代理类对象
        /// </summary>
        ESBProxy m_ESBProxy = ESBProxy.GetInstance();

        /// <summary>
        /// 构造器
        /// </summary>
        public RabbitQueueManager()
        {
            ESBConfig esbConfig = m_ESBProxy.RegistryConsumerClient.ESBConfig;

            if (esbConfig != null && esbConfig.MessageQueue.Count > 0)
            {
                //String esbQueue = Config.GetConfig<String>("ESB.Queue");
                String esbQueue = esbConfig.MessageQueue[0].Uri;
                XTrace.WriteLine("读取到ESB队列地址：{0}", esbQueue);

                String[] paramMQ = esbQueue.Split(':');
                m_RabbitMQ = new RabbitMQClient(paramMQ[0], paramMQ[2], paramMQ[3], Int32.Parse(paramMQ[1]));
            }
            else
            {
                String err = "无法获取到有效的队列地址！";
                XTrace.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 建立独立线程启动处理程序
        /// </summary>
        public void StartReceive()
        {
            m_ThreadQC = new Thread(x =>
            {
                ProcessInvokeQueueMessage();

            });
            m_ThreadQC.Start();
        }

        /// <summary>
        /// 停止接收
        /// </summary>
        public void StopReceive()
        {
            if (m_RabbitMQ != null)
                m_RabbitMQ.Dispose();

            if (m_ThreadQC != null)
                m_ThreadQC.Abort();
        }

        /// <summary>
        /// 处理队列调用消息
        /// </summary>
        public void ProcessInvokeQueueMessage()
        {
            //--#代表ESB专用队列
            m_RabbitMQ.ListenInvokeQueue("#", x =>
            {
                m_ESBProxy.Invoke(x.ServiceName, x.MethodName, x.Message, x.Version);
            });
        }
    }
}
