using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using NewLife.Log;
using NewLife.Configuration;
using System.Threading;

namespace Audit.WindowsService
{
    public partial class StoreAuditService : ServiceBase
    {
        QueueManager m_QueueManager = null;

        public StoreAuditService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            XTrace.WriteLine("日志队列处理服务启动。");


            Thread thread = new Thread(x =>
            {
                try
                {
                    m_QueueManager = new QueueManager();
                    m_QueueManager.StartReceive();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("从队列中获取消息失败：" + ex.ToString());
                }

            });
            thread.Start();
        }

        protected override void OnStop()
        {
            if (m_QueueManager != null)
                m_QueueManager.StopReceive();

            XTrace.WriteLine("日志队列处理服务关闭。");
        }
    }
}
