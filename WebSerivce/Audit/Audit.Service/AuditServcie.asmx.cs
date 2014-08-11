using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using JN.ESB.Audit.DataAccess;
using JN.ESB.Audit.Logic;


namespace JN.ESB.Audit.Service
{
    /// <summary>
    /// AuditServcie 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.jn.com/ESB/Audit")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class AuditServcie : System.Web.Services.WebService
    {
        /// <summary>
        /// 新增审计日志
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="messageBody"></param>
        /// <param name="messageID"></param>
        /// <param name="methodName"></param>
        /// <param name="reqBeginTime"></param>
        /// <param name="serviceName"></param>
        /// <param name="status"></param>
        /// <param name="callBeginTime"></param>
        /// <param name="callEndTime"></param>
        [WebMethod]
        public void AddAuditBusiness(string hostName, string messageBody, string messageID, string methodName, string reqBeginTime, string serviceName, int status, string callBeginTime, string callEndTime)
        {
            AuditBusiness business = new AuditBusiness();
            AuditLogic auditLogic = new AuditLogic();

            business.HostName = hostName;
            business.MessageID = messageID;
            business.MethodName = methodName;
            business.ReqBeginTime = reqBeginTime;
            business.ReqEndTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            business.ServiceName = serviceName;
            business.Status = status;
            business.CallBeginTime = callBeginTime;
            business.CallEndTime = callEndTime;

            auditLogic.AddAuditBusiness(business);
        }

        /// <summary>
        /// 异常归档
        /// </summary>
        /// <param name="MessageID"></param>
        [WebMethod]
        public void ExceptionPigeonhole(Guid messageID)
        {
            AuditLogic auditLogic = new AuditLogic();
            auditLogic.ExceptionPigeonhole(messageID);
        }

        /// <summary>
        /// 异常重发
        /// Tony　2010-06-10: 增加bingdingID以区分是出一对多
        /// </summary>
        /// <param name="MessageID"></param>
        /// <param name="bindingID"></param>
        [WebMethod]
        public void ExceptionResend(Guid messageID, Guid bindingID)
        {
            AuditLogic auditLogic = new AuditLogic();
            auditLogic.ExceptionResend(messageID, bindingID);
        }

        /// <summary>
        /// 审计日志查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<AuditBusiness> AuditBusinessSearch(AuditBusinessSearchCondition condition)
        {
            AuditLogic auditLogic = new AuditLogic();
            return auditLogic.AuditBusinessSearch(condition);
        }

        /// <summary>
        /// 审计日志查询分页版本
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [WebMethod]
        public List<AuditBusiness> AuditBusinessSearch(AuditBusinessSearchCondition condition, int pageIndex, int pageSize)
        {
            AuditLogic auditLogic = new AuditLogic();
            return auditLogic.AuditBusinessSearch(condition, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取到审计日志的数量
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        [WebMethod]
        public int GetAuditBusinessCount(AuditBusinessSearchCondition condition)
        {
            AuditLogic auditLogic = new AuditLogic();
            return auditLogic.GetAuditBusinessCount(condition);
        }

        /// <summary>
        /// 根据日志OID获取到单条审计日志
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        [WebMethod]
        public AuditBusiness GetAuditBusinessByOID(Guid oid)
        {
            AuditLogic auditLogic = new AuditLogic();
            return auditLogic.GetAuditBusinessByOID(oid);
        }

    }
}
