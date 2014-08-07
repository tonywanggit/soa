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

        public RegistryCenter() { }

        public void Start()
        {
            IPEndPoint localep = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName())[0], 5555);
            m_TcpListener = new TcpListener(localep);
            m_TcpListener.Start(2000);

            m_TcpListener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), m_TcpListener);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                TcpListener listener = ar.AsyncState as TcpListener;
                Socket socket = listener.EndAcceptSocket(ar);
                listener.BeginAcceptSocket(new AsyncCallback(AcceptCallback), listener);

                RegistryClient registryClient = new RegistryClient(socket);
                registryClient.ClearBuffer();
                registryClient.Socket.BeginReceive(registryClient.ReceiveBuffer, 0, registryClient.ReceiveBuffer.Length
                    , SocketFlags.None, new AsyncCallback(ReceiveCallback), registryClient);

                lock (m_RegistryClients)
                {
                    m_RegistryClients.Add(registryClient);
                }
            }
            catch (Exception ex)
            {
                XTrace.WriteLine(ex.ToString());
                throw;
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
                    lock(m_RegistryClients){
                        m_RegistryClients.Remove(registryClient);
                        registryClient.Dispose();
                    }
                }else
                {
                    String data = Encoding.UTF8.GetString(registryClient.ReceiveBuffer, 0, dataLength);

                    registryClient.ClearBuffer();
                    registryClient.Socket.BeginReceive(registryClient.ReceiveBuffer, 0, registryClient.ReceiveBuffer.Length
                        , SocketFlags.None, new AsyncCallback(ReceiveCallback), registryClient);

                    //--进行业务操作

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
                throw;
            }
        }
    }
}
