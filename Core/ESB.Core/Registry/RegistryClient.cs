using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using ESB.Core.Registry;
using ESB.Core.Rpc;
using System.Xml.Serialization;
using ESB.Core.Configuration;

namespace ESB.Core.Registry
{
    /// <summary>
    /// 对连接到注册中心的Socket进行封装
    /// </summary>
    public class RegistryClient
    {
        /// <summary>
        /// 客户端Socket
        /// </summary>
        [XmlIgnore]
        public Socket Socket { get; set; }
        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        [XmlIgnore]
        public Byte[] ReceiveBuffer { get; set; }
        /// <summary>
        /// 客户端连接到注册中心的时间
        /// </summary>
        public DateTime ReceiveDateTime { get; set; }
        /// <summary>
        /// 客户端类型
        /// </summary>
        public CometClientType RegistryClientType { get; set; }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public String ClientIP { get; set; }
        /// <summary>
        /// 客户端的程序集版本
        /// </summary>
        public String ClientVersion { get; set; }
        /// <summary>
        /// 客户端进程号
        /// </summary>
        public Int32 ProcessorID { get; set; }

        /// <summary>
        /// .NET版本
        /// </summary>
        public String DotNetFramworkVersion { get; set; }

        /// <summary>
        /// OS版本
        /// </summary>
        public String OSVersion { get; set; }

        /// <summary>
        /// Consumer.config中配置的ApplicationName
        /// </summary>
        public String ClientApplicationName { get; set; }

        /// <summary>
        /// 客户端配置文件
        /// </summary>
        [XmlIgnore]
        public ConsumerConfig ConsumerConfig { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="socket"></param>
        public RegistryClient(Socket socket)
        {
            Socket = socket;
        }

        /// <summary>
        /// 无参的构造函数用于序列化，禁止手工调用
        /// </summary>
        public RegistryClient()
        {

        }

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        public void ClearBuffer()
        {
            ReceiveBuffer = new Byte[1024 * 10];
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (Socket != null)
                {
                    Socket.Shutdown(SocketShutdown.Both);
                    Socket.Close();
                }
            }
            finally
            {
                Socket = null;
                ReceiveBuffer = null;
            }
        }
    }
}
