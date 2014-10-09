﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using NewLife.Log;
using XCode;
using XCode.Configuration;
using System.Data;

namespace ESB.Core.Entity
{
    /// <summary></summary>
    [ModelCheckMode(ModelCheckModes.CheckTableWhenFirstUse)]
    public class BusinessServiceVersion : BusinessServiceVersion<BusinessServiceVersion> { }
    
    /// <summary></summary>
    public partial class BusinessServiceVersion<TEntity> : Entity<TEntity> where TEntity : BusinessServiceVersion<TEntity>, new()
    {
        #region 对象操作﻿
        static BusinessServiceVersion()
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
        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}BusinessServiceVersion数据……", typeof(TEntity).Name);

        //    var entity = new BusinessServiceVersion();
        //    entity.Name = "admin";
        //    entity.Password = DataHelper.Hash("admin");
        //    entity.DisplayName = "管理员";
        //    entity.RoleID = 1;
        //    entity.IsEnable = true;
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}BusinessServiceVersion数据！", typeof(TEntity).Name);
        //}
        #endregion

        #region 扩展属性﻿
        /// <summary>
        /// 状态描述
        /// </summary>
        public String StatusDesc
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "编辑中";
                    case 1:
                        return "提交评审";
                    case 2:
                        return "已发布";
                    case 3:
                        return "评审拒绝";
                    case 9:
                        return "已废弃";
                    default:
                        return "异常状态";
                }
            }
            set { }
        }
        /// <summary>
        /// 服务提供者
        /// </summary>
        public String BusinessName{ get; set; }
        /// <summary>
        /// 服务描述
        /// </summary>
        public String ServiceDesc { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public String ServiceName { get; set; }
        /// <summary>
        /// 服务管理员
        /// </summary>
        public String Manager { get; set; }
        /// <summary>
        /// 默认版本
        /// </summary>
        public Int32 DefaultVersion { get; set; }
        #endregion

        #region 扩展查询﻿
        /// <summary>根据主健查找</summary>
        /// <param name="oid">主健</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static TEntity FindByOID(String oid)
        {
            if (Meta.Count >= 1000)
                return Find(_.OID, oid);
            else // 实体缓存
                return Meta.Cache.Entities.Find(_.OID, oid);
            // 单对象缓存
            //return Meta.SingleCache[oid];
        }

        /// <summary>根据服务ID查找</summary>
        /// <param name="oid">主健</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EntityList<TEntity> FindAllByServiceID(String serviceID)
        {
            if (Meta.Count >= 1000)
                return FindAll(_.ServiceID, serviceID);
            else // 实体缓存
                return Meta.Cache.Entities.FindAll(_.ServiceID, serviceID);
            // 单对象缓存
            //return Meta.SingleCache[oid];
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 将服务关联的服务版本统一删除
        /// </summary>
        /// <param name="serviceID"></param>
        public static void DeleteByServiceID(String serviceID)
        {
            Delete(new String[] { _.ServiceID }, new String[] { serviceID });
        }

        /// <summary>
        /// 修改指定版本的状态
        /// </summary>
        /// <param name="versionID"></param>
        /// <param name="status"></param>
        public static void UpdateServiceVersionStatus(String versionID, Int32 status, String opinion)
        {
            TEntity version = FindByOID(versionID);
            if (version != null)
            {
                if (status == 1)//--将状态置为提交审批
                {
                    version.CommitDateTime = DateTime.Now;
                }
                else if (status > 1)//--将状态置为 审批通过或审批拒绝
                {
                    version.ConfirmDateTime = DateTime.Now;
                    version.Opinion = opinion;

                    //--当发布一个版本时需要将其修订版本的状态设置为已废弃
                    if (status == 2)
                        SetServiceVersionObsoleteWhenPublish(version);
                }
                version.Status = status;
                version.Update();
            }
        }

        /// <summary>
        /// 当一个版本发布的同时需要将该版本下的所有修订版设置为已废弃
        /// 例如版本5下有5.0版已发布，当发布5.1版时需要将5.0版设置为已废弃
        /// </summary>
        /// <param name="entity">需要发布的版本</param>
        private static void SetServiceVersionObsoleteWhenPublish(TEntity entity)
        {
            EntityList<TEntity> lstEntity = FindAllByServiceID(entity.ServiceID);
            foreach (var ver in lstEntity)
            {
                if (ver.BigVer == entity.BigVer && ver.OID != entity.OID)
                {
                    ver.Status = 9;//--将版本状态设置为已废弃
                    ver.ObsoletePersonID = entity.CreatePersionID;//--默认为发布版本的创建人将修订版本废弃
                    ver.ObsoleteDateTime = DateTime.Now;
                    ver.Update();
                }
            }
        }

        /// <summary>
        /// 修改指定版本的信息
        /// </summary>
        /// <param name="versionID"></param>
        /// <param name="status"></param>
        public static void UpdateServiceVersionInfo(String versionID, String confirmPersonID, String desc)
        {
            BusinessServiceVersion.Update(new String[] { _.ConfirmPersonID, _.Description }, new Object[] { confirmPersonID, desc }, new String[] { _.OID }, new Object[] { versionID });
        }

        

        #endregion

        #region 业务
        /// <summary>
        /// 修订版本
        /// </summary>
        /// <param name="versionID"></param>
        /// <param name="status"></param>
        public static void ReviseServiceVersion(String versionID, String personalID)
        {
            TEntity versionBase = FindByOID(versionID);
            if (versionBase != null)
            {
                TEntity versionNew = new TEntity()
                {
                    OID = Guid.NewGuid().ToString(),
                    CreatePersionID = personalID,
                    CreateDateTime = DateTime.Now,
                    BigVer = versionBase.BigVer,
                    SmallVer = versionBase.SmallVer + 1,
                    ServiceID = versionBase.ServiceID,
                    Description = String.Format("{0}.{1}的修订版本。", versionBase.BigVer, versionBase.SmallVer),
                    Status = 0
                };
                versionNew.Insert();

                //--将原先的方法契约拷贝到新的服务版本上
                ServiceContract.CopyServiceContract(versionID, versionNew.OID);
            }
        }

        /// <summary>
        /// 升级版本
        /// </summary>
        /// <param name="versionID"></param>
        /// <param name="status"></param>
        public static void UpgradeServiceVersion(String versionID, String personalID)
        {
            TEntity versionBase = FindByOID(versionID);

            if (versionBase != null)
            {
                List<TEntity> lstEntity = FindAllByServiceID(versionBase.ServiceID);
                Int32 BigVer = lstEntity.Max(x => x.BigVer);

                TEntity versionNew = new TEntity()
                {
                    OID = Guid.NewGuid().ToString(),
                    CreatePersionID = personalID,
                    CreateDateTime = DateTime.Now,
                    BigVer = BigVer + 1,
                    SmallVer = 0,
                    ServiceID = versionBase.ServiceID,
                    Description = String.Format("{0}.{1}的升级版本。", versionBase.BigVer, versionBase.SmallVer),
                    Status = 0
                };
                versionNew.Insert();

                //--将原先的方法契约拷贝到新的服务版本上
                ServiceContract.CopyServiceContract(versionID, versionNew.OID);
            }
        }

        /// <summary>
        /// 删除服务版本和关联契约
        /// </summary>
        /// <param name="versionID"></param>
        /// <param name="personalID"></param>
        public static void DeleteServiceVersionAndContract(String versionID)
        {
            ServiceContract.DeleteAllContract(versionID);
            Delete(new String[] { _.OID }, new Object[] { versionID });
        }

        /// <summary>
        /// 废弃服务版本
        /// 一但废弃一个版本，则相同大版本下的所有版本都将被置为已废弃
        /// </summary>
        /// <param name="versionID"></param>
        public static void ObsoleteServiceVersion(String versionID, String personalID)
        {
            TEntity version = FindByOID(versionID);
            if (version != null)
            {
                EntityList<TEntity> lstEntity = FindAllByServiceID(version.ServiceID);
                foreach (var ver in lstEntity)
                {
                    if (ver.BigVer == version.BigVer)
                    {
                        ver.Status = 9;//--将版本状态设置为已废弃
                        ver.ObsoletePersonID = personalID;
                        ver.ObsoleteDateTime = DateTime.Now;
                        ver.Update();
                    }
                }
            }
        }

        /// <summary>
        /// 获取到所有发布的服务版本
        /// </summary>
        /// <param name="businessID"></param>
        /// <returns></returns>
        public static EntityList<BusinessServiceVersion> GetPublishServiceVersion(String businessID)
        {
            String sql = String.Format("EXEC GetPublishServiceVersion '{0}'", businessID);
            DataSet ds = Meta.Query(sql);

            return BusinessServiceVersion.LoadData(ds);
        }

        #endregion
    }
}