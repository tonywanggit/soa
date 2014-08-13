using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

        public String TraceContent { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            this.context.Response.ContentType = context.Request.ContentType;
            if (String.IsNullOrEmpty(this.context.Response.ContentType))
            {
                this.context.Response.ContentType = "text/plain; charset=gb2312";
            }
            //this.context.Response.ContentType = "text/plain; charset=gb2312";
            this.context.Response.ContentEncoding = System.Text.Encoding.UTF8;

            String esbAction = context.Request["EsbAction"];
            if (String.IsNullOrEmpty(esbAction))
            {
                this.context.Response.ContentType = "text/plain; charset=gb2312";
                this.context.Response.Write("未经授权的访问！");
                //this.context.Response.End();
                return;
            }

            String request = String.Empty;
            using (StreamReader srRead = new StreamReader(context.Request.InputStream, System.Text.Encoding.Default))
            {
                request = srRead.ReadToEnd();
            }

            String acceptEncoding = context.Request.Headers["Accept-Encoding"];//.ToString().ToUpperInvariant();
            
            if (!String.IsNullOrEmpty(acceptEncoding))
            {
                acceptEncoding = acceptEncoding.ToString().ToUpperInvariant();
                if (acceptEncoding.Contains("GZIP"))
                {
                    this.context.Response.AppendHeader("Content-encoding", "gzip");
                    this.context.Response.Filter = new GZipStream(this.context.Response.Filter, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("DEFLATE"))
                {
                    this.context.Response.AppendHeader("Content-encoding", "deflate");
                    this.context.Response.Filter = new DeflateStream(this.context.Response.Filter, CompressionMode.Compress);
                }
            }

            String retMessage = DoEsbAction(esbAction, request);
            Byte[] retBuffer = Encoding.UTF8.GetBytes(retMessage);

            this.context.Response.OutputStream.Write(retBuffer, 0, retBuffer.Length);


        }

        protected abstract String DoEsbAction(String esbAction, String request);
    }
}
