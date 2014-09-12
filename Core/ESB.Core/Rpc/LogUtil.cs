using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESB.Core.Entity;
using ESB.Core.Util;
using ESB.Core.Configuration;

namespace ESB.Core.Rpc
{
    internal class LogUtil
    {
        /// <summary>
        /// 增加审计日志
        /// </summary>
        public static String AddAuditLog(int status
            , String bindingTemplateID
            , String serviceID
            , String address
            , CallState callState
            , String message
            , ESB.Core.Schema.服务请求 request)
        {
            callState.RequestEndTime = DateTime.Now;
            TimeSpan ReqTimeSpan = callState.RequestEndTime.Subtract(callState.RequestBeginTime);

            ConfigurationManager cm = ConfigurationManager.GetInstance();

            AuditBusiness log = new AuditBusiness()
            {
                OID = Guid.NewGuid().ToString(),
                HostName = request.主机名称,
                ServiceName = request.服务名称,
                MethodName = request.方法名称,
                ReqBeginTime = callState.RequestBeginTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                ReqEndTime = callState.RequestEndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                Status = status,
                MessageID = callState.MessageID,
                MessageBody = request.消息内容,
                CallBeginTime = callState.CallBeginTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                CallEndTime = callState.CallEndTime.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),
                BindingTemplateID = bindingTemplateID,
                ServiceID = serviceID,
                BindingAddress = address,
                ReturnMessageBody = message,
                BusinessName = "",
                BusinessID = "",
                ServiceBeginTime = callState.ServiceBeginTime,
                ServiceEndTime = callState.ServiceEndTime,
                TraceID = callState.TraceContext.TraceID,
                InvokeLevel = callState.TraceContext.InvokeLevel,
                InvokeOrder = callState.TraceContext.InvokeOrder,
                InvokeID = callState.TraceContext.InvokeID,
                InvokeTimeSpan = ReqTimeSpan.TotalMilliseconds,
                ConsumerIP = cm.LocalIP,
                Version = callState.ServiceVersion
            };

            //log.Insert();
            //String mqHost = ESBProxy.GetInstance().ESBConfig.Monitor[0].Uri;
            //MSMQUtil.SendMessage<AuditBusiness>(log, String.Format(@"FormatName:DIRECT=TCP:{0}\Private$\EsbAuditQueue", "192.168.56.2"));

            ESBProxy.GetInstance().MessageQueueClient.SendAuditMessage(log);

            //--每调用完一次需要增加调用次数
            callState.TraceContext.IncreaseInvokeOrder();

            return log.OID;
        }


        /// <summary>
        /// 增加审计日志
        /// </summary>
        public static String AddAuditLog(int status
            , BindingTemplate binding
            , CallState callState
            , String message
            , ESB.Core.Schema.服务请求 request)
        {
            return AddAuditLog(status, binding.TemplateID, binding.ServiceID, binding.Address
             , callState
             , message, request);
        }

        /// <summary>
        /// 抛出异常并写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="binding"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Exception ExceptionAndLog(String exceptionDesc, String message, ESB.Core.Schema.服务请求 request)
        {
            return ExceptionAndLog(exceptionDesc, message, null, request);
        }

        /// <summary>
        /// 抛出异常并写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="binding"></param>
        /// <param name="request"></param>
        public static Exception ExceptionAndLog(String exceptionDesc, String message, BindingTemplate binding, ESB.Core.Schema.服务请求 request)
        {
            CallState state = new CallState()
            {
                RequestBeginTime = DateTime.Now,
                RequestEndTime = DateTime.Now,
                CallBeginTime = DateTime.Now,
                CallEndTime = DateTime.Now
            };

            return ExceptionAndLog(state, exceptionDesc, message, binding, request);
        }

        /// <summary>
        /// 抛出异常并写日志, 并需要记录异常的时间
        /// </summary>
        /// <param name="message"></param>
        /// <param name="binding"></param>
        /// <param name="request"></param>
        public static Exception ExceptionAndLog(CallState state, String exceptionDesc, String message, BindingTemplate binding, ESB.Core.Schema.服务请求 request)
        {
            String messageID = String.Empty;
            String exceptionMessage = String.Format("{0}：{1}", exceptionDesc, message);


            if (state.CallBeginTime.Year != DateTime.Now.Year) state.CallBeginTime = DateTime.Now;
            if (state.CallEndTime.Year != DateTime.Now.Year) state.CallEndTime = DateTime.Now;

            if (binding != null)
                messageID = AddAuditLog(0, binding, state , exceptionMessage, request);
            else
                messageID = AddAuditLog(0, "00000000-0000-0000-0000-000000000000"
                    , String.Empty, string.Empty, state, exceptionMessage, request);


            ExceptionCoreTb exception = new ExceptionCoreTb()
            {
                ExceptionID = Guid.NewGuid().ToString(),
                BindingTemplateID = binding == null ? "" : binding.TemplateID,
                BindingType = 0,
                Description = exceptionDesc,
                ExceptionCode = String.Empty,
                ExceptionInfo = exceptionMessage,
                ExceptionLevel = 0,
                ExceptionStatus = 0,
                ExceptionTime = DateTime.Now,
                ExceptionType = 0,
                HostName = request.主机名称,
                MessageBody = request.消息内容,
                MessageID = messageID,
                MethodName = request.方法名称,
                RequestPwd = request.密码,
                RequestType = 0
            };

            //exception.Insert();
            ESBProxy.GetInstance().MessageQueueClient.SendExceptionMessage(exception);

            return new Exception(message);
        }
    }
}