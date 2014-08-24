using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using ESB.Core.Registry;
using ESB.Core.Monitor;
using ESB.Core.Rpc;

namespace Monitor.WindowsService
{
    /// <summary>
    /// 对连接到监控中心的Socket进行封装
    /// </summary>
    public class MonitorClient
    {
        public Socket Socket { get; set; }
        public Byte[] ReceiveBuffer { get; set; }
        public DateTime ReceiveDateTime { get; set; }
        public CometClientType MonitorClientType { get; set; }

        public MonitorClient(Socket socket)
        {
            Socket = socket;
        }

        public void ClearBuffer()
        {
            ReceiveBuffer = new Byte[1024 * 10];
        }

        public void Dispose()
        {
            try
            {
                Socket.Shutdown(SocketShutdown.Both);
                Socket.Close();
            }
            finally
            {
                Socket = null;
                ReceiveBuffer = null;
            }
        }
    }
}
