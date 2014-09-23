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
    [BindTable("EsbView_ServiceConfig", Description = "", ConnName = "EsbServiceDirectoryDB", DbType = DatabaseType.SqlServer, IsView = true)]
    public partial class EsbView_ServiceConfig<TEntity> : IEsbView_ServiceConfig
    {
        #region 属性
        private String _ServiceName;
        /// <summary></summary>
        [DisplayName("ServiceName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(1, "ServiceName", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceName
        {
            get { return _ServiceName; }
            set { if (OnPropertyChanging(__.ServiceName, value)) { _ServiceName = value; OnPropertyChanged(__.ServiceName); } }
        }

        private String _QueueCenterUri;
        /// <summary></summary>
        [DisplayName("QueueCenterUri")]
        [Description("")]
        [DataObjectField(false, false, true, 61)]
        [BindColumn(2, "QueueCenterUri", "", null, "nvarchar(61)", 0, 0, true)]
        public virtual String QueueCenterUri
        {
            get { return _QueueCenterUri; }
            set { if (OnPropertyChanging(__.QueueCenterUri, value)) { _QueueCenterUri = value; OnPropertyChanged(__.QueueCenterUri); } }
        }

        private String _BusinessID;
        /// <summary></summary>
        [DisplayName("BusinessID")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(3, "BusinessID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String BusinessID
        {
            get { return _BusinessID; }
            set { if (OnPropertyChanging(__.BusinessID, value)) { _BusinessID = value; OnPropertyChanged(__.BusinessID); } }
        }

        private String _OID;
        /// <summary></summary>
        [DisplayName("OID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(4, "OID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String OID
        {
            get { return _OID; }
            set { if (OnPropertyChanging(__.OID, value)) { _OID = value; OnPropertyChanged(__.OID); } }
        }

        private String _ServiceID;
        /// <summary></summary>
        [DisplayName("ServiceID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(5, "ServiceID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceID
        {
            get { return _ServiceID; }
            set { if (OnPropertyChanging(__.ServiceID, value)) { _ServiceID = value; OnPropertyChanged(__.ServiceID); } }
        }

        private String _MethodName;
        /// <summary></summary>
        [DisplayName("MethodName")]
        [Description("")]
        [DataObjectField(false, false, false, 100)]
        [BindColumn(6, "MethodName", "", null, "nvarchar(100)", 0, 0, true)]
        public virtual String MethodName
        {
            get { return _MethodName; }
            set { if (OnPropertyChanging(__.MethodName, value)) { _MethodName = value; OnPropertyChanged(__.MethodName); } }
        }

        private Int32 _IsAudit;
        /// <summary></summary>
        [DisplayName("IsAudit")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(7, "IsAudit", "", null, "int", 10, 0, false)]
        public virtual Int32 IsAudit
        {
            get { return _IsAudit; }
            set { if (OnPropertyChanging(__.IsAudit, value)) { _IsAudit = value; OnPropertyChanged(__.IsAudit); } }
        }

        private Int32 _Timeout;
        /// <summary></summary>
        [DisplayName("Timeout")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(8, "Timeout", "", null, "int", 10, 0, false)]
        public virtual Int32 Timeout
        {
            get { return _Timeout; }
            set { if (OnPropertyChanging(__.Timeout, value)) { _Timeout = value; OnPropertyChanged(__.Timeout); } }
        }

        private Int32 _CacheDuration;
        /// <summary></summary>
        [DisplayName("CacheDuration")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(9, "CacheDuration", "", null, "int", 10, 0, false)]
        public virtual Int32 CacheDuration
        {
            get { return _CacheDuration; }
            set { if (OnPropertyChanging(__.CacheDuration, value)) { _CacheDuration = value; OnPropertyChanged(__.CacheDuration); } }
        }

        private String _QueueCenter;
        /// <summary></summary>
        [DisplayName("QueueCenter")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(10, "QueueCenter", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String QueueCenter
        {
            get { return _QueueCenter; }
            set { if (OnPropertyChanging(__.QueueCenter, value)) { _QueueCenter = value; OnPropertyChanged(__.QueueCenter); } }
        }

        private Int32 _HBPolicy;
        /// <summary></summary>
        [DisplayName("HBPolicy")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(11, "HBPolicy", "", null, "int", 10, 0, false)]
        public virtual Int32 HBPolicy
        {
            get { return _HBPolicy; }
            set { if (OnPropertyChanging(__.HBPolicy, value)) { _HBPolicy = value; OnPropertyChanged(__.HBPolicy); } }
        }

        private String _WhiteList;
        /// <summary></summary>
        [DisplayName("WhiteList")]
        [Description("")]
        [DataObjectField(false, false, true, 300)]
        [BindColumn(12, "WhiteList", "", null, "nvarchar(300)", 0, 0, true)]
        public virtual String WhiteList
        {
            get { return _WhiteList; }
            set { if (OnPropertyChanging(__.WhiteList, value)) { _WhiteList = value; OnPropertyChanged(__.WhiteList); } }
        }

        private String _BlackList;
        /// <summary></summary>
        [DisplayName("BlackList")]
        [Description("")]
        [DataObjectField(false, false, true, 300)]
        [BindColumn(13, "BlackList", "", null, "nvarchar(300)", 0, 0, true)]
        public virtual String BlackList
        {
            get { return _BlackList; }
            set { if (OnPropertyChanging(__.BlackList, value)) { _BlackList = value; OnPropertyChanged(__.BlackList); } }
        }

        private String _MockObject;
        /// <summary></summary>
        [DisplayName("MockObject")]
        [Description("")]
        [DataObjectField(false, false, true, 300)]
        [BindColumn(14, "MockObject", "", null, "nvarchar(300)", 0, 0, true)]
        public virtual String MockObject
        {
            get { return _MockObject; }
            set { if (OnPropertyChanging(__.MockObject, value)) { _MockObject = value; OnPropertyChanged(__.MockObject); } }
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
                    case __.ServiceName: return _ServiceName;
                    case __.QueueCenterUri: return _QueueCenterUri;
                    case __.BusinessID: return _BusinessID;
                    case __.OID: return _OID;
                    case __.ServiceID: return _ServiceID;
                    case __.MethodName: return _MethodName;
                    case __.IsAudit: return _IsAudit;
                    case __.Timeout: return _Timeout;
                    case __.CacheDuration: return _CacheDuration;
                    case __.QueueCenter: return _QueueCenter;
                    case __.HBPolicy: return _HBPolicy;
                    case __.WhiteList: return _WhiteList;
                    case __.BlackList: return _BlackList;
                    case __.MockObject: return _MockObject;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ServiceName: _ServiceName = Convert.ToString(value); break;
                    case __.QueueCenterUri: _QueueCenterUri = Convert.ToString(value); break;
                    case __.BusinessID: _BusinessID = Convert.ToString(value); break;
                    case __.OID: _OID = Convert.ToString(value); break;
                    case __.ServiceID: _ServiceID = Convert.ToString(value); break;
                    case __.MethodName: _MethodName = Convert.ToString(value); break;
                    case __.IsAudit: _IsAudit = Convert.ToInt32(value); break;
                    case __.Timeout: _Timeout = Convert.ToInt32(value); break;
                    case __.CacheDuration: _CacheDuration = Convert.ToInt32(value); break;
                    case __.QueueCenter: _QueueCenter = Convert.ToString(value); break;
                    case __.HBPolicy: _HBPolicy = Convert.ToInt32(value); break;
                    case __.WhiteList: _WhiteList = Convert.ToString(value); break;
                    case __.BlackList: _BlackList = Convert.ToString(value); break;
                    case __.MockObject: _MockObject = Convert.ToString(value); break;
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
            public static readonly Field ServiceName = FindByName(__.ServiceName);

            ///<summary></summary>
            public static readonly Field QueueCenterUri = FindByName(__.QueueCenterUri);

            ///<summary></summary>
            public static readonly Field BusinessID = FindByName(__.BusinessID);

            ///<summary></summary>
            public static readonly Field OID = FindByName(__.OID);

            ///<summary></summary>
            public static readonly Field ServiceID = FindByName(__.ServiceID);

            ///<summary></summary>
            public static readonly Field MethodName = FindByName(__.MethodName);

            ///<summary></summary>
            public static readonly Field IsAudit = FindByName(__.IsAudit);

            ///<summary></summary>
            public static readonly Field Timeout = FindByName(__.Timeout);

            ///<summary></summary>
            public static readonly Field CacheDuration = FindByName(__.CacheDuration);

            ///<summary></summary>
            public static readonly Field QueueCenter = FindByName(__.QueueCenter);

            ///<summary></summary>
            public static readonly Field HBPolicy = FindByName(__.HBPolicy);

            ///<summary></summary>
            public static readonly Field WhiteList = FindByName(__.WhiteList);

            ///<summary></summary>
            public static readonly Field BlackList = FindByName(__.BlackList);

            ///<summary></summary>
            public static readonly Field MockObject = FindByName(__.MockObject);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String ServiceName = "ServiceName";

            ///<summary></summary>
            public const String QueueCenterUri = "QueueCenterUri";

            ///<summary></summary>
            public const String BusinessID = "BusinessID";

            ///<summary></summary>
            public const String OID = "OID";

            ///<summary></summary>
            public const String ServiceID = "ServiceID";

            ///<summary></summary>
            public const String MethodName = "MethodName";

            ///<summary></summary>
            public const String IsAudit = "IsAudit";

            ///<summary></summary>
            public const String Timeout = "Timeout";

            ///<summary></summary>
            public const String CacheDuration = "CacheDuration";

            ///<summary></summary>
            public const String QueueCenter = "QueueCenter";

            ///<summary></summary>
            public const String HBPolicy = "HBPolicy";

            ///<summary></summary>
            public const String WhiteList = "WhiteList";

            ///<summary></summary>
            public const String BlackList = "BlackList";

            ///<summary></summary>
            public const String MockObject = "MockObject";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IEsbView_ServiceConfig
    {
        #region 属性
        /// <summary></summary>
        String ServiceName { get; set; }

        /// <summary></summary>
        String QueueCenterUri { get; set; }

        /// <summary></summary>
        String BusinessID { get; set; }

        /// <summary></summary>
        String OID { get; set; }

        /// <summary></summary>
        String ServiceID { get; set; }

        /// <summary></summary>
        String MethodName { get; set; }

        /// <summary></summary>
        Int32 IsAudit { get; set; }

        /// <summary></summary>
        Int32 Timeout { get; set; }

        /// <summary></summary>
        Int32 CacheDuration { get; set; }

        /// <summary></summary>
        String QueueCenter { get; set; }

        /// <summary></summary>
        Int32 HBPolicy { get; set; }

        /// <summary></summary>
        String WhiteList { get; set; }

        /// <summary></summary>
        String BlackList { get; set; }

        /// <summary></summary>
        String MockObject { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}