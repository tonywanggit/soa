using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ESB.Core.Monitor
{
    public class RequestMessage
    {
        public Guid MessageId { get; set; }
        public String Message { get; set; }
    }

    public class MonitorClient
    {
        ConnectionFactory factory = new ConnectionFactory();

        public MonitorClient(String uri)
        {
            factory.HostName = uri;
            factory.UserName = "tony";
            factory.Password = "efun102@mb";
        }

        public void Send()
        {
            //定义要发送的数据
            RequestMessage message = new RequestMessage() { MessageId = Guid.NewGuid(), Message = "this is a 请求。" };

            //创建一个 AMQP 连接
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    //在MQ上定义一个队列
                    channel.QueueDeclare("esb.test.queue", false, false, false, null);

                    //序列化消息对象，RabbitMQ并不支持复杂对象的序列化，所以对于自定义的类型需要自己序列化
                    XmlSerializer xs = new XmlSerializer(typeof(RequestMessage));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        xs.Serialize(ms, message);
                        byte[] bytes = ms.ToArray();
                        //指定发送的路由，通过默认的exchange直接发送到指定的队列中。
                        channel.BasicPublish("", "esb.test.queue", null, bytes);
                    }

                    Console.WriteLine(string.Format("Request Message Sent, Id:{0}, Message:{1}", message.MessageId, message.Message));
                }
            }
        }
    }
}