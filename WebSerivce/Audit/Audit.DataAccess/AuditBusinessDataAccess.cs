using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace JN.ESB.Audit.DataAccess
{
  
    public class AuditBusinessDataAccess
    {
        AuditBusinessDataClassesDataContext execptionDC = new AuditBusinessDataClassesDataContext();

        public AuditBusinessDataAccess() { }

        public Guid AddAuditBusiness(AuditBusiness auditBusiness)
        {
            Guid newId = Guid.NewGuid();
            try
            {
                auditBusiness.OID = newId.ToString();
                execptionDC.AuditBusiness.InsertOnSubmit(auditBusiness);
                execptionDC.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            catch
            {
                newId = Guid.Empty;
            }
            return newId;
        }

        /// <summary>
        /// 异常归档　状态改为9
        /// </summary>
        /// <param name="messageID"></param>
        public void ExceptionPigeonhole(Guid messageID)
        {
            string _SSE = string.Format("Update AuditBusiness Set Status = 9 Where MessageID = '{0}'", messageID.ToString());
            execptionDC.ExecuteCommand(_SSE);
        }

        /// <summary>
        /// 异常重发　状态改为8
        /// Tony　2010-06-10: 增加bingdingID以区分是出一对多
        /// </summary>
        /// <param name="messageID"></param>
        public void ExceptionResend(Guid messageID, Guid bingdingID)
        {
            string _SSE;
            if (bingdingID == Guid.Empty)
                _SSE = string.Format("Update AuditBusiness Set Status = 8 Where MessageID = '{0}'", messageID.ToString());
            else
                _SSE = string.Format("Update AuditBusiness Set Status = 8 Where MessageID = '{0}' AND BindingTemplateID = '{1}'", messageID.ToString(), bingdingID.ToString());

            execptionDC.ExecuteCommand(_SSE);
        }

        /// <summary>
        /// 获取到所有的审计日志
        /// </summary>
        /// <returns></returns>
        public List<AuditBusiness> GetAllAuditBusiness()
        {
            var businessList = from b in execptionDC.AuditBusiness
                               select new AuditBusiness { 
                                   BindingAddress = b.BindingAddress, 
                                   BusinessID = b.BusinessID, 
                                   BusinessName = b.BusinessName, 
                                   CallBeginTime = b.CallBeginTime, 
                                   CallEndTime = b.CallEndTime, 
                                   HostName = b.HostName, 
                                   MessageID = b.MessageID, 
                                   MethodName = b.MethodName, 
                                   OID = b.OID, 
                                   ReqBeginTime = b.ReqBeginTime,
                                   ReqEndTime =b.ReqEndTime,
                                   ServiceID = b.ServiceID,
                                   ServiceName = b.ServiceName,
                                   Status = b.Status
                               };
            return businessList.ToList<AuditBusiness>();
        }

        /// <summary>
        /// 根据OID获取到单条通讯记录
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public AuditBusiness GetAuditBusinessByOID(Guid oid)
        {
            var oneBusiness = from b in execptionDC.AuditBusiness
                              where b.OID.ToUpper() == oid.ToString().ToUpper()
                              select b;

            return oneBusiness.Single<AuditBusiness>();
        }

        /// <summary>
        /// 搜索通讯日志
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<AuditBusiness> AuditBusinessSearch(AuditBusinessSearchCondition condition)
        {
            DateTime today = DateTime.Now;
            DateTime todayStart = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);

            if (condition.DateScopeBegin == DateTime.MinValue)
            {
                condition.DateScopeBegin = todayStart;

                switch (condition.DateScope)
                {
                    case DateScopeEnum.OneDay:
                        break;
                    case DateScopeEnum.OneWeek:
                        condition.DateScopeBegin = condition.DateScopeBegin.AddDays(-7);
                        break;
                    case DateScopeEnum.OneMonth:
                        condition.DateScopeBegin = condition.DateScopeBegin.AddDays(-30);
                        break;
                    case DateScopeEnum.OneYear:
                        condition.DateScopeBegin = condition.DateScopeBegin.AddDays(-365);
                        break;
                    case DateScopeEnum.All:
                        condition.DateScopeBegin = condition.DateScopeBegin.AddYears(-100);
                        break;
                    default:
                        condition.DateScopeBegin = condition.DateScopeBegin.AddYears(-100);
                        break;
                }
            }

            if (condition.DateScopeEnd == DateTime.MinValue)
            {
                condition.DateScopeEnd = today;
            }

            string businessID = condition.BusinessID == Guid.Empty ? "" : condition.BusinessID.ToString();
            string serviceID = condition.ServiceID == Guid.Empty ? "" : condition.ServiceID.ToString();
            string _SSE = string.Format("exec AuditBusinessPage {0},{1},'{2}','{3}','{4}','{5}','{6}'",
                10, //PageSize
                1,  //PageIndex
                condition.DateScopeBegin.ToString("yyyy-MM-dd") + " 00:00:00",   //开始时间
                condition.DateScopeEnd.ToString("yyyy-MM-dd") + " 23:59:59",     //结束时间
                condition.Status.GetHashCode(),                                  //通讯状态   
                businessID,                                                              //实体ID   
                serviceID);
            IEnumerable<AuditBusiness> auditList = execptionDC.ExecuteQuery<AuditBusiness>(_SSE);            

            return auditList.ToList<AuditBusiness>();
        }


        /// <summary>
        /// 搜索通讯日志　分页版
        /// Tony 2011-07-29 按增加主机名称搜索
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<AuditBusiness> AuditBusinessSearch(AuditBusinessSearchCondition condition, int pageIndex, int pageSize)
        {
            condition = FormatSearchCondition(condition);

            string businessID = condition.BusinessID == Guid.Empty ? "" : condition.BusinessID.ToString();
            string serviceID = condition.ServiceID == Guid.Empty ? "" : condition.ServiceID.ToString();
            pageIndex = pageIndex / pageSize + 1;

            string _SSE = string.Format("exec AuditBusinessPage {0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}'",
                pageSize, //PageSize
                pageIndex,  //PageIndex
                condition.DateScopeBegin.ToString("yyyy-MM-dd") + " 00:00:00.000",   //开始时间
                condition.DateScopeEnd.ToString("yyyy-MM-dd") + " 23:59:59.999",     //结束时间
                condition.Status.GetHashCode(),                                  //通讯状态   
                businessID,                                                              //实体ID   
                serviceID,
                condition.HostName,
                condition.IfShowHeartBeat);

            IEnumerable<AuditBusiness> auditList = execptionDC.ExecuteQuery<AuditBusiness>(_SSE);

            return auditList.ToList<AuditBusiness>();
        }

        /// <summary>
        /// 获取到审计日志的数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int GetAuditBusinessCount(AuditBusinessSearchCondition condition)
        {
            condition = FormatSearchCondition(condition);

            string businessID = condition.BusinessID == Guid.Empty ? "" : condition.BusinessID.ToString();
            string serviceID = condition.ServiceID == Guid.Empty ? "" : condition.ServiceID.ToString();
            string _SSE = string.Format("exec GetAuditBusinessRowsCount '{0}','{1}','{2}','{3}','{4}','{5}','{6}'",
                condition.DateScopeBegin.ToString("yyyy-MM-dd") + " 00:00:00.000",   //开始时间
                condition.DateScopeEnd.ToString("yyyy-MM-dd") + " 23:59:59.999",     //结束时间
                condition.Status.GetHashCode(),                                  //通讯状态   
                businessID,                                                              //实体ID   
                serviceID,
                condition.HostName,
                condition.IfShowHeartBeat);

            var rowsCount = execptionDC.ExecuteQuery<int>(_SSE);

            return rowsCount.First<int>();
        }

        /// <summary>
        /// 格式化搜索条件
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private AuditBusinessSearchCondition FormatSearchCondition(AuditBusinessSearchCondition condition)
        {
            DateTime today = DateTime.Now;
            DateTime todayStart = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);

            if (condition.DateScopeBegin == DateTime.MinValue)
            {
                condition.DateScopeBegin = todayStart;

                switch (condition.DateScope)
                {
                    case DateScopeEnum.OneDay:
                        break;
                    case DateScopeEnum.OneWeek:
                        condition.DateScopeBegin = condition.DateScopeBegin.AddDays(-7);
                        break;
                    case DateScopeEnum.OneMonth:
                        condition.DateScopeBegin = condition.DateScopeBegin.AddDays(-30);
                        break;
                    case DateScopeEnum.OneYear:
                        condition.DateScopeBegin = condition.DateScopeBegin.AddDays(-365);
                        break;
                    case DateScopeEnum.All:
                        condition.DateScopeBegin = condition.DateScopeBegin.AddYears(-100);
                        break;
                    default:
                        condition.DateScopeBegin = condition.DateScopeBegin.AddYears(-100);
                        break;
                }
            }

            if (condition.DateScopeEnd == DateTime.MinValue)
            {
                condition.DateScopeEnd = today;
            }

            return condition;
        }

    }
}
