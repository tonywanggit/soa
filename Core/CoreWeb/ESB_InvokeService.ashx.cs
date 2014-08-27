using ESB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ESB.CallCenter
{
    /// <summary>
    /// 总线请求响应调用接口
    /// </summary>
    public class ESB_InvokeService : IHttpHandler
    {
        ESBProxy esbProxy = ESBProxy.GetInstance();

        public void ProcessRequest(HttpContext context)
        {
            String serviceName = context.Request["ServiceName"].Trim();
            String methodName = context.Request["MethodName"].Trim();
            String callback = context.Request["callback"];
            String message = GetMessageFromUrl(context);

            String response = esbProxy.Invoke(serviceName, methodName, message);
            if (!String.IsNullOrEmpty(callback))
            {
                response = String.Format("{0}({{message:'{1}'}})", callback, response);
            }

            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.Default;
            context.Response.Write(response);
        }

        /// <summary>
        /// 从原始请求链接中获取到Message
        /// </summary>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        private String GetMessageFromUrl(HttpContext context)
        {
            String rawUrl = context.Request.RawUrl;

            if (rawUrl.Contains("&MessageURLEncoder="))
                return context.Request["MessageURLEncoder"].Trim();

            if (rawUrl.Contains("&Message="))
                return rawUrl.Substring(rawUrl.IndexOf("&Message=") + 9);

            throw new Exception("无效的调用方式！");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}