using ESB.Core.Configuration;
using ESB.Core.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace ESB.Core.Adapter
{
    /// <summary>
    /// WebService适配器
    /// </summary>
    [WebService(Namespace="http://mb.com")]
    public abstract class WebServiceAdapter : WebService
    {
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
                    String traceContext = Context.Request.Headers[Constant.ESB_HEAD_TRACE_CONTEXT];
                    String[] traceContextParams = traceContext.Split(":");

                    m_TraceContext = new ESBTraceContext(traceContextParams[0], Int32.Parse(traceContextParams[1]) + 1);
                }

                return m_TraceContext;
            }
        }

        [WebMethod(Description="ESB路由接口")]
        public string EsbAction(String request)
        {
            String serviceBegin = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");

            //--将跟踪上下文放入HttpContext,减少程序员传输的工作
            HttpContext.Current.Items[Constant.ESB_HEAD_TRACE_CONTEXT] = TraceContext;

            //--从Http请求头信息中获取到调用方法的名称
            String esbAction = Context.Request.Headers[Constant.ESB_HEAD_ANVOKE_ACTION];
            String message = DoEsbAction(esbAction, request);

            String serviceEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");

            this.Context.Response.AppendHeader(Constant.ESB_HEAD_SERVICE_BEGIN, serviceBegin);
            this.Context.Response.AppendHeader(Constant.ESB_HEAD_SERVICE_END, serviceEnd);

            return message;
        }

        protected abstract String DoEsbAction(String esbAction, String request);
    }
}
