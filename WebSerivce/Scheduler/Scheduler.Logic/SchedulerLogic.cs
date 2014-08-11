using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JN.ESB.Scheduler.DataAccess;
using System.Collections.Specialized;
using Quartz.Impl;
using Quartz;
using JN.ESB.Core.Service.Common;
using JN.ESB.Scheduler.Job;
using Quartz.Impl.Triggers;

namespace JN.ESB.Scheduler.Logic
{
    public class SchedulerLogic
    {
        private IScheduler sched;

        public SchedulerLogic()
        {
            NameValueCollection properties = new NameValueCollection(); 
            properties["quartz.scheduler.instanceName"] = "RemoteClient";

            // set remoting expoter
            properties["quartz.scheduler.proxy"] = "true";
            properties["quartz.scheduler.proxy.address"] = EsbConfig.getConfigValue("QuartzServer");

            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory(properties);
            int count = sf.AllSchedulers.Count;

            if (count > 0)
                sched = sf.AllSchedulers.Cast<IScheduler>().First();
            else
                sched = sf.GetScheduler();
        }

        /// <summary>
        /// 新增Esb WebService调度任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="esbWS"></param>
        public void AddEsbWebServcieScheduler(ESB_SCHD scheduler, ESB_SCHD_EsbWS esbWS)
        {
            scheduler.SCHD_ID = Guid.NewGuid().ToString().ToUpper();
            AddQuartzWebServiceScheduler(scheduler, esbWS);

            try
            {
                SchedulerDataAccess.AddEsbWebServcieScheduler(scheduler, esbWS);
            }
            catch (Exception ex)
            {
                RemoveQuartzWebServiceScheduler(scheduler.TRIG_NAME, scheduler.JOB_NAME, scheduler.TRIG_GROUP);
                throw new Exception("新增Esb WebService调度任务 失败！" + ex.Message);
            }
            
        }

        /// <summary>
        /// 修改Esb WebService调度任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="esbWS"></param>
        public void UpdateEsbWebServcieScheduler(ESB_SCHD scheduler, ESB_SCHD_EsbWS esbWS)
        {
            try
            {
                ESB_SCHD sched = SchedulerDataAccess.UpdateEsbWebServcieScheduler(scheduler, esbWS);
                ESB_SCHD_EsbWS esbWebService = SchedulerDataAccess.GetEsbWebServiceBySchedID(sched.SCHD_ID);
                
                RemoveQuartzWebServiceScheduler(sched.TRIG_NAME, sched.JOB_NAME, sched.TRIG_GROUP);
                AddQuartzWebServiceScheduler(sched, esbWebService);
            }
            catch (Exception ex)
            {
                throw new Exception("修改Esb WebService调度任务 失败！" + ex.Message);
            }
        }

        /// <summary>
        /// 删除Esb WebService调度任务
        /// </summary>
        /// <param name="schedID"></param>
        public void DeleteEsbWebServcieScheduler(string schedID)
        {
            try
            {
                ESB_SCHD scheduler = SchedulerDataAccess.GetEsbSchedulerBySchedID(schedID);
                ESB_SCHD_EsbWS esbWS = SchedulerDataAccess.GetEsbWebServiceBySchedID(schedID);

                SchedulerDataAccess.DeleteEsbWebServcieScheduler(scheduler, esbWS);

                RemoveQuartzWebServiceScheduler(scheduler.TRIG_NAME, scheduler.JOB_NAME, scheduler.TRIG_GROUP);
            }
            catch (Exception ex)
            {
                throw new Exception("删除Esb WebService调度任务 失败！" + ex.Message);
            }

        }

        /// <summary>
        /// 停用Esb WebService调度任务
        /// </summary>
        /// <param name="schedID"></param>
        public void PauseQuartzWebServiceScheduler(string schedID)
        {
            try
            {
                ESB_SCHD scheduler = SchedulerDataAccess.GetEsbSchedulerBySchedID(schedID);

                TriggerKey tk = new TriggerKey(scheduler.TRIG_NAME, scheduler.TRIG_GROUP);
                sched.PauseTrigger(tk);
            }
            catch (Exception ex)
            {
                throw new Exception("停用Esb WebService调度任务 失败！" + ex.Message);
            }
        }

