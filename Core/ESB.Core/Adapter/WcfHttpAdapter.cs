using ESB.Core.Configuration;
using ESB.Core.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;

namespace ESB.Core.Adapter
{

    [ServiceContract(Namespace = "http://mb.com", Name = "IEsbAction")]
    public interface IEsbAction
    {
        [OperationContract(Action = "http://mb.com/EsbAction")]
        String EsbAction(String request);
    }

    /// <summary>
    /// WcfHttp适配器
    /// 使用时需要将继承类标识为 [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    /// 同时需要将Config文件中增加配置如下：
    /// <system.serviceModel>
    /// <serviceHostingEnvironment multipleSiteBindingsEnabled="true" aspNetCompatibilityEnabled="true" />
    /// </system.serviceModel>
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public abstract class WcfHttpAdapter : IEsbAction
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
                    String traceContext = HttpContext.Current.Request.Headers[Constant.ESB_HEAD_TRACE_CONTEXT];
                    String[] traceContextParams = traceContext.Split(":");

                    m_TraceContext = new ESBTraceContext(traceContextParams[0], Int32.Parse(traceContextParams[1]) + 1);
                }

                return m_TraceContext;
            }
        }

        /// <summary>
        /// 执行服务方法，并加入跟踪
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string EsbAction(String request)
        {
            String serviceBegin = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");

            //--将跟踪上下文放入HttpContext,减少程序员传输的工作
            HttpContext.Current.Items[Constant.ESB_HEAD_TRACE_CONTEXT] = TraceContext;

            //--从Http请求头信息中获取到调用方法的名称
            String esbAction = HttpContext.Current.Request.Headers[Constant.ESB_HEAD_ANVOKE_ACTION];
            String message = DoEsbAction(esbAction, request);

            String serviceEnd = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");

            HttpContext.Current.Response.AppendHeader(Constant.ESB_HEAD_SERVICE_BEGIN, serviceBegin);
            HttpContext.Current.Response.AppendHeader(Constant.ESB_HEAD_SERVICE_END, serviceEnd);

            return message;
        }

        protected abstract String DoEsbAction(String esbAction, String request);
    }
}
