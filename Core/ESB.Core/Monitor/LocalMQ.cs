using ESB.Core.Configuration;
using ESB.Core.Entity;
using ESB.Core.Util;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ESB.Core.Monitor
{
    /// <summary>
    /// 本地的消息队列
    /// </summary>
    internal class LocalMQ
    {
        /// <summary>
        /// 本地消息的数量
        /// </summary>
        private Int32 m_LocalMessageNum = 0;

        /// <summary>
        /// 重新发送消息锁
        /// </summary>
        private Object m_ResendLock = new Object();

        /// <summary>
        /// 监控中心客户端
        /// </summary>
        private MessageQueueClient m_MonitorClient = null;
        /// <summary>
        /// 成功日志存储路径
        /// </summary>
        private String m_SuccessPath = null;
        /// <summary>
        /// 失败日志存储路径
        /// </summary>
        private String m_FailurePath = null;

        /// <summary>
        /// 配置管理器
        /// </summary>
        private ConfigurationManager m_ConfigurationManager = ConfigurationManager.GetInstance();

        /// <summary>
        /// 构造函数：需要异步获取到本地文件的数量
        /// </summary>
        public LocalMQ(MessageQueueClient mc)
        {
            m_MonitorClient = mc;
            m_SuccessPath = Path.Combine(m_ConfigurationManager.ESBMonitorDataPath, "Success");
            m_FailurePath = Path.Combine(m_ConfigurationManager.ESBMonitorDataPath, "Failure");

            CheckLocalMQ();
        }

        /// <summary>
        /// 检测本地的消息队列
        /// </summary>
        public void CheckLocalMQ()
        {
            if (!Directory.Exists(m_ConfigurationManager.ESBMonitorDataPath))
            {
                Directory.CreateDirectory(m_ConfigurationManager.ESBMonitorDataPath);
                Directory.CreateDirectory(m_SuccessPath);
                Directory.CreateDirectory(m_FailurePath);
            }

            CheckAndResendMessage();
        }

        /// <summary>
        /// 检测本地队列是否有消息，如果有则重送消息
        /// </summary>
        public void CheckAndResendMessage()
        {
            ThreadPoolX.QueueUserWorkItem(x =>
            {
                Int32 successFileNum = Directory.GetFiles(m_SuccessPath).Count();
                Int32 failureFileNum = Directory.GetFiles(m_FailurePath).Count();

                m_LocalMessageNum = successFileNum + failureFileNum;

                if (m_LocalMessageNum > 0 && m_MonitorClient.RabbitMQAvailable)
                {
                    ResendMessage();
                }
            });
        }

        /// <summary>
        /// 重新发送消息
        /// </summary>
        private void ResendMessage()
        {
            lock (m_ResendLock)
            {
                //--检测成功消息，并重新发送
                String[] successFiles = Directory.GetFiles(m_SuccessPath);
                if (successFiles.Length > 0)
                {
                    for (int i = 0; i < successFiles.Length; i++)
                    {
                        String message = File.ReadAllText(successFiles[i]);
                        AuditBusiness ab = XmlUtil.LoadObjFromXML<AuditBusiness>(message);

                        m_MonitorClient.SendAuditMessage(ab);

                        File.Delete(successFiles[i]);
                    }
                }

                //--检测异常消息，并重新发送
                String[] failureFiles = Directory.GetFiles(m_FailurePath);
                if (failureFiles.Length > 0)
                {
                    for (int i = 0; i < failureFiles.Length; i++)
                    {
                        String message = File.ReadAllText(failureFiles[i]);
                        ExceptionCoreTb ab = XmlUtil.LoadObjFromXML<ExceptionCoreTb>(message);

                        m_MonitorClient.SendExceptionMessage(ab);

                        File.Delete(failureFiles[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 将监控消息插进本地队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        public void QueueMessage<T>(String queueName, T message)
        {
            ThreadPool.QueueUserWorkItem(x =>
            {
                String filePath = Guid.NewGuid().ToString() + ".xml";
                if (queueName == Constant.ESB_AUDIT_QUEUE)
                    filePath = Path.Combine(m_SuccessPath, filePath);
                else
                    filePath = Path.Combine(m_FailurePath, filePath);

                File.WriteAllText(filePath, XmlUtil.SaveXmlFromObj<T>(message));
            });


            //ThreadPoolX.QueueUserWorkItem(x =>
            //{
            //    String filePath = Guid.NewGuid().ToString() + ".xml";
            //    if (queueName == Constant.ESB_AUDIT_QUEUE)
            //        filePath = Path.Combine(m_SuccessPath, filePath);
            //    else
            //        filePath = Path.Combine(m_FailurePath, filePath);

            //    File.WriteAllText(filePath, XmlUtil.SaveXmlFromObj<T>(message));
            //});
        }
    }
}
