using ESB.Core.Configuration;
using ESB.Core.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ESB.Core.Adapter
{
    /// <summary>
    /// ESB HttpModule 用于记录服务响应时间和跟踪上下文
    /// </summary>
    public class ESBHttpModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            HttpContext context = application.Context;
            HttpResponse response = context.Response;

            //--记录服务开始时间
            context.Response.AppendHeader(Constant.ESB_HEAD_SERVICE_BEGIN, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"));

            //--记录传递过来的跟踪上下文
            String traceContextString = context.Request.Headers[Constant.ESB_HEAD_TRACE_CONTEXT];
            if (!String.IsNullOrEmpty(traceContextString))
            {
                String[] traceContextParams = traceContextString.Split(":");
                ESBTraceContext traceContext = new ESBTraceContext(traceContextParams[0], Int32.Parse(traceContextParams[1]) + 1, traceContextParams[2]);

                //--将跟踪上下文放入HttpContext,减少程序员传输的工作
                context.Items[Constant.ESB_HEAD_TRACE_CONTEXT] = traceContext;
            }

        }

        void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            HttpContext context = application.Context;
            HttpResponse response = context.Response;

            //--记录服务开始时间
            response.AppendHeader(Constant.ESB_HEAD_SERVICE_END, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
        }

    }
}
