using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESB.Core.Entity;
using System.Net;
using System.IO;
using System.Xml;
using ESB.Core.Util;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// WCF-HTTP服务调用代理类
    /// </summary>
    public class WcfClient
    {
        private const String SOAP_MESSAGE_TEMPLATE = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body><Demo xmlns=""{0}""><request>{1}</request></Demo></s:Body></s:Envelope>";

        public static ESB.Core.Schema.服务响应 CallWcfService(CallState callState)
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
                webRequest.ContentType = "text/xml";

                if (String.IsNullOrEmpty(callState.Request.方法名称))
                {
                    throw LogUtil.ExceptionAndLog(callState, "WCF-HTTP服务的方法名称必须填写", "", binding, request);
                }
                else
                {
                    webRequest.Headers.Add("SOAPAction", String.Format("{0}/{1}", EsbClient.COMPANY_URL, request.方法名称));
                }

                //--STEP.3.1.如果是POST请求，则需要将消息内容发送出去
                if (!String.IsNullOrEmpty(message))
                {
                    String reqMessage = CommonUtil.XmlEncoding(request.消息内容);
                    String soapMessage = String.Format(SOAP_MESSAGE_TEMPLATE, EsbClient.COMPANY_URL, reqMessage);
                    byte[] data = System.Text.Encoding.Default.GetBytes(soapMessage);
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

                        response.消息内容 = GetMessageFromSOAP(outString, "//" + callState.Request.方法名称 + "Result");
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

        /// <summary>
        /// 从SOAP消息中获取到消息内容
        /// </summary>
        /// <param name="soapXml"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        private static String GetMessageFromSOAP(String soapXml, String xpath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(soapXml);

            XmlNodeList nodes = doc.GetElementsByTagName("DemoResult");
            if (nodes != null && nodes.Count > 0)
                return nodes[0].InnerText;
            else
                return soapXml;
        }
    }
}