using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using JN.ESB.Scheduler.DataAccess;
using JN.ESB.Scheduler.Logic;

namespace JN.ESB.Scheduler.Service
{
    /// <summary>
    /// SchedulerService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.jn.com/ESB/Scheduler")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SchedulerService : System.Web.Services.WebService
    {
        /// <summary>
        /// 新增Esb WebService计划调度任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="esbWS"></param>
        [WebMethod]
        public void AddEsbWebServcieScheduler(ESB_SCHD scheduler, ESB_SCHD_EsbWS esbWS)
        {
            SchedulerLogic schedulerLogic = new SchedulerLogic();

            schedulerLogic.AddEsbWebServcieScheduler(scheduler, esbWS);
        }

        /// <summary>
        /// 修改Esb WebService计划调度任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="esbWS"></param>
        [WebMethod]
        public void UpdateEsbWebServcieScheduler(ESB_SCHD scheduler, ESB_SCHD_EsbWS esbWS)
        {
            SchedulerLogic schedulerLogic = new SchedulerLogic();

            schedulerLogic.UpdateEsbWebServcieScheduler(scheduler, esbWS);
        }

        /// <summary>
        /// 删除Esb WebService计划调度任务
        /// </summary>
        /// <param name="schedID"></param>
        [WebMethod]
        public void DeleteEsbWebServcieScheduler(string schedID)
        {
            SchedulerLogic schedulerLogic = new SchedulerLogic();

            schedulerLogic.DeleteEsbWebServcieScheduler(schedID);
        }

        /// <summary>
        /// 停用Esb WebService计划调度任务
        /// </summary>
        /// <param name="schedID"></param>
        [WebMethod]
        public void PauseQuartzWebServiceScheduler(string schedID)
        {
            SchedulerLogic schedulerLogic = new SchedulerLogic();

            schedulerLogic.PauseQuartzWebServiceScheduler(schedID);
        }

        /// <summary>
        /// 启用Esb WebService计划调度任务
        /// </summary>
        /// <param name="schedID"></param>
        [WebMethod]
        public void ResumeQuartzWebServiceScheduler(string schedID)
        {
            SchedulerLogic schedulerLogic = new SchedulerLogic();

            schedulerLogic.ResumeQuartzWebServiceScheduler(schedID);
        }

        /// <summary>
        /// 新增调度历史记录
        /// </summary>
        /// <param name="history"></param>
        [WebMethod]
        public void AddScheduleHistory(ESB_SCHD_HISTORY history)
        {
            SchedulerLogic schedulerLogic = new SchedulerLogic();

            schedulerLogic.AddScheduleHistory(history);
        }

        /// <summary>
        /// 按条件查询调度历史记录
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [WebMethod]
        public List<ESB_SCHD_HISTORY_VIEW> SearchScheduleHistory(ScheduleHistoryCondition condition)
        {
            SchedulerLogic schedulerLogic = new SchedulerLogic();

            return schedulerLogic.SearchScheduleHistory(condition);
        }

        /// <summary>
        /// 按照调度主机查找调度实例
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        [WebMethod]
        public List<ESB_SCHD_VIEW> GetSchedulerByHost(Guid host)
        {
            SchedulerLogic schedulerLogic = new SchedulerLogic();

            return schedulerLogic.GetSchedulerByHost(host);
        }

        /// <summary>
        /// 根据schedID 获取EsbWebService任务信息
        /// </summary>
        /// <param name="schedID"></param>
        /// <returns></returns>
        [WebMethod]
        public ESB_SCHD_EsbWS GetEsbWebServiceBySchedID(string schedID)
        {
            SchedulerLogic schedulerLogic = new SchedulerLogic();
            ESB_SCHD_EsbWS　esbWS = schedulerLogic.GetEsbWebServiceBySchedID(schedID);
            ESB_SCHD_EsbWS ws = new ESB_SCHD_EsbWS();

            ws.EntityID = esbWS.EntityID;
            ws.EntityName = esbWS.EntityName;
            ws.MethodName = esbWS.MethodName;
            ws.OID = esbWS.OID;
            ws.ParamString = esbWS.ParamString;
            ws.ParamUrl = esbWS.ParamUrl;
            ws.PassWord = esbWS.PassWord;
            ws.SCHD_ID = esbWS.SCHD_ID;
            ws.ServiceID = esbWS.ServiceID;
            ws.ServiceName = esbWS.ServiceName;

            return ws;
        }
    }
}
