using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;

namespace MB.ESB.CallCenterTest
{
    /// <summary>
    /// 调用中心代理类：此为演示代码，可以更具要求自行定义
    /// </summary>
    internal class CallCenterProxy
    {
        private static CallCenterProxy m_Instance;

        private String m_CallCenterUri;
        private Int32 m_Timeout ;

        private CallCenterProxy(String callCenterUri)
        {
            m_CallCenterUri = callCenterUri;

            //--默认100S, 因为IIS的executionTimeout默认配置为110S, 考虑网络因素100S较为安全
            //--http://msdn.microsoft.com/zh-cn/library/vstudio/e1f13641(v=vs.100).aspx
            m_Timeout = 100 * 1000;
        }

        public static CallCenterProxy GetInstance()
        {
            if (m_Instance != null) return m_Instance;

            //--此处可以降低第一次调用的时间：2~3秒减少到200ms左右
            HttpWebRequest.DefaultWebProxy = null;
            //HttpWebRequest.DefaultCachePolicy = null;

            //--客户端的连接数
            //--http://www.cnblogs.com/summer_adai/archive/2013/04/26/3045274.html
            ServicePointManager.DefaultConnectionLimit = 10000;

            //--创建客户端代理
            String ccUri = ConfigurationManager.AppSettings["MB.SOA.CallCenterUri"];
            if (String.IsNullOrEmpty(ccUri))
            {
                throw new Exception("请在AppSettings节配置调用中心地址：MB.SOA.CallCenterUri。");
            }

            CallCenterProxy proxy = new CallCenterProxy(ccUri);
            Interlocked.CompareExchange<CallCenterProxy>(ref m_Instance, proxy, null);

            return proxy;
        }

        /// <summary>
        /// 获取或设置超时
        /// </summary>
        public Int32 Timeout{
            get { return m_Timeout; }
            set {
                if (value > 100 * 1000)
                    throw new Exception("超时配置应该小于100 * 1000。");
                m_Timeout = value; 
            }
        }

        /// <summary>
        /// 调用HttpWebRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private String CallWebRequet(String request)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(m_CallCenterUri);
            webRequest.Method = "POST";
            webRequest.Timeout = Timeout;
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

            byte[] data = System.Text.Encoding.Default.GetBytes(request);
            using (Stream stream = webRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            String response;
            using (Stream newstream = webResponse.GetResponseStream())
            {
                using (StreamReader srRead = new StreamReader(newstream, System.Text.Encoding.UTF8))
                {
                    response = srRead.ReadToEnd();
                }
            }

            if (!String.IsNullOrEmpty(response) && response.StartsWith("MBSOA-CallCenter-Error:"))
                throw new Exception(response.Substring(23));

            return response;
        }

        /// <summary>
        /// 请求响应调用端口
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public String Invoke(String serviceName, String methodName, String message, Int32 version = 0, Boolean noCache = false)
        {
            String request = String.Format("ServiceName={0}&MethodName={1}&Message={2}&Version={3}&NoCache={4}"
                , HttpUtility.UrlEncode(serviceName)
                , HttpUtility.UrlEncode(methodName)
                , HttpUtility.UrlEncode(message), version
                , noCache ? 1 : 0);

            String response = CallWebRequet(request);

            return response;
        }

        /// <summary>
        /// 队列化调用
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        /// <param name="version"></param>
        public void InvokeQueue(String serviceName, String methodName, String message, Int32 version = 0)
        {
            String request = String.Format("ServiceName={0}&MethodName={1}&Message={2}&Version={3}&IsQueue=1"
                , HttpUtility.UrlEncode(serviceName)
                , HttpUtility.UrlEncode(methodName)
                , HttpUtility.UrlEncode(message), version);


            String response = CallWebRequet(request);

            if (response != "OK")
            {
                throw new Exception(response);
            }
        }
    }
}
