using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ESB.Core.Adapter
{
    /// <summary>
    /// Asp.NET一般处理程序ESB包装器
    /// </summary>
    public abstract class AshxAdapter : IHttpHandler
    {
        HttpContext context;

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            this.context.Response.ContentType = context.Request.ContentType;
            if (String.IsNullOrEmpty(this.context.Response.ContentType))
            {
                this.context.Response.ContentType = "text/plain; charset=utf-8";
            }

            String esbAction = context.Request["EsbAction"];
            if (String.IsNullOrEmpty(esbAction))
            {
                this.context.Response.ContentType = "text/plain; charset=utf-8";
                this.context.Response.Write("未经授权的访问！");
                //this.context.Response.End();
                return;
            }

            String request = String.Empty;
            using (StreamReader srRead = new StreamReader(context.Request.InputStream, System.Text.Encoding.Default))
            {
                request = srRead.ReadToEnd();
            }

            this.context.Response.Write(DoEsbAction(esbAction, request));
        }

        protected abstract String DoEsbAction(String esbAction, String request);
    }
}
