using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using NewLife.Log;
using NewLife.Configuration;
using System.Xml;
using System.Configuration;

namespace Registry.WindowsService
{
    static class Program
    {
      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            /*ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new RegistryService() 
			};
            ServiceBase.Run(ServicesToRun);*/

            XTrace.WriteLine("注册中心服务启动。");

            try
            {
                RegistryCenter registryCenter = new RegistryCenter();
                registryCenter.Start();

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("注册中心服务启动失败：" + ex.ToString());
            }
        }
    }
}
