using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESB.Core.Entity;
using System.Net;
using System.IO;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// ASHX一般服务接口程序调用客户端
    /// </summary>
    public class HandlerClient
    {
        public static ESB.Core.Schema.服务响应 CallAshxService(CallState callState)
        {
            //--STEP.1.从CallState中获取到需要的信息
            ESB.Core.Schema.服务请求 request = callState.Request;
            String message = callState.Request.消息内容;
            BindingTemplate binding = callState.Binding;
            String uri = callState.Binding.Address;
            String contentType = String.Equals(callState.Request.消息编码, "XML", StringComparison.OrdinalIgnoreCase)
                ? EsbClient.CONTENT_TYPE_XML : EsbClient.CONTENT_TYPE_JSON;

            //--STEP.2.根据method拼接URL
            if (String.IsNullOrEmpty(callState.Request.方法名称))
            {
                throw LogUtil.ExceptionAndLog(callState, "ASHX服务的方法名称必须填写", "", binding, request);
            }
            else
            {
                uri = uri + "?EsbAction=" + callState.Request.方法名称;
            }

            //--STEP.3.构造HTTP请求并调用ASHX服务
            ESB.Core.Schema.服务响应 response = new ESB.Core.Schema.服务响应();
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Accept = contentType;
                webRequest.Method = "POST";
                webRequest.ContentType = contentType;

                //--STEP.3.1.如果是POST请求，则需要将消息内容发送出去
                if (!String.IsNullOrEmpty(message))
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
                using (Stream newstream = webResponse.GetResponseStream())
                {
                    using (StreamReader srRead = new StreamReader(newstream, System.Text.Encoding.Default))
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
                    , callState.RequestBeginTime, callState.RequestEndTime, callState.CallBeginTime, callState.CallEndTime
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