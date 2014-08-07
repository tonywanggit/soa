using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace NewLife.Net
{
    /// <summary>
    /// 网络端口快速检测类
    /// </summary>
    public class NetPortON
    {
        private static Boolean IsConnectionSuccessful = false;
        private static Exception socketException;
        private static ManualResetEvent timeoutObject = new ManualResetEvent(false);

        public static bool ConnValidate(String host, int port, int timeoutMSec)
        {
            bool ret = false;
            timeoutObject.Reset();
            socketException = null;

            TcpClient tcpClient = new TcpClient();
            tcpClient.BeginConnect(host, port, new AsyncCallback(CallBackMethod), tcpClient);

            if (timeoutObject.WaitOne(timeoutMSec, false))
            {
                if (IsConnectionSuccessful)
                {
                    ret = true;
                    tcpClient.Close();
                }
                else
                {
                    //throw socketException;
                }
            }
            else
            {
                //throw new TimeoutException();
            }
            tcpClient.Close();

            return ret;
        }

        private static void CallBackMethod(IAsyncResult asyncResult)
        {
            try
            {
                IsConnectionSuccessful = false;
                TcpClient tcpClient = asyncResult.AsyncState as TcpClient;
                if (tcpClient != null)
                {
                    tcpClient.EndConnect(asyncResult);
                    IsConnectionSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                IsConnectionSuccessful = false;
                socketException = ex;
            }
            finally
            {
                timeoutObject.Set();
            }
        }
    }
}
