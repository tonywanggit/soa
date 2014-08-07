using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using NewLife.Log;

namespace Registry.WindowsService
{
    public partial class RegistryService : ServiceBase
    {
        public RegistryService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            XTrace.WriteLine("注册中心服务启动。");

            try
            {
                RegistryCenter registryCenter = new RegistryCenter();
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("注册中心服务启动失败：" + ex.ToString());
            }
        }

        protected override void OnStop()
        {
            XTrace.WriteLine("注册中心服务关闭。");
        }
    }
}
