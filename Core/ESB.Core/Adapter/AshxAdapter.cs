using ESB.Core.Configuration;
using ESB.Core.Monitor;
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
    public abstract class AshxAdapter : IHttpHandler, IServiceAdapter
    {
        HttpContext m_HttpContext;

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private ESBTraceContext m_TraceContext;
        /// <summary>
        /// 跟踪上下文，用于追踪服务的调用情况
        /// </summary>
        public ESBTraceContext TraceContext
        {
            get
            {
                if (m_TraceContext == null)
                {
                    String traceContext = m_HttpContext.Request.Headers[Constant.ESB_HEAD_TRACE_CONTEXT];
                    String[] traceContextParams = traceContext.Split(":");
                    String parentInvokeID = String.Format("{0}{1}", traceContextParams[1], traceContextParams[2]);

                    m_TraceContext = new ESBTraceContext(traceContextParams[0], Int32.Parse(traceContextParams[1]) + 1, parentInvokeID);
                }

                return m_TraceContext;
            }
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            String serviceBegin = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");

            this.m_HttpContext = context;
            this.m_HttpContext.Response.ContentType = context.Request.ContentType;

            //--将跟踪上下文放入HttpContext,减少程序员传输的工作
            HttpContext.Current.Items[Constant.ESB_HEAD_TRACE_CONTEXT] = TraceContext;

            if (String.IsNullOrEmpty(this.m_HttpContext.Response.ContentType))
            {
                this.m_HttpContext.Response.ContentType = "text/plain; charset=gb2312";
            }
            //this.context.Response.ContentType = "text/plain; charset=gb2312";
            this.m_HttpContext.Response.ContentEncoding = System.Text.Encoding.UTF8;

            String esbAction = context.Request.Headers[Constant.ESB_HEAD_ANVOKE_ACTION];
            if (String.IsNullOrEmpty(esbAction))
            {
                this.m_HttpContext.Response.ContentType = "text/plain; charset=gb2312";
                this.m_HttpContext.Response.Write("未经授权的访问！");
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
                    this.m_HttpContext.Response.AppendHeader("Content-encoding", "gzip");
                    this.m_HttpContext.Response.Filter = new GZipStream(this.m_HttpContext.Response.Filter, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("DEFLATE"))
                {
                    this.m_HttpContext.Response.AppendHeader("Content-encoding", "deflate");
                    this.m_HttpContext.Response.Filter = new DeflateStream(this.m_HttpContext.Response.Filter, CompressionMode.Compress);
                }
            }
            String retMessage = DoEsbAction(esbAction, request);
            Byte[] retBuffer = Encoding.UTF8.GetBytes(retMessage);
            String serviceEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");


            this.m_HttpContext.Response.AppendHeader(Constant.ESB_HEAD_SERVICE_BEGIN, serviceBegin);
            this.m_HttpContext.Response.AppendHeader(Constant.ESB_HEAD_SERVICE_END, serviceEnd);

            this.m_HttpContext.Response.OutputStream.Write(retBuffer, 0, retBuffer.Length);
        }

        protected abstract String DoEsbAction(String esbAction, String request);

    }
}
