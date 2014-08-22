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
            String message = context.Request["Message"].Trim();
            String callback = context.Request["callback"];

            String response = esbProxy.Invoke(serviceName, methodName, message);
            if (!String.IsNullOrEmpty(callback))
            {
                response = String.Format("{0}({{message:'{1}'}})", callback, response);
            }

            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.Default;
            context.Response.Write(response);
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