using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JN.ESB.Audit.DataAccess
{
    /// <summary>
    /// 日期范围
    /// </summary>
    public enum DateScopeEnum
    {
        OneDay = 1,     //当天
        OneWeek = 2,    //近一周
        OneMonth = 3,   //近一月
        OneYear = 4,    //近一年
        All = 5         //所有时间范围
    }

    /// <summary>
    /// 通讯日志状态
    /// </summary>
    public enum AuditBusinessStatus
    {
        Success = 1,            //成功
        Exception = 0,          //异常
        ExceptionReSend = 8,    //异常已重送
        ExceptionPigeonhole = 9 //异常异归档
    }

    /// <summary>
    /// 通讯日志查询条件
    /// </summary>
    public class AuditBusinessSearchCondition
    {
        public AuditBusinessSearchCondition(){}

        public AuditBusinessStatus Status { get; set; }
        public Guid BusinessID { get; set; }
        public Guid ServiceID { get; set; }
        public DateScopeEnum DateScope { get; set; }
        public DateTime DateScopeBegin { get; set; }
        public DateTime DateScopeEnd { get; set; }
        public String HostName { get; set; }
        public Boolean IfShowHeartBeat { get; set; }
    }
}
