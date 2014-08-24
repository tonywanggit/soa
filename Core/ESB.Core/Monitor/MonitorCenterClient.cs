using ESB.Core.Registry;
using ESB.Core.Rpc;
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

        private static MonitorCenterClient m_Instance;

        public static MonitorCenterClient GetInstance()
        {
            if (m_Instance != null) return m_Instance;

            MonitorCenterClient mcClient = new MonitorCenterClient();

            Interlocked.CompareExchange<MonitorCenterClient>(ref m_Instance, mcClient, null);

            return m_Instance;
        }

        /// <summary>
        /// 注册中心消费者客户端
        /// </summary>
        /// <param name="esbProxy"></param>
        private MonitorCenterClient()
        {
        }
    }
}
