using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JN.ESB.Audit.DataAccess;

namespace JN.ESB.Audit.Logic
{
    public class AnalyseLogic
    {
        AnalyseDataAccess dataAccess = new AnalyseDataAccess();

        /// <summary>
        /// 获取到调用次数分析数据
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable GetCallNumAnalyseData()
        {
            return dataAccess.GetCallNumAnalyseData();
        }

        /// <summary>
        /// 获取到按服务细分的调用次数分析数据
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable GetCallNumAnalyseDataByService()
        {
            return dataAccess.GetCallNumAnalyseDataByService();
        }

        /// <summary>
        /// 获取到按服务、方法细分的响应时间分析数据
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable GetResTimeAnalyseData()
        {
            return dataAccess.GetResTimeAnalyseData();
        }

        /// <summary>
        /// 获取正常通讯与异常情况的数据
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable GetExceptionAnalyseData()
        {
            return dataAccess.GetExceptionAnalyseData();
        }
    }
}
