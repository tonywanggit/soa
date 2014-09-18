using ESB.Core;
using ESB.Core.Configuration;
using ESB.Core.Entity;
using ESB.Core.Monitor;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QueueCenter.WindowsService
{
    /// <summary>
    /// RabbitMQ管理器
    /// </summary>
    internal class RabbitQueueManager
    {
        /// <summary>
        /// RabbitMQ参数
        /// </summary>
        String[] m_ParamMQ;

        /// <summary>
        /// 队列服务处理进程组
        /// </summary>
        Dictionary<String, QueueThread> m_QueueThreadDict;

        /// <summary>
        /// ESB代理类对象
        /// </summary>
        ESBProxy m_ESBProxy = ESBProxy.GetInstance();

        /// <summary>
        /// ESBConfig文件
        /// </summary>
        ESBConfig m_EsbConfig;

        /// <summary>
        /// ConsumerConfig文件
        /// </summary>
        ConsumerConfig m_ConsumerConfig;

        /// <summary>
        /// 构造器
        /// </summary>
        public RabbitQueueManager()
        {
            m_EsbConfig = m_ESBProxy.RegistryConsumerClient.ESBConfig;
            m_ConsumerConfig = m_ESBProxy.RegistryConsumerClient.ConsumerConfig;
            m_QueueThreadDict = new Dictionary<String, QueueThread>();
            
            if (m_EsbConfig != null && m_EsbConfig.MessageQueue.Count > 0)
            {
                //String esbQueue = Config.GetConfig<String>("ESB.Queue");
                String esbQueue = m_EsbConfig.MessageQueue[0].Uri;
                XTrace.WriteLine("读取到ESB队列地址：{0}", esbQueue);

                m_ParamMQ = esbQueue.Split(':');
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
            ConfigQueueThread();
            m_ESBProxy.RegistryConsumerClient.OnServiceConfigChange += RegistryConsumerClient_OnServiceConfigChange;
        }

        /// <summary>
        /// 配置队列线程
        /// </summary>
        private void ConfigQueueThread()
        {
            String appName = m_ConsumerConfig.ApplicationName;

            if (m_EsbConfig.ServiceConfig != null && m_EsbConfig.ServiceConfig.Count > 0)
            {
                //--根据QueueCenter的消费者配置文件中的ApplicationName找到和本机相关的配置文件
                List<EsbView_ServiceConfig> lstServcieConfig = m_EsbConfig.ServiceConfig.FindAll(x => appName.EndsWith(x.QueueCenterUri));

                //--删除不应该由本机消费的队列并停用相关线程
                DeleteQueueThread(lstServcieConfig);

                //--添加本机应该消费的队列
                foreach (EsbView_ServiceConfig sc in lstServcieConfig)
                {
                    if (!m_QueueThreadDict.ContainsKey(sc.ServiceName))
                    {
                        RabbitMQClient rabbitMQ = new RabbitMQClient(m_ParamMQ[0], m_ParamMQ[2], m_ParamMQ[3], Int32.Parse(m_ParamMQ[1]));
                        QueueThread qt = new QueueThread(sc.ServiceName, rabbitMQ);
                        qt.Start();

                        m_QueueThreadDict[sc.ServiceName] = qt;
                    }
                }
            }
            else
            {
                //--删除不应该由本机消费的队列并停用相关线程
                DeleteQueueThread(null);
            }
        }

        /// <summary>
        /// 删除无效的服务配置队列
        /// </summary>
        /// <param name="lstServiceConfig">跟本机相关的服务配置</param>
        private void DeleteQueueThread(List<EsbView_ServiceConfig> lstServiceConfig)
        {
            //--判断是否有本机需要订阅的服务
            Boolean hasMyServiceConfig = true;
            if (lstServiceConfig == null || lstServiceConfig.Count == 0)
            {
                hasMyServiceConfig = false;
            }

            if (m_QueueThreadDict.Count > 0)
            {
                List<String> lstRemoveService = new List<String>();
                foreach (QueueThread qt in m_QueueThreadDict.Values)
                {
                    //--如果没有本机的队列服务配置或者本机有服务不在配置表中则需要删除对队列的消费行为
                    if (!hasMyServiceConfig || lstServiceConfig.Count(x => x.ServiceName == qt.ServiceName) == 0)
                    {
                        qt.Stop();
                        lstRemoveService.Add(qt.ServiceName);
                    }
                }

                foreach (String serviceName in lstRemoveService)
                {
                    m_QueueThreadDict.Remove(serviceName);
                }
            }
        }

        /// <summary>
        /// 订阅注册中心的服务变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistryConsumerClient_OnServiceConfigChange(object sender, ESB.Core.Registry.RegistryEventArgs e)
        {
            m_EsbConfig = e.ESBConfig;
            ConfigQueueThread();
        }

        /// <summary>
        /// 停止接收
        /// </summary>
        public void StopReceive()
        {
        }
    }
}
