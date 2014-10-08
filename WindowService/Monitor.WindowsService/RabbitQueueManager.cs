using ESB.Core;
using ESB.Core.Configuration;
using ESB.Core.Entity;
using ESB.Core.Monitor;
using NewLife.Configuration;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;

namespace Monitor.WindowsService
{
    /// <summary>
    /// RabbitMQ管理器
    /// </summary>
    internal class RabbitQueueManager
    {
        /// <summary>
        /// 队列客户端
        /// </summary>
        RabbitMQClient m_RabbitMQ;
        /// <summary>
        /// 实时监控管理器
        /// </summary>
        MonitorStatManager m_MonitorStatManager = MonitorStatManager.GetInstance();
        /// <summary>
        /// ESB代理类对象
        /// </summary>
        ESBProxy m_ESBProxy = ESBProxy.GetInstance();

        /// <summary>
        /// 构造器
        /// </summary>
        public RabbitQueueManager()
        {
            ESBConfig esbConfig = m_ESBProxy.RegistryConsumerClient.ESBConfig;


            if (esbConfig != null && esbConfig.MessageQueue.Count > 0)
            {
                //String esbQueue = Config.GetConfig<String>("ESB.Queue");
                String esbQueue = esbConfig.MessageQueue[0].Uri;
                XTrace.WriteLine("读取到ESB队列地址：{0}", esbQueue);

                String[] paramMQ = esbQueue.Split(':');
                m_RabbitMQ = new RabbitMQClient(paramMQ[0], paramMQ[2], paramMQ[3], Int32.Parse(paramMQ[1]));
            }
            else
            {
                String err = "无法获取到有效的队列地址！";
                XTrace.WriteLine(err);
                throw new Exception(err);
            }
        }

        /// <summary>
        /// 建立独立线程启动处理程序
        /// </summary>
        public void StartReceive()
        {
            ///ProcessAuditMessage();

            Thread threadAudit = new Thread(x =>
            {
                ProcessAuditMessage();

            });
            threadAudit.Start();

            Thread threadException = new Thread(x =>
            {
                ProcessExceptionMessage();

            });
            threadException.Start();
        }

        /// <summary>
        /// 停止接收
        /// </summary>
        public void StopReceive()
        {
            m_RabbitMQ.Dispose();
        }

        /// <summary>
        /// 处理日志消息
        /// </summary>
        public void ProcessAuditMessage()
        {
            try
            {

                m_RabbitMQ.Listen<AuditBusiness>(Constant.ESB_AUDIT_QUEUE, x =>
                {
                    if (x != null)
                    {
                        x.InBytes = GetStringByteLength(x.MessageBody);
                        x.OutBytes = GetStringByteLength(x.ReturnMessageBody);
                        x.RowMethodName = GetMethodName(x.MethodName);

                        String bindingID = x.BindingTemplateID;
                        if (!String.IsNullOrWhiteSpace(bindingID))
                        {
                            BindingTemplate binding = BindingTemplate.FindByTemplateID(bindingID);
                            if (binding != null)
                            {
                                x.ServiceID = binding.ServiceID;
                                if (binding.Service != null)
                                {
                                    x.BusinessID = binding.Service.BusinessID;
                                }
                            }
                        }

                        m_MonitorStatManager.Record(x);

                        //--采用线程池将数据提交到数据库中，增加统计发布的速度。
                        ThreadPool.QueueUserWorkItem(y =>
                        {
                            x.Insert();
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("处理审计日志发生异常，消息将停止接收！异常详情：{0}", ex.ToString());
            }
        }

        /// <summary>
        /// 获取到消息的字节数
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Int64 GetStringByteLength(String message)
        {
            if(String.IsNullOrEmpty(message))
                return 0;
            else
                return Encoding.Default.GetByteCount(message);
        }

        /// <summary>
        /// 从类似GET:JSON:MethodName的字符串中抽取到MethodName
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private String GetMethodName(String methodName)
        {
            if (!methodName.Contains(":")) return methodName;

            String[] methodParams = methodName.Split(":");
            return methodParams[methodParams.Length - 1];
        }



        /// <summary>
        /// 处理异常消息
        /// </summary>
        public void ProcessExceptionMessage()
        {
            try
            {
                m_RabbitMQ.Listen<ExceptionCoreTb>(Constant.ESB_EXCEPTION_QUEUE, x =>
                {
                    if (x != null)
                    {
                        String bindingID = x.BindingTemplateID;
                        if (!String.IsNullOrWhiteSpace(bindingID))
                        {
                            BindingTemplate binding = BindingTemplate.FindByTemplateID(bindingID);
                            if (binding != null)
                            {
                                x.ServiceID = binding.ServiceID;
                                if (binding.Service != null)
                                {
                                    x.BusinessID = binding.Service.BusinessID;
                                }
                            }
                        }

                        if (x.ExceptionInfo.Contains("操作超时"))
                            x.ExceptionCode = "操作超时";

                        x.Insert();
                    }
                });
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("处理异常日志发生异常，消息将停止接收！异常详情：{0}", ex.ToString());
            }
        }
    }
}
