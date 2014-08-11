using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewLife.Configuration;
using System.Messaging;
using NewLife.Log;
using XCode.DataAccessLayer;
using JN.ESB.Entity;
using System.Configuration;

namespace Audit.WindowsService
{
    /// <summary>
    /// 消息队列管理器
    /// </summary>
    internal class QueueManager
    {
        String m_ESBQueueName = String.Empty;
        MessageQueue m_MessageQueue = null;
        Boolean m_StopFlag = false;          // 停止标志
        Boolean m_ProcessMessage = false;    // True 表明正在处理消息

        public QueueManager()
        {
            m_ESBQueueName = Config.GetConfig<String>("ESB.Queue.AuditName");
            XTrace.WriteLine("读取到ESB队列名称：{0}", m_ESBQueueName);

            m_MessageQueue = GetMessageQueue(m_ESBQueueName);
            m_MessageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(AuditBusiness) });
        }

        /// <summary>
        /// 获取到ESB的消息队列
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        private MessageQueue GetMessageQueue(string queueName)
        {
            if (!MessageQueue.Exists(queueName))
            {
                MessageQueue queue = MessageQueue.Create(queueName);
                XTrace.WriteLine("消息队列[{0}]创建成功！", queueName);
                return queue;
            }
            else
            {
                MessageQueue queue = new MessageQueue(queueName);
                XTrace.WriteLine("消息队列[{0}]已经存在！", queueName);
                return queue;
            }
        }

        /// <summary>
        /// 开始接收队列中的消息
        /// </summary>
        public void StartReceive()
        {
            m_StopFlag = false;
            m_ProcessMessage = false;
            do
            {
                try
                {
                    Message myMessage = m_MessageQueue.Receive(); //当消息队列空时，线程会挂起
                    m_ProcessMessage = true;

                    AuditBusiness log = (AuditBusiness)myMessage.Body;
                    if (log != null) log.Insert();

                    m_ProcessMessage = false;
                }
                catch (Exception ex){
                    XTrace.WriteLine("接收消息时发生异常：{0}，服务将忽略此消息并继续运行！", ex.ToString());
                }

            } while (m_StopFlag == false);

            XTrace.WriteLine("服务已经停止接收消息！");
        }

        /// <summary>
        /// 停止接收队列中的消息
        /// </summary>
        public void StopReceive()
        {
            m_StopFlag = true;
            while (m_ProcessMessage)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
