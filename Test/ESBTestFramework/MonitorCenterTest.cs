using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.TestFramework
{
    /// <summary>
    /// 和监控中心连接的测试
    /// </summary>
    class MonitorCenterTest
    {
        public static void DoTest()
        {
            ESB.Core.Monitor.MonitorCenterClient mcClient = ESB.Core.Monitor.MonitorCenterClient.GetInstance(ESB.Core.Rpc.CometClientType.Portal);
            mcClient.OnMonitorStatPublish += mcClient_OnMonitorStatPublish;
            mcClient.Connect();


            Console.ReadKey();
        }

        private static void mcClient_OnMonitorStatPublish(object sender, Core.Monitor.MonitorStatEventArgs e)
        {
            //List<ESB.Core.Entity.ServiceMonitor> lstServiceMonitor = e.ListServiceMonitor;

            Console.WriteLine("收到监控中心发布的数据。");
        }
    }
}