        /// <summary>
        /// 启用Esb WebService调度任务
        /// </summary>
        /// <param name="schedID"></param>
        public void ResumeQuartzWebServiceScheduler(string schedID)
        {
            try
            {
                ESB_SCHD scheduler = SchedulerDataAccess.GetEsbSchedulerBySchedID(schedID);

                sched.ResumeTrigger(new TriggerKey(scheduler.TRIG_NAME, scheduler.TRIG_GROUP));
            }
            catch (Exception ex)
            {
                throw new Exception("启用Esb WebService调度任务 失败！" + ex.Message);
            }
        }

        /// <summary>
        /// 新增调度历史记录
        /// </summary>
        /// <param name="history"></param>
        public void AddScheduleHistory(ESB_SCHD_HISTORY history)
        {
            try
            {
                SchedulerDataAccess.AddScheduleHistory(history);
            }
            catch (Exception ex)
            {
                throw new Exception("新增调度历史记录 失败！" + ex.Message);
            }
        }

        /// <summary>
        /// 按条件查询调度历史记录
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<ESB_SCHD_HISTORY_VIEW> SearchScheduleHistory(ScheduleHistoryCondition condition)
        {
            try
            {
                return SchedulerDataAccess.GetScheduleHistroyByCondition(condition);
            }
            catch (Exception ex)
            {
                throw new Exception("按条件查询调度历史记录 失败！" + ex.Message);
            }
        }

        /// <summary>
        /// 按照调度主机查找调度实例
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public List<ESB_SCHD_VIEW> GetSchedulerByHost(Guid host)
        {
            try
            {
                List<ESB_SCHD_VIEW> list = SchedulerDataAccess.GetAllScheduler();

                if (host != Guid.Empty)
                {
                    var ret = from s in list
                          where s.SCHD_HOST == host
                          select s;
                    return ret.ToList();
                }
                else
                {
                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("按照调度主机查找调度实例 失败！" + ex.Message);
            }
        }        

        /// <summary>
        /// 新增Quartz WebService调度任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="esbWS"></param>
        private void AddQuartzWebServiceScheduler(ESB_SCHD scheduler, ESB_SCHD_EsbWS esbWS)
        {
            // define the job and ask it to run
            JobDetailImpl job = new JobDetailImpl(scheduler.JOB_NAME, scheduler.TRIG_GROUP, typeof(EsbWebServiceJob));
            job.Description = scheduler.SCHD_DESC;
            job.Durable = true;

            JobDataMap map = new JobDataMap();
            map.Put(EsbWebServiceJob.HOST, scheduler.HOST_NAME);
            map.Put(EsbWebServiceJob.SERVICE_NAME, esbWS.ServiceName);
            map.Put(EsbWebServiceJob.METHOD_NAME, esbWS.MethodName);
            map.Put(EsbWebServiceJob.PASSWORD, esbWS.PassWord);
            map.Put(EsbWebServiceJob.SCHD_ID, scheduler.SCHD_ID);
            map.Put(EsbWebServiceJob.PARAM_STRING, esbWS.ParamString);
            map.Put(EsbWebServiceJob.PARAM_URL, esbWS.ParamUrl);
            job.JobDataMap = map;

            DateTime startTime = DateTime.Parse(scheduler.START_TIME).ToUniversalTime();
            DateTime endTime = DateTime.Parse(scheduler.END_TIME).ToUniversalTime();

            CronTriggerImpl trigger = new CronTriggerImpl(scheduler.TRIG_NAME, scheduler.TRIG_GROUP, scheduler.JOB_NAME, scheduler.TRIG_GROUP, startTime, endTime, scheduler.SCHD_CRON);
            trigger.Description = scheduler.SCHD_DESC;
            try
            {
                sched.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除Quartz WebService调度任务
        /// </summary>
        /// <param name="trigName"></param>
        /// <param name="jobName"></param>
        /// <param name="group"></param>
        private void RemoveQuartzWebServiceScheduler(string trigName, string jobName, string group)
        {
            sched.UnscheduleJob(new TriggerKey(trigName, group));
            sched.DeleteJob(new JobKey(jobName, group));
        }

        /// <summary>
        /// 根据schedID 获取EsbWebService
        /// </summary>
        /// <param name="schedID"></param>
        /// <returns></returns>
        public ESB_SCHD_EsbWS GetEsbWebServiceBySchedID(string schedID)
        {
            return SchedulerDataAccess.GetEsbWebServiceBySchedID(schedID);
        }


    }
}
