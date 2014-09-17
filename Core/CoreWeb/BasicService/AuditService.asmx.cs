using ESB.Core.Entity;
using ESB.Core.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using XCode;

namespace ESB.CallCenter.Audit
{
    /// <summary>
    /// 审计日志服务
    /// </summary>
    [WebService(Namespace = "http://esb.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class AuditService : System.Web.Services.WebService
    {
        [WebMethod(Description="获取到审计日志")]
        public AuditBusiness GetAuditBusinessByOID(String auditID)
        {
            return AuditBusiness.GetAuditBusinessByOID(auditID);
        }


        [WebMethod(Description = "获取到同一次调用的所有审计日志")]
        public List<AuditBusiness> GetAuditListByTraceID(String traceID)
        {
            AuditBusiness audit = AuditBusiness.Find(AuditBusiness._.OID, traceID);

            List<AuditBusiness> lstAuditBusiness = AuditBusiness.FindAllByTraceID(audit.TraceID);

            foreach (var item in lstAuditBusiness)
            {
                item.ReqBeginTime = item.ReqBeginTime.Substring(11);
                item.ReqEndTime = item.ReqEndTime.Substring(11);
            }

            return lstAuditBusiness;
        }

        [WebMethod(Description = "获取到同一次调用的所有审计日志")]
        public List<AuditBusiness> GetAuditList()
        {
            return GetAuditListByTraceID("b33183db-d3dd-4493-bd38-02097237eaa5");
        }

        [WebMethod(Description = "将日志标记为归档状态")]
        public void ExceptionPigeonhole(String messageID)
        {
            AuditBusiness audit = AuditBusiness.Find(AuditBusiness._.OID, messageID);
            if (audit != null)
            {
                audit.Status = 9;
                audit.Update();
            }
        }

        /// <summary>
        /// 审计日志查询分页版本
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [WebMethod(Description = "审计日志查询分页版本")]
        public List<AuditBusiness> AuditBusinessSearch(AuditBusinessSearchCondition condition, int pageIndex, int pageSize)
        {
            return AuditBusiness.AuditBusinessSearch(condition, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取到审计日志的数量
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        [WebMethod(Description = "获取到审计日志的数量")]
        public int GetAuditBusinessCount(AuditBusinessSearchCondition condition)
        {
            return AuditBusiness.GetAuditBusinessCount(condition);
        }
    }
}
