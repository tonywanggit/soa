using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using JN.ESB.Audit.Logic;

namespace JN.ESB.Audit.Service
{
    /// <summary>
    /// AuditAnalyseService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.jn.com/ESB/Audit")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class AuditAnalyseService : System.Web.Services.WebService
    {

        /// <summary>
        /// 获取到调用次数分析数据
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetCallNumAnalyseData()
        {
            AnalyseLogic auditLogic = new AnalyseLogic();
            return auditLogic.GetCallNumAnalyseData();
        }

        /// <summary>
        /// 获取到按服务细分的调用次数分析数据
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetCallNumAnalyseDataByService()
        {
            AnalyseLogic auditLogic = new AnalyseLogic();
            return auditLogic.GetCallNumAnalyseDataByService();
        }

        /// <summary>
        /// 获取到按服务、方法细分的响应时间分析数据
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetResTimeAnalyseData()
        {
            AnalyseLogic auditLogic = new AnalyseLogic();
            return auditLogic.GetResTimeAnalyseData();
        }

        /// <summary>
        /// 获取正常通讯与异常情况的数据
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataTable GetExceptionAnalyseData()
        {
            AnalyseLogic auditLogic = new AnalyseLogic();
            return auditLogic.GetExceptionAnalyseData();
        }
    }
}
