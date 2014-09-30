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
    [BindIndex("PK_AuditBusiness", true, "OID")]
    [BindTable("AuditBusiness", Description = "", ConnName = "EsbAuditDB", DbType = DatabaseType.SqlServer)]
    public partial class AuditBusiness<TEntity> : IAuditBusiness
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

        private String _HostName;
        /// <summary></summary>
        [DisplayName("HostName")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(2, "HostName", "", "", "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(3, "ServiceName", "", "", "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(4, "MethodName", "", "", "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(5, "ReqBeginTime", "", "", "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(6, "ReqEndTime", "", "", "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(7, "Status", "", "0", "int", 10, 0, false)]
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
        [BindColumn(8, "MessageID", "", "", "nvarchar(50)", 0, 0, true)]
        public virtual String MessageID
        {
            get { return _MessageID; }
            set { if (OnPropertyChanging(__.MessageID, value)) { _MessageID = value; OnPropertyChanged(__.MessageID); } }
        }

        private String _MessageBody;
        /// <summary></summary>
        [DisplayName("MessageBody")]
        [Description("")]
        [DataObjectField(false, false, false, 1073741823)]
        [BindColumn(9, "MessageBody", "", "", "ntext", 0, 0, true)]
        public virtual String MessageBody
        {
            get { return _MessageBody; }
            set { if (OnPropertyChanging(__.MessageBody, value)) { _MessageBody = value; OnPropertyChanged(__.MessageBody); } }
        }

        private String _CallBeginTime;
        /// <summary></summary>
        [DisplayName("CallBeginTime")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(10, "CallBeginTime", "", "", "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(11, "CallEndTime", "", "", "nvarchar(50)", 0, 0, true)]
        public virtual String CallEndTime
        {
            get { return _CallEndTime; }
            set { if (OnPropertyChanging(__.CallEndTime, value)) { _CallEndTime = value; OnPropertyChanged(__.CallEndTime); } }
        }

        private String _BindingAddress;
        /// <summary></summary>
        [DisplayName("BindingAddress")]
        [Description("")]
        [DataObjectField(false, false, false, 200)]
        [BindColumn(12, "BindingAddress", "", "", "nvarchar(200)", 0, 0, true)]
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
        [BindColumn(13, "BindingTemplateID", "", "", "nvarchar(50)", 0, 0, true)]
        public virtual String BindingTemplateID
        {
            get { return _BindingTemplateID; }
            set { if (OnPropertyChanging(__.BindingTemplateID, value)) { _BindingTemplateID = value; OnPropertyChanged(__.BindingTemplateID); } }
        }

        private String _ReturnMessageBody;
        /// <summary></summary>
        [DisplayName("ReturnMessageBody")]
        [Description("")]
        [DataObjectField(false, false, true, 1073741823)]
        [BindColumn(14, "ReturnMessageBody", "", "", "ntext", 0, 0, true)]
        public virtual String ReturnMessageBody
        {
            get { return _ReturnMessageBody; }
            set { if (OnPropertyChanging(__.ReturnMessageBody, value)) { _ReturnMessageBody = value; OnPropertyChanged(__.ReturnMessageBody); } }
        }

        private String _BusinessID;
        /// <summary></summary>
        [DisplayName("BusinessID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(15, "BusinessID", "", "", "nvarchar(50)", 0, 0, true)]
        public virtual String BusinessID
        {
            get { return _BusinessID; }
            set { if (OnPropertyChanging(__.BusinessID, value)) { _BusinessID = value; OnPropertyChanged(__.BusinessID); } }
        }

        private String _BusinessName;
        /// <summary></summary>
        [DisplayName("BusinessName")]
        [Description("")]
        [DataObjectField(false, false, false, 100)]
        [BindColumn(16, "BusinessName", "", "", "nvarchar(100)", 0, 0, true)]
        public virtual String BusinessName
        {
            get { return _BusinessName; }
            set { if (OnPropertyChanging(__.BusinessName, value)) { _BusinessName = value; OnPropertyChanged(__.BusinessName); } }
        }

        private String _ServiceID;
        /// <summary></summary>
        [DisplayName("ServiceID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(17, "ServiceID", "", "", "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceID
        {
            get { return _ServiceID; }
            set { if (OnPropertyChanging(__.ServiceID, value)) { _ServiceID = value; OnPropertyChanged(__.ServiceID); } }
        }

        private String _ServiceBeginTime;
        /// <summary></summary>
        [DisplayName("ServiceBeginTime")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(18, "ServiceBeginTime", "", "", "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(19, "ServiceEndTime", "", "", "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceEndTime
        {
            get { return _ServiceEndTime; }
            set { if (OnPropertyChanging(__.ServiceEndTime, value)) { _ServiceEndTime = value; OnPropertyChanged(__.ServiceEndTime); } }
        }

        private Double _InvokeTimeSpan;
        /// <summary></summary>
        [DisplayName("InvokeTimeSpan")]
        [Description("")]
        [DataObjectField(false, false, false, 53)]
        [BindColumn(20, "InvokeTimeSpan", "", "0", "float", 53, 0, false)]
        public virtual Double InvokeTimeSpan
        {
            get { return _InvokeTimeSpan; }
            set { if (OnPropertyChanging(__.InvokeTimeSpan, value)) { _InvokeTimeSpan = value; OnPropertyChanged(__.InvokeTimeSpan); } }
        }

        private Int64 _InBytes;
        /// <summary></summary>
        [DisplayName("InBytes")]
        [Description("")]
        [DataObjectField(false, false, false, 19)]
        [BindColumn(21, "InBytes", "", "0", "bigint", 19, 0, false)]
        public virtual Int64 InBytes
        {
            get { return _InBytes; }
            set { if (OnPropertyChanging(__.InBytes, value)) { _InBytes = value; OnPropertyChanged(__.InBytes); } }
        }

        private Int64 _OutBytes;
        /// <summary></summary>
        [DisplayName("OutBytes")]
        [Description("")]
        [DataObjectField(false, false, false, 19)]
        [BindColumn(22, "OutBytes", "", "0", "bigint", 19, 0, false)]
        public virtual Int64 OutBytes
        {
            get { return _OutBytes; }
            set { if (OnPropertyChanging(__.OutBytes, value)) { _OutBytes = value; OnPropertyChanged(__.OutBytes); } }
        }

        private String _TraceID;
        /// <summary></summary>
        [DisplayName("TraceID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(23, "TraceID", "", "0", "nvarchar(50)", 0, 0, true)]
        public virtual String TraceID
        {
            get { return _TraceID; }
            set { if (OnPropertyChanging(__.TraceID, value)) { _TraceID = value; OnPropertyChanged(__.TraceID); } }
        }

        private Int32 _InvokeLevel;
        /// <summary></summary>
        [DisplayName("InvokeLevel")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(24, "InvokeLevel", "", "0", "int", 10, 0, false)]
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
        [BindColumn(25, "InvokeOrder", "", "0", "int", 10, 0, false)]
        public virtual Int32 InvokeOrder
        {
            get { return _InvokeOrder; }
            set { if (OnPropertyChanging(__.InvokeOrder, value)) { _InvokeOrder = value; OnPropertyChanged(__.InvokeOrder); } }
        }

        private String _InvokeID;
        /// <summary></summary>
        [DisplayName("InvokeID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(26, "InvokeID", "", "", "nvarchar(50)", 0, 0, true)]
        public virtual String InvokeID
        {
            get { return _InvokeID; }
            set { if (OnPropertyChanging(__.InvokeID, value)) { _InvokeID = value; OnPropertyChanged(__.InvokeID); } }
        }

        private String _ConsumerIP;
        /// <summary>消费者IP</summary>
        [DisplayName("消费者IP")]
        [Description("消费者IP")]
        [DataObjectField(false, false, false, 30)]
        [BindColumn(27, "ConsumerIP", "消费者IP", "", "nvarchar(30)", 0, 0, true)]
        public virtual String ConsumerIP
        {
            get { return _ConsumerIP; }
            set { if (OnPropertyChanging(__.ConsumerIP, value)) { _ConsumerIP = value; OnPropertyChanged(__.ConsumerIP); } }
        }

        private Int32 _Version;
        /// <summary>服务版本</summary>
        [DisplayName("服务版本")]
        [Description("服务版本")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(28, "Version", "服务版本", "0", "int", 10, 0, false)]
        public virtual Int32 Version
        {
            get { return _Version; }
            set { if (OnPropertyChanging(__.Version, value)) { _Version = value; OnPropertyChanged(__.Version); } }
        }

        private Int32 _IsCache;
        /// <summary>是否为缓存调用：0-否，1-是，用于统计缓存命中率</summary>
        [DisplayName("是否为缓存调用：0-否，1-是，用于统计缓存命中率")]
        [Description("是否为缓存调用：0-否，1-是，用于统计缓存命中率")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(29, "IsCache", "是否为缓存调用：0-否，1-是，用于统计缓存命中率", "0", "int", 10, 0, false)]
        public virtual Int32 IsCache
        {
            get { return _IsCache; }
            set { if (OnPropertyChanging(__.IsCache, value)) { _IsCache = value; OnPropertyChanged(__.IsCache); } }
        }

        private Int32 _IsQueue;
        /// <summary>是否为队列调用：0-否，1-是</summary>
        [DisplayName("是否为队列调用：0-否，1-是")]
        [Description("是否为队列调用：0-否，1-是")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(30, "IsQueue", "是否为队列调用：0-否，1-是", "0", "int", 10, 0, false)]
        public virtual Int32 IsQueue
        {
            get { return _IsQueue; }
            set { if (OnPropertyChanging(__.IsQueue, value)) { _IsQueue = value; OnPropertyChanged(__.IsQueue); } }
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
                    case __.MessageBody: return _MessageBody;
                    case __.CallBeginTime: return _CallBeginTime;
                    case __.CallEndTime: return _CallEndTime;
                    case __.BindingAddress: return _BindingAddress;
                    case __.BindingTemplateID: return _BindingTemplateID;
                    case __.ReturnMessageBody: return _ReturnMessageBody;
                    case __.BusinessID: return _BusinessID;
                    case __.BusinessName: return _BusinessName;
                    case __.ServiceID: return _ServiceID;
                    case __.ServiceBeginTime: return _ServiceBeginTime;
                    case __.ServiceEndTime: return _ServiceEndTime;
                    case __.InvokeTimeSpan: return _InvokeTimeSpan;
                    case __.InBytes: return _InBytes;
                    case __.OutBytes: return _OutBytes;
                    case __.TraceID: return _TraceID;
                    case __.InvokeLevel: return _InvokeLevel;
                    case __.InvokeOrder: return _InvokeOrder;
                    case __.InvokeID: return _InvokeID;
                    case __.ConsumerIP: return _ConsumerIP;
                    case __.Version: return _Version;
                    case __.IsCache: return _IsCache;
                    case __.IsQueue: return _IsQueue;
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
                    case __.MessageBody: _MessageBody = Convert.ToString(value); break;
                    case __.CallBeginTime: _CallBeginTime = Convert.ToString(value); break;
                    case __.CallEndTime: _CallEndTime = Convert.ToString(value); break;
                    case __.BindingAddress: _BindingAddress = Convert.ToString(value); break;
                    case __.BindingTemplateID: _BindingTemplateID = Convert.ToString(value); break;
                    case __.ReturnMessageBody: _ReturnMessageBody = Convert.ToString(value); break;
                    case __.BusinessID: _BusinessID = Convert.ToString(value); break;
                    case __.BusinessName: _BusinessName = Convert.ToString(value); break;
                    case __.ServiceID: _ServiceID = Convert.ToString(value); break;
                    case __.ServiceBeginTime: _ServiceBeginTime = Convert.ToString(value); break;
                    case __.ServiceEndTime: _ServiceEndTime = Convert.ToString(value); break;
                    case __.InvokeTimeSpan: _InvokeTimeSpan = Convert.ToDouble(value); break;
                    case __.InBytes: _InBytes = Convert.ToInt64(value); break;
                    case __.OutBytes: _OutBytes = Convert.ToInt64(value); break;
                    case __.TraceID: _TraceID = Convert.ToString(value); break;
                    case __.InvokeLevel: _InvokeLevel = Convert.ToInt32(value); break;
                    case __.InvokeOrder: _InvokeOrder = Convert.ToInt32(value); break;
                    case __.InvokeID: _InvokeID = Convert.ToString(value); break;
                    case __.ConsumerIP: _ConsumerIP = Convert.ToString(value); break;
                    case __.Version: _Version = Convert.ToInt32(value); break;
                    case __.IsCache: _IsCache = Convert.ToInt32(value); break;
                    case __.IsQueue: _IsQueue = Convert.ToInt32(value); break;
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
            public static readonly Field MessageBody = FindByName(__.MessageBody);

            ///<summary></summary>
            public static readonly Field CallBeginTime = FindByName(__.CallBeginTime);

            ///<summary></summary>
            public static readonly Field CallEndTime = FindByName(__.CallEndTime);

            ///<summary></summary>
            public static readonly Field BindingAddress = FindByName(__.BindingAddress);

            ///<summary></summary>
            public static readonly Field BindingTemplateID = FindByName(__.BindingTemplateID);

            ///<summary></summary>
            public static readonly Field ReturnMessageBody = FindByName(__.ReturnMessageBody);

            ///<summary></summary>
            public static readonly Field BusinessID = FindByName(__.BusinessID);

            ///<summary></summary>
            public static readonly Field BusinessName = FindByName(__.BusinessName);

            ///<summary></summary>
            public static readonly Field ServiceID = FindByName(__.ServiceID);

            ///<summary></summary>
            public static readonly Field ServiceBeginTime = FindByName(__.ServiceBeginTime);

            ///<summary></summary>
            public static readonly Field ServiceEndTime = FindByName(__.ServiceEndTime);

            ///<summary></summary>
            public static readonly Field InvokeTimeSpan = FindByName(__.InvokeTimeSpan);

            ///<summary></summary>
            public static readonly Field InBytes = FindByName(__.InBytes);

            ///<summary></summary>
            public static readonly Field OutBytes = FindByName(__.OutBytes);

            ///<summary></summary>
            public static readonly Field TraceID = FindByName(__.TraceID);

            ///<summary></summary>
            public static readonly Field InvokeLevel = FindByName(__.InvokeLevel);

            ///<summary></summary>
            public static readonly Field InvokeOrder = FindByName(__.InvokeOrder);

            ///<summary></summary>
            public static readonly Field InvokeID = FindByName(__.InvokeID);

            ///<summary>消费者IP</summary>
            public static readonly Field ConsumerIP = FindByName(__.ConsumerIP);

            ///<summary>服务版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            ///<summary>是否为缓存调用：0-否，1-是，用于统计缓存命中率</summary>
            public static readonly Field IsCache = FindByName(__.IsCache);

            ///<summary>是否为队列调用：0-否，1-是</summary>
            public static readonly Field IsQueue = FindByName(__.IsQueue);

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
            public const String MessageBody = "MessageBody";

            ///<summary></summary>
            public const String CallBeginTime = "CallBeginTime";

            ///<summary></summary>
            public const String CallEndTime = "CallEndTime";

            ///<summary></summary>
            public const String BindingAddress = "BindingAddress";

            ///<summary></summary>
            public const String BindingTemplateID = "BindingTemplateID";

            ///<summary></summary>
            public const String ReturnMessageBody = "ReturnMessageBody";

            ///<summary></summary>
            public const String BusinessID = "BusinessID";

            ///<summary></summary>
            public const String BusinessName = "BusinessName";

            ///<summary></summary>
            public const String ServiceID = "ServiceID";

            ///<summary></summary>
            public const String ServiceBeginTime = "ServiceBeginTime";

            ///<summary></summary>
            public const String ServiceEndTime = "ServiceEndTime";

            ///<summary></summary>
            public const String InvokeTimeSpan = "InvokeTimeSpan";

            ///<summary></summary>
            public const String InBytes = "InBytes";

            ///<summary></summary>
            public const String OutBytes = "OutBytes";

            ///<summary></summary>
            public const String TraceID = "TraceID";

            ///<summary></summary>
            public const String InvokeLevel = "InvokeLevel";

            ///<summary></summary>
            public const String InvokeOrder = "InvokeOrder";

            ///<summary></summary>
            public const String InvokeID = "InvokeID";

            ///<summary>消费者IP</summary>
            public const String ConsumerIP = "ConsumerIP";

            ///<summary>服务版本</summary>
            public const String Version = "Version";

            ///<summary>是否为缓存调用：0-否，1-是，用于统计缓存命中率</summary>
            public const String IsCache = "IsCache";

            ///<summary>是否为队列调用：0-否，1-是</summary>
            public const String IsQueue = "IsQueue";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IAuditBusiness
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
        String MessageBody { get; set; }

        /// <summary></summary>
        String CallBeginTime { get; set; }

        /// <summary></summary>
        String CallEndTime { get; set; }

        /// <summary></summary>
        String BindingAddress { get; set; }

        /// <summary></summary>
        String BindingTemplateID { get; set; }

        /// <summary></summary>
        String ReturnMessageBody { get; set; }

        /// <summary></summary>
        String BusinessID { get; set; }

        /// <summary></summary>
        String BusinessName { get; set; }

        /// <summary></summary>
        String ServiceID { get; set; }

        /// <summary></summary>
        String ServiceBeginTime { get; set; }

        /// <summary></summary>
        String ServiceEndTime { get; set; }

        /// <summary></summary>
        Double InvokeTimeSpan { get; set; }

        /// <summary></summary>
        Int64 InBytes { get; set; }

        /// <summary></summary>
        Int64 OutBytes { get; set; }

        /// <summary></summary>
        String TraceID { get; set; }

        /// <summary></summary>
        Int32 InvokeLevel { get; set; }

        /// <summary></summary>
        Int32 InvokeOrder { get; set; }

        /// <summary></summary>
        String InvokeID { get; set; }

        /// <summary>消费者IP</summary>
        String ConsumerIP { get; set; }

        /// <summary>服务版本</summary>
        Int32 Version { get; set; }

        /// <summary>是否为缓存调用：0-否，1-是，用于统计缓存命中率</summary>
        Int32 IsCache { get; set; }

        /// <summary>是否为队列调用：0-否，1-是</summary>
        Int32 IsQueue { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}