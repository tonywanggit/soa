using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using NewLife.Log;
using NewLife.Configuration;

namespace Audit.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new StoreAuditService() 
            };
            ServiceBase.Run(ServicesToRun);

            //String m_ESBQueueName = Config.AppSettings["ESB.Queue.AuditName"];
            //String m_ESBQueueName = Config.GetConfig<String>("ESB.Queue.AuditName");

            //XTrace.WriteLine("读取到ESB队列名称：{0}", m_ESBQueueName);


            //QueueManager m_QueueManager = new QueueManager();
            //m_QueueManager.StartReceive();
        }
    }
}
