﻿using ESB.Core.Configuration;
using ESB.Core.Entity;
using NewLife.Log;
using NewLife.Threading;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace ESB.Core.Monitor
{
    /// <summary>
    /// 消息队列客户端（RabbitMQ）
    /// </summary>
    internal class MessageQueueClient
    {
        /// <summary>
        /// 消息队列
        /// </summary>
        private RabbitMQClient m_RabbitMQ;

        private Boolean m_RabbitMQAvailable = false;
        /// <summary>
        /// 远程消息队列是否可用
        /// </summary>
        public Boolean RabbitMQAvailable
        {
            get { return m_RabbitMQAvailable; }
        }

        /// <summary>
        /// 本地消息队列
        /// </summary>
        private LocalMQ m_LocalMQ;

        /// <summary>
        /// ESBProxy实例
        /// </summary>
        private ESBProxy m_ESBProxy;

        /// <summary>
        /// 定时器：用于检测调用中心是否可以使用
        /// </summary>
        private TimerX m_TimerX;

        /// <summary>
        /// 失败重连次数
        /// </summary>
        private Int32 m_ReConnectNum = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MessageQueueClient(ESBProxy esbProxy)
        {
            m_ESBProxy = esbProxy;
            m_LocalMQ = new LocalMQ(this);
        }

        /// <summary>
        /// 异步调用连接
        /// </summary>
        public void ConnectAsync()
        {
            ThreadPool.QueueUserWorkItem(x =>
            {
                Connect();
            });
        }

        /// <summary>
        /// 连接到监控中心
        /// </summary>
        private void Connect()
        {
            if (m_ESBProxy.ESBConfig != null && m_ESBProxy.ESBConfig.MessageQueue != null && m_ESBProxy.ESBConfig.MessageQueue.Count > 0)
            {
                try
                {
                    String[] paramMQ = m_ESBProxy.ESBConfig.MessageQueue[0].Uri.Split(':');
                    m_RabbitMQ = new RabbitMQClient(paramMQ[0], paramMQ[2], paramMQ[3], Int32.Parse(paramMQ[1]));

                    if (m_TimerX != null)   //--说明监控中心曾经不可用过
                    {
                        m_TimerX.Dispose();
                        m_TimerX = null;
                        m_ReConnectNum = 0;
                    }

                    //--当监控中心可用时需要检测本地消息队列，如果有消息则需要发送
                    m_LocalMQ.CheckAndResendMessage();

                    m_RabbitMQAvailable = true;
                    XTrace.WriteLine("成功连接到消息队列。");
                }
                catch(Exception ex)
                {
                    if (m_TimerX == null)
                    {
                        m_TimerX = new TimerX(x => Connect(), null, 5000, 5000);
                        XTrace.WriteLine("无法连接到消息队列：" + ex.ToString());
                    }
                    else
                    {
                        XTrace.WriteLine("第{0}次重连消息队列失败, 抛出异常：{1}", m_ReConnectNum, ex.ToString());
                        m_ReConnectNum++;
                    }
                }
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
            if (m_RabbitMQ != null)
            {
                try
                {
                    if (queueName == Constant.ESB_INVOKE_QUEUE)
                        m_RabbitMQ.SendToInvokeQueue(message as QueueMessage);
                    else
                        m_RabbitMQ.SendMessage<T>(queueName, message);
                }
                catch (Exception ex)
                {
                    m_LocalMQ.QueueMessage<T>(queueName, message);
                    XTrace.WriteLine("向消息队列发送数据产生异常, 这段时间消息将被存在本地：{0}", ex.ToString());

                    m_RabbitMQ.Dispose();
                    m_RabbitMQ = null;

                    if (m_TimerX == null)
                    {
                        m_TimerX = new TimerX(x => Connect(), null, 5000, 5000);
                        XTrace.WriteLine("5秒之后将会重新连接消息队列...");
                    }
                }
            }
            else
            {
                m_LocalMQ.QueueMessage<T>(queueName, message);
            }
        }

        /// <summary>
        /// 发送审计消息
        /// </summary>
        public void SendAuditMessage(AuditBusiness auditBussiness)
        {
            SendMessage<AuditBusiness>(Constant.ESB_AUDIT_QUEUE, auditBussiness);
        }

        /// <summary>
        /// 发送异常消息
        /// </summary>
        public void SendExceptionMessage(ExceptionCoreTb exception)
        {
            SendMessage<ExceptionCoreTb>(Constant.ESB_EXCEPTION_QUEUE, exception);
        }

        /// <summary>
        /// 发送消息到ESB专用队列
        /// </summary>
        public void SendToInvokeQueue(QueueMessage qm)
        {
            SendMessage<QueueMessage>(Constant.ESB_INVOKE_QUEUE, qm);
        }
    }
}