using ESB.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Cluster
{
    /// <summary>
    /// 负载均衡策略算法
    /// </summary>
    internal class LoadBalance
    {
        /// <summary>
        /// 随机数
        /// </summary>
        private static Random m_Random = new Random();
        /// <summary>
        /// 记录服务调用的地址Index
        /// Key:SerivceID-Version
        /// Value:bindings.Index
        /// </summary>
        private static Dictionary<String, Int32> m_CallRecord = new Dictionary<string, int>();

        /// <summary>
        /// 根据负载均衡算法返回绑定地址信息
        /// </summary>
        /// <param name="bindings"></param>
        /// <param name="lbType"></param>
        /// <returns></returns>
        public static BindingTemplate GetBinding(List<BindingTemplate> bindings, Int32 lbType)
        {
            int cnt = bindings.Count;

            //--如果绑定中只有一个地址，则直接返回
            if (cnt == 1)
                return bindings[0];

            //--随机算法，不带权重
            if (lbType == 0)
                return bindings[m_Random.Next(cnt)];

            //--轮询算法，不带权重
            if (lbType == 1)
                return bindings[GetRoundRobin(bindings[0], cnt)];

            return bindings[0];
        }

        /// <summary>
        /// 获取到下一个轮询的值
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        private static Int32 GetRoundRobin(BindingTemplate binding, Int32 cnt)
        {
            String key = binding.ServiceID + "_" + binding.Version.ToString();
            if (!m_CallRecord.ContainsKey(key))
            {
                lock (m_CallRecord)
                {
                    m_CallRecord.Add(key, 0);
                }
                return 0;
            }
            else
            {
                Int32 index = m_CallRecord[key];

                if (index == (cnt - 1))
                {
                    index = 0;
                }
                else
                {
                    index++;
                }

                lock (m_CallRecord)
                {
                    m_CallRecord[key] = index;
                }

                return index;
            }
        }
    }
}
