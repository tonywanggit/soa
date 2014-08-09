using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;
using NewLife.Log;
using ESB.Core.Util;

namespace ESB.Core.Registry
{
    /// <summary>
    /// 长连接客户端：用于和注册中心保持联系
    /// </summary>
    public class CometClient
    {
        private Socket m_SocketClient = null;
        private String m_IP = String.Empty;
        private Int32 m_Port = 0;
        private Byte[] m_RecvBuff;
        private RegistryClientType m_ClientType;    //表明和监控中心通讯的客户端身份
        public event EventHandler<CometEventArgs> OnReceiveNotify; 

        public CometClient(String uri, RegistryClientType clientType)
        {
            m_IP = uri.Split(':')[0];
            m_Port = Int32.Parse(uri.Split(':')[1]);
            m_ClientType = clientType;
        }

        /// <summary>
        /// 连接到注册中心
        /// </summary>
        public void Connect()
        {
            if (m_SocketClient == null)
            {
                m_SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }

            if (!m_SocketClient.Connected)
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(m_IP), m_Port);
                m_SocketClient.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), m_SocketClient);

                Console.WriteLine("正在尝试和服务器建立连接...");
            }
        }

        /// <summary>
        /// 连接回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                m_SocketClient.EndConnect(ar);
                m_RecvBuff = new Byte[m_SocketClient.SendBufferSize];
                m_SocketClient.BeginReceive(m_RecvBuff, 0, m_RecvBuff.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), m_SocketClient);

                Console.WriteLine("与服务器成功建立连接！");

                ReceiveNotify(CometEventType.Connected, String.Empty);

            }
            catch (Exception ex)
            {
                String err = "无法与服务器建立连接：" + ex.ToString();
                OnLostConnection(err);
            }
        }

        /// <summary>
        /// 接收回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                Int32 dataLength = m_SocketClient.EndReceive(ar);
                String data = Encoding.UTF8.GetString(m_RecvBuff, 0, dataLength);

                //--进行业务处理
                Console.WriteLine("接收到来自服务器的响应：{0}", data);

                m_RecvBuff = new Byte[m_SocketClient.SendBufferSize];
                m_SocketClient.BeginReceive(m_RecvBuff, 0, m_RecvBuff.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), m_SocketClient);

            }
            catch (Exception ex)
            {
                OnLostConnection(ex.ToString());
            }
        }

        /// <summary>
        /// 向服务器发送数据
        /// </summary>
        /// <param name="message"></param>
        public void SendData(String message)
        {
            try
            {
                RegistryMessage regMessage = new RegistryMessage()
                {
                    Action = RegistryMessageAction.Hello,
                    ClientType = m_ClientType,
                    MessageBody = message
                };

                String dataMessage = XmlUtil.SaveXmlFromObj<RegistryMessage>(regMessage);
                Console.WriteLine("发送数据：{0}", dataMessage);

                Byte[] data = Encoding.UTF8.GetBytes(dataMessage);
                m_SocketClient.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), m_SocketClient);
            }
            catch (Exception ex)
            {
                OnLostConnection(ex.ToString());
            }
        }

        /// <summary>
        /// 发送回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                m_SocketClient.EndSend(ar);
            }
            catch (Exception ex)
            {
                OnLostConnection(ex.ToString());
            }
        }

        /// <summary>
        /// 结束和服务器的连接，并释放资源
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (m_SocketClient.Connected)
                {
                    m_SocketClient.Shutdown(SocketShutdown.Both);
                }
                m_SocketClient.Close();
            }
            finally
            {
                m_SocketClient = null;
                m_RecvBuff = null;
            }
        }

        /// <summary>
        /// 将网络层的通知传递到应用层
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="message"></param>
        private void ReceiveNotify(CometEventType notifyType, String message)
        {
            if (OnReceiveNotify != null)
            {
                CometEventArgs e = new CometEventArgs(){ Type = notifyType, Response = message };

                OnReceiveNotify(this, e);
            }
        }

        /// <summary>
        /// 当和服务器失去联系的时候进行处理
        /// </summary>
        /// <param name="reason"></param>
        private void OnLostConnection(String reason)
        {
            Dispose();
            String error = String.Format("与服务器失去联系：" + reason);
            XTrace.WriteLine(error);
            ReceiveNotify(CometEventType.Lost, error);
        }
    }

    /// <summary>
    /// 长连接
    /// </summary>
    public enum CometEventType
    {
        /// <summary>
        /// 连接成功
        /// </summary>
        Connected,
        /// <summary>
        /// 与注册中心失去联系
        /// </summary>
        Lost,
        /// <summary>
        /// 接收到来自注册中心的数据
        /// </summary>
        ReceiveMessage
    }

    /// <summary>
    /// 长连接客户端事件
    /// </summary>
    public class CometEventArgs : EventArgs
    {
        public CometEventType Type { get; set; }
        public String Response { get; set; }
    }
}
