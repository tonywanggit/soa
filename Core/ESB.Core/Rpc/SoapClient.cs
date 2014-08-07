using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESB.Core.Entity;
using System.Reflection;
using NewLife.Log;

namespace ESB.Core.Rpc
{
    public class SoapClient
    {
        /// <summary>
        /// 利用反射调用目标服务
        /// </summary>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static ESB.Core.Schema.服务响应 CallWebService(CallState state)
        {
            CallState callState = state as CallState;
            BindingTemplate binding = callState.Binding;
            ESB.Core.Schema.服务请求 request = callState.Request;

            SoapClientItem soapClient = null;

            try
            {
                soapClient = SoapClientCache.GetItem(binding.Address, request.服务名称);
            }
            catch (Exception ex)
            {
                throw LogUtil.ExceptionAndLog(callState, "获取目标服务的代理程序集时发生异常", ex.Message, binding, request);
            }

            if (soapClient != null)
            {
                MethodInfo method = soapClient.GetMethodInfo(request.方法名称);
                if (method == null)
                    throw LogUtil.ExceptionAndLog(callState, "目标服务未实现方法", request.方法名称, binding, request);

                Object req = GetRequest(soapClient, request);
                if (req == null)
                    throw LogUtil.ExceptionAndLog(callState, "无法将请求转换成目标服务所需要的输入参数", "", binding, request);


                Object res = null;
                try
                {
                    callState.CallBeginTime = DateTime.Now;
                    res = method.Invoke(soapClient.PortObject, new Object[] { req });
                    callState.CallEndTime = DateTime.Now;
                }
                catch (Exception ex)
                {
                    callState.CallEndTime = DateTime.Now;

                    String message = String.Empty;
                    if (ex.InnerException != null)
                        message = ex.InnerException.Message;
                    else
                        message = ex.Message;

                    throw LogUtil.ExceptionAndLog(callState, "调用目标服务抛出异常", message, binding, request);
                }

                ESB.Core.Schema.服务响应 response = GetResponse(res, method.ReturnType);
                LogUtil.AddAuditLog(
                    1
                    , binding
                    , callState.RequestBeginTime, callState.RequestEndTime, callState.CallBeginTime, callState.CallEndTime
                    , response.消息内容, request);

                return response;
            }
            else
            {
                throw LogUtil.ExceptionAndLog(callState, "获取目标服务的代理程序集失败！", "", binding, request);
            }
        }

        /// <summary>
        /// 将目标服务响应转换成标准的ESB响应
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static ESB.Core.Schema.服务响应 GetResponse(object target, Type type)
        {
            ESB.Core.Schema.服务响应 response = new ESB.Core.Schema.服务响应();
            if (target != null)
            {
                response.消息内容 = type.InvokeMember("消息内容", BindingFlags.GetProperty, null, target, null) as String;
                response.消息类型 = type.InvokeMember("消息类型", BindingFlags.GetProperty, null, target, null) as String;
                response.返回消息编码 = type.InvokeMember("返回消息编码", BindingFlags.GetProperty, null, target, null) as String;
            }
            else
            {
                response.消息内容 = "返回消息为NULL";
                response.消息类型 = String.Empty;
                response.返回消息编码 = String.Empty;
            }
            response.返回服务时间 = DateTime.Now;

            return response;
        }


        /// <summary>
        /// 将请求消息转换成目标服务的输入参数
        /// </summary>
        /// <param name="asm"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private static Object GetRequest(SoapClientItem soapClient, ESB.Core.Schema.服务请求 request)
        {
            object target = null;
            try
            {
                target = soapClient.CreateRequestObject();
                if (target != null)
                {
                    soapClient.SetReqObjProperty(target, "主机名称", request.主机名称);
                    soapClient.SetReqObjProperty(target, "服务名称", request.服务名称);
                    soapClient.SetReqObjProperty(target, "方法名称", request.方法名称);
                    soapClient.SetReqObjProperty(target, "消息内容", request.消息内容);
                    soapClient.SetReqObjProperty(target, "密码", request.密码);
                    soapClient.SetReqObjProperty(target, "请求时间", request.请求时间);
                }
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("构造请求参数失败：" + ex.Message);
            }
            return target;
        }

    }
}