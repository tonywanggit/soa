using ESB.Core.Monitor;
using ESB.Core.Util;
using NewLife.Configuration;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Monitor.WindowsService
{
    /// <summary>
    /// 监控中心：负责将监控数据发布出去，和Portal客户端通讯
    /// </summary>
    internal class MonitorCenter
    {
        List<MonitorClient> m_MonitorClients = new List<MonitorClient>();
        TcpListener m_TcpListener;

        /// <summary>
        /// 启动监控服务
        /// </summary>
        public void Start()
        {
            Int32 port = Config.GetConfig<Int32>("ESB.MonitorCenter.Port");

            //IPEndPoint localep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            IPEndPoint localep = new IPEndPoint(IPAddress.Any, port);
            m_TcpListener = new TcpListener(localep);
            m_TcpListener.Start(2000);

            m_TcpListener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), m_TcpListener);
            Console.WriteLine("监控中心服务正在监控：{0}端口！", port);

        }

        /// <summary>
        /// 接受客户端连接
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                TcpListener listener = ar.AsyncState as TcpListener;
                Socket socket = listener.EndAcceptSocket(ar);
                listener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), listener);

                MonitorClient monitorClient = new MonitorClient(socket);
                monitorClient.ReceiveDateTime = DateTime.Now;
                monitorClient.MonitorClientType = CometClientType.RegistryCenter;
                monitorClient.ClearBuffer();
                monitorClient.Socket.BeginReceive(monitorClient.ReceiveBuffer, 0, monitorClient.ReceiveBuffer.Length
                    , SocketFlags.None, new AsyncCallback(ReceiveCallback), monitorClient);

                Console.WriteLine("接收客户端：{0}", monitorClient.Socket.RemoteEndPoint.ToString());

                lock (m_MonitorClients)
                {
                    m_MonitorClients.Add(monitorClient);
                }
            }
            catch (Exception ex)
            {
                XTrace.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 处理客户端的请求
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            MonitorClient monitorClient = (MonitorClient)ar.AsyncState;
            try
            {
                int dataLength = monitorClient.Socket.EndReceive(ar);

                if (dataLength == 0)
                {
                    Console.WriteLine("接收客户端：{0}已经断开连接。", monitorClient.Socket.RemoteEndPoint.ToString());
                    lock (m_MonitorClients)
                    {
                        m_MonitorClients.Remove(monitorClient);
                        monitorClient.Dispose();
                    }
                }
                else
                {
                    String data = Encoding.UTF8.GetString(monitorClient.ReceiveBuffer, 0, dataLength);

                    Console.WriteLine("接收客户端：{0}发送的数据：{1}。", monitorClient.Socket.RemoteEndPoint.ToString(), data);

                    //--解析来自客户端的类型
                    MonitorMessage regMessage = XmlUtil.LoadObjFromXML<MonitorMessage>(data);
                    monitorClient.MonitorClientType = regMessage.ClientType;

                    monitorClient.ClearBuffer();
                    monitorClient.Socket.BeginReceive(monitorClient.ReceiveBuffer, 0, monitorClient.ReceiveBuffer.Length
                        , SocketFlags.None, new AsyncCallback(ReceiveCallback), monitorClient);

                    //--由消息处理器进行处理
                    //m_MessageProcessor.Process(registryClient, regMessage);
                };
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("接收数据时发生异常：" + ex.ToString());
                lock (m_MonitorClients)
                {
                    m_MonitorClients.Remove(monitorClient);
                    monitorClient.Dispose();
                }
            }
        }


        /// <summary>
        /// 向所有连接的客户端发送数据
        /// </summary>
        public void Publish(String data)
        {
            XTrace.WriteLine("发布数据：" + data);

            foreach (var item in m_MonitorClients)
            {
                SendData(item, MonitorMessageAction.Publish, data);
            }
        }

        /// <summary>
        /// 向单个客户端发送数据
        /// </summary>
        /// <param name="rsClient"></param>
        /// <param name="data"></param>
        /// <param name="isAsync">默认为异步调用m</param>
        public void SendData(MonitorClient monitorClient, MonitorMessageAction action, String data, Boolean isAsync = true)
        {
            try
            {
                MonitorMessage rm = new MonitorMessage()
                {
                    Action = action,
                    MessageBody = data,
                    IsAsync = isAsync
                };

                String messageData = XmlUtil.SaveXmlFromObj<MonitorMessage>(rm);
                Console.WriteLine("发送数据：{0}", messageData);

                Byte[] msg = Encoding.UTF8.GetBytes(messageData);
                monitorClient.Socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(SendCallback), monitorClient);
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("发送数据时发生异常：" + ex.ToString());
                lock (m_MonitorClients)
                {
                    m_MonitorClients.Remove(monitorClient);
                    monitorClient.Dispose();
                }
            }
        }

        /// <summary>
        /// 发送数据回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            MonitorClient monitorClient = ar.AsyncState as MonitorClient;

            try
            {
                monitorClient.Socket.EndSend(ar);
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("发送数据时发生异常：" + ex.ToString());
                lock (m_MonitorClients)
                {
                    m_MonitorClients.Remove(monitorClient);
                    monitorClient.Dispose();
                }
            }
        }

    }
}
