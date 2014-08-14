using ESB.Core.Entity;
using NewLife.Log;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ESB.Core.Monitor
{
    /// <summary>
    /// 监控中心客户端
    /// </summary>
    internal class MonitorClient
    {
        /// <summary>
        /// ESB审计消息队列
        /// </summary>
        public const String ESB_AUDIT_QUEUE = "esb.audit";
        /// <summary>
        /// ESB异常消息队列
        /// </summary>
        public const String ESB_EXCEPTION_QUEUE = "esb.exception";

        /// <summary>
        /// 消息队列
        /// </summary>
        private RabbitMQClient m_RabbitMQ;

        /// <summary>
        /// ESBProxy实例
        /// </summary>
        private ESBProxy m_ESBProxy;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MonitorClient(ESBProxy esbProxy)
        {
            m_ESBProxy = esbProxy;
        }

        /// <summary>
        /// 连接到监控中心
        /// </summary>
        public void Connect()
        {
            if (m_ESBProxy.ESBConfig.Monitor != null && m_ESBProxy.ESBConfig.Monitor.Count > 0)
            {
                String[] paramMQ = m_ESBProxy.ESBConfig.Monitor[0].Uri.Split(':');
                m_RabbitMQ = new RabbitMQClient(paramMQ[0], paramMQ[2], paramMQ[3], Int32.Parse(paramMQ[1]));
            }
        }

        /// <summary>
        /// 将消息发送到队列，如果失败则存储在本地
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        private void SendMessage<T>(String queueName, T message)
        {
            if (m_RabbitMQ == null)
            {
                m_RabbitMQ.SendMessage<T>(queueName, message);
            }
            else
            {
                XTrace.WriteLine("无法连接消息队列，将采用本地存储。");
            }
        }

        /// <summary>
        /// 发送审计消息
        /// </summary>
        public void SendAuditMessage(AuditBusiness auditBussiness)
        {
            SendMessage<AuditBusiness>(ESB_AUDIT_QUEUE, auditBussiness);
        }

        /// <summary>
        /// 发送异常消息
        /// </summary>
        public void SendExceptionMessage(ExceptionCoreTb exception)
        {
            SendMessage<ExceptionCoreTb>(ESB_EXCEPTION_QUEUE, exception);
        }
    }
}