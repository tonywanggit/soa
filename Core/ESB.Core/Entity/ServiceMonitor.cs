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
    [BindIndex("PK_ServiceMonitor", true, "OID")]
    [BindTable("ServiceMonitor", Description = "", ConnName = "EsbMonitorDB", DbType = DatabaseType.SqlServer)]
    public partial class ServiceMonitor<TEntity> : IServiceMonitor
    {
        #region 属性
        private String _OID;
        /// <summary></summary>
        [DisplayName("OID")]
        [Description("")]
        [DataObjectField(true, false, false, 50)]
        [BindColumn(1, "OID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String OID
        {
            get { return _OID; }
            set { if (OnPropertyChanging(__.OID, value)) { _OID = value; OnPropertyChanged(__.OID); } }
        }

        private DateTime _MonitorStamp;
        /// <summary>采集时间</summary>
        [DisplayName("采集时间")]
        [Description("采集时间")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn(2, "MonitorStamp", "采集时间", "getdate()", "datetime", 3, 0, false)]
        public virtual DateTime MonitorStamp
        {
            get { return _MonitorStamp; }
            set { if (OnPropertyChanging(__.MonitorStamp, value)) { _MonitorStamp = value; OnPropertyChanged(__.MonitorStamp); } }
        }

        private String _ServiceName;
        /// <summary>服务名称</summary>
        [DisplayName("服务名称")]
        [Description("服务名称")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(3, "ServiceName", "服务名称", "", "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceName
        {
            get { return _ServiceName; }
            set { if (OnPropertyChanging(__.ServiceName, value)) { _ServiceName = value; OnPropertyChanged(__.ServiceName); } }
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

        private String _BindingAddress;
        /// <summary>绑定地址</summary>
        [DisplayName("绑定地址")]
        [Description("绑定地址")]
        [DataObjectField(false, false, false, 100)]
        [BindColumn(5, "BindingAddress", "绑定地址", "", "nvarchar(100)", 0, 0, true)]
        public virtual String BindingAddress
        {
            get { return _BindingAddress; }
            set { if (OnPropertyChanging(__.BindingAddress, value)) { _BindingAddress = value; OnPropertyChanged(__.BindingAddress); } }
        }

        private String _ConsumerIP;
        /// <summary>消费者IP地址</summary>
        [DisplayName("消费者IP地址")]
        [Description("消费者IP地址")]
        [DataObjectField(false, false, false, 20)]
        [BindColumn(6, "ConsumerIP", "消费者IP地址", "", "nvarchar(20)", 0, 0, true)]
        public virtual String ConsumerIP
        {
            get { return _ConsumerIP; }
            set { if (OnPropertyChanging(__.ConsumerIP, value)) { _ConsumerIP = value; OnPropertyChanged(__.ConsumerIP); } }
        }

        private Int32 _CallSuccessNum;
        /// <summary>成功调用次数</summary>
        [DisplayName("成功调用次数")]
        [Description("成功调用次数")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(7, "CallSuccessNum", "成功调用次数", "0", "int", 10, 0, false)]
        public virtual Int32 CallSuccessNum
        {
            get { return _CallSuccessNum; }
            set { if (OnPropertyChanging(__.CallSuccessNum, value)) { _CallSuccessNum = value; OnPropertyChanged(__.CallSuccessNum); } }
        }

        private Int32 _CallFailureNum;
        /// <summary>失败调用次数</summary>
        [DisplayName("失败调用次数")]
        [Description("失败调用次数")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(8, "CallFailureNum", "失败调用次数", "0", "int", 10, 0, false)]
        public virtual Int32 CallFailureNum
        {
            get { return _CallFailureNum; }
            set { if (OnPropertyChanging(__.CallFailureNum, value)) { _CallFailureNum = value; OnPropertyChanged(__.CallFailureNum); } }
        }

        private Int64 _InBytes;
        /// <summary>进入流量</summary>
        [DisplayName("进入流量")]
        [Description("进入流量")]
        [DataObjectField(false, false, false, 19)]
        [BindColumn(9, "InBytes", "进入流量", "0", "bigint", 19, 0, false)]
        public virtual Int64 InBytes
        {
            get { return _InBytes; }
            set { if (OnPropertyChanging(__.InBytes, value)) { _InBytes = value; OnPropertyChanged(__.InBytes); } }
        }

        private Int64 _OutBytes;
        /// <summary>输出流量</summary>
        [DisplayName("输出流量")]
        [Description("输出流量")]
        [DataObjectField(false, false, false, 19)]
        [BindColumn(10, "OutBytes", "输出流量", "0", "bigint", 19, 0, false)]
        public virtual Int64 OutBytes
        {
            get { return _OutBytes; }
            set { if (OnPropertyChanging(__.OutBytes, value)) { _OutBytes = value; OnPropertyChanged(__.OutBytes); } }
        }

        private Int32 _CallLevel1Num;
        /// <summary>20~100毫秒以内的调用次数</summary>
        [DisplayName("20~100毫秒以内的调用次数")]
        [Description("20~100毫秒以内的调用次数")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(11, "CallLevel1Num", "20~100毫秒以内的调用次数", "0", "int", 10, 0, false)]
        public virtual Int32 CallLevel1Num
        {
            get { return _CallLevel1Num; }
            set { if (OnPropertyChanging(__.CallLevel1Num, value)) { _CallLevel1Num = value; OnPropertyChanged(__.CallLevel1Num); } }
        }

        private Int32 _CallLevel2Num;
        /// <summary>100~200ms的调用次数</summary>
        [DisplayName("100~200ms的调用次数")]
        [Description("100~200ms的调用次数")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(12, "CallLevel2Num", "100~200ms的调用次数", "0", "int", 10, 0, false)]
        public virtual Int32 CallLevel2Num
        {
            get { return _CallLevel2Num; }
            set { if (OnPropertyChanging(__.CallLevel2Num, value)) { _CallLevel2Num = value; OnPropertyChanged(__.CallLevel2Num); } }
        }

        private Int32 _CallLevel3Num;
        /// <summary>〉200ms的调用次数</summary>
        [DisplayName("〉200ms的调用次数")]
        [Description("〉200ms的调用次数")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(13, "CallLevel3Num", "〉200ms的调用次数", "0", "int", 10, 0, false)]
        public virtual Int32 CallLevel3Num
        {
            get { return _CallLevel3Num; }
            set { if (OnPropertyChanging(__.CallLevel3Num, value)) { _CallLevel3Num = value; OnPropertyChanged(__.CallLevel3Num); } }
        }

        private Double _TpsPeak;
        /// <summary>统计时间内的TPS峰值</summary>
        [DisplayName("统计时间内的TPS峰值")]
        [Description("统计时间内的TPS峰值")]
        [DataObjectField(false, false, false, 53)]
        [BindColumn(14, "TpsPeak", "统计时间内的TPS峰值", "0", "float", 53, 0, false)]
        public virtual Double TpsPeak
        {
            get { return _TpsPeak; }
            set { if (OnPropertyChanging(__.TpsPeak, value)) { _TpsPeak = value; OnPropertyChanged(__.TpsPeak); } }
        }

        private Int32 _CallHitCacheNum;
        /// <summary>命中缓存的调用次数</summary>
        [DisplayName("命中缓存的调用次数")]
        [Description("命中缓存的调用次数")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(15, "CallHitCacheNum", "命中缓存的调用次数", "0", "int", 10, 0, false)]
        public virtual Int32 CallHitCacheNum
        {
            get { return _CallHitCacheNum; }
            set { if (OnPropertyChanging(__.CallHitCacheNum, value)) { _CallHitCacheNum = value; OnPropertyChanged(__.CallHitCacheNum); } }
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
                    case __.MonitorStamp: return _MonitorStamp;
                    case __.ServiceName: return _ServiceName;
                    case __.MethodName: return _MethodName;
                    case __.BindingAddress: return _BindingAddress;
                    case __.ConsumerIP: return _ConsumerIP;
                    case __.CallSuccessNum: return _CallSuccessNum;
                    case __.CallFailureNum: return _CallFailureNum;
                    case __.InBytes: return _InBytes;
                    case __.OutBytes: return _OutBytes;
                    case __.CallLevel1Num: return _CallLevel1Num;
                    case __.CallLevel2Num: return _CallLevel2Num;
                    case __.CallLevel3Num: return _CallLevel3Num;
                    case __.TpsPeak: return _TpsPeak;
                    case __.CallHitCacheNum: return _CallHitCacheNum;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.OID: _OID = Convert.ToString(value); break;
                    case __.MonitorStamp: _MonitorStamp = Convert.ToDateTime(value); break;
                    case __.ServiceName: _ServiceName = Convert.ToString(value); break;
                    case __.MethodName: _MethodName = Convert.ToString(value); break;
                    case __.BindingAddress: _BindingAddress = Convert.ToString(value); break;
                    case __.ConsumerIP: _ConsumerIP = Convert.ToString(value); break;
                    case __.CallSuccessNum: _CallSuccessNum = Convert.ToInt32(value); break;
                    case __.CallFailureNum: _CallFailureNum = Convert.ToInt32(value); break;
                    case __.InBytes: _InBytes = Convert.ToInt64(value); break;
                    case __.OutBytes: _OutBytes = Convert.ToInt64(value); break;
                    case __.CallLevel1Num: _CallLevel1Num = Convert.ToInt32(value); break;
                    case __.CallLevel2Num: _CallLevel2Num = Convert.ToInt32(value); break;
                    case __.CallLevel3Num: _CallLevel3Num = Convert.ToInt32(value); break;
                    case __.TpsPeak: _TpsPeak = Convert.ToDouble(value); break;
                    case __.CallHitCacheNum: _CallHitCacheNum = Convert.ToInt32(value); break;
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

            ///<summary>采集时间</summary>
            public static readonly Field MonitorStamp = FindByName(__.MonitorStamp);

            ///<summary>服务名称</summary>
            public static readonly Field ServiceName = FindByName(__.ServiceName);

            ///<summary>方法名称</summary>
            public static readonly Field MethodName = FindByName(__.MethodName);

            ///<summary>绑定地址</summary>
            public static readonly Field BindingAddress = FindByName(__.BindingAddress);

            ///<summary>消费者IP地址</summary>
            public static readonly Field ConsumerIP = FindByName(__.ConsumerIP);

            ///<summary>成功调用次数</summary>
            public static readonly Field CallSuccessNum = FindByName(__.CallSuccessNum);

            ///<summary>失败调用次数</summary>
            public static readonly Field CallFailureNum = FindByName(__.CallFailureNum);

            ///<summary>进入流量</summary>
            public static readonly Field InBytes = FindByName(__.InBytes);

            ///<summary>输出流量</summary>
            public static readonly Field OutBytes = FindByName(__.OutBytes);

            ///<summary>20~100毫秒以内的调用次数</summary>
            public static readonly Field CallLevel1Num = FindByName(__.CallLevel1Num);

            ///<summary>100~200ms的调用次数</summary>
            public static readonly Field CallLevel2Num = FindByName(__.CallLevel2Num);

            ///<summary>〉200ms的调用次数</summary>
            public static readonly Field CallLevel3Num = FindByName(__.CallLevel3Num);

            ///<summary>统计时间内的TPS峰值</summary>
            public static readonly Field TpsPeak = FindByName(__.TpsPeak);

            ///<summary>命中缓存的调用次数</summary>
            public static readonly Field CallHitCacheNum = FindByName(__.CallHitCacheNum);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String OID = "OID";

            ///<summary>采集时间</summary>
            public const String MonitorStamp = "MonitorStamp";

            ///<summary>服务名称</summary>
            public const String ServiceName = "ServiceName";

            ///<summary>方法名称</summary>
            public const String MethodName = "MethodName";

            ///<summary>绑定地址</summary>
            public const String BindingAddress = "BindingAddress";

            ///<summary>消费者IP地址</summary>
            public const String ConsumerIP = "ConsumerIP";

            ///<summary>成功调用次数</summary>
            public const String CallSuccessNum = "CallSuccessNum";

            ///<summary>失败调用次数</summary>
            public const String CallFailureNum = "CallFailureNum";

            ///<summary>进入流量</summary>
            public const String InBytes = "InBytes";

            ///<summary>输出流量</summary>
            public const String OutBytes = "OutBytes";

            ///<summary>20~100毫秒以内的调用次数</summary>
            public const String CallLevel1Num = "CallLevel1Num";

            ///<summary>100~200ms的调用次数</summary>
            public const String CallLevel2Num = "CallLevel2Num";

            ///<summary>〉200ms的调用次数</summary>
            public const String CallLevel3Num = "CallLevel3Num";

            ///<summary>统计时间内的TPS峰值</summary>
            public const String TpsPeak = "TpsPeak";

            ///<summary>命中缓存的调用次数</summary>
            public const String CallHitCacheNum = "CallHitCacheNum";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IServiceMonitor
    {
        #region 属性
        /// <summary></summary>
        String OID { get; set; }

        /// <summary>采集时间</summary>
        DateTime MonitorStamp { get; set; }

        /// <summary>服务名称</summary>
        String ServiceName { get; set; }

        /// <summary>方法名称</summary>
        String MethodName { get; set; }

        /// <summary>绑定地址</summary>
        String BindingAddress { get; set; }

        /// <summary>消费者IP地址</summary>
        String ConsumerIP { get; set; }

        /// <summary>成功调用次数</summary>
        Int32 CallSuccessNum { get; set; }

        /// <summary>失败调用次数</summary>
        Int32 CallFailureNum { get; set; }

        /// <summary>进入流量</summary>
        Int64 InBytes { get; set; }

        /// <summary>输出流量</summary>
        Int64 OutBytes { get; set; }

        /// <summary>20~100毫秒以内的调用次数</summary>
        Int32 CallLevel1Num { get; set; }

        /// <summary>100~200ms的调用次数</summary>
        Int32 CallLevel2Num { get; set; }

        /// <summary>〉200ms的调用次数</summary>
        Int32 CallLevel3Num { get; set; }

        /// <summary>统计时间内的TPS峰值</summary>
        Double TpsPeak { get; set; }

        /// <summary>命中缓存的调用次数</summary>
        Int32 CallHitCacheNum { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}