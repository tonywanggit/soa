using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using ESB.Core.Entity;
using System.Text;
using ESB.Core.Configuration;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// Restful服务调用客户端
    /// </summary>
    internal class RestfulClient
    {

        public static ESB.Core.Schema.服务响应 CallRestfulService(CallState callState)
        {
            //--STEP.1.从CallState中获取到需要的信息
            ESB.Core.Schema.服务请求 request = callState.Request;
            String message = callState.Request.消息内容;
            BindingTemplate binding = callState.Binding;
            String uri = callState.Binding.Address.TrimEnd('/');
            String contentType = Constant.CONTENT_TYPE_JSON;
            String method = "POST";
            String methodName = String.Empty;

            String[] methodParams = callState.Request.方法名称.Split(":");
            if (methodParams.Length == 1)
            {
                method = "POST";
                methodName = methodParams[0];
                contentType = Constant.CONTENT_TYPE_JSON;
            }
            else if (methodParams.Length == 2)
            {
                method = methodParams[0];
                methodName = methodParams[1];
                contentType = Constant.CONTENT_TYPE_JSON;
            }
            else if (methodParams.Length == 3)
            {
                method = methodParams[0];
                methodName = methodParams[2];
                contentType = String.Compare("JSON", methodParams[1], true) == 0 ? Constant.CONTENT_TYPE_JSON : Constant.CONTENT_TYPE_XML;
            }

            //contentType = Constant.CONTENT_TYPE_JSON;


            //--STEP.2.根据method拼接URL
            if (method.ToUpper() == "GET")
            {
                uri = uri + "/" + methodName + "?" + message;
            }
            else
            {
                uri = uri + "/" + methodName;
            }

            //uri = "http://10.100.20.180:8016/" + methodName + "?format=json";
            //uri = "http://localhost:8080/" + methodName;// +"?format=json";

            //--STEP.3.构造HTTP请求并调用RESTful服务
            ESB.Core.Schema.服务响应 response = new ESB.Core.Schema.服务响应();
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Accept = contentType;
                webRequest.Method = method;
                webRequest.ContentType = contentType;
                webRequest.Timeout = callState.ServiceConfig.Timeout;
                webRequest.Headers.Add(Constant.ESB_HEAD_TRACE_CONTEXT, callState.TraceContext.ToString());


                //--STEP.3.1.如果是POST请求，则需要将消息内容发送出去
                if (method == "POST" && !String.IsNullOrEmpty(message))
                {
                    byte[] data = System.Text.Encoding.Default.GetBytes(message);
                    using (Stream stream = webRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }

                //--STEP.3.2.获取到响应消息
                callState.CallBeginTime = DateTime.Now;
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                //--从返回头消息中取到服务调用的时间
                callState.ServiceBeginTime = webResponse.Headers[Constant.ESB_HEAD_SERVICE_BEGIN];
                callState.ServiceEndTime = webResponse.Headers[Constant.ESB_HEAD_SERVICE_END];

                using (Stream newstream = webResponse.GetResponseStream())
                {
                    using (StreamReader srRead = new StreamReader(newstream, System.Text.Encoding.UTF8))
                    {
                        String outString = srRead.ReadToEnd();
                        callState.CallEndTime = DateTime.Now;

                        response.消息内容 = outString;
                        srRead.Close();
                    }
                }

                //--STEP.3.3.记录日志并返回ESB响应
                LogUtil.AddAuditLog(
                    1
                    , binding
                    , callState
                    , response.消息内容, request);

            }
            catch (Exception ex)
            {
                callState.CallEndTime = DateTime.Now;

                String exMessage = String.Empty;
                if (ex.InnerException != null)
                    exMessage = ex.InnerException.Message;
                else
                    exMessage = ex.Message;

                throw LogUtil.ExceptionAndLog(callState, "调用目标服务抛出异常", exMessage, binding, request);
            }

            return response;
        }
    }
}