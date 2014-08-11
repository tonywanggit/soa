using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JN.ESB.Audit.DataAccess;

namespace JN.ESB.Audit.Logic
{
    public class AuditLogic
    {
        AuditBusinessDataAccess dataAccess = new AuditBusinessDataAccess();

        /// <summary>
        /// 添加审计日志
        /// </summary>
        /// <param name="business"></param>
        public void AddAuditBusiness(AuditBusiness business)
        {
            dataAccess.AddAuditBusiness(business);
        }

        /// <summary>
        /// 异常消息归档
        /// </summary>
        /// <param name="messageID"></param>
        public void ExceptionPigeonhole(Guid messageID)
        {
            dataAccess.ExceptionPigeonhole(messageID);
        }

        /// <summary>
        /// 异常消息重发
        /// Tony　2010-06-10: 增加bingdingID以区分是出一对多
        /// </summary>
        /// <param name="messageID"></param>
        public void ExceptionResend(Guid messageID, Guid bindingID)
        {
            dataAccess.ExceptionResend(messageID, bindingID);
        }

        /// <summary>
        /// 搜索通讯日志
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<AuditBusiness> AuditBusinessSearch(AuditBusinessSearchCondition condition)
        {
            return dataAccess.AuditBusinessSearch(condition);
        }

        /// <summary>
        /// 搜索通讯日志 带分页版本
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<AuditBusiness> AuditBusinessSearch(AuditBusinessSearchCondition condition, int pageIndex, int pageSize)
        {
            return dataAccess.AuditBusinessSearch(condition, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据日志OID获取到单条审计日志
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public AuditBusiness GetAuditBusinessByOID(Guid oid)
        {
            AuditBusiness ab = dataAccess.GetAuditBusinessByOID(oid);

            return ab;
        }

        /// <summary>
        /// 获取到审计日之的数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int GetAuditBusinessCount(AuditBusinessSearchCondition condition)
        {
            return dataAccess.GetAuditBusinessCount(condition);
        }
    }
}
