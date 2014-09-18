﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace ESB.Core.Entity
{
    /// <summary>服务配置表</summary>
    [Serializable]
    [DataObject]
    [Description("服务配置表")]
    [BindIndex("PK_ServiceConfig", true, "OID")]
    [BindTable("ServiceConfig", Description = "服务配置表", ConnName = "EsbServiceDirectoryDB", DbType = DatabaseType.SqlServer)]
    public partial class ServiceConfig<TEntity> : IServiceConfig
    {
        #region 属性
        private String _OID;
        /// <summary>主键</summary>
        [DisplayName("主键")]
        [Description("主键")]
        [DataObjectField(true, false, false, 50)]
        [BindColumn(1, "OID", "主键", null, "nvarchar(50)", 0, 0, true)]
        [XmlIgnore]
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
        [BindColumn(2, "ServiceID", "服务ID", null, "nvarchar(50)", 0, 0, true)]
        [XmlIgnore]
        public virtual String ServiceID
        {
            get { return _ServiceID; }
            set { if (OnPropertyChanging(__.ServiceID, value)) { _ServiceID = value; OnPropertyChanged(__.ServiceID); } }
        }

        private String _MethodName;
        /// <summary>方法名称：*代表所有方法</summary>
        [DisplayName("方法名称：*代表所有方法")]
        [Description("方法名称：*代表所有方法")]
        [DataObjectField(false, false, false, 100)]
        [BindColumn(3, "MethodName", "方法名称：*代表所有方法", "*", "nvarchar(100)", 0, 0, true)]
        public virtual String MethodName
        {
            get { return _MethodName; }
            set { if (OnPropertyChanging(__.MethodName, value)) { _MethodName = value; OnPropertyChanged(__.MethodName); } }
        }

        private Int32 _IsAudit;
        /// <summary>是否开启审计功能：0关闭，1开启，默认开启</summary>
        [DisplayName("是否开启审计功能：0关闭，1开启，默认开启")]
        [Description("是否开启审计功能：0关闭，1开启，默认开启")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(4, "IsAudit", "是否开启审计功能：0关闭，1开启，默认开启", "1", "int", 10, 0, false)]
        public virtual Int32 IsAudit
        {
            get { return _IsAudit; }
            set { if (OnPropertyChanging(__.IsAudit, value)) { _IsAudit = value; OnPropertyChanged(__.IsAudit); } }
        }

        private Int32 _Timeout;
        /// <summary>超时（单位ms）：默认100,000</summary>
        [DisplayName("超时单位ms：默认100,000")]
        [Description("超时（单位ms）：默认100,000")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(5, "Timeout", "超时（单位ms）：默认100,000", "100000", "int", 10, 0, false)]
        public virtual Int32 Timeout
        {
            get { return _Timeout; }
            set { if (OnPropertyChanging(__.Timeout, value)) { _Timeout = value; OnPropertyChanged(__.Timeout); } }
        }

        private Int32 _CacheDuration;
        /// <summary>缓存到期时间（单位s）:0代表不允许缓存</summary>
        [DisplayName("缓存到期时间单位s:0代表不允许缓存")]
        [Description("缓存到期时间（单位s）:0代表不允许缓存")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(6, "CacheDuration", "缓存到期时间（单位s）:0代表不允许缓存", "0", "int", 10, 0, false)]
        public virtual Int32 CacheDuration
        {
            get { return _CacheDuration; }
            set { if (OnPropertyChanging(__.CacheDuration, value)) { _CacheDuration = value; OnPropertyChanged(__.CacheDuration); } }
        }

        private String _QueueCenter;
        /// <summary>队列服务处理中心：如果没有填写代表不允许进行队列调用</summary>
        [DisplayName("队列服务处理中心：如果没有填写代表不允许进行队列调用")]
        [Description("队列服务处理中心：如果没有填写代表不允许进行队列调用")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(7, "QueueCenter", "队列服务处理中心：如果没有填写代表不允许进行队列调用", "", "nvarchar(50)", 0, 0, true)]
        public virtual String QueueCenter
        {
            get { return _QueueCenter; }
            set { if (OnPropertyChanging(__.QueueCenter, value)) { _QueueCenter = value; OnPropertyChanged(__.QueueCenter); } }
        }

        private Int32 _HBPolicy;
        /// <summary>负载均衡策略：0-随机，1-轮询，2-最少并发</summary>
        [DisplayName("负载均衡策略：0-随机，1-轮询，2-最少并发")]
        [Description("负载均衡策略：0-随机，1-轮询，2-最少并发")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(8, "HBPolicy", "负载均衡策略：0-随机，1-轮询，2-最少并发", "0", "int", 10, 0, false)]
        public virtual Int32 HBPolicy
        {
            get { return _HBPolicy; }
            set { if (OnPropertyChanging(__.HBPolicy, value)) { _HBPolicy = value; OnPropertyChanged(__.HBPolicy); } }
        }

        private String _WhiteList;
        /// <summary>白名单，优先级高</summary>
        [DisplayName("白名单，优先级高")]
        [Description("白名单，优先级高")]
        [DataObjectField(false, false, true, 300)]
        [BindColumn(9, "WhiteList", "白名单，优先级高", null, "nvarchar(300)", 0, 0, true)]
        public virtual String WhiteList
        {
            get { return _WhiteList; }
            set { if (OnPropertyChanging(__.WhiteList, value)) { _WhiteList = value; OnPropertyChanged(__.WhiteList); } }
        }

        private String _BlackList;
        /// <summary>黑名单</summary>
        [DisplayName("黑名单")]
        [Description("黑名单")]
        [DataObjectField(false, false, true, 300)]
        [BindColumn(10, "BlackList", "黑名单", null, "nvarchar(300)", 0, 0, true)]
        public virtual String BlackList
        {
            get { return _BlackList; }
            set { if (OnPropertyChanging(__.BlackList, value)) { _BlackList = value; OnPropertyChanged(__.BlackList); } }
        }

        private String _MockObject;
        /// <summary>用于服务容错或则屏蔽的空对象</summary>
        [DisplayName("用于服务容错或则屏蔽的空对象")]
        [Description("用于服务容错或则屏蔽的空对象")]
        [DataObjectField(false, false, true, 300)]
        [BindColumn(11, "MockObject", "用于服务容错或则屏蔽的空对象", null, "nvarchar(300)", 0, 0, true)]
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
        /// <summary>取得服务配置表字段信息的快捷方式</summary>
        public class _
        {
            ///<summary>主键</summary>
            public static readonly Field OID = FindByName(__.OID);

            ///<summary>服务ID</summary>
            public static readonly Field ServiceID = FindByName(__.ServiceID);

            ///<summary>方法名称：*代表所有方法</summary>
            public static readonly Field MethodName = FindByName(__.MethodName);

            ///<summary>是否开启审计功能：0关闭，1开启，默认开启</summary>
            public static readonly Field IsAudit = FindByName(__.IsAudit);

            ///<summary>超时（单位ms）：默认100,000</summary>
            public static readonly Field Timeout = FindByName(__.Timeout);

            ///<summary>缓存到期时间（单位s）:0代表不允许缓存</summary>
            public static readonly Field CacheDuration = FindByName(__.CacheDuration);

            ///<summary>队列服务处理中心：如果没有填写代表不允许进行队列调用</summary>
            public static readonly Field QueueCenter = FindByName(__.QueueCenter);

            ///<summary>负载均衡策略：0-随机，1-轮询，2-最少并发</summary>
            public static readonly Field HBPolicy = FindByName(__.HBPolicy);

            ///<summary>白名单，优先级高</summary>
            public static readonly Field WhiteList = FindByName(__.WhiteList);

            ///<summary>黑名单</summary>
            public static readonly Field BlackList = FindByName(__.BlackList);

            ///<summary>用于服务容错或则屏蔽的空对象</summary>
            public static readonly Field MockObject = FindByName(__.MockObject);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得服务配置表字段名称的快捷方式</summary>
        class __
        {
            ///<summary>主键</summary>
            public const String OID = "OID";

            ///<summary>服务ID</summary>
            public const String ServiceID = "ServiceID";

            ///<summary>方法名称：*代表所有方法</summary>
            public const String MethodName = "MethodName";

            ///<summary>是否开启审计功能：0关闭，1开启，默认开启</summary>
            public const String IsAudit = "IsAudit";

            ///<summary>超时（单位ms）：默认100,000</summary>
            public const String Timeout = "Timeout";

            ///<summary>缓存到期时间（单位s）:0代表不允许缓存</summary>
            public const String CacheDuration = "CacheDuration";

            ///<summary>队列服务处理中心：如果没有填写代表不允许进行队列调用</summary>
            public const String QueueCenter = "QueueCenter";

            ///<summary>负载均衡策略：0-随机，1-轮询，2-最少并发</summary>
            public const String HBPolicy = "HBPolicy";

            ///<summary>白名单，优先级高</summary>
            public const String WhiteList = "WhiteList";

            ///<summary>黑名单</summary>
            public const String BlackList = "BlackList";

            ///<summary>用于服务容错或则屏蔽的空对象</summary>
            public const String MockObject = "MockObject";

        }
        #endregion
    }

    /// <summary>服务配置表接口</summary>
    public partial interface IServiceConfig
    {
        #region 属性
        /// <summary>主键</summary>
        String OID { get; set; }

        /// <summary>服务ID</summary>
        String ServiceID { get; set; }

        /// <summary>方法名称：*代表所有方法</summary>
        String MethodName { get; set; }

        /// <summary>是否开启审计功能：0关闭，1开启，默认开启</summary>
        Int32 IsAudit { get; set; }

        /// <summary>超时（单位ms）：默认100,000</summary>
        Int32 Timeout { get; set; }

        /// <summary>缓存到期时间（单位s）:0代表不允许缓存</summary>
        Int32 CacheDuration { get; set; }

        /// <summary>队列服务处理中心：如果没有填写代表不允许进行队列调用</summary>
        String QueueCenter { get; set; }

        /// <summary>负载均衡策略：0-随机，1-轮询，2-最少并发</summary>
        Int32 HBPolicy { get; set; }

        /// <summary>白名单，优先级高</summary>
        String WhiteList { get; set; }

        /// <summary>黑名单</summary>
        String BlackList { get; set; }

        /// <summary>用于服务容错或则屏蔽的空对象</summary>
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