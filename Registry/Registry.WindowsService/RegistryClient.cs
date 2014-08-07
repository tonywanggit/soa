using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Registry.WindowsService
{
    internal class RegistryClient
    {
        public Socket Socket { get; set; }
        public Byte[] ReceiveBuffer { get; set; }

        public RegistryClient(Socket socket)
        {
            Socket = socket;
        }

        public void ClearBuffer()
        {
            ReceiveBuffer = new Byte[1024];
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
