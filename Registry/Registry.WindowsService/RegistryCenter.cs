using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using NewLife.Log;

namespace Registry.WindowsService
{
    internal class RegistryCenter
    {
        List<RegistryClient> m_RegistryClients = new List<RegistryClient>();
        TcpListener m_TcpListener;
        MonitorThread m_monitorThread;

        public RegistryCenter() { }

        public void Start()
        {
            IPEndPoint localep = new IPEndPoint(IPAddress.Parse("192.168.156.138"), 5555);
            m_TcpListener = new TcpListener(localep);
            m_TcpListener.Start(2000);

            m_TcpListener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), m_TcpListener);
            Console.WriteLine("注册中心服务正在监控：5555端口！");

            m_monitorThread = new MonitorThread(this);
            m_monitorThread.Start();
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                TcpListener listener = ar.AsyncState as TcpListener;
                Socket socket = listener.EndAcceptSocket(ar);
                listener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), listener);

                RegistryClient registryClient = new RegistryClient(socket);
                registryClient.ReceiveDateTime = DateTime.Now;
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

        private void ReceiveCallback(IAsyncResult ar)
        {
            RegistryClient registryClient = (RegistryClient)ar.AsyncState;
            try
            {
                int dataLength = registryClient.Socket.EndReceive(ar);
                if(dataLength == 0)
                {
                    Console.WriteLine("接收客户端：{0}已经断开连接。", registryClient.Socket.RemoteEndPoint.ToString());
                    lock(m_RegistryClients){
                        m_RegistryClients.Remove(registryClient);
                        registryClient.Dispose();
                    }
                }else
                {
                    String data = Encoding.UTF8.GetString(registryClient.ReceiveBuffer, 0, dataLength);

                    Console.WriteLine("接收客户端：{0}发送的数据：{1}。", registryClient.Socket.RemoteEndPoint.ToString(), data);

                    registryClient.ClearBuffer();
                    registryClient.Socket.BeginReceive(registryClient.ReceiveBuffer, 0, registryClient.ReceiveBuffer.Length
                        , SocketFlags.None, new AsyncCallback(ReceiveCallback), registryClient);

                    //--进行业务操作
                    SendData(registryClient, "Hello From Server!");
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
        private void SendData(RegistryClient registryClient, String data)
        {
            try
            {
                Console.WriteLine("发送数据：{0}", data);

                Byte[] msg = Encoding.UTF8.GetBytes(data);
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
        public void SendDataToAllClient(String message)
        {
            foreach (var client in m_RegistryClients)
            {
                SendData(client, message);
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
