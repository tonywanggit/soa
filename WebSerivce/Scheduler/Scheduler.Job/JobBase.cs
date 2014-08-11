using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using JN.ESB.Scheduler.Job.SCHD;
using Common.Logging;

namespace JN.ESB.Scheduler.Job
{
    public class JobBase: IJob
    {
        public const string SCHD_ID = "SchedID";

        private string schedID = "";
        protected string beginTime = "";
        protected string endTime = "";
        protected string callMemo = "";
        protected int callStatus = 0;

        protected static ILog _log = LogManager.GetLogger(typeof(EsbWebServiceJob));

        #region IJob 成员

        public void Execute(IJobExecutionContext context)
        {
            beginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            schedID = context.JobDetail.JobDataMap.GetString(SCHD_ID);

            ExecuteInner(context);

            AfterCall();
        }

        #endregion

        public virtual void ExecuteInner(IJobExecutionContext context)
        {

        }

        private void AfterCall()
        {
            try
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SchedulerService ss = new SchedulerService();

                ESB_SCHD_HISTORY history = new ESB_SCHD_HISTORY();
                history.BEGIN_TIME = beginTime;
                history.CALL_MEMO = callMemo;
                history.CALL_STATUS = callStatus;
                history.END_TIME = endTime;
                history.SCHD_ID = schedID;

                ss.AddScheduleHistory(history);
            }
            catch (Exception ex)
            {
                _log.Error("添加作业历史记录 失败！ 错误信息：" + ex.Message);
            }

        }
    }
}
