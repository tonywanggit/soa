using ESB.Core.Entity;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor.WindowsService
{
    /// <summary>
    /// 监控数据统计结构
    /// </summary>
    internal class MonitorStatData
    {
        /// <summary>
        /// 服务监控对象：存放一分钟的监控数据
        /// </summary>
        private Dictionary<MonitorStatDimension, ServiceMonitor[]> m_ServiceMonitor;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MonitorStatData()
        {
            m_ServiceMonitor = new Dictionary<MonitorStatDimension, ServiceMonitor[]>();
        }

        /// <summary>
        /// 获取到一分钟的汇总数据
        /// </summary>
        /// <returns></returns>
        public List<ServiceMonitor> GetOneMinuteData()
        {
            List<ServiceMonitor> lstServiceMinute = new List<ServiceMonitor>();

            foreach (var item in m_ServiceMonitor)
            {
                ServiceMonitor smOneMinute = null;

                for (int i = 0; i < 60; i++)
                {
                    ServiceMonitor sm = item.Value[i];
                    if (sm == null) continue;

                    if (smOneMinute == null)
                        smOneMinute = sm;
                    else
                    {
                        smOneMinute.CallSuccessNum += sm.CallSuccessNum;
                        smOneMinute.CallFailureNum += sm.CallFailureNum;
                        smOneMinute.CallLevel1Num += sm.CallLevel1Num;
                        smOneMinute.CallLevel2Num += sm.CallLevel2Num;
                        smOneMinute.CallLevel3Num += sm.CallLevel3Num;
                        smOneMinute.InBytes += sm.InBytes;
                        smOneMinute.OutBytes += sm.OutBytes;

                        if (smOneMinute.TpsPeak < sm.TpsPeak)
                            smOneMinute.TpsPeak = sm.TpsPeak;
                    }
                }

                if (smOneMinute != null)
                {
                    lstServiceMinute.Add(smOneMinute);
                }
            }

            return lstServiceMinute;
        }

        /// <summary>
        /// 获取到一秒种的数据
        /// </summary>
        /// <returns></returns>
        public List<ServiceMonitor> GetOnSecondData(Int32 second)
        {
            List<ServiceMonitor> lstServiceMonitor = new List<ServiceMonitor>();

            foreach (var item in m_ServiceMonitor)
            {
                if (item.Value[second] != null)
                {
                    lstServiceMonitor.Add(item.Value[second]);
                }
            }

            return lstServiceMonitor;
        }

        /// <summary>
        /// 接收一条审计记录
        /// </summary>
        /// <param name="?"></param>
        public void Push(AuditBusiness ab)
        {
            String serviceName = ab.ServiceName;
            ServiceMonitor[] serviceMonitorArray;
            MonitorStatDimension msDimension = GetMonitorStatDimension(ab);


            //--如果统计中没有相应维度的数据，则创建
            if (msDimension == null)
            {
                serviceMonitorArray = new ServiceMonitor[60];
                msDimension = new MonitorStatDimension()
                {
                    ServiceName = ab.ServiceName,
                    BindingAddress = ab.BindingAddress,
                    MethodName = ab.MethodName
                };
                m_ServiceMonitor.Add(msDimension, serviceMonitorArray);
            }
            else
            {
                serviceMonitorArray = m_ServiceMonitor[msDimension];
            }

            RecordItem(ab, serviceMonitorArray);
        }

        /// <summary>
        /// 从监控数据中查找是否有审计日志对应维度的数据
        /// </summary>
        /// <param name="ab"></param>
        /// <returns></returns>
        private MonitorStatDimension GetMonitorStatDimension(AuditBusiness ab)
        {
            foreach (var item in m_ServiceMonitor.Keys)
            {
                if (item.ServiceName == ab.ServiceName && item.MethodName == ab.MethodName && item.BindingAddress == ab.BindingAddress)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// 记录一条监控日志
        /// </summary>
        /// <param name="ab"></param>
        /// <param name="serviceMonitorArray"></param>
        private void RecordItem(AuditBusiness ab, ServiceMonitor[] serviceMonitorArray)
        {
           // DateTime serviceBeginTime = DateTime.ParseExact(ab.ServiceBeginTime, "yyyy-MM-dd HH:mm:ss.ffffff", null);
            DateTime monitorStamp = DateTime.Now;
            Int32 second = monitorStamp.Second;
            ServiceMonitor serviceMonitor = serviceMonitorArray[second];


            //XTrace.WriteLine("第{0}秒记录。", second);

            if (serviceMonitor == null)
            {
                serviceMonitor = new ServiceMonitor(){
                    OID = Guid.NewGuid().ToString(),
                    ServiceName = ab.ServiceName,
                    MethodName = ab.MethodName,
                    MonitorStamp = monitorStamp,
                    ConsumerIP = ab.ConsumerIP,
                    BindingAddress = ab.BindingAddress,
                    CallSuccessNum = (ab.Status == 1) ? 1 : 0,
                    CallFailureNum =  (ab.Status == 1) ? 0 : 1,
                    CallLevel1Num = (ab.InvokeTimeSpan > 20 && ab.InvokeTimeSpan < 100) ? 1 : 0,
                    CallLevel2Num = (ab.InvokeTimeSpan > 100 && ab.InvokeTimeSpan < 200) ? 1 : 0,
                    CallLevel3Num = (ab.InvokeTimeSpan > 200) ? 1 : 0,
                    InBytes = ab.InBytes,
                    OutBytes = ab.OutBytes,
                    TpsPeak = 1
                };
                serviceMonitorArray[second] = serviceMonitor;
            }
            else
            {
                if (ab.Status == 1)
                    serviceMonitor.CallSuccessNum++;
                else
                    serviceMonitor.CallFailureNum++;

                if (ab.InvokeTimeSpan > 200)
                    serviceMonitor.CallLevel3Num++;
                else if (ab.InvokeTimeSpan > 100 && ab.InvokeTimeSpan <= 200)
                    serviceMonitor.CallLevel2Num++;
                else if(ab.InvokeTimeSpan > 20 && ab.InvokeTimeSpan <= 100)
                    serviceMonitor.CallLevel1Num++;

                serviceMonitor.InBytes += ab.InBytes;
                serviceMonitor.OutBytes += ab.OutBytes;

                serviceMonitor.TpsPeak++;
            }
        }
    }

    /// <summary>
    /// 监控数据统计维度
    /// </summary>
    internal class MonitorStatDimension
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public String ServiceName{ get; set; }
        /// <summary>
        /// 绑定地址
        /// </summary>
        public String BindingAddress { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public String MethodName { get; set; }
    }
}
