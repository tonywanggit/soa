using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Messaging;

namespace ESB.Core.Util
{
    /// <summary>
    /// MSMQ工具
    /// </summary>
    public class MSMQUtil
    {       /// <summary>
        /// 创建MSMQ队列辅助类
        /// </summary>
        /// <param name="queuePath">队列路径</param>
        /// <param name="transactional">是否事务队列</param>
        public static void Createqueue(string queuePath, bool transactional = false)
        {
            try
            {
                //判断队列是否存在
                if (!MessageQueue.Exists(queuePath))
                {
                    MessageQueue.Create(queuePath);
                    Console.WriteLine(queuePath + "已成功创建！");
                }
                else
                {
                    Console.WriteLine(queuePath + "已经存在！");
                }
            }
            catch (MessageQueueException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 删除队列
        /// </summary>
        /// <param name="queuePath"></param>
        public static void Deletequeue(string queuePath)
        {
            try
            {
                //判断队列是否存在
                if (MessageQueue.Exists(queuePath))
                {
                    MessageQueue.Delete(@".\private$\myQueue");
                    Console.WriteLine(queuePath + "已删除！");
                }
                else
                {
                    Console.WriteLine(queuePath + "不存在！");
                }
            }
            catch (MessageQueueException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T">用户数据类型</typeparam>
        /// <param name="target">用户数据</param>
        /// <param name="queuePath">队列名称</param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static bool SendMessage<T>(T target, string queuePath, MessageQueueTransaction tran = null)
        {
            try
            {
                //连接到本地的队列
                MessageQueue myQueue = new MessageQueue(queuePath);
                System.Messaging.Message myMessage = new System.Messaging.Message();
                myMessage.Body = target;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
                //发送消息到队列中
                if (tran == null)
                {
                    myQueue.Send(myMessage);
                }
                else
                {
                    myQueue.Send(myMessage, tran);
                }
                Console.WriteLine("消息已成功发送到" + queuePath + "队列！");
                return true;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <typeparam name="T">用户的数据类型</typeparam>
        /// <param name="queuePath">消息路径</param>
        /// <returns>用户填充在消息当中的数据</returns>
        public static T ReceiveMessage<T>(string queuePath, MessageQueueTransaction tran = null)
        {
            //连接到本地队列
            MessageQueue myQueue = new MessageQueue(queuePath);
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            try
            {
                //从队列中接收消息
                System.Messaging.Message myMessage = tran == null ? myQueue.Receive() : myQueue.Receive(tran);
                return (T)myMessage.Body; //获取消息的内容
            }
            catch (MessageQueueException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
            return default(T);
        }


        /// <summary>
        /// 采用Peek方法接收消息
        /// </summary>
        /// <typeparam name="T">用户数据类型</typeparam>
        /// <param name="queuePath">队列路径</param>
        /// <returns>用户数据</returns>
        public static T ReceiveMessageByPeek<T>(string queuePath)
        {
            //连接到本地队列
            MessageQueue myQueue = new MessageQueue(queuePath);
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            try
            {
                //从队列中接收消息
                System.Messaging.Message myMessage = myQueue.Peek();
                return (T)myMessage.Body; //获取消息的内容
            }
            catch (MessageQueueException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
            return default(T);
        }


        /// <summary>
        /// 获取队列中的所有消息
        /// </summary>
        /// <typeparam name="T">用户数据类型</typeparam>
        /// <param name="queuePath">队列路径</param>
        /// <returns>用户数据集合</returns>
        public static List<T> GetAllMessage<T>(string queuePath)
        {
            MessageQueue myQueue = new MessageQueue(queuePath);
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            try
            {
                Message[] msgArr = myQueue.GetAllMessages();
                List<T> list = new List<T>();
                msgArr.ToList().ForEach((o) =>
                {
                    list.Add((T)o.Body);
                });
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}