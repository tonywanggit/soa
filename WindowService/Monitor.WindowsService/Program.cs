using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using NewLife.Log;
using NewLife.Configuration;
using Monitor.WindowsService;

namespace Audit.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new StoreAuditService() 
            //};
            //ServiceBase.Run(ServicesToRun);

            Console.WriteLine("日志队列处理服务启动。");

            RabbitQueueManager m_RabbitQueueManager = m_RabbitQueueManager = new RabbitQueueManager();
            m_RabbitQueueManager.StartReceive();

            Console.WriteLine("启动成功！");
        }
    }
}
