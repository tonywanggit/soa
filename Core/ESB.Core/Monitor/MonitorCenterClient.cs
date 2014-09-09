using ESB.Core.Entity;
using ESB.Core.Registry;
using ESB.Core.Rpc;
using ESB.Core.Util;
using NewLife.Configuration;
using NewLife.Log;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ESB.Core.Monitor
{
    /// <summary>
    /// 和监控中心的连接类：主要由注册中心和管理中心使用
    /// </summary>
    public class MonitorCenterClient
    {
        private CometClient m_CometClient = null;
        private CometClientType m_CometClientType;
        private String m_MonitorCenterUri;

        /// <summary>
        /// 定时器：用于检测调用中心是否可以使用
        /// </summary>
        private TimerX m_TimerX;

        private static MonitorCenterClient m_Instance;

        public static MonitorCenterClient GetInstance(CometClientType ccType, String monitorCenterUri)
        {
            if (m_Instance != null) return m_Instance;

            MonitorCenterClient mcClient = new MonitorCenterClient(ccType, monitorCenterUri);

            Interlocked.CompareExchange<MonitorCenterClient>(ref m_Instance, mcClient, null);

            return m_Instance;
        }

        /// <summary>
        /// 监控中心消费者客户端
        /// </summary>
        /// <param name="esbProxy"></param>
        private MonitorCenterClient(CometClientType ccType, String monitorCenterUri)
        {
            m_MonitorCenterUri = monitorCenterUri;
            m_CometClientType = ccType;
        }

        /// <summary>
        /// 对外暴露监控数据发布接口
        /// </summary>
        public event EventHandler<MonitorStatEventArgs> OnMonitorStatPublish;

        /// <summary>
        /// 是否已经被订阅
        /// </summary>
        public Boolean IsSubscribe
        {
            get { return OnMonitorStatPublish != null; }
        }

        /// <summary>
        /// 连接到监控中心
        /// </summary>
        /// <returns></returns>
        public void Connect()
        {
            m_CometClient = new CometClient(m_MonitorCenterUri, m_CometClientType);
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
        /// 重新连接到监控中心，采用定时器5秒后重连
        /// </summary>
        private void ReConnect()
        {
            if (m_CometClient != null)
                m_CometClient.Dispose();

            m_TimerX = new TimerX(x => Connect(), null, 5000, 5000);
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

                    if (rm.Action == CometMessageAction.Publish)
                    {
                        List<ServiceMonitor> lstServiceMonitor = XmlUtil.LoadObjFromXML<List<ServiceMonitor>>(rm.MessageBody);

                        if (OnMonitorStatPublish != null)
                        {
                            OnMonitorStatPublish(this, new MonitorStatEventArgs(lstServiceMonitor));
                        }
                    }
                }
                else if (e.Type == CometEventType.Lost)     // 当和监控中心断开连接时
                {
                    Console.WriteLine("和监控中心断开连接, 5秒钟后将重新连接。");
                    ReConnect();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("接收监控中心消息时发生错误:" + ex.ToString());
            }
        }
    }

    /// <summary>
    /// 监控数据发布事件参数
    /// </summary>
    public class MonitorStatEventArgs : EventArgs
    {
        /// <summary>
        /// 1秒钟的监控统计数据
        /// </summary>
        public List<ServiceMonitor> ListServiceMonitor { get; private set; }

        public MonitorStatEventArgs(List<ServiceMonitor> listServiceMonitor)
        {
            ListServiceMonitor = listServiceMonitor;
        }
    }
}
