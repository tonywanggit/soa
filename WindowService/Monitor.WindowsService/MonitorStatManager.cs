using ESB.Core.Entity;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Monitor.WindowsService
{
    /// <summary>
    /// 监控统计管理器
    /// </summary>
    internal class MonitorStatManager
    {
        private static MonitorStatManager m_Instance;

        public static MonitorStatManager GetInstance()
        {
            if (m_Instance != null) return m_Instance;

            MonitorStatManager msManager = new MonitorStatManager();

            Interlocked.CompareExchange<MonitorStatManager>(ref m_Instance, msManager, null);

            return m_Instance;
        }

        /// <summary>
        /// 监控统计数据存储一分钟的实时数据
        /// </summary>
        MonitorStatData m_MonitorStatData;

        /// <summary>
        /// 统计数据定时器
        /// </summary>
        TimerX m_TimerMonitorStatData;

        /// <summary>
        /// 构造器
        /// </summary>
        private MonitorStatManager()
        {
            m_MonitorStatData = new MonitorStatData();

            //--每格60秒钟将监控数据保存到数据库中并清空
            m_TimerMonitorStatData = new TimerX(x =>
            {
                SaveToDatabase(m_MonitorStatData);
                lock (m_TimerMonitorStatData)
                {
                    m_MonitorStatData = new MonitorStatData();
                }

            }, null, 1000 * 60, 1000 * 60);
        }

        /// <summary>
        /// 将一分钟的统计数据序列化到数据库中
        /// </summary>
        /// <param name="msData"></param>
        private void SaveToDatabase(MonitorStatData msData)
        {
            List<ServiceMonitor> lstServiceMinute = msData.GetOneMinuteData();
            foreach (var item in lstServiceMinute)
            {
                item.Insert();
            }
        }

        /// <summary>
        /// 记录一条记录
        /// </summary>
        /// <param name="auditBusiness"></param>
        public void Record(AuditBusiness auditBusiness)
        {
            m_MonitorStatData.Push(auditBusiness);
        }
    }
}
