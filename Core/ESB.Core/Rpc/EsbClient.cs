using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using ESB.Core.Entity;
using XCode;
using NewLife.Threading;
using NewLife.Log;
using ESB.Core.Monitor;
using ESB.Core.Configuration;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// ESB调用各种协议的接口的统一入口
    /// </summary>
    internal class EsbClient
    {

        /// <summary>
        /// 动态调用WebService主函数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ESB.Core.Schema.服务响应 DynamicalCallWebService(
              Boolean needResponse
            , ESB.Core.Schema.服务请求 request
            , List<BindingTemplate> bindings
            , Int32 version)
        {
            //--记录接收时间并放在请求结束时间中，用于判读ESB在接到请求到开始调用的耗时
            DateTime receiveDateTime = DateTime.Now;

            //--验证并预处理请求参数
            InvalidRequest(request);

            ESBTraceContext esbTraceContext = GetEsbTraceContext();


            //--获取到请求对应服务的绑定
            //EntityList<BindingTemplate> bindings = GetBindings(request);

            //--如果只有一个绑定，且需要返回结果
            if (bindings.Count == 1 && needResponse == true)
            {
                CallState state = new CallState()
                {
                    Binding = bindings[0],
                    Request = request,
                    RequestBeginTime = request.请求时间,
                    RequestEndTime = receiveDateTime,
                    TraceContext = esbTraceContext,
                    ServiceVersion = version
                };

                return CallService(state);
            }
            else //--此处实现软负载均衡、队列调用等一系列功能
            {
                CallState state = new CallState()
                {
                    Binding = bindings[0],
                    Request = request,
                    RequestBeginTime = request.请求时间,
                    RequestEndTime = receiveDateTime,
                    TraceContext = esbTraceContext,
                    ServiceVersion = version
                };

                return CallService(state);

                //foreach (BindingTemplate binding in bindings)
                //{
                //    CallState state = new CallState()
                //    {
                //        Binding = binding,
                //        Request = request,
                //        RequestBeginTime = request.请求时间,
                //        RequestEndTime = receiveDateTime,
                //        TraceContext = esbTraceContext
                //    };

                //    ThreadPoolX.QueueUserWorkItem(() => CallService(state));
                //}
                //return GetMultiResponse();
            }
        }

        /// <summary>
        /// 获取到EsbTraceConext
        /// </summary>
        /// <returns></returns>
        private static ESBTraceContext GetEsbTraceContext()
        {
            //--在Asp.NET环境下调用,可以从HttpContext.Current中获取到跟踪上下文
            if (HttpContext.Current != null && HttpContext.Current.Items[Constant.ESB_HEAD_TRACE_CONTEXT] != null)    
            {
                //--HttpContext.Current.Items[Constant.ESB_HEAD_TRACE_CONTEXT]的值由服务适配器确保实现
                return HttpContext.Current.Items[Constant.ESB_HEAD_TRACE_CONTEXT] as ESBTraceContext;
            }

            //--如果获取不到则代表第一次调用，进行跟踪上下文初始化
            return new ESBTraceContext(Guid.NewGuid().ToString(), 0, "");
        }


        #region 验证并进行动态路由
        /// <summary>
        /// 验证并预处理请求参数
        /// </summary>
        /// <param name="request"></param>
        private static void InvalidRequest(ESB.Core.Schema.服务请求 request)
        {
            if (String.IsNullOrEmpty(request.主机名称))
            {
                throw LogUtil.ExceptionAndLog("主机名称必须填写！", "", request);
            }

            if (String.IsNullOrEmpty(request.服务名称))
            {
                throw LogUtil.ExceptionAndLog("服务名称必须填写！", "", request);
            }
            
            if (String.IsNullOrEmpty(request.方法名称))
            {
                EntityList<BindingTemplate> bindings = GetBindings(request);
                if(bindings == null)
                    throw LogUtil.ExceptionAndLog("方法名称必须填写！", "", request);
                else
                    throw LogUtil.ExceptionAndLog("方法名称必须填写！", "", bindings[0], request);
            }

            //--如果用户没有传递请求时间，或则传入时间无效，则默认为当前时间
            if (request.请求时间.Year != DateTime.Now.Year)
                request.请求时间 = DateTime.Now;


            if (String.IsNullOrEmpty(request.消息内容))
                request.消息内容 = String.Empty;

            if (String.IsNullOrEmpty(request.密码))
                request.密码 = String.Empty;
        }

        /// <summary>
        /// 根据服务请求获取到对应的URL绑定信息
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static EntityList<BindingTemplate> GetBindings(ESB.Core.Schema.服务请求 request)
        {
            String serviceName = request.服务名称;
            BusinessService service = BusinessService.FindByServiceName(serviceName);

            if (service == null)
                throw LogUtil.ExceptionAndLog(String.Format("服务【{0}】尚未注册！", serviceName), "", request);


            //--查找某个服务下所有启用状态的绑定
            EntityList<BindingTemplate> bings = BindingTemplate.FindAllByServiceID(service.ServiceID);
            if (bings == null || bings.Count == 0)
            {
                throw LogUtil.ExceptionAndLog(String.Format("服务【{0}】尚未绑定任何路由地址！", serviceName), "", request);
            }

            return bings;
        }

        /// <summary>
        /// 根据绑定中的协议类型调用不同的实现方法
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private static ESB.Core.Schema.服务响应 CallService(CallState callState)
        {
            switch (callState.Binding.BindingType)
            {
                case (int)BindingType.WebService:
                    return WebServiceClient.CallWebService(callState);
                case (int)BindingType.REST:
                    return RestfulClient.CallRestfulService(callState);
                case (int)BindingType.ASHX:
                    return HandlerClient.CallAshxService(callState);
                case (int)BindingType.WCF_HTTP:
                    return WcfClient.CallWcfService(callState);
                case (int)BindingType.DUBBO_JSON:
                    return DubboClient.CallDubboService(callState);
                default:
                    throw LogUtil.ExceptionAndLog(callState, "未知的绑定类型", callState.Binding.BindingType.ToString(), callState.Binding, callState.Request);
            }
        }

        /// <summary>
        /// 返回多个路由地址的默认响应
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static ESB.Core.Schema.服务响应 GetMultiResponse()
        {
            ESB.Core.Schema.服务响应 response = new ESB.Core.Schema.服务响应();
            response.消息内容 = "此服务为发布订阅模式，请求已经交给ESB处理！";
            response.消息类型 = String.Empty;
            response.返回服务时间 = DateTime.Now;
            response.返回消息编码 = String.Empty;

            return response;
        }
        #endregion

    }
}
