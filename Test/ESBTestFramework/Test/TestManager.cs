using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace ESB.TestFramework.Test
{
    /// <summary>
    /// 测试管理器
    /// </summary>
    internal class TestManager
    {
        /// <summary>
        /// 线程数
        /// </summary>
        private Int32 m_ThreadNum = 1;
        /// <summary>
        /// 调用次数
        /// </summary>
        private Int32 m_InvokeNum = 0;
        /// <summary>
        /// 调用次数
        /// </summary>
        public Int32 InvokeNum
        {
            get { return m_InvokeNum; }
        }
        /// <summary>
        /// 线程池
        /// </summary>
        private List<Thread> m_ThreadPool;
        /// <summary>
        /// 是否停止测试
        /// </summary>
        private Boolean m_Stop = true;
        /// <summary>
        /// 调用参数
        /// </summary>
        ESBInvokeParam m_ESBInvokeParam;

        public TestManager()
        {
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        public void Start(ESBInvokeParam invokeParam)
        {
            m_Stop = false;
            m_ThreadPool = new List<Thread>();
            m_ESBInvokeParam = invokeParam;

            for (int i = 0; i < m_ThreadNum; i++)
            {
                AddThread();
            }
        }

        /// <summary>
        /// 增加线程
        /// </summary>
        private void AddThread()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(DoTest));
            thread.Start(m_ESBInvokeParam);
            m_ThreadPool.Add(thread);
        }

        /// <summary>
        /// 停止测试
        /// </summary>
        public void Stop()
        {
            m_Stop = true;
        }

        /// <summary>
        /// 设置线程数量
        /// </summary>
        /// <param name="threadNum"></param>
        public void SetThreadNum(Int32 threadNum)
        {
            if (threadNum < m_ThreadNum)
            {
                for (int i = 0; i < m_ThreadNum - threadNum; i++)
                {
                    Thread thread = m_ThreadPool[0];
                    thread.Abort();
                    m_ThreadPool.Remove(thread);
                }
            }
            else if (threadNum > m_ThreadNum)
            {
                for (int i = 0; i < threadNum - m_ThreadNum; i++)
                {
                    AddThread();
                }
            }

            this.m_ThreadNum = threadNum;
        }

        /// <summary>
        /// 单个线程执行测试方法
        /// </summary>
        /// <param name="param"></param>
        private void DoTest(Object param)
        {
            while (!m_Stop)
            {
                CallService(param as ESBInvokeParam);
                Interlocked.Increment(ref m_InvokeNum);
            }

            Console.WriteLine("测试线程已经停止！");
        }

        /// <summary>
        /// 调用测试
        /// </summary>
        /// <param name="param"></param>
        private void CallService(ESBInvokeParam esbParam)
        {
            String uri = String.Format("{0}?ServiceName={1}&Version={2}&MethodName={3}&Message={4}",
                esbParam.CallCenterUrl, esbParam.ServiceName, esbParam.Version, esbParam.MethodName, esbParam.Message);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "GET";
            webRequest.ContentType = "text/xml; charset=utf-8";


            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            using (Stream newstream = webResponse.GetResponseStream())
            {
                using (StreamReader srRead = new StreamReader(newstream, System.Text.Encoding.UTF8))
                {
                    String outString = srRead.ReadToEnd();

                    //txtMessage.Text = outString;
                }
            }
        }
    }
}
