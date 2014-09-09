using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using NewLife.Log;
using ESB.Core.Registry;
using ESB.Core.Util;
using NewLife.Configuration;
using ESB.Core.Rpc;

namespace Registry.WindowsService
{
    /// <summary>
    /// 注册中心
    /// </summary>
    public class RegistryCenter
    {
        /// <summary>
        /// 客户端列表
        /// </summary>
        List<RegistryClient> m_RegistryClients = new List<RegistryClient>();
        /// <summary>
        /// 监听器
        /// </summary>
        TcpListener m_TcpListener;
        /// <summary>
        /// 守护线程
        /// </summary>
        MonitorThread m_monitorThread;
        /// <summary>
        /// 消息处理器
        /// </summary>
        MessageProcessor m_MessageProcessor;

        /// <summary>
        /// 连接到注册中心的客户端列表
        /// </summary>
        internal List<RegistryClient> RegistryClients
        {
            get
            {
                return m_RegistryClients;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RegistryCenter() {
            m_MessageProcessor = new MessageProcessor(this);
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            Int32 port = Config.GetConfig<Int32>("ESB.RegistryService.Port");

            //IPEndPoint localep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            IPEndPoint localep = new IPEndPoint(IPAddress.Any, port);
            m_TcpListener = new TcpListener(localep);
            m_TcpListener.Start(2000);

            m_TcpListener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), m_TcpListener);
            Console.WriteLine("注册中心服务正在监控：{0}端口！", port);

            //m_monitorThread = new MonitorThread(this);
            //m_monitorThread.Start();
        }

        /// <summary>
        /// 接收请求
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                TcpListener listener = ar.AsyncState as TcpListener;
                Socket socket = listener.EndAcceptSocket(ar);
                listener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), listener);

                RegistryClient registryClient = new RegistryClient(socket);
                registryClient.ReceiveDateTime = DateTime.Now;
                registryClient.RegistryClientType = CometClientType.AnKnown;
                registryClient.ClearBuffer();
                registryClient.Socket.BeginReceive(registryClient.ReceiveBuffer, 0, registryClient.ReceiveBuffer.Length
                    , SocketFlags.None, new AsyncCallback(ReceiveCallback), registryClient);

                Console.WriteLine("接收客户端：{0}", registryClient.Socket.RemoteEndPoint.ToString());

                lock (m_RegistryClients)
                {
                    m_RegistryClients.Add(registryClient);
                }
            }
            catch (Exception ex)
            {
                XTrace.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 接收请求回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            RegistryClient registryClient = (RegistryClient)ar.AsyncState;
            try
            {
                int dataLength = registryClient.Socket.EndReceive(ar);

                if(dataLength == 0)
                {
                    Console.WriteLine("接收客户端：{0}已经断开连接。", registryClient.ClientIP);
                    lock(m_RegistryClients){
                        m_RegistryClients.Remove(registryClient);
                        registryClient.Dispose();
                    }
                }
                else
                {
                    String data = Encoding.UTF8.GetString(registryClient.ReceiveBuffer, 0, dataLength);

                    Console.WriteLine("接收客户端：{0}发送的数据：{1}。", registryClient.ClientIP, data);

                    //--解析来自客户端的类型
                    CometMessage regMessage = XmlUtil.LoadObjFromXML<CometMessage>(data);
                    registryClient.RegistryClientType = regMessage.ClientType;
                    registryClient.ClientVersion = regMessage.ClientVersion;

                    registryClient.ClearBuffer();
                    registryClient.Socket.BeginReceive(registryClient.ReceiveBuffer, 0, registryClient.ReceiveBuffer.Length
                        , SocketFlags.None, new AsyncCallback(ReceiveCallback), registryClient);

                    //--由消息处理器进行处理
                    m_MessageProcessor.Process(registryClient, regMessage);
                };
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("接收数据时发生异常：" + ex.ToString());
                lock (m_RegistryClients)
                {
                    m_RegistryClients.Remove(registryClient);
                    registryClient.Dispose();
                }
            }
        }

        /// <summary>
        /// 向单个客户端发送数据
        /// </summary>
        /// <param name="rsClient"></param>
        /// <param name="data"></param>
        /// <param name="isAsync">默认为异步调用</param>
        public void SendData(RegistryClient registryClient, CometMessageAction action, String data, Boolean isAsync = true)
        {
            try
            {
                CometMessage rm = new CometMessage()
                {
                    Action = action,
                    MessageBody = data,
                    IsAsync = isAsync
                };

                String messageData = XmlUtil.SaveXmlFromObj<CometMessage>(rm);
                Console.WriteLine("发送数据：{0}", messageData);

                Byte[] msg = Encoding.UTF8.GetBytes(messageData);
                registryClient.Socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, new AsyncCallback(SendCallback), registryClient);
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("发送数据时发生异常：" + ex.ToString());
                lock (m_RegistryClients)
                {
                    m_RegistryClients.Remove(registryClient);
                    registryClient.Dispose();
                }
            }
        }

        /// <summary>
        /// 给所有的客户端发送消息
        /// </summary>
        public void SendDataToAllClient(CometMessageAction action, String message)
        {
            foreach (var client in m_RegistryClients)
            {
                SendData(client, action, message);
            }
        }

        /// <summary>
        /// 发送数据回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            RegistryClient registryClient = ar.AsyncState as RegistryClient;

            try
            {
                registryClient.Socket.EndSend(ar);
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("发送数据时发生异常：" + ex.ToString());
                lock (m_RegistryClients)
                {
                    m_RegistryClients.Remove(registryClient);
                    registryClient.Dispose();
                }
            }
        }
    }
}
