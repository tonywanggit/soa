﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using NewLife.Log;
using XCode;
using XCode.Configuration;
using ESB.Core.Monitor;
using System.Data;

namespace ESB.Core.Entity
{
    /// <summary></summary>
    [ModelCheckMode(ModelCheckModes.CheckTableWhenFirstUse)]
    public class AuditBusiness : AuditBusiness<AuditBusiness> { }
    
    /// <summary></summary>
    public partial class AuditBusiness<TEntity> : Entity<TEntity> where TEntity : AuditBusiness<TEntity>, new()
    {
        #region 对象操作﻿
        static AuditBusiness()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            TEntity entity = new TEntity();
        }

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnInsert()
        //{
        //    return base.OnInsert();
        //}

        ///// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        ///// <param name="isNew"></param>
        //public override void Valid(Boolean isNew)
        //{
        //    // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
        //    if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(_.Name, _.Name.DisplayName + "无效！");
        //    if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(_.ID, _.ID.DisplayName + "必须大于0！");

        //    // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
        //    base.Valid(isNew);

        //    // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
        //    if (isNew || Dirtys[_.Name]) CheckExist(_.Name);
        //    if (isNew || Dirtys[_.Name] || Dirtys[_.DbType]) CheckExist(_.Name, _.DbType);
        //    if ((isNew || Dirtys[_.Name]) && Exist(_.Name)) throw new ArgumentException(_.Name, "值为" + Name + "的" + _.Name.DisplayName + "已存在！");
        //}


        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    base.InitData();

        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    // Meta.Count是快速取得表记录数
        //    if (Meta.Count > 0) return;

        //    // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}AuditBusiness数据……", typeof(TEntity).Name);

        //    var entity = new AuditBusiness();
        //    entity.Name = "admin";
        //    entity.Password = DataHelper.Hash("admin");
        //    entity.DisplayName = "管理员";
        //    entity.RoleID = 1;
        //    entity.IsEnable = true;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}AuditBusiness数据！", typeof(TEntity).Name);
        //}
        #endregion

        #region 扩展属性﻿
        /// <summary>
        /// 上一级调用的ID
        /// </summary>
        private String m_ParentInvokeID;
        public String ParentInvokeID
        {
            get
            {
                if (m_ParentInvokeID != null) return m_ParentInvokeID;

                if (InvokeID == "00")
                    return "0";
                else
                    return InvokeID.Substring(0, InvokeID.Length - 3);
            }
            set
            {
                m_ParentInvokeID = value;
            }
        }
        #endregion

        #region 扩展查询﻿
        /// <summary>
        /// 根据主键查找审计日志
        /// </summary>
        /// <param name="auditID"></param>
        /// <returns></returns>
        public static TEntity GetAuditBusinessByOID(String auditID)
        {
            return Find(_.OID, auditID);
        }


        public static EntityList<TEntity> FindAllByTraceID(String traceID)
        {
            EntityList<TEntity> lstAudit = FindAll(_.TraceID == traceID, _.InvokeID
                , "OID, HostName, ServiceName, MethodName, ReqBeginTime, ReqEndTime, InvokeTimeSpan, InvokeID, InvokeLevel, InvokeOrder", 0, 0);

            return lstAudit;
        }
        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        ///// <summary>
        ///// 查询满足条件的记录集，分页、排序
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>实体集</returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public static EntityList<TEntity> Search(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindAll(SearchWhere(key), orderClause, null, startRowIndex, maximumRows);
        //}

        ///// <summary>
        ///// 查询满足条件的记录总数，分页和排序无效，带参数是因为ObjectDataSource要求它跟Search统一
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>记录数</returns>
        //public static Int32 SearchCount(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindCount(SearchWhere(key), null, null, 0, 0);
        //}

        /// <summary>构造搜索条件</summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        private static String SearchWhere(String key)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            var exp = new WhereExpression();

            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索
            if (!String.IsNullOrEmpty(key)) SearchWhereByKeys(exp.Builder, key);

            // 以下仅为演示，2、3行是同一个意思的不同写法，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符（第4行）
            //exp &= _.Name == "testName"
            //    & !String.IsNullOrEmpty(key) & _.Name == key
            //    .AndIf(!String.IsNullOrEmpty(key), _.Name == key)
            //    | _.ID > 0;

            return exp;
        }

        public static EntityList<AuditBusiness> AuditBusinessSearch(AuditBusinessSearchCondition condition, int pageIndex, int pageSize)
        {
            condition = FormatSearchCondition(condition);

            string businessID = condition.BusinessID;
            string serviceID = condition.ServiceID;
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

            //IEnumerable<AuditBusiness> auditList = execptionDC.ExecuteQuery<AuditBusiness>(_SSE);

            DataSet dsAudit = AuditBusiness.Meta.Query(_SSE);

            return AuditBusiness.LoadData(dsAudit);
        }

        /// <summary>
        /// 获取到审计日志的数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int GetAuditBusinessCount(AuditBusinessSearchCondition condition)
        {
            condition = FormatSearchCondition(condition);

            string businessID = condition.BusinessID;
            string serviceID = condition.ServiceID;
            string _SSE = string.Format("exec GetAuditBusinessRowsCount '{0}','{1}','{2}','{3}','{4}','{5}','{6}'",
                condition.DateScopeBegin.ToString("yyyy-MM-dd") + " 00:00:00.000",   //开始时间
                condition.DateScopeEnd.ToString("yyyy-MM-dd") + " 23:59:59.999",     //结束时间
                condition.Status.GetHashCode(),                                  //通讯状态   
                businessID,                                                              //实体ID   
                serviceID,
                condition.HostName,
                condition.IfShowHeartBeat);

            //var rowsCount = execptionDC.ExecuteQuery<int>(_SSE);
            DataSet dsAudit = AuditBusiness.Meta.Query(_SSE);

            return Int32.Parse(dsAudit.Tables[0].Rows[0][0].ToString());
        }

        /// <summary>
        /// 格式化搜索条件
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private static AuditBusinessSearchCondition FormatSearchCondition(AuditBusinessSearchCondition condition)
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

        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        /// <summary>
        /// 获取到看板的统计数据
        /// </summary>
        /// <param name="businessID"></param>
        /// <returns></returns>
        public static DataSet GetDashboardOverview(String businessID)
        {
            String sql = String.Format("EXEC AuditDashboard '{0}','{1}', '{2}'"
                , DateTime.Now.ToString("yyyy-MM-dd 00:00:00")
                , DateTime.Now.ToString("yyyy-MM-dd 23:59:59")
                , businessID);


            return Meta.Query(sql);
        }
        #endregion
    }
}