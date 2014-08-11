using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using JN.ESB.Scheduler.Job.ESB;

namespace JN.ESB.Scheduler.Job
{
    public class EsbWebServiceJob : JobBase, IJob
    {
        public const string SERVICE_NAME = "ServiceName";
        public const string METHOD_NAME = "MethodName";
        public const string PASSWORD = "PassWord";
        public const string HOST = "Host";
        public const string PARAM_STRING = "ParamString";
        public const string PARAM_URL = "ParamUrl";

        public override void ExecuteInner(IJobExecutionContext context)
        {
            callStatus = 1;

            try
            {
                string jobName = context.JobDetail.Key.ToString();
                string ServiceName = context.JobDetail.JobDataMap.GetString(SERVICE_NAME);
                string MethodName = context.JobDetail.JobDataMap.GetString(METHOD_NAME);
                string PassWord = context.JobDetail.JobDataMap.GetString(PASSWORD);
                string HostName = context.JobDetail.JobDataMap.GetString(HOST);
                string ParamString = context.JobDetail.JobDataMap.GetString(PARAM_STRING);
                string ParamUrl = context.JobDetail.JobDataMap.GetString(PARAM_URL);

                EsbRequest req = new EsbRequest();
                req.HostName = HostName;
                req.MessageBody = ParamString;
                req.MethodName = MethodName;
                req.PassWord = PassWord;
                req.RequestTime = DateTime.Now;
                req.ServiceName = ServiceName;

                EsbResponse res = new EsbResponse();
                ESB.Core_Service_Bus_MainBus_ReceiveSendPort_EN port = new Core_Service_Bus_MainBus_ReceiveSendPort_EN();
                port.Timeout = 1000 * 60 * 20;

                res = port.ReceiveRequest(req);
                callMemo = res.MessageBody;

                _log.Info("调用成功！ [ jobName: " + jobName + ", ServiceName:" + ServiceName + ", MethodName:" + MethodName + ", PassWord:" + PassWord + ", Host:" + HostName + "]");
            
            }
            catch (Exception ex)
            {
                callStatus = 0;
                callMemo = ex.Message;
            }

            base.ExecuteInner(context);
        }
    }
}
