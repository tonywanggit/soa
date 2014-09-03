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
            Int32 version = String.IsNullOrEmpty(context.Request["Version"]) ? 0 : Int32.Parse(context.Request["Version"]);
            String callback = context.Request["callback"];
            String message = GetMessageFromRequest(context.Request);

            String response = esbProxy.Invoke(serviceName, methodName, message);
            if (!String.IsNullOrEmpty(callback))
            {
                response = String.Format("{0}({{message:'{1}'}})", callback, response, version);
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
        private String GetMessageFromRequest(HttpRequest request)
        {
            //--如果取不到Message直接返回空
            if (request["Message"] == null)
                return String.Empty;

            //--如果能够从表单上获取到Message则优先使用
            if (request.Form["Message"] != null)
                return request.Form["Message"];

            //--如果表单上无法取到Message，则表示请求为GET请求，直接从URL上获取数据
            String rawUrl = request.Url.ToString();

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