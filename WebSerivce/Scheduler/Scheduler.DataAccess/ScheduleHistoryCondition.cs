using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JN.ESB.Scheduler.DataAccess
{

    /// <summary>
    /// 调度历史记录查询条件
    /// </summary>
    public class ScheduleHistoryCondition
    {
        public ScheduleHistoryCondition() { }

        public int Status { get; set; }
        public string Type { get; set; }
        public DateTime DateScopeBegin { get; set; }
        public DateTime DateScopeEnd { get; set; }
        public Guid Host { get; set; }
        public string SchedID{ get; set;}
    }
}
