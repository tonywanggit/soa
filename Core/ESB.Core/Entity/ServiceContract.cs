﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ESB.Core.Entity
{
    /// <summary></summary>
    [Serializable]
    [DataObject]
    [Description("")]
    [BindIndex("PK_ServiceMethod", true, "OID")]
    [BindTable("ServiceContract", Description = "", ConnName = "EsbServiceDirectoryDB", DbType = DatabaseType.SqlServer)]
    public partial class ServiceContract<TEntity> : IServiceContract
    {
        #region 属性
        private String _OID;
        /// <summary></summary>
        [DisplayName("OID")]
        [Description("")]
        [DataObjectField(true, false, false, 50)]
        [BindColumn(1, "OID", "", "", "nvarchar(50)", 0, 0, true)]
        public virtual String OID
        {
            get { return _OID; }
            set { if (OnPropertyChanging(__.OID, value)) { _OID = value; OnPropertyChanged(__.OID); } }
        }

        private String _ServiceID;
        /// <summary>服务ID </summary>
        [DisplayName("服务ID")]
        [Description("服务ID ")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(2, "ServiceID", "服务ID ", "", "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceID
        {
            get { return _ServiceID; }
            set { if (OnPropertyChanging(__.ServiceID, value)) { _ServiceID = value; OnPropertyChanged(__.ServiceID); } }
        }

        private String _ServiceVersionID;
        /// <summary>服务版本ID</summary>
        [DisplayName("服务版本ID")]
        [Description("服务版本ID")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(3, "ServiceVersionID", "服务版本ID", "0", "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceVersionID
        {
            get { return _ServiceVersionID; }
            set { if (OnPropertyChanging(__.ServiceVersionID, value)) { _ServiceVersionID = value; OnPropertyChanged(__.ServiceVersionID); } }
        }

        private String _MethodName;
        /// <summary>方法名称</summary>
        [DisplayName("方法名称")]
        [Description("方法名称")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(4, "MethodName", "方法名称", "", "nvarchar(50)", 0, 0, true)]
        public virtual String MethodName
        {
            get { return _MethodName; }
            set { if (OnPropertyChanging(__.MethodName, value)) { _MethodName = value; OnPropertyChanged(__.MethodName); } }
        }

        private String _MethodContract;
        /// <summary>方法描述</summary>
        [DisplayName("方法描述")]
        [Description("方法描述")]
        [DataObjectField(false, false, false, 4000)]
        [BindColumn(5, "MethodContract", "方法描述", "", "nvarchar(4000)", 0, 0, true)]
        public virtual String MethodContract
        {
            get { return _MethodContract; }
            set { if (OnPropertyChanging(__.MethodContract, value)) { _MethodContract = value; OnPropertyChanged(__.MethodContract); } }
        }

        private String _CreatePersonID;
        /// <summary>契约撰写者</summary>
        [DisplayName("契约撰写者")]
        [Description("契约撰写者")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(6, "CreatePersonID", "契约撰写者", "", "nvarchar(50)", 0, 0, true)]
        public virtual String CreatePersonID
        {
            get { return _CreatePersonID; }
            set { if (OnPropertyChanging(__.CreatePersonID, value)) { _CreatePersonID = value; OnPropertyChanged(__.CreatePersonID); } }
        }

        private DateTime _CreateDateTime;
        /// <summary>撰写时间</summary>
        [DisplayName("撰写时间")]
        [Description("撰写时间")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn(7, "CreateDateTime", "撰写时间", "getdate()", "datetime", 3, 0, false)]
        public virtual DateTime CreateDateTime
        {
            get { return _CreateDateTime; }
            set { if (OnPropertyChanging(__.CreateDateTime, value)) { _CreateDateTime = value; OnPropertyChanged(__.CreateDateTime); } }
        }

        private Int32 _Status;
        /// <summary>0：当前，1：提交评审，2：评审通过</summary>
        [DisplayName("0：当前，1：提交评审，2：评审通过")]
        [Description("0：当前，1：提交评审，2：评审通过")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(8, "Status", "0：当前，1：提交评审，2：评审通过", "0", "int", 10, 0, false)]
        public virtual Int32 Status
        {
            get { return _Status; }
            set { if (OnPropertyChanging(__.Status, value)) { _Status = value; OnPropertyChanged(__.Status); } }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.OID : return _OID;
                    case __.ServiceID : return _ServiceID;
                    case __.ServiceVersionID : return _ServiceVersionID;
                    case __.MethodName : return _MethodName;
                    case __.MethodContract : return _MethodContract;
                    case __.CreatePersonID : return _CreatePersonID;
                    case __.CreateDateTime : return _CreateDateTime;
                    case __.Status : return _Status;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.OID : _OID = Convert.ToString(value); break;
                    case __.ServiceID : _ServiceID = Convert.ToString(value); break;
                    case __.ServiceVersionID : _ServiceVersionID = Convert.ToString(value); break;
                    case __.MethodName : _MethodName = Convert.ToString(value); break;
                    case __.MethodContract : _MethodContract = Convert.ToString(value); break;
                    case __.CreatePersonID : _CreatePersonID = Convert.ToString(value); break;
                    case __.CreateDateTime : _CreateDateTime = Convert.ToDateTime(value); break;
                    case __.Status : _Status = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得字段信息的快捷方式</summary>
        public class _
        {
            ///<summary></summary>
            public static readonly Field OID = FindByName(__.OID);

            ///<summary>服务ID </summary>
            public static readonly Field ServiceID = FindByName(__.ServiceID);

            ///<summary>服务版本ID</summary>
            public static readonly Field ServiceVersionID = FindByName(__.ServiceVersionID);

            ///<summary>方法名称</summary>
            public static readonly Field MethodName = FindByName(__.MethodName);

            ///<summary>方法描述</summary>
            public static readonly Field MethodContract = FindByName(__.MethodContract);

            ///<summary>契约撰写者</summary>
            public static readonly Field CreatePersonID = FindByName(__.CreatePersonID);

            ///<summary>撰写时间</summary>
            public static readonly Field CreateDateTime = FindByName(__.CreateDateTime);

            ///<summary>0：当前，1：提交评审，2：评审通过</summary>
            public static readonly Field Status = FindByName(__.Status);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String OID = "OID";

            ///<summary>服务ID </summary>
            public const String ServiceID = "ServiceID";

            ///<summary>服务版本ID</summary>
            public const String ServiceVersionID = "ServiceVersionID";

            ///<summary>方法名称</summary>
            public const String MethodName = "MethodName";

            ///<summary>方法描述</summary>
            public const String MethodContract = "MethodContract";

            ///<summary>契约撰写者</summary>
            public const String CreatePersonID = "CreatePersonID";

            ///<summary>撰写时间</summary>
            public const String CreateDateTime = "CreateDateTime";

            ///<summary>0：当前，1：提交评审，2：评审通过</summary>
            public const String Status = "Status";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IServiceContract
    {
        #region 属性
        /// <summary></summary>
        String OID { get; set; }

        /// <summary>服务ID </summary>
        String ServiceID { get; set; }

        /// <summary>服务版本ID</summary>
        String ServiceVersionID { get; set; }

        /// <summary>方法名称</summary>
        String MethodName { get; set; }

        /// <summary>方法描述</summary>
        String MethodContract { get; set; }

        /// <summary>契约撰写者</summary>
        String CreatePersonID { get; set; }

        /// <summary>撰写时间</summary>
        DateTime CreateDateTime { get; set; }

        /// <summary>0：当前，1：提交评审，2：评审通过</summary>
        Int32 Status { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}