using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ESB.Core.Registry
{
    /// <summary>
    /// 长连接客户端：用于和注册中心保持联系
    /// </summary>
    public class CometClient
    {
        private String m_Uri = String.Empty;
        public event EventHandler<CometEventArgs> ReceiveNotify; 

        public CometClient(String uri)
        {
            m_Uri = uri;
        }

        public void Connect()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(m_Uri);
            webRequest.KeepAlive = true;
            webRequest.Method = "GET";


            IAsyncResult result = (IAsyncResult)webRequest.BeginGetResponse(new AsyncCallback(RespCallback), webRequest);
           
        }

        private void RespCallback(IAsyncResult asynchronousResult)
        {
            // State of request is asynchronous.
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse webResponse = (HttpWebResponse)myHttpWebRequest.EndGetResponse(asynchronousResult);

            String response;
            using (Stream newstream = webResponse.GetResponseStream())
            {
                using (StreamReader srRead = new StreamReader(newstream, System.Text.Encoding.Default))
                {
                    response = srRead.ReadToEnd();
                    srRead.Close();
                }
            }

            if (ReceiveNotify != null) {
                CometEventArgs eventArgs = new CometEventArgs();
                eventArgs.Response = response;

                ReceiveNotify(this, eventArgs);
            }

            Connect();
        }
    }

    /// <summary>
    /// 长连接客户端
    /// </summary>
    public class CometEventArgs : EventArgs
    {
        public String Response { get; set; }
    }
}
