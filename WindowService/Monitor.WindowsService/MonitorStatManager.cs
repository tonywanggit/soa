using ESB.Core.Entity;
using ESB.Core.Util;
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
        /// 监控中心
        /// </summary>
        MonitorCenter m_MonitorCenter;

        /// <summary>
        /// 监控统计数据存储一分钟的实时数据
        /// </summary>
        MonitorStatData m_MonitorStatData;

        /// <summary>
        /// 统计数据定时器: 用于将数据序列化到数据库中 1分钟间隔
        /// </summary>
        TimerX m_TimerMonitorStatDataSave;
        /// <summary>
        /// 统计数据定时器: 用于将数据发布到各个客户端 1秒钟间隔
        /// </summary>
        TimerX m_TimerMonitorStatDataPublish;

        /// <summary>
        /// 构造器
        /// </summary>
        private MonitorStatManager()
        {
            //--启动监控中心对外提供服务
            m_MonitorCenter = new MonitorCenter();
            m_MonitorCenter.Start();

            //--初始化监控统计数据结构
            m_MonitorStatData = new MonitorStatData();

            //--每隔60秒钟将监控数据保存到数据库中并清空
            m_TimerMonitorStatDataSave = new TimerX(x =>
            {
                SaveToDatabase(m_MonitorStatData);
                lock (m_TimerMonitorStatDataSave)
                {
                    m_MonitorStatData = new MonitorStatData();
                }

            }, null, 1000 * 60, 1000 * 60);

            //--每隔1秒钟对外发布一次监控统计数据
            m_TimerMonitorStatDataPublish = new TimerX(x =>
            {
                PublishMointorData(m_MonitorStatData);
            }, null, 1000, 1000);
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
        /// 对外发布监控统计数据：1秒钟
        /// </summary>
        /// <param name="msData"></param>
        private void PublishMointorData(MonitorStatData msData)
        {
            Int32 second = DateTime.Now.Second;
            String data = XmlUtil.SaveXmlFromObj<List<ServiceMonitor>>(msData.GetOnSecondData(second));

            m_MonitorCenter.Publish(data);
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
