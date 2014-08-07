using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Registry.WindowsService
{
    /// <summary>
    /// 监控线程
    /// </summary>
    internal class MonitorThread
    {
        Thread m_MonitorThread;
        RegistryCenter m_RegistryCenter;

        public MonitorThread(RegistryCenter regCenter)
        {
            m_RegistryCenter = regCenter;
            m_MonitorThread = new Thread(x =>
            {
                DoWork();
            });
        }

        public void Start()
        {
            m_MonitorThread.Start();
        }

        private void DoWork()
        {
            while (true)
            {
                m_RegistryCenter.SendDataToAllClient("Beat Heart");
                Thread.Sleep(5000);
            }
        }
    }
}
