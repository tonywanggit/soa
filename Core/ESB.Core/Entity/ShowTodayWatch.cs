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
    [BindTable("ShowTodayWatch", Description = "", ConnName = "EsbAuditDB", DbType = DatabaseType.SqlServer, IsView = true)]
    public partial class ShowTodayWatch<TEntity> : IShowTodayWatch
    {
        #region 属性
        private String _HostName;
        /// <summary></summary>
        [DisplayName("HostName")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(1, "HostName", "", null, "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(2, "ServiceName", "", null, "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(3, "MethodName", "", null, "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(4, "ReqBeginTime", "", null, "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(5, "ReqEndTime", "", null, "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(6, "Status", "", null, "int", 10, 0, false)]
        public virtual Int32 Status
        {
            get { return _Status; }
            set { if (OnPropertyChanging(__.Status, value)) { _Status = value; OnPropertyChanged(__.Status); } }
        }

        private String _CallBeginTime;
        /// <summary></summary>
        [DisplayName("CallBeginTime")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(7, "CallBeginTime", "", null, "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(8, "CallEndTime", "", null, "nvarchar(50)", 0, 0, true)]
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
        [BindColumn(9, "BindingAddress", "", null, "nvarchar(200)", 0, 0, true)]
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
        [BindColumn(10, "BindingTemplateID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String BindingTemplateID
        {
            get { return _BindingTemplateID; }
            set { if (OnPropertyChanging(__.BindingTemplateID, value)) { _BindingTemplateID = value; OnPropertyChanged(__.BindingTemplateID); } }
        }

        private String _OID;
        /// <summary></summary>
        [DisplayName("OID")]
        [Description("")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(11, "OID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String OID
        {
            get { return _OID; }
            set { if (OnPropertyChanging(__.OID, value)) { _OID = value; OnPropertyChanged(__.OID); } }
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
                    case __.HostName : return _HostName;
                    case __.ServiceName : return _ServiceName;
                    case __.MethodName : return _MethodName;
                    case __.ReqBeginTime : return _ReqBeginTime;
                    case __.ReqEndTime : return _ReqEndTime;
                    case __.Status : return _Status;
                    case __.CallBeginTime : return _CallBeginTime;
                    case __.CallEndTime : return _CallEndTime;
                    case __.BindingAddress : return _BindingAddress;
                    case __.BindingTemplateID : return _BindingTemplateID;
                    case __.OID : return _OID;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.HostName : _HostName = Convert.ToString(value); break;
                    case __.ServiceName : _ServiceName = Convert.ToString(value); break;
                    case __.MethodName : _MethodName = Convert.ToString(value); break;
                    case __.ReqBeginTime : _ReqBeginTime = Convert.ToString(value); break;
                    case __.ReqEndTime : _ReqEndTime = Convert.ToString(value); break;
                    case __.Status : _Status = Convert.ToInt32(value); break;
                    case __.CallBeginTime : _CallBeginTime = Convert.ToString(value); break;
                    case __.CallEndTime : _CallEndTime = Convert.ToString(value); break;
                    case __.BindingAddress : _BindingAddress = Convert.ToString(value); break;
                    case __.BindingTemplateID : _BindingTemplateID = Convert.ToString(value); break;
                    case __.OID : _OID = Convert.ToString(value); break;
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
            public static readonly Field CallBeginTime = FindByName(__.CallBeginTime);

            ///<summary></summary>
            public static readonly Field CallEndTime = FindByName(__.CallEndTime);

            ///<summary></summary>
            public static readonly Field BindingAddress = FindByName(__.BindingAddress);

            ///<summary></summary>
            public static readonly Field BindingTemplateID = FindByName(__.BindingTemplateID);

            ///<summary></summary>
            public static readonly Field OID = FindByName(__.OID);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
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
            public const String CallBeginTime = "CallBeginTime";

            ///<summary></summary>
            public const String CallEndTime = "CallEndTime";

            ///<summary></summary>
            public const String BindingAddress = "BindingAddress";

            ///<summary></summary>
            public const String BindingTemplateID = "BindingTemplateID";

            ///<summary></summary>
            public const String OID = "OID";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IShowTodayWatch
    {
        #region 属性
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
        String CallBeginTime { get; set; }

        /// <summary></summary>
        String CallEndTime { get; set; }

        /// <summary></summary>
        String BindingAddress { get; set; }

        /// <summary></summary>
        String BindingTemplateID { get; set; }

        /// <summary></summary>
        String OID { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}