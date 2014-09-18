using ESB.Core;
using ESB.Core.Configuration;
using ESB.Core.Entity;
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
        /// 队列服务处理进程组
        /// </summary>
        List<QueueThread> m_QueueThreadList;

        /// <summary>
        /// ESB代理类对象
        /// </summary>
        ESBProxy m_ESBProxy = ESBProxy.GetInstance();

        /// <summary>
        /// ESBConfig文件
        /// </summary>
        ESBConfig m_EsbConfig;

        /// <summary>
        /// ConsumerConfig文件
        /// </summary>
        ConsumerConfig m_ConsumerConfig;

        /// <summary>
        /// 构造器
        /// </summary>
        public RabbitQueueManager()
        {
            m_EsbConfig = m_ESBProxy.RegistryConsumerClient.ESBConfig;
            m_ConsumerConfig = m_ESBProxy.RegistryConsumerClient.ConsumerConfig;
            m_QueueThreadList = new List<QueueThread>();

            if (m_EsbConfig != null && m_EsbConfig.MessageQueue.Count > 0)
            {
                //String esbQueue = Config.GetConfig<String>("ESB.Queue");
                String esbQueue = m_EsbConfig.MessageQueue[0].Uri;
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
            String appName = m_ConsumerConfig.ApplicationName;

            if (m_EsbConfig.ServiceConfig != null && m_EsbConfig.ServiceConfig.Count > 0)
            {
                List<EsbView_ServiceConfig> lstServcieConfig = m_EsbConfig.ServiceConfig.FindAll(x => appName.EndsWith(x.QueueCenterUri));
                foreach (EsbView_ServiceConfig sc in lstServcieConfig)
                {
                    QueueThread qt = new QueueThread(sc.ServiceName, m_RabbitMQ);
                    qt.Start();

                    m_QueueThreadList.Add(qt);
                }
            }
        }

        /// <summary>
        /// 停止接收
        /// </summary>
        public void StopReceive()
        {
            if (m_RabbitMQ != null)
                m_RabbitMQ.Dispose();

        }
    }
}
