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
using Monitor.WindowsService;

namespace Audit.WindowsService
{
    public partial class StoreAuditService : ServiceBase
    {
        RabbitQueueManager m_RabbitQueueManager = null;

        public StoreAuditService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            XTrace.WriteLine("日志队列处理服务启动。");

            m_RabbitQueueManager = new RabbitQueueManager();
            m_RabbitQueueManager.StartReceive();

            //Thread thread = new Thread(x =>
            //{
            //    try
            //    {
            //        m_QueueManager = new QueueManager();
            //        m_QueueManager.StartReceive();
            //    }
            //    catch (Exception ex)
            //    {
            //        Trace.WriteLine("从队列中获取消息失败：" + ex.ToString());
            //    }

            //});
            //thread.Start();
        }

        protected override void OnStop()
        {
            if (m_RabbitQueueManager != null)
                m_RabbitQueueManager.StopReceive();

            XTrace.WriteLine("日志队列处理服务关闭。");
        }
    }
}
