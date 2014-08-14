using ESB.Core.Configuration;
using ESB.Core.Entity;
using ESB.Core.Monitor;
using NewLife.Configuration;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Monitor.WindowsService
{
    /// <summary>
    /// RabbitMQ管理器
    /// </summary>
    internal class RabbitQueueManager
    {
        RabbitMQClient m_RabbitMQ;

        public RabbitQueueManager()
        {
            String esbQueue = Config.GetConfig<String>("ESB.Queue");
            XTrace.WriteLine("读取到ESB队列地址：{0}", esbQueue);
            
            String[] paramMQ = esbQueue.Split(':');
            m_RabbitMQ = new RabbitMQClient(paramMQ[0], paramMQ[2], paramMQ[3], Int32.Parse(paramMQ[1]));
        }

        /// <summary>
        /// 建立独立线程启动处理程序
        /// </summary>
        public void StartReceive()
        {
            Thread threadAudit = new Thread(x =>
            {
                ProcessAuditMessage();

            });
            threadAudit.Start();

            Thread threadException = new Thread(x =>
            {
                ProcessExceptionMessage();

            });
            threadException.Start();
        }

        /// <summary>
        /// 处理日志消息
        /// </summary>
        public void ProcessAuditMessage()
        {
            m_RabbitMQ.Listen<AuditBusiness>(Constant.ESB_AUDIT_QUEUE, x =>
            {
                if (x != null)
                    x.Insert();
            });
        }

        /// <summary>
        /// 处理异常消息
        /// </summary>
        public void ProcessExceptionMessage()
        {
            m_RabbitMQ.Listen<ExceptionCoreTb>(Constant.ESB_EXCEPTION_QUEUE, x =>
            {
                if (x != null)
                    x.Insert();
            });
        }
    }
}
