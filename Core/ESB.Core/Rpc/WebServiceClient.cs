using ESB.Core.Configuration;
using ESB.Core.Entity;
using ESB.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// WebService调用客户端
    /// </summary>
    internal class WebServiceClient
    {
        private const String SOAP_MESSAGE_TEMPLATE = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body><EsbAction xmlns=""{0}""><request>{1}</request></EsbAction></s:Body></s:Envelope>";

        public static ESB.Core.Schema.服务响应 CallWebService(CallState callState)
        {
            //--STEP.1.从CallState中获取到需要的信息
            ESB.Core.Schema.服务请求 request = callState.Request;
            String message = callState.Request.消息内容;
            BindingTemplate binding = callState.Binding;
            String uri = callState.Binding.Address;

            //--STEP.3.构造HTTP请求并调用ASHX服务
            ESB.Core.Schema.服务响应 response = new ESB.Core.Schema.服务响应();
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Timeout = callState.ServiceConfig.Timeout;
                webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
                webRequest.Headers.Add(Constant.ESB_HEAD_TRACE_CONTEXT, callState.TraceContext.ToString());
                webRequest.Headers.Add(Constant.ESB_HEAD_ANVOKE_ACTION, callState.Request.方法名称);

                if (String.IsNullOrEmpty(callState.Request.方法名称))
                {
                    throw LogUtil.ExceptionAndLog(callState, "WCF-HTTP服务的方法名称必须填写", "", binding, request);
                }
                else
                {
                    webRequest.Headers.Add("SOAPAction", String.Format("{0}/EsbAction", Constant.COMPANY_URL));
                }

                //--STEP.3.1.如果是POST请求，则需要将消息内容发送出去
                //if (!String.IsNullOrEmpty(message))
                {
                    String reqMessage = CommonUtil.XmlEncoding(request.消息内容);
                    String esbAction = request.方法名称;
                    String soapMessage = String.Format(SOAP_MESSAGE_TEMPLATE, Constant.COMPANY_URL, reqMessage);
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(soapMessage);
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

                if (webResponse.ContentEncoding.ToLower().Contains("gzip"))
                {
                    using (GZipStream stream = new GZipStream(webResponse.GetResponseStream(), CompressionMode.Decompress))
                    {
                        using (StreamReader srRead = new StreamReader(stream, System.Text.Encoding.UTF8))
                        {
                            String outString = srRead.ReadToEnd();
                            callState.CallEndTime = DateTime.Now;

                            response.消息内容 = GetMessageFromSOAP(outString);
                            srRead.Close();
                        }
                    }
                }
                else if (webResponse.ContentEncoding.ToLower().Contains("deflate"))
                {
                    using (DeflateStream stream = new DeflateStream(webResponse.GetResponseStream(), CompressionMode.Decompress))
                    {
                        using (StreamReader srRead = new StreamReader(stream, System.Text.Encoding.UTF8))
                        {
                            String outString = srRead.ReadToEnd();
                            callState.CallEndTime = DateTime.Now;

                            response.消息内容 = GetMessageFromSOAP(outString);
                            srRead.Close();
                        }
                    }
                }
                else
                {
                    using (Stream newstream = webResponse.GetResponseStream())
                    {
                        using (StreamReader srRead = new StreamReader(newstream, System.Text.Encoding.UTF8))
                        {
                            String outString = srRead.ReadToEnd();
                            callState.CallEndTime = DateTime.Now;

                            response.消息内容 = GetMessageFromSOAP(outString);
                            srRead.Close();
                        }
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

        /// <summary>
        /// 从SOAP消息中获取到消息内容
        /// </summary>
        /// <param name="soapXml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        private static String GetMessageFromSOAP(String soapXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(soapXml);

            XmlNodeList nodes = doc.GetElementsByTagName("EsbActionResult");
            if (nodes != null && nodes.Count > 0)
                return nodes[0].InnerText;
            else
                return soapXml;
        }
    }
}
