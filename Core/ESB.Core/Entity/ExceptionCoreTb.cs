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
    [BindIndex("PK_ExceptionCoreTb", true, "ExceptionID")]
    [BindTable("ExceptionCoreTb", Description = "", ConnName = "EsbExceptionDB", DbType = DatabaseType.SqlServer)]
    public partial class ExceptionCoreTb<TEntity> : IExceptionCoreTb
    {
        #region 属性
        private String _ExceptionID;
        /// <summary></summary>
        [DisplayName("ExceptionID")]
        [Description("")]
        [DataObjectField(true, false, false, 50)]
        [BindColumn(1, "ExceptionID", "", "newid()", "nvarchar(50)", 0, 0, true)]
        public virtual String ExceptionID
        {
            get { return _ExceptionID; }
            set { if (OnPropertyChanging(__.ExceptionID, value)) { _ExceptionID = value; OnPropertyChanged(__.ExceptionID); } }
        }

        private DateTime _ExceptionTime;
        /// <summary></summary>
        [DisplayName("ExceptionTime")]
        [Description("")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn(2, "ExceptionTime", "", "getdate()", "datetime", 3, 0, false)]
        public virtual DateTime ExceptionTime
        {
            get { return _ExceptionTime; }
            set { if (OnPropertyChanging(__.ExceptionTime, value)) { _ExceptionTime = value; OnPropertyChanged(__.ExceptionTime); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, true, -1)]
        [BindColumn(3, "Description", "", null, "nvarchar(MAX)", 0, 0, true)]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private String _ExceptionCode;
        /// <summary></summary>
        [DisplayName("ExceptionCode")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(4, "ExceptionCode", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ExceptionCode
        {
            get { return _ExceptionCode; }
            set { if (OnPropertyChanging(__.ExceptionCode, value)) { _ExceptionCode = value; OnPropertyChanged(__.ExceptionCode); } }
        }

        private String _ExceptionInfo;
        /// <summary></summary>
        [DisplayName("ExceptionInfo")]
        [Description("")]
        [DataObjectField(false, false, true, 1073741823)]
        [BindColumn(5, "ExceptionInfo", "", null, "ntext", 0, 0, true)]
        public virtual String ExceptionInfo
        {
            get { return _ExceptionInfo; }
            set { if (OnPropertyChanging(__.ExceptionInfo, value)) { _ExceptionInfo = value; OnPropertyChanged(__.ExceptionInfo); } }
        }

        private Int32 _ExceptionLevel;
        /// <summary>消息,错误,重要,紧急</summary>
        [DisplayName("消息,错误,重要,紧急")]
        [Description("消息,错误,重要,紧急")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(6, "ExceptionLevel", "消息,错误,重要,紧急", "0", "int", 10, 0, false)]
        public virtual Int32 ExceptionLevel
        {
            get { return _ExceptionLevel; }
            set { if (OnPropertyChanging(__.ExceptionLevel, value)) { _ExceptionLevel = value; OnPropertyChanged(__.ExceptionLevel); } }
        }

        private Int32 _ExceptionType;
        /// <summary></summary>
        [DisplayName("ExceptionType")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(7, "ExceptionType", "", "0", "int", 10, 0, false)]
        public virtual Int32 ExceptionType
        {
            get { return _ExceptionType; }
            set { if (OnPropertyChanging(__.ExceptionType, value)) { _ExceptionType = value; OnPropertyChanged(__.ExceptionType); } }
        }

        private String _MethodName;
        /// <summary></summary>
        [DisplayName("MethodName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(8, "MethodName", "", null, "nchar(50)", 0, 0, true)]
        public virtual String MethodName
        {
            get { return _MethodName; }
            set { if (OnPropertyChanging(__.MethodName, value)) { _MethodName = value; OnPropertyChanged(__.MethodName); } }
        }

        private String _HostName;
        /// <summary></summary>
        [DisplayName("HostName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(9, "HostName", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String HostName
        {
            get { return _HostName; }
            set { if (OnPropertyChanging(__.HostName, value)) { _HostName = value; OnPropertyChanged(__.HostName); } }
        }

        private String _MessageID;
        /// <summary>关联到Audit.OID</summary>
        [DisplayName("关联到Audit")]
        [Description("关联到Audit.OID")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(10, "MessageID", "关联到Audit.OID", null, "nvarchar(50)", 0, 0, true)]
        public virtual String MessageID
        {
            get { return _MessageID; }
            set { if (OnPropertyChanging(__.MessageID, value)) { _MessageID = value; OnPropertyChanged(__.MessageID); } }
        }

        private String _BindingTemplateID;
        /// <summary></summary>
        [DisplayName("BindingTemplateID")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(11, "BindingTemplateID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String BindingTemplateID
        {
            get { return _BindingTemplateID; }
            set { if (OnPropertyChanging(__.BindingTemplateID, value)) { _BindingTemplateID = value; OnPropertyChanged(__.BindingTemplateID); } }
        }

        private Int32 _ExceptionStatus;
        /// <summary></summary>
        [DisplayName("ExceptionStatus")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(12, "ExceptionStatus", "", "0", "int", 10, 0, false)]
        public virtual Int32 ExceptionStatus
        {
            get { return _ExceptionStatus; }
            set { if (OnPropertyChanging(__.ExceptionStatus, value)) { _ExceptionStatus = value; OnPropertyChanged(__.ExceptionStatus); } }
        }

        private String _MessageBody;
        /// <summary></summary>
        [DisplayName("MessageBody")]
        [Description("")]
        [DataObjectField(false, false, true, 1073741823)]
        [BindColumn(13, "MessageBody", "", null, "ntext", 0, 0, true)]
        public virtual String MessageBody
        {
            get { return _MessageBody; }
            set { if (OnPropertyChanging(__.MessageBody, value)) { _MessageBody = value; OnPropertyChanged(__.MessageBody); } }
        }

        private Int32 _BindingType;
        /// <summary></summary>
        [DisplayName("BindingType")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(14, "BindingType", "", null, "int", 10, 0, false)]
        public virtual Int32 BindingType
        {
            get { return _BindingType; }
            set { if (OnPropertyChanging(__.BindingType, value)) { _BindingType = value; OnPropertyChanged(__.BindingType); } }
        }

        private String _RequestPwd;
        /// <summary></summary>
        [DisplayName("RequestPwd")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(15, "RequestPwd", "", null, "nchar(50)", 0, 0, true)]
        public virtual String RequestPwd
        {
            get { return _RequestPwd; }
            set { if (OnPropertyChanging(__.RequestPwd, value)) { _RequestPwd = value; OnPropertyChanged(__.RequestPwd); } }
        }

        private Int32 _RequestType;
        /// <summary></summary>
        [DisplayName("RequestType")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(16, "RequestType", "", null, "int", 10, 0, false)]
        public virtual Int32 RequestType
        {
            get { return _RequestType; }
            set { if (OnPropertyChanging(__.RequestType, value)) { _RequestType = value; OnPropertyChanged(__.RequestType); } }
        }

        private String _BusinessID;
        /// <summary>业务实体ID</summary>
        [DisplayName("业务实体ID")]
        [Description("业务实体ID")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(17, "BusinessID", "业务实体ID", null, "nvarchar(50)", 0, 0, true)]
        public virtual String BusinessID
        {
            get { return _BusinessID; }
            set { if (OnPropertyChanging(__.BusinessID, value)) { _BusinessID = value; OnPropertyChanged(__.BusinessID); } }
        }

        private String _ServiceID;
        /// <summary>服务ID</summary>
        [DisplayName("服务ID")]
        [Description("服务ID")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(18, "ServiceID", "服务ID", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceID
        {
            get { return _ServiceID; }
            set { if (OnPropertyChanging(__.ServiceID, value)) { _ServiceID = value; OnPropertyChanged(__.ServiceID); } }
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
                    case __.ExceptionID: return _ExceptionID;
                    case __.ExceptionTime: return _ExceptionTime;
                    case __.Description: return _Description;
                    case __.ExceptionCode: return _ExceptionCode;
                    case __.ExceptionInfo: return _ExceptionInfo;
                    case __.ExceptionLevel: return _ExceptionLevel;
                    case __.ExceptionType: return _ExceptionType;
                    case __.MethodName: return _MethodName;
                    case __.HostName: return _HostName;
                    case __.MessageID: return _MessageID;
                    case __.BindingTemplateID: return _BindingTemplateID;
                    case __.ExceptionStatus: return _ExceptionStatus;
                    case __.MessageBody: return _MessageBody;
                    case __.BindingType: return _BindingType;
                    case __.RequestPwd: return _RequestPwd;
                    case __.RequestType: return _RequestType;
                    case __.BusinessID: return _BusinessID;
                    case __.ServiceID: return _ServiceID;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ExceptionID: _ExceptionID = Convert.ToString(value); break;
                    case __.ExceptionTime: _ExceptionTime = Convert.ToDateTime(value); break;
                    case __.Description: _Description = Convert.ToString(value); break;
                    case __.ExceptionCode: _ExceptionCode = Convert.ToString(value); break;
                    case __.ExceptionInfo: _ExceptionInfo = Convert.ToString(value); break;
                    case __.ExceptionLevel: _ExceptionLevel = Convert.ToInt32(value); break;
                    case __.ExceptionType: _ExceptionType = Convert.ToInt32(value); break;
                    case __.MethodName: _MethodName = Convert.ToString(value); break;
                    case __.HostName: _HostName = Convert.ToString(value); break;
                    case __.MessageID: _MessageID = Convert.ToString(value); break;
                    case __.BindingTemplateID: _BindingTemplateID = Convert.ToString(value); break;
                    case __.ExceptionStatus: _ExceptionStatus = Convert.ToInt32(value); break;
                    case __.MessageBody: _MessageBody = Convert.ToString(value); break;
                    case __.BindingType: _BindingType = Convert.ToInt32(value); break;
                    case __.RequestPwd: _RequestPwd = Convert.ToString(value); break;
                    case __.RequestType: _RequestType = Convert.ToInt32(value); break;
                    case __.BusinessID: _BusinessID = Convert.ToString(value); break;
                    case __.ServiceID: _ServiceID = Convert.ToString(value); break;
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
            public static readonly Field ExceptionID = FindByName(__.ExceptionID);

            ///<summary></summary>
            public static readonly Field ExceptionTime = FindByName(__.ExceptionTime);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field ExceptionCode = FindByName(__.ExceptionCode);

            ///<summary></summary>
            public static readonly Field ExceptionInfo = FindByName(__.ExceptionInfo);

            ///<summary>消息,错误,重要,紧急</summary>
            public static readonly Field ExceptionLevel = FindByName(__.ExceptionLevel);

            ///<summary></summary>
            public static readonly Field ExceptionType = FindByName(__.ExceptionType);

            ///<summary></summary>
            public static readonly Field MethodName = FindByName(__.MethodName);

            ///<summary></summary>
            public static readonly Field HostName = FindByName(__.HostName);

            ///<summary>关联到Audit.OID</summary>
            public static readonly Field MessageID = FindByName(__.MessageID);

            ///<summary></summary>
            public static readonly Field BindingTemplateID = FindByName(__.BindingTemplateID);

            ///<summary></summary>
            public static readonly Field ExceptionStatus = FindByName(__.ExceptionStatus);

            ///<summary></summary>
            public static readonly Field MessageBody = FindByName(__.MessageBody);

            ///<summary></summary>
            public static readonly Field BindingType = FindByName(__.BindingType);

            ///<summary></summary>
            public static readonly Field RequestPwd = FindByName(__.RequestPwd);

            ///<summary></summary>
            public static readonly Field RequestType = FindByName(__.RequestType);

            ///<summary>业务实体ID</summary>
            public static readonly Field BusinessID = FindByName(__.BusinessID);

            ///<summary>服务ID</summary>
            public static readonly Field ServiceID = FindByName(__.ServiceID);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String ExceptionID = "ExceptionID";

            ///<summary></summary>
            public const String ExceptionTime = "ExceptionTime";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String ExceptionCode = "ExceptionCode";

            ///<summary></summary>
            public const String ExceptionInfo = "ExceptionInfo";

            ///<summary>消息,错误,重要,紧急</summary>
            public const String ExceptionLevel = "ExceptionLevel";

            ///<summary></summary>
            public const String ExceptionType = "ExceptionType";

            ///<summary></summary>
            public const String MethodName = "MethodName";

            ///<summary></summary>
            public const String HostName = "HostName";

            ///<summary>关联到Audit.OID</summary>
            public const String MessageID = "MessageID";

            ///<summary></summary>
            public const String BindingTemplateID = "BindingTemplateID";

            ///<summary></summary>
            public const String ExceptionStatus = "ExceptionStatus";

            ///<summary></summary>
            public const String MessageBody = "MessageBody";

            ///<summary></summary>
            public const String BindingType = "BindingType";

            ///<summary></summary>
            public const String RequestPwd = "RequestPwd";

            ///<summary></summary>
            public const String RequestType = "RequestType";

            ///<summary>业务实体ID</summary>
            public const String BusinessID = "BusinessID";

            ///<summary>服务ID</summary>
            public const String ServiceID = "ServiceID";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IExceptionCoreTb
    {
        #region 属性
        /// <summary></summary>
        String ExceptionID { get; set; }

        /// <summary></summary>
        DateTime ExceptionTime { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        String ExceptionCode { get; set; }

        /// <summary></summary>
        String ExceptionInfo { get; set; }

        /// <summary>消息,错误,重要,紧急</summary>
        Int32 ExceptionLevel { get; set; }

        /// <summary></summary>
        Int32 ExceptionType { get; set; }

        /// <summary></summary>
        String MethodName { get; set; }

        /// <summary></summary>
        String HostName { get; set; }

        /// <summary>关联到Audit.OID</summary>
        String MessageID { get; set; }

        /// <summary></summary>
        String BindingTemplateID { get; set; }

        /// <summary></summary>
        Int32 ExceptionStatus { get; set; }

        /// <summary></summary>
        String MessageBody { get; set; }

        /// <summary></summary>
        Int32 BindingType { get; set; }

        /// <summary></summary>
        String RequestPwd { get; set; }

        /// <summary></summary>
        Int32 RequestType { get; set; }

        /// <summary>业务实体ID</summary>
        String BusinessID { get; set; }

        /// <summary>服务ID</summary>
        String ServiceID { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}