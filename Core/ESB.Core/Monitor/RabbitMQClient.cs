using ESB.Core.Configuration;
using NewLife.Log;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ESB.Core.Monitor
{
    /// <summary>
    /// RabbitMQ客户端
    /// </summary>
    public class RabbitMQClient
    {
        ConnectionFactory m_Factory = new ConnectionFactory();
        IConnection m_Connection;
        Dictionary<String, IModel> m_ChannelDict = new Dictionary<string, IModel>();


        /// <summary>
        /// RabbitMQ构造函数
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="port"></param>
        public RabbitMQClient(String hostName, String userName, String passWord, Int32 port = 5672)
        {
            m_Factory.HostName = hostName;
            m_Factory.UserName = userName;
            m_Factory.Password = passWord;
            m_Factory.Port = port;

            m_Connection = m_Factory.CreateConnection();

            m_ChannelDict.Add(Constant.ESB_AUDIT_QUEUE, m_Connection.CreateModel());
            m_ChannelDict.Add(Constant.ESB_EXCEPTION_QUEUE, m_Connection.CreateModel());
        }

        /// <summary>
        /// 发送特定类型的消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="queueName">队列名称</param>
        /// <param name="message">消息内容</param>
        public void SendMessage<T>(String queueName, T message)
        {
            //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
            //在MQ上定义一个队列
            IModel channel = m_ChannelDict[queueName];
            channel.QueueDeclare(queueName, true, false, false, null);

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
            //序列化消息对象，RabbitMQ并不支持复杂对象的序列化，所以对于自定义的类型需要自己序列化
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                xs.Serialize(ms, message);
                byte[] bytes = ms.ToArray();
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
                //指定发送的路由，通过默认的exchange直接发送到指定的队列中。
                channel.BasicPublish("", queueName, properties, bytes);
                //Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff"));
            }
        }

        /// <summary>
        /// Rabbit客户端监听程序
        /// </summary>
        /// <typeparam name="T">队列中的消息类型</typeparam>
        /// <param name="queueName"></param>
        /// <param name="processMethod"></param>
        public void Listen<T>(String queueName, Action<T> processMethod)
        {
            //在MQ上定义一个队列，如果名称相同不会重复创建
            IModel channel = m_ChannelDict[queueName];
            channel.QueueDeclare(queueName, true, false, false, null);

            //在队列上定义一个消费者
            QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
            channel.BasicConsume(queueName, true, consumer);

            while (true)
            {
                try
                {
                    //阻塞函数，获取队列中的消息
                    BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                    byte[] body = ea.Body;

                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    using (MemoryStream ms = new MemoryStream(body))
                    {
                        T message = (T)xs.Deserialize(ms);
                        processMethod(message);
                    }
                }
                catch(Exception ex)
                {
                    XTrace.WriteLine("处理队列消息时发生异常：" + ex.Message);
                }
            }
        }
    }
}
