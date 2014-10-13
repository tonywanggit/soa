using MB.Dep.DataPort;
using MB.Dep.DataPort.Agent;
using MB.Dep.DataTransferObjects.ConfigModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.SOA.Client
{
    /// <summary>
    /// SOA调用中心代理类：用于数据交换平台的使用
    /// </summary>
    public class CallCenterAgent : AgentBase
    {
        /// <summary>
        /// 调用中心代理类
        /// </summary>
        private CallCenterProxy m_CallCenterProxy;

        /// <summary>
        /// 端口类型
        /// </summary>
        public override DataportType PortType
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataport"></param>
        public CallCenterAgent(DataPortBase dataport)
            : base(dataport)
        {
            m_CallCenterProxy = CallCenterProxy.GetInstance("");

            //--设置超时为10分钟
            m_CallCenterProxy.Timeout = 60 * 1000 * 10;
        }

        /// <summary>
        /// 获取到数据端口配置信息
        /// </summary>
        public WcfDataPort WcfPort
        {
            get { return (WcfDataPort)DataPort; }
        }

        /// <summary>
        /// 调用SOA服务
        /// </summary>
        /// <param name="userCode">这个参数是调度框架必须的，对业务没有任何意义</param>
        public String Invoke(String userCode)
        {
            String serviceName = "";
            String methodName = "";
            String message = "";
            Int32 version = 0;

            return m_CallCenterProxy.Invoke(serviceName, methodName, message, version);
        }
    }
}
