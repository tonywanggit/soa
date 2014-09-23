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
    [BindTable("AuditBusinessView", Description = "", ConnName = "EsbAuditDB", DbType = DatabaseType.SqlServer, IsView = true)]
    public partial class AuditBusinessView<TEntity> : IAuditBusinessView
    {
        #region 属性
        private String _OID;
        /// <summary></summary>
        [DisplayName("OID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(1, "OID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String OID
        {
            get { return _OID; }
            set { if (OnPropertyChanging(__.OID, value)) { _OID = value; OnPropertyChanged(__.OID); } }
        }

        private String _HostName;
        /// <summary></summary>
        [DisplayName("HostName")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(2, "HostName", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String HostName
        {
            get { return _HostName; }
            set { if (OnPropertyChanging(__.HostName, value)) { _HostName = value; OnPropertyChanged(__.HostName); } }
        }

        private String _ServiceName;
        /// <summary></summary>
        [DisplayName("ServiceName")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(3, "ServiceName", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceName
        {
            get { return _ServiceName; }
            set { if (OnPropertyChanging(__.ServiceName, value)) { _ServiceName = value; OnPropertyChanged(__.ServiceName); } }
        }

        private String _MethodName;
        /// <summary></summary>
        [DisplayName("MethodName")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(4, "MethodName", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String MethodName
        {
            get { return _MethodName; }
            set { if (OnPropertyChanging(__.MethodName, value)) { _MethodName = value; OnPropertyChanged(__.MethodName); } }
        }

        private String _ReqBeginTime;
        /// <summary></summary>
        [DisplayName("ReqBeginTime")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(5, "ReqBeginTime", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ReqBeginTime
        {
            get { return _ReqBeginTime; }
            set { if (OnPropertyChanging(__.ReqBeginTime, value)) { _ReqBeginTime = value; OnPropertyChanged(__.ReqBeginTime); } }
        }

        private String _ReqEndTime;
        /// <summary></summary>
        [DisplayName("ReqEndTime")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(6, "ReqEndTime", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ReqEndTime
        {
            get { return _ReqEndTime; }
            set { if (OnPropertyChanging(__.ReqEndTime, value)) { _ReqEndTime = value; OnPropertyChanged(__.ReqEndTime); } }
        }

        private Int32 _Status;
        /// <summary></summary>
        [DisplayName("Status")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(7, "Status", "", null, "int", 10, 0, false)]
        public virtual Int32 Status
        {
            get { return _Status; }
            set { if (OnPropertyChanging(__.Status, value)) { _Status = value; OnPropertyChanged(__.Status); } }
        }

        private String _MessageID;
        /// <summary></summary>
        [DisplayName("MessageID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(8, "MessageID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String MessageID
        {
            get { return _MessageID; }
            set { if (OnPropertyChanging(__.MessageID, value)) { _MessageID = value; OnPropertyChanged(__.MessageID); } }
        }

        private String _CallBeginTime;
        /// <summary></summary>
        [DisplayName("CallBeginTime")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(9, "CallBeginTime", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String CallBeginTime
        {
            get { return _CallBeginTime; }
            set { if (OnPropertyChanging(__.CallBeginTime, value)) { _CallBeginTime = value; OnPropertyChanged(__.CallBeginTime); } }
        }

        private String _CallEndTime;
        /// <summary></summary>
        [DisplayName("CallEndTime")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(10, "CallEndTime", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String CallEndTime
        {
            get { return _CallEndTime; }
            set { if (OnPropertyChanging(__.CallEndTime, value)) { _CallEndTime = value; OnPropertyChanged(__.CallEndTime); } }
        }

        private String _ServiceBeginTime;
        /// <summary></summary>
        [DisplayName("ServiceBeginTime")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(11, "ServiceBeginTime", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceBeginTime
        {
            get { return _ServiceBeginTime; }
            set { if (OnPropertyChanging(__.ServiceBeginTime, value)) { _ServiceBeginTime = value; OnPropertyChanged(__.ServiceBeginTime); } }
        }

        private String _ServiceEndTime;
        /// <summary></summary>
        [DisplayName("ServiceEndTime")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(12, "ServiceEndTime", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceEndTime
        {
            get { return _ServiceEndTime; }
            set { if (OnPropertyChanging(__.ServiceEndTime, value)) { _ServiceEndTime = value; OnPropertyChanged(__.ServiceEndTime); } }
        }

        private String _BindingAddress;
        /// <summary></summary>
        [DisplayName("BindingAddress")]
        [Description("")]
        [DataObjectField(false, false, false, 200)]
        [BindColumn(13, "BindingAddress", "", null, "nvarchar(200)", 0, 0, true)]
        public virtual String BindingAddress
        {
            get { return _BindingAddress; }
            set { if (OnPropertyChanging(__.BindingAddress, value)) { _BindingAddress = value; OnPropertyChanged(__.BindingAddress); } }
        }

        private String _BindingTemplateID;
        /// <summary></summary>
        [DisplayName("BindingTemplateID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(14, "BindingTemplateID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String BindingTemplateID
        {
            get { return _BindingTemplateID; }
            set { if (OnPropertyChanging(__.BindingTemplateID, value)) { _BindingTemplateID = value; OnPropertyChanged(__.BindingTemplateID); } }
        }

        private String _TraceID;
        /// <summary></summary>
        [DisplayName("TraceID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(15, "TraceID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String TraceID
        {
            get { return _TraceID; }
            set { if (OnPropertyChanging(__.TraceID, value)) { _TraceID = value; OnPropertyChanged(__.TraceID); } }
        }

        private String _InvokeID;
        /// <summary></summary>
        [DisplayName("InvokeID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(16, "InvokeID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String InvokeID
        {
            get { return _InvokeID; }
            set { if (OnPropertyChanging(__.InvokeID, value)) { _InvokeID = value; OnPropertyChanged(__.InvokeID); } }
        }

        private Int32 _InvokeLevel;
        /// <summary></summary>
        [DisplayName("InvokeLevel")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(17, "InvokeLevel", "", null, "int", 10, 0, false)]
        public virtual Int32 InvokeLevel
        {
            get { return _InvokeLevel; }
            set { if (OnPropertyChanging(__.InvokeLevel, value)) { _InvokeLevel = value; OnPropertyChanged(__.InvokeLevel); } }
        }

        private Int32 _InvokeOrder;
        /// <summary></summary>
        [DisplayName("InvokeOrder")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(18, "InvokeOrder", "", null, "int", 10, 0, false)]
        public virtual Int32 InvokeOrder
        {
            get { return _InvokeOrder; }
            set { if (OnPropertyChanging(__.InvokeOrder, value)) { _InvokeOrder = value; OnPropertyChanged(__.InvokeOrder); } }
        }

        private String _ConsumerIP;
        /// <summary></summary>
        [DisplayName("ConsumerIP")]
        [Description("")]
        [DataObjectField(false, false, false, 30)]
        [BindColumn(19, "ConsumerIP", "", null, "nvarchar(30)", 0, 0, true)]
        public virtual String ConsumerIP
        {
            get { return _ConsumerIP; }
            set { if (OnPropertyChanging(__.ConsumerIP, value)) { _ConsumerIP = value; OnPropertyChanged(__.ConsumerIP); } }
        }

        private Int32 _Version;
        /// <summary></summary>
        [DisplayName("Version")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(20, "Version", "", null, "int", 10, 0, false)]
        public virtual Int32 Version
        {
            get { return _Version; }
            set { if (OnPropertyChanging(__.Version, value)) { _Version = value; OnPropertyChanged(__.Version); } }
        }

        private Int32 _IsCache;
        /// <summary></summary>
        [DisplayName("IsCache")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(21, "IsCache", "", null, "int", 10, 0, false)]
        public virtual Int32 IsCache
        {
            get { return _IsCache; }
            set { if (OnPropertyChanging(__.IsCache, value)) { _IsCache = value; OnPropertyChanged(__.IsCache); } }
        }

        private String _BusinessID;
        /// <summary></summary>
        [DisplayName("BusinessID")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(22, "BusinessID", "", null, "varchar(50)", 0, 0, false)]
        public virtual String BusinessID
        {
            get { return _BusinessID; }
            set { if (OnPropertyChanging(__.BusinessID, value)) { _BusinessID = value; OnPropertyChanged(__.BusinessID); } }
        }

        private String _BusinessName;
        /// <summary></summary>
        [DisplayName("BusinessName")]
        [Description("")]
        [DataObjectField(false, false, true, 252)]
        [BindColumn(23, "BusinessName", "", null, "nvarchar(252)", 0, 0, true)]
        public virtual String BusinessName
        {
            get { return _BusinessName; }
            set { if (OnPropertyChanging(__.BusinessName, value)) { _BusinessName = value; OnPropertyChanged(__.BusinessName); } }
        }

        private String _ServiceID;
        /// <summary></summary>
        [DisplayName("ServiceID")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(24, "ServiceID", "", null, "varchar(50)", 0, 0, false)]
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
                    case __.OID: return _OID;
                    case __.HostName: return _HostName;
                    case __.ServiceName: return _ServiceName;
                    case __.MethodName: return _MethodName;
                    case __.ReqBeginTime: return _ReqBeginTime;
                    case __.ReqEndTime: return _ReqEndTime;
                    case __.Status: return _Status;
                    case __.MessageID: return _MessageID;
                    case __.CallBeginTime: return _CallBeginTime;
                    case __.CallEndTime: return _CallEndTime;
                    case __.ServiceBeginTime: return _ServiceBeginTime;
                    case __.ServiceEndTime: return _ServiceEndTime;
                    case __.BindingAddress: return _BindingAddress;
                    case __.BindingTemplateID: return _BindingTemplateID;
                    case __.TraceID: return _TraceID;
                    case __.InvokeID: return _InvokeID;
                    case __.InvokeLevel: return _InvokeLevel;
                    case __.InvokeOrder: return _InvokeOrder;
                    case __.ConsumerIP: return _ConsumerIP;
                    case __.Version: return _Version;
                    case __.IsCache: return _IsCache;
                    case __.BusinessID: return _BusinessID;
                    case __.BusinessName: return _BusinessName;
                    case __.ServiceID: return _ServiceID;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.OID: _OID = Convert.ToString(value); break;
                    case __.HostName: _HostName = Convert.ToString(value); break;
                    case __.ServiceName: _ServiceName = Convert.ToString(value); break;
                    case __.MethodName: _MethodName = Convert.ToString(value); break;
                    case __.ReqBeginTime: _ReqBeginTime = Convert.ToString(value); break;
                    case __.ReqEndTime: _ReqEndTime = Convert.ToString(value); break;
                    case __.Status: _Status = Convert.ToInt32(value); break;
                    case __.MessageID: _MessageID = Convert.ToString(value); break;
                    case __.CallBeginTime: _CallBeginTime = Convert.ToString(value); break;
                    case __.CallEndTime: _CallEndTime = Convert.ToString(value); break;
                    case __.ServiceBeginTime: _ServiceBeginTime = Convert.ToString(value); break;
                    case __.ServiceEndTime: _ServiceEndTime = Convert.ToString(value); break;
                    case __.BindingAddress: _BindingAddress = Convert.ToString(value); break;
                    case __.BindingTemplateID: _BindingTemplateID = Convert.ToString(value); break;
                    case __.TraceID: _TraceID = Convert.ToString(value); break;
                    case __.InvokeID: _InvokeID = Convert.ToString(value); break;
                    case __.InvokeLevel: _InvokeLevel = Convert.ToInt32(value); break;
                    case __.InvokeOrder: _InvokeOrder = Convert.ToInt32(value); break;
                    case __.ConsumerIP: _ConsumerIP = Convert.ToString(value); break;
                    case __.Version: _Version = Convert.ToInt32(value); break;
                    case __.IsCache: _IsCache = Convert.ToInt32(value); break;
                    case __.BusinessID: _BusinessID = Convert.ToString(value); break;
                    case __.BusinessName: _BusinessName = Convert.ToString(value); break;
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
            public static readonly Field OID = FindByName(__.OID);

            ///<summary></summary>
            public static readonly Field HostName = FindByName(__.HostName);

            ///<summary></summary>
            public static readonly Field ServiceName = FindByName(__.ServiceName);

            ///<summary></summary>
            public static readonly Field MethodName = FindByName(__.MethodName);

            ///<summary></summary>
            public static readonly Field ReqBeginTime = FindByName(__.ReqBeginTime);

            ///<summary></summary>
            public static readonly Field ReqEndTime = FindByName(__.ReqEndTime);

            ///<summary></summary>
            public static readonly Field Status = FindByName(__.Status);

            ///<summary></summary>
            public static readonly Field MessageID = FindByName(__.MessageID);

            ///<summary></summary>
            public static readonly Field CallBeginTime = FindByName(__.CallBeginTime);

            ///<summary></summary>
            public static readonly Field CallEndTime = FindByName(__.CallEndTime);

            ///<summary></summary>
            public static readonly Field ServiceBeginTime = FindByName(__.ServiceBeginTime);

            ///<summary></summary>
            public static readonly Field ServiceEndTime = FindByName(__.ServiceEndTime);

            ///<summary></summary>
            public static readonly Field BindingAddress = FindByName(__.BindingAddress);

            ///<summary></summary>
            public static readonly Field BindingTemplateID = FindByName(__.BindingTemplateID);

            ///<summary></summary>
            public static readonly Field TraceID = FindByName(__.TraceID);

            ///<summary></summary>
            public static readonly Field InvokeID = FindByName(__.InvokeID);

            ///<summary></summary>
            public static readonly Field InvokeLevel = FindByName(__.InvokeLevel);

            ///<summary></summary>
            public static readonly Field InvokeOrder = FindByName(__.InvokeOrder);

            ///<summary></summary>
            public static readonly Field ConsumerIP = FindByName(__.ConsumerIP);

            ///<summary></summary>
            public static readonly Field Version = FindByName(__.Version);

            ///<summary></summary>
            public static readonly Field IsCache = FindByName(__.IsCache);

            ///<summary></summary>
            public static readonly Field BusinessID = FindByName(__.BusinessID);

            ///<summary></summary>
            public static readonly Field BusinessName = FindByName(__.BusinessName);

            ///<summary></summary>
            public static readonly Field ServiceID = FindByName(__.ServiceID);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String OID = "OID";

            ///<summary></summary>
            public const String HostName = "HostName";

            ///<summary></summary>
            public const String ServiceName = "ServiceName";

            ///<summary></summary>
            public const String MethodName = "MethodName";

            ///<summary></summary>
            public const String ReqBeginTime = "ReqBeginTime";

            ///<summary></summary>
            public const String ReqEndTime = "ReqEndTime";

            ///<summary></summary>
            public const String Status = "Status";

            ///<summary></summary>
            public const String MessageID = "MessageID";

            ///<summary></summary>
            public const String CallBeginTime = "CallBeginTime";

            ///<summary></summary>
            public const String CallEndTime = "CallEndTime";

            ///<summary></summary>
            public const String ServiceBeginTime = "ServiceBeginTime";

            ///<summary></summary>
            public const String ServiceEndTime = "ServiceEndTime";

            ///<summary></summary>
            public const String BindingAddress = "BindingAddress";

            ///<summary></summary>
            public const String BindingTemplateID = "BindingTemplateID";

            ///<summary></summary>
            public const String TraceID = "TraceID";

            ///<summary></summary>
            public const String InvokeID = "InvokeID";

            ///<summary></summary>
            public const String InvokeLevel = "InvokeLevel";

            ///<summary></summary>
            public const String InvokeOrder = "InvokeOrder";

            ///<summary></summary>
            public const String ConsumerIP = "ConsumerIP";

            ///<summary></summary>
            public const String Version = "Version";

            ///<summary></summary>
            public const String IsCache = "IsCache";

            ///<summary></summary>
            public const String BusinessID = "BusinessID";

            ///<summary></summary>
            public const String BusinessName = "BusinessName";

            ///<summary></summary>
            public const String ServiceID = "ServiceID";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IAuditBusinessView
    {
        #region 属性
        /// <summary></summary>
        String OID { get; set; }

        /// <summary></summary>
        String HostName { get; set; }

        /// <summary></summary>
        String ServiceName { get; set; }

        /// <summary></summary>
        String MethodName { get; set; }

        /// <summary></summary>
        String ReqBeginTime { get; set; }

        /// <summary></summary>
        String ReqEndTime { get; set; }

        /// <summary></summary>
        Int32 Status { get; set; }

        /// <summary></summary>
        String MessageID { get; set; }

        /// <summary></summary>
        String CallBeginTime { get; set; }

        /// <summary></summary>
        String CallEndTime { get; set; }

        /// <summary></summary>
        String ServiceBeginTime { get; set; }

        /// <summary></summary>
        String ServiceEndTime { get; set; }

        /// <summary></summary>
        String BindingAddress { get; set; }

        /// <summary></summary>
        String BindingTemplateID { get; set; }

        /// <summary></summary>
        String TraceID { get; set; }

        /// <summary></summary>
        String InvokeID { get; set; }

        /// <summary></summary>
        Int32 InvokeLevel { get; set; }

        /// <summary></summary>
        Int32 InvokeOrder { get; set; }

        /// <summary></summary>
        String ConsumerIP { get; set; }

        /// <summary></summary>
        Int32 Version { get; set; }

        /// <summary></summary>
        Int32 IsCache { get; set; }

        /// <summary></summary>
        String BusinessID { get; set; }

        /// <summary></summary>
        String BusinessName { get; set; }

        /// <summary></summary>
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