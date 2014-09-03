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
    [BindIndex("PK_BusinessServiceVersion", true, "OID")]
    [BindTable("BusinessServiceVersion", Description = "", ConnName = "EsbServiceDirectoryDB", DbType = DatabaseType.SqlServer)]
    public partial class BusinessServiceVersion<TEntity> : IBusinessServiceVersion
    {
        #region 属性
        private String _OID;
        /// <summary>主健</summary>
        [DisplayName("主健")]
        [Description("主健")]
        [DataObjectField(true, false, false, 50)]
        [BindColumn(1, "OID", "主健", "", "nvarchar(50)", 0, 0, true)]
        public virtual String OID
        {
            get { return _OID; }
            set { if (OnPropertyChanging(__.OID, value)) { _OID = value; OnPropertyChanged(__.OID); } }
        }

        private String _ServiceID;
        /// <summary>服务ID</summary>
        [DisplayName("服务ID")]
        [Description("服务ID")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(2, "ServiceID", "服务ID", "", "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceID
        {
            get { return _ServiceID; }
            set { if (OnPropertyChanging(__.ServiceID, value)) { _ServiceID = value; OnPropertyChanged(__.ServiceID); } }
        }

        private Int32 _BigVer;
        /// <summary>大版本</summary>
        [DisplayName("大版本")]
        [Description("大版本")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(3, "BigVer", "大版本", "0", "int", 10, 0, false)]
        public virtual Int32 BigVer
        {
            get { return _BigVer; }
            set { if (OnPropertyChanging(__.BigVer, value)) { _BigVer = value; OnPropertyChanged(__.BigVer); } }
        }

        private Int32 _SmallVer;
        /// <summary>小版本</summary>
        [DisplayName("小版本")]
        [Description("小版本")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(4, "SmallVer", "小版本", "0", "int", 10, 0, false)]
        public virtual Int32 SmallVer
        {
            get { return _SmallVer; }
            set { if (OnPropertyChanging(__.SmallVer, value)) { _SmallVer = value; OnPropertyChanged(__.SmallVer); } }
        }

        private String _CreatePersionID;
        /// <summary>创建人</summary>
        [DisplayName("创建人")]
        [Description("创建人")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(5, "CreatePersionID", "创建人", "", "nvarchar(50)", 0, 0, true)]
        public virtual String CreatePersionID
        {
            get { return _CreatePersionID; }
            set { if (OnPropertyChanging(__.CreatePersionID, value)) { _CreatePersionID = value; OnPropertyChanged(__.CreatePersionID); } }
        }

        private String _ConfirmPersonID;
        /// <summary>契约确认人</summary>
        [DisplayName("契约确认人")]
        [Description("契约确认人")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(6, "ConfirmPersonID", "契约确认人", "", "nvarchar(50)", 0, 0, true)]
        public virtual String ConfirmPersonID
        {
            get { return _ConfirmPersonID; }
            set { if (OnPropertyChanging(__.ConfirmPersonID, value)) { _ConfirmPersonID = value; OnPropertyChanged(__.ConfirmPersonID); } }
        }

        private DateTime _CreateDateTime;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn(7, "CreateDateTime", "创建时间", "getdate()", "datetime", 3, 0, false)]
        public virtual DateTime CreateDateTime
        {
            get { return _CreateDateTime; }
            set { if (OnPropertyChanging(__.CreateDateTime, value)) { _CreateDateTime = value; OnPropertyChanged(__.CreateDateTime); } }
        }

        private DateTime _CommitDateTime;
        /// <summary>提交评审时间</summary>
        [DisplayName("提交评审时间")]
        [Description("提交评审时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(8, "CommitDateTime", "提交评审时间", null, "datetime", 3, 0, false)]
        public virtual DateTime CommitDateTime
        {
            get { return _CommitDateTime; }
            set { if (OnPropertyChanging(__.CommitDateTime, value)) { _CommitDateTime = value; OnPropertyChanged(__.CommitDateTime); } }
        }

        private DateTime _ConfirmDateTime;
        /// <summary>评审通过时间</summary>
        [DisplayName("评审通过时间")]
        [Description("评审通过时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(9, "ConfirmDateTime", "评审通过时间", null, "datetime", 3, 0, false)]
        public virtual DateTime ConfirmDateTime
        {
            get { return _ConfirmDateTime; }
            set { if (OnPropertyChanging(__.ConfirmDateTime, value)) { _ConfirmDateTime = value; OnPropertyChanged(__.ConfirmDateTime); } }
        }

        private Int32 _Status;
        /// <summary>0：未启用，1：提交评审，2：评审通过，3：评审拒绝</summary>
        [DisplayName("0：未启用，1：提交评审，2：评审通过，3：评审拒绝")]
        [Description("0：未启用，1：提交评审，2：评审通过，3：评审拒绝")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(10, "Status", "0：未启用，1：提交评审，2：评审通过，3：评审拒绝", "0", "int", 10, 0, false)]
        public virtual Int32 Status
        {
            get { return _Status; }
            set { if (OnPropertyChanging(__.Status, value)) { _Status = value; OnPropertyChanged(__.Status); } }
        }

        private String _Description;
        /// <summary>版本描述</summary>
        [DisplayName("版本描述")]
        [Description("版本描述")]
        [DataObjectField(false, false, false, 500)]
        [BindColumn(11, "Description", "版本描述", "", "nvarchar(500)", 0, 0, true)]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private String _Opinion;
        /// <summary>审批意见</summary>
        [DisplayName("审批意见")]
        [Description("审批意见")]
        [DataObjectField(false, false, false, 500)]
        [BindColumn(12, "Opinion", "审批意见", "", "nvarchar(500)", 0, 0, true)]
        public virtual String Opinion
        {
            get { return _Opinion; }
            set { if (OnPropertyChanging(__.Opinion, value)) { _Opinion = value; OnPropertyChanged(__.Opinion); } }
        }

        private String _ObsoletePersonID;
        /// <summary>废弃人</summary>
        [DisplayName("废弃人")]
        [Description("废弃人")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(13, "ObsoletePersonID", "废弃人", "", "nvarchar(50)", 0, 0, true)]
        public virtual String ObsoletePersonID
        {
            get { return _ObsoletePersonID; }
            set { if (OnPropertyChanging(__.ObsoletePersonID, value)) { _ObsoletePersonID = value; OnPropertyChanged(__.ObsoletePersonID); } }
        }

        private DateTime _ObsoleteDateTime;
        /// <summary>废弃时间</summary>
        [DisplayName("废弃时间")]
        [Description("废弃时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(14, "ObsoleteDateTime", "废弃时间", null, "datetime", 3, 0, false)]
        public virtual DateTime ObsoleteDateTime
        {
            get { return _ObsoleteDateTime; }
            set { if (OnPropertyChanging(__.ObsoleteDateTime, value)) { _ObsoleteDateTime = value; OnPropertyChanged(__.ObsoleteDateTime); } }
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
                    case __.OID: return _OID;
                    case __.ServiceID: return _ServiceID;
                    case __.BigVer: return _BigVer;
                    case __.SmallVer: return _SmallVer;
                    case __.CreatePersionID: return _CreatePersionID;
                    case __.ConfirmPersonID: return _ConfirmPersonID;
                    case __.CreateDateTime: return _CreateDateTime;
                    case __.CommitDateTime: return _CommitDateTime;
                    case __.ConfirmDateTime: return _ConfirmDateTime;
                    case __.Status: return _Status;
                    case __.Description: return _Description;
                    case __.Opinion: return _Opinion;
                    case __.ObsoletePersonID: return _ObsoletePersonID;
                    case __.ObsoleteDateTime: return _ObsoleteDateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.OID: _OID = Convert.ToString(value); break;
                    case __.ServiceID: _ServiceID = Convert.ToString(value); break;
                    case __.BigVer: _BigVer = Convert.ToInt32(value); break;
                    case __.SmallVer: _SmallVer = Convert.ToInt32(value); break;
                    case __.CreatePersionID: _CreatePersionID = Convert.ToString(value); break;
                    case __.ConfirmPersonID: _ConfirmPersonID = Convert.ToString(value); break;
                    case __.CreateDateTime: _CreateDateTime = Convert.ToDateTime(value); break;
                    case __.CommitDateTime: _CommitDateTime = Convert.ToDateTime(value); break;
                    case __.ConfirmDateTime: _ConfirmDateTime = Convert.ToDateTime(value); break;
                    case __.Status: _Status = Convert.ToInt32(value); break;
                    case __.Description: _Description = Convert.ToString(value); break;
                    case __.Opinion: _Opinion = Convert.ToString(value); break;
                    case __.ObsoletePersonID: _ObsoletePersonID = Convert.ToString(value); break;
                    case __.ObsoleteDateTime: _ObsoleteDateTime = Convert.ToDateTime(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得字段信息的快捷方式</summary>
        public class _
        {
            ///<summary>主健</summary>
            public static readonly Field OID = FindByName(__.OID);

            ///<summary>服务ID</summary>
            public static readonly Field ServiceID = FindByName(__.ServiceID);

            ///<summary>大版本</summary>
            public static readonly Field BigVer = FindByName(__.BigVer);

            ///<summary>小版本</summary>
            public static readonly Field SmallVer = FindByName(__.SmallVer);

            ///<summary>创建人</summary>
            public static readonly Field CreatePersionID = FindByName(__.CreatePersionID);

            ///<summary>契约确认人</summary>
            public static readonly Field ConfirmPersonID = FindByName(__.ConfirmPersonID);

            ///<summary>创建时间</summary>
            public static readonly Field CreateDateTime = FindByName(__.CreateDateTime);

            ///<summary>提交评审时间</summary>
            public static readonly Field CommitDateTime = FindByName(__.CommitDateTime);

            ///<summary>评审通过时间</summary>
            public static readonly Field ConfirmDateTime = FindByName(__.ConfirmDateTime);

            ///<summary>0：未启用，1：提交评审，2：评审通过，3：评审拒绝</summary>
            public static readonly Field Status = FindByName(__.Status);

            ///<summary>版本描述</summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary>审批意见</summary>
            public static readonly Field Opinion = FindByName(__.Opinion);

            ///<summary>废弃人</summary>
            public static readonly Field ObsoletePersonID = FindByName(__.ObsoletePersonID);

            ///<summary>废弃时间</summary>
            public static readonly Field ObsoleteDateTime = FindByName(__.ObsoleteDateTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary>主健</summary>
            public const String OID = "OID";

            ///<summary>服务ID</summary>
            public const String ServiceID = "ServiceID";

            ///<summary>大版本</summary>
            public const String BigVer = "BigVer";

            ///<summary>小版本</summary>
            public const String SmallVer = "SmallVer";

            ///<summary>创建人</summary>
            public const String CreatePersionID = "CreatePersionID";

            ///<summary>契约确认人</summary>
            public const String ConfirmPersonID = "ConfirmPersonID";

            ///<summary>创建时间</summary>
            public const String CreateDateTime = "CreateDateTime";

            ///<summary>提交评审时间</summary>
            public const String CommitDateTime = "CommitDateTime";

            ///<summary>评审通过时间</summary>
            public const String ConfirmDateTime = "ConfirmDateTime";

            ///<summary>0：未启用，1：提交评审，2：评审通过，3：评审拒绝</summary>
            public const String Status = "Status";

            ///<summary>版本描述</summary>
            public const String Description = "Description";

            ///<summary>审批意见</summary>
            public const String Opinion = "Opinion";

            ///<summary>废弃人</summary>
            public const String ObsoletePersonID = "ObsoletePersonID";

            ///<summary>废弃时间</summary>
            public const String ObsoleteDateTime = "ObsoleteDateTime";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IBusinessServiceVersion
    {
        #region 属性
        /// <summary>主健</summary>
        String OID { get; set; }

        /// <summary>服务ID</summary>
        String ServiceID { get; set; }

        /// <summary>大版本</summary>
        Int32 BigVer { get; set; }

        /// <summary>小版本</summary>
        Int32 SmallVer { get; set; }

        /// <summary>创建人</summary>
        String CreatePersionID { get; set; }

        /// <summary>契约确认人</summary>
        String ConfirmPersonID { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateDateTime { get; set; }

        /// <summary>提交评审时间</summary>
        DateTime CommitDateTime { get; set; }

        /// <summary>评审通过时间</summary>
        DateTime ConfirmDateTime { get; set; }

        /// <summary>0：未启用，1：提交评审，2：评审通过，3：评审拒绝</summary>
        Int32 Status { get; set; }

        /// <summary>版本描述</summary>
        String Description { get; set; }

        /// <summary>审批意见</summary>
        String Opinion { get; set; }

        /// <summary>废弃人</summary>
        String ObsoletePersonID { get; set; }

        /// <summary>废弃时间</summary>
        DateTime ObsoleteDateTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}