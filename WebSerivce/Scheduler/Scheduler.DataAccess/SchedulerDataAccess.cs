using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JN.ESB.Scheduler.DataAccess
{
    public class SchedulerDataAccess
    {
        private static SchedulerDataClassesDataContext schedulerDC = new SchedulerDataClassesDataContext();

        public SchedulerDataAccess() { }

        /// <summary>
        /// 新增Esb调度
        /// </summary>
        /// <param name="esbScheduler"></param>
        /// <returns></returns>
        public static void AddEsbScheduler(ESB_SCHD esbScheduler)
        {
            schedulerDC.ESB_SCHD.InsertOnSubmit(esbScheduler);
            schedulerDC.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
        }

        /// <summary>
        /// 新增Esb WebServcied任务
        /// </summary>
        /// <param name="esbWS"></param>
        public static void AddEsbWS(ESB_SCHD_EsbWS esbWS)
        {
            Guid newId = Guid.NewGuid();

            esbWS.OID = newId.ToString();

            schedulerDC.ESB_SCHD_EsbWS.InsertOnSubmit(esbWS);
            schedulerDC.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
        }

        /// <summary>
        /// 新增Esb WebService调度任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="esbWS"></param>
        public static void AddEsbWebServcieScheduler(ESB_SCHD scheduler, ESB_SCHD_EsbWS esbWS)
        {
            schedulerDC.ESB_SCHD.InsertOnSubmit(scheduler);

            esbWS.OID = Guid.NewGuid().ToString().ToUpper();
            esbWS.SCHD_ID = scheduler.SCHD_ID;
            schedulerDC.ESB_SCHD_EsbWS.InsertOnSubmit(esbWS);

            schedulerDC.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

        }

        /// <summary>
        /// 修改Esb WebService调度任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="esbWS"></param>
        public static ESB_SCHD UpdateEsbWebServcieScheduler(ESB_SCHD scheduler, ESB_SCHD_EsbWS esbWS)
        {
            ESB_SCHD sched = schedulerDC.ESB_SCHD.Single(p => p.SCHD_ID == scheduler.SCHD_ID);
            sched.END_TIME = scheduler.END_TIME;
            sched.SCHD_CRON = scheduler.SCHD_CRON;
            sched.SCHD_DESC = scheduler.SCHD_DESC;
            sched.SCHD_FREQ = scheduler.SCHD_FREQ;
            sched.SCHD_HOST = scheduler.SCHD_HOST;
            sched.SCHD_NAME = scheduler.SCHD_NAME;
            sched.SCHD_TIME = scheduler.SCHD_TIME;
            sched.SCHD_USER = scheduler.SCHD_USER;
            sched.START_TIME = scheduler.START_TIME;
            sched.HOST_NAME = scheduler.HOST_NAME;


            ESB_SCHD_EsbWS esbWebServcie = schedulerDC.ESB_SCHD_EsbWS.Single(p => p.SCHD_ID == scheduler.SCHD_ID);
            esbWebServcie.EntityID = esbWS.EntityID;
            esbWebServcie.EntityName = esbWS.EntityName;
            esbWebServcie.MethodName = esbWS.MethodName;
            esbWebServcie.ParamString = esbWS.ParamString;
            esbWebServcie.ParamUrl = esbWS.ParamUrl;
            esbWebServcie.PassWord = esbWS.PassWord;
            esbWebServcie.ServiceID = esbWS.ServiceID;
            esbWebServcie.ServiceName = esbWS.ServiceName;

            schedulerDC.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

            return sched;
        }

        /// <summary>
        /// 删除Esb WebService调度任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="esbWS"></param>
        public static void DeleteEsbWebServcieScheduler(ESB_SCHD scheduler, ESB_SCHD_EsbWS esbWS)
        {
            schedulerDC.ESB_SCHD_EsbWS.DeleteOnSubmit(esbWS);
            schedulerDC.ESB_SCHD.DeleteOnSubmit(scheduler);

            schedulerDC.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
        }

        /// <summary>
        /// 根据schedID 获取EsbScheduler
        /// </summary>
        /// <param name="schedID"></param>
        /// <returns></returns>
        public static ESB_SCHD GetEsbSchedulerBySchedID(string schedID)
        {
            return schedulerDC.ESB_SCHD.Single(p => p.SCHD_ID == schedID);
        }

        /// <summary>
        /// 根据schedID 获取EsbWebService
        /// </summary>
        /// <param name="schedID"></param>
        /// <returns></returns>
        public static ESB_SCHD_EsbWS GetEsbWebServiceBySchedID(string schedID)
        {
            return schedulerDC.ESB_SCHD_EsbWS.Single(p => p.SCHD_ID == schedID);
        }

        /// <summary>
        /// 新增调度历史记录
        /// </summary>
        /// <param name="history"></param>
        public static void AddScheduleHistory(ESB_SCHD_HISTORY history)
        {
            Guid newId = Guid.NewGuid();

            history.OID = newId.ToString().ToUpper();
            schedulerDC.ESB_SCHD_HISTORY.InsertOnSubmit(history);
            schedulerDC.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
        }

        /// <summary>
        /// 按条件查询调度历史记录
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static List<ESB_SCHD_HISTORY_VIEW> GetScheduleHistroyByCondition(ScheduleHistoryCondition condition)
        {
            var pSelect = from p in schedulerDC.ESB_SCHD_HISTORY_VIEW
                          where 
                          (
                            p.BEGIN_TIME >= condition.DateScopeBegin
                            && p.BEGIN_TIME <= condition.DateScopeEnd
                            && p.CALL_STATUS == condition.Status
                          ) 
                          select p;

            if (!String.IsNullOrEmpty(condition.Type))
            {
                pSelect = from p in pSelect
                          where p.TRIG_GROUP == condition.Type
                          select p;
            }

            if (condition.Host != Guid.Empty)
            {
                pSelect = from p in pSelect
                          where p.SCHD_HOST == condition.Host
                          select p;
            }

            if (!String.IsNullOrEmpty(condition.SchedID))
            {
                pSelect = from p in pSelect
                          where p.SCHD_ID == condition.SchedID
                          select p;
            }

            return pSelect.OrderByDescending(p=>p.BEGIN_TIME).ToList<ESB_SCHD_HISTORY_VIEW>();
        }

        /// <summary>
        /// 获取到所有的调度
        /// </summary>
        /// <returns></returns>
        public static List<ESB_SCHD_VIEW> GetAllScheduler()
        {
            return schedulerDC.ESB_SCHD_VIEW.ToList();
        }
    }
}
