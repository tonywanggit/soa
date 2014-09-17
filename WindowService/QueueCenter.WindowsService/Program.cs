using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace QueueCenter.WindowsService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new QCService() 
            //};
            //ServiceBase.Run(ServicesToRun);

            Console.WriteLine("队列处理服务启动。");

            RabbitQueueManager m_RabbitQueueManager = m_RabbitQueueManager = new RabbitQueueManager();
            m_RabbitQueueManager.StartReceive();

            Console.WriteLine("启动成功！");
        }
    }
}
