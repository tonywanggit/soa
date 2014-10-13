using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;

namespace MB.SOA.Client
{
    /// <summary>
    /// 调用中心代理类
    /// </summary>
    public class CallCenterProxy
    {
        /// <summary>
        /// 根据调用中心的地址获取到
        /// </summary>
        private static Dictionary<String, CallCenterProxy> m_InstanceDict = new Dictionary<String,CallCenterProxy>();

        /// <summary>
        /// 调用中心地址
        /// </summary>
        private String m_CallCenterUri;
        /// <summary>
        /// 超时配置：默认100s
        /// </summary>
        private Int32 m_Timeout ;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="callCenterUri"></param>
        private CallCenterProxy(String callCenterUri)
        {
            if (String.IsNullOrEmpty(callCenterUri))
                throw new Exception("传入有效的调用中心地址。");

            m_CallCenterUri = callCenterUri;

            //--默认100S, 因为IIS的executionTimeout默认配置为110S, 考虑网络因素100S较为安全
            //--http://msdn.microsoft.com/zh-cn/library/vstudio/e1f13641(v=vs.100).aspx
            m_Timeout = 100 * 1000;
        }

        /// <summary>
        /// 获取到调用中心代理实例
        /// </summary>
        /// <param name="callCenterUrl"></param>
        /// <returns></returns>
        public static CallCenterProxy GetInstance(String callCenterUrl)
        {
            if (m_InstanceDict.ContainsKey(callCenterUrl))
                return m_InstanceDict[callCenterUrl];

            //if (m_Instance != null) return m_Instance;

            //--此处可以降低第一次调用的时间：2~3秒减少到200ms左右
            HttpWebRequest.DefaultWebProxy = null;
            //HttpWebRequest.DefaultCachePolicy = null;

            //--客户端的连接数
            //--http://www.cnblogs.com/summer_adai/archive/2013/04/26/3045274.html
            ServicePointManager.DefaultConnectionLimit = 10000;

            //--创建客户端代理
            //String ccUri = ConfigurationManager.AppSettings["MB.SOA.CallCenterUri"];
            CallCenterProxy proxy;
            lock (m_InstanceDict)
            {
                if (m_InstanceDict.ContainsKey(callCenterUrl))
                    proxy = m_InstanceDict[callCenterUrl];
                else
                {
                    proxy = new CallCenterProxy(callCenterUrl);
                    m_InstanceDict[callCenterUrl] = proxy;
                }
            }

            return proxy;
        }

        /// <summary>
        /// 获取或设置超时
        /// </summary>
        public Int32 Timeout{
            get { return m_Timeout; }
            set {
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
