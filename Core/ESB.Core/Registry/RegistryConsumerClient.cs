using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core.Configuration;
using System.Net;
using System.IO;
using ESB.Core.Util;
using NewLife.Threading;
using NewLife.Log;
using System.Threading;
using ESB.Core.Rpc;

namespace ESB.Core.Registry
{
    /// <summary>
    /// 注册中心客户端，负责和注册中心保持联系
    /// </summary>
    public class RegistryConsumerClient
    {
        private CometClient m_CometClient = null;
        private ESBProxy m_ESBProxy = null;
        private ConfigurationManager m_ConfigurationManager = null;
        private Object m_SyncESBConfigLock = new Object();  //同步获取到ESBConfig配置锁
        private AutoResetEvent m_AutoResetEvent = new AutoResetEvent(false);

        /// <summary>
        /// 定时器：用于检测注册中心是否可以使用
        /// </summary>
        private TimerX m_TimerX;

        /// <summary>
        /// 注册中心消费者客户端
        /// </summary>
        /// <param name="esbProxy"></param>
        public RegistryConsumerClient(ESBProxy esbProxy)
        {
            m_ESBProxy = esbProxy;
            m_ConfigurationManager = ConfigurationManager.GetInstance();
        }

        /// <summary>
        /// 连接到注册中心
        /// </summary>
        /// <returns></returns>
        public void Connect()
        {
            String uri = m_ESBProxy.ConsumerConfig.Registry[0].Uri;

            if(m_ESBProxy.ConsumerConfig.ApplicationName.StartsWith("ESB_CallCenter"))
                m_CometClient = new CometClient(uri, CometClientType.CallCenter);
            else
                m_CometClient = new CometClient(uri, CometClientType.Consumer);

            m_CometClient.OnReceiveNotify += m_CometClient_OnReceiveNotify;
            m_CometClient.Connect();

            //--连接成功后,释放定时器
            if (m_TimerX != null)
            {
                m_TimerX.Dispose();
                m_TimerX = null;
            }
        }

        /// <summary>
        /// 重新连接到注册中心，采用定时器5秒后重连
        /// </summary>
        private void ReConnect()
        {
            if (m_CometClient != null)
                m_CometClient.Dispose();

            m_TimerX = new TimerX(x => Connect(), null, 5000, 5000);
        }

        /// <summary>
        /// 同步获取到ESBConfig文件
        /// </summary>
        public void SyncESBConfig()
        {
            lock (m_SyncESBConfigLock)
            {
                m_CometClient.SendData(CometMessageAction.Hello, m_ESBProxy.ConsumerConfig.ToXml(), false);
                m_AutoResetEvent.WaitOne();
            }
        }

        /// <summary>
        /// 接收注册中心的消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_CometClient_OnReceiveNotify(object sender, CometEventArgs e)
        {
            try
            {
                if (e.Type == CometEventType.ReceiveMessage)    // 接收到来自服务器的配置信息
                {
                    CometMessage rm = XmlUtil.LoadObjFromXML<CometMessage>(e.Response);

                    if(rm.Action == CometMessageAction.ServiceConfig){

                        //--如果ESBConfig为NULL，则说明注册中心还没连上
                        if (m_ESBProxy.ESBConfig == null)
                        {
                            m_ESBProxy.ESBConfig = XmlUtil.LoadObjFromXML<ESBConfig>(rm.MessageBody);
                            m_ESBProxy.MonitorClient.Connect();
                        }
                        else
                        {
                            //--从返回消息中加载ESBConfig配置文件
                            m_ESBProxy.ESBConfig = XmlUtil.LoadObjFromXML<ESBConfig>(rm.MessageBody);
                            if (m_ESBProxy.ESBConfig != null)
                                m_ESBProxy.Status = ESBProxy.ESBProxyStatus.Ready;
                        }

                        //--如果是同步调用，则需要在消息返回时释放信号
                        if (!rm.IsAsync) m_AutoResetEvent.Set();

                        //--异步将配置文件序列化到本地存储
                        ThreadPoolX.QueueUserWorkItem(x =>
                        {
                            m_ConfigurationManager.SaveESBConfig(m_ESBProxy.ESBConfig);
                        });
                    }
                }
                else if (e.Type == CometEventType.Connected)   // 当和服务器取得联系时发送消费者配置文件到服务端
                {
                    m_CometClient.SendData(CometMessageAction.Hello, m_ESBProxy.ConsumerConfig.ToXml());
                }
                else if (e.Type == CometEventType.Lost)     // 当和注册中心断开连接时
                {
                    Console.WriteLine("和注册中心断开连接。");
                    ReConnect();
                }

            }
            catch (Exception ex)
            {
                XTrace.WriteLine("接收注册中心消息时发生错误：" + ex.ToString());
            }
        }

    }

    /// <summary>
    /// 注册中心变化事件
    /// </summary>
    internal class RegistryEventArgs : EventArgs
    {
        public ESBConfig ESBConfig { get; set; }
    }
}
