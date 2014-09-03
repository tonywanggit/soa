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
    [BindTable("EsbView_ServiceVersion", Description = "", ConnName = "EsbServiceDirectoryDB", DbType = DatabaseType.SqlServer, IsView = true)]
    public partial class EsbView_ServiceVersion<TEntity> : IEsbView_ServiceVersion
    {
        #region 属性
        private String _BusinessID;
        /// <summary></summary>
        [DisplayName("BusinessID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(1, "BusinessID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String BusinessID
        {
            get { return _BusinessID; }
            set { if (OnPropertyChanging(__.BusinessID, value)) { _BusinessID = value; OnPropertyChanged(__.BusinessID); } }
        }

        private String _ServiceName;
        /// <summary></summary>
        [DisplayName("ServiceName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(2, "ServiceName", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceName
        {
            get { return _ServiceName; }
            set { if (OnPropertyChanging(__.ServiceName, value)) { _ServiceName = value; OnPropertyChanged(__.ServiceName); } }
        }

        private Int32 _BigVer;
        /// <summary></summary>
        [DisplayName("BigVer")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(3, "BigVer", "", null, "int", 10, 0, false)]
        public virtual Int32 BigVer
        {
            get { return _BigVer; }
            set { if (OnPropertyChanging(__.BigVer, value)) { _BigVer = value; OnPropertyChanged(__.BigVer); } }
        }

        private Int32 _SmallVer;
        /// <summary></summary>
        [DisplayName("SmallVer")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(4, "SmallVer", "", null, "int", 10, 0, false)]
        public virtual Int32 SmallVer
        {
            get { return _SmallVer; }
            set { if (OnPropertyChanging(__.SmallVer, value)) { _SmallVer = value; OnPropertyChanged(__.SmallVer); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, false, 500)]
        [BindColumn(5, "Description", "", null, "nvarchar(500)", 0, 0, true)]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private String _ConfirmPersonID;
        /// <summary></summary>
        [DisplayName("ConfirmPersonID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(6, "ConfirmPersonID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ConfirmPersonID
        {
            get { return _ConfirmPersonID; }
            set { if (OnPropertyChanging(__.ConfirmPersonID, value)) { _ConfirmPersonID = value; OnPropertyChanged(__.ConfirmPersonID); } }
        }

        private DateTime _CommitDateTime;
        /// <summary></summary>
        [DisplayName("CommitDateTime")]
        [Description("")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(7, "CommitDateTime", "", null, "datetime", 3, 0, false)]
        public virtual DateTime CommitDateTime
        {
            get { return _CommitDateTime; }
            set { if (OnPropertyChanging(__.CommitDateTime, value)) { _CommitDateTime = value; OnPropertyChanged(__.CommitDateTime); } }
        }

        private DateTime _CreateDateTime;
        /// <summary></summary>
        [DisplayName("CreateDateTime")]
        [Description("")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn(8, "CreateDateTime", "", null, "datetime", 3, 0, false)]
        public virtual DateTime CreateDateTime
        {
            get { return _CreateDateTime; }
            set { if (OnPropertyChanging(__.CreateDateTime, value)) { _CreateDateTime = value; OnPropertyChanged(__.CreateDateTime); } }
        }

        private String _CreatePersionID;
        /// <summary></summary>
        [DisplayName("CreatePersionID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(9, "CreatePersionID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String CreatePersionID
        {
            get { return _CreatePersionID; }
            set { if (OnPropertyChanging(__.CreatePersionID, value)) { _CreatePersionID = value; OnPropertyChanged(__.CreatePersionID); } }
        }

        private String _ServiceID;
        /// <summary></summary>
        [DisplayName("ServiceID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(10, "ServiceID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceID
        {
            get { return _ServiceID; }
            set { if (OnPropertyChanging(__.ServiceID, value)) { _ServiceID = value; OnPropertyChanged(__.ServiceID); } }
        }

        private String _VersionID;
        /// <summary></summary>
        [DisplayName("VersionID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(11, "VersionID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String VersionID
        {
            get { return _VersionID; }
            set { if (OnPropertyChanging(__.VersionID, value)) { _VersionID = value; OnPropertyChanged(__.VersionID); } }
        }

        private Int32 _VerStatus;
        /// <summary></summary>
        [DisplayName("VerStatus")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(12, "VerStatus", "", null, "int", 10, 0, false)]
        public virtual Int32 VerStatus
        {
            get { return _VerStatus; }
            set { if (OnPropertyChanging(__.VerStatus, value)) { _VerStatus = value; OnPropertyChanged(__.VerStatus); } }
        }

        private Int32 _DefaultVersion;
        /// <summary></summary>
        [DisplayName("DefaultVersion")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(13, "DefaultVersion", "", null, "int", 10, 0, false)]
        public virtual Int32 DefaultVersion
        {
            get { return _DefaultVersion; }
            set { if (OnPropertyChanging(__.DefaultVersion, value)) { _DefaultVersion = value; OnPropertyChanged(__.DefaultVersion); } }
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
                    case __.BusinessID: return _BusinessID;
                    case __.ServiceName: return _ServiceName;
                    case __.BigVer: return _BigVer;
                    case __.SmallVer: return _SmallVer;
                    case __.Description: return _Description;
                    case __.ConfirmPersonID: return _ConfirmPersonID;
                    case __.CommitDateTime: return _CommitDateTime;
                    case __.CreateDateTime: return _CreateDateTime;
                    case __.CreatePersionID: return _CreatePersionID;
                    case __.ServiceID: return _ServiceID;
                    case __.VersionID: return _VersionID;
                    case __.VerStatus: return _VerStatus;
                    case __.DefaultVersion: return _DefaultVersion;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.BusinessID: _BusinessID = Convert.ToString(value); break;
                    case __.ServiceName: _ServiceName = Convert.ToString(value); break;
                    case __.BigVer: _BigVer = Convert.ToInt32(value); break;
                    case __.SmallVer: _SmallVer = Convert.ToInt32(value); break;
                    case __.Description: _Description = Convert.ToString(value); break;
                    case __.ConfirmPersonID: _ConfirmPersonID = Convert.ToString(value); break;
                    case __.CommitDateTime: _CommitDateTime = Convert.ToDateTime(value); break;
                    case __.CreateDateTime: _CreateDateTime = Convert.ToDateTime(value); break;
                    case __.CreatePersionID: _CreatePersionID = Convert.ToString(value); break;
                    case __.ServiceID: _ServiceID = Convert.ToString(value); break;
                    case __.VersionID: _VersionID = Convert.ToString(value); break;
                    case __.VerStatus: _VerStatus = Convert.ToInt32(value); break;
                    case __.DefaultVersion: _DefaultVersion = Convert.ToInt32(value); break;
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
            public static readonly Field BusinessID = FindByName(__.BusinessID);

            ///<summary></summary>
            public static readonly Field ServiceName = FindByName(__.ServiceName);

            ///<summary></summary>
            public static readonly Field BigVer = FindByName(__.BigVer);

            ///<summary></summary>
            public static readonly Field SmallVer = FindByName(__.SmallVer);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field ConfirmPersonID = FindByName(__.ConfirmPersonID);

            ///<summary></summary>
            public static readonly Field CommitDateTime = FindByName(__.CommitDateTime);

            ///<summary></summary>
            public static readonly Field CreateDateTime = FindByName(__.CreateDateTime);

            ///<summary></summary>
            public static readonly Field CreatePersionID = FindByName(__.CreatePersionID);

            ///<summary></summary>
            public static readonly Field ServiceID = FindByName(__.ServiceID);

            ///<summary></summary>
            public static readonly Field VersionID = FindByName(__.VersionID);

            ///<summary></summary>
            public static readonly Field VerStatus = FindByName(__.VerStatus);

            ///<summary></summary>
            public static readonly Field DefaultVersion = FindByName(__.DefaultVersion);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String BusinessID = "BusinessID";

            ///<summary></summary>
            public const String ServiceName = "ServiceName";

            ///<summary></summary>
            public const String BigVer = "BigVer";

            ///<summary></summary>
            public const String SmallVer = "SmallVer";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String ConfirmPersonID = "ConfirmPersonID";

            ///<summary></summary>
            public const String CommitDateTime = "CommitDateTime";

            ///<summary></summary>
            public const String CreateDateTime = "CreateDateTime";

            ///<summary></summary>
            public const String CreatePersionID = "CreatePersionID";

            ///<summary></summary>
            public const String ServiceID = "ServiceID";

            ///<summary></summary>
            public const String VersionID = "VersionID";

            ///<summary></summary>
            public const String VerStatus = "VerStatus";

            ///<summary></summary>
            public const String DefaultVersion = "DefaultVersion";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IEsbView_ServiceVersion
    {
        #region 属性
        /// <summary></summary>
        String BusinessID { get; set; }

        /// <summary></summary>
        String ServiceName { get; set; }

        /// <summary></summary>
        Int32 BigVer { get; set; }

        /// <summary></summary>
        Int32 SmallVer { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        String ConfirmPersonID { get; set; }

        /// <summary></summary>
        DateTime CommitDateTime { get; set; }

        /// <summary></summary>
        DateTime CreateDateTime { get; set; }

        /// <summary></summary>
        String CreatePersionID { get; set; }

        /// <summary></summary>
        String ServiceID { get; set; }

        /// <summary></summary>
        String VersionID { get; set; }

        /// <summary></summary>
        Int32 VerStatus { get; set; }

        /// <summary></summary>
        Int32 DefaultVersion { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}