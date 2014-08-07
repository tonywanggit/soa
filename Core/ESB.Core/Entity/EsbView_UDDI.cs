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
    [BindTable("EsbView_UDDI", Description = "", ConnName = "ServiceDirectoryDB", DbType = DatabaseType.SqlServer, IsView = true)]
    public partial class EsbView_UDDI<TEntity> : IEsbView_UDDI
    {
        #region 属性
        private String _业务系统;
        /// <summary></summary>
        [DisplayName("业务系统")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(1, "业务系统", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String 业务系统
        {
            get { return _业务系统; }
            set { if (OnPropertyChanging(__.业务系统, value)) { _业务系统 = value; OnPropertyChanged(__.业务系统); } }
        }

        private String _业务系统描述;
        /// <summary></summary>
        [DisplayName("业务系统描述")]
        [Description("")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(2, "业务系统描述", "", null, "nvarchar(200)", 0, 0, true)]
        public virtual String 业务系统描述
        {
            get { return _业务系统描述; }
            set { if (OnPropertyChanging(__.业务系统描述, value)) { _业务系统描述 = value; OnPropertyChanged(__.业务系统描述); } }
        }

        private String _服务名称;
        /// <summary></summary>
        [DisplayName("服务名称")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(3, "服务名称", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String 服务名称
        {
            get { return _服务名称; }
            set { if (OnPropertyChanging(__.服务名称, value)) { _服务名称 = value; OnPropertyChanged(__.服务名称); } }
        }

        private String _服务描述;
        /// <summary></summary>
        [DisplayName("服务描述")]
        [Description("")]
        [DataObjectField(false, false, true, 254)]
        [BindColumn(4, "服务描述", "", null, "nvarchar(254)", 0, 0, true)]
        public virtual String 服务描述
        {
            get { return _服务描述; }
            set { if (OnPropertyChanging(__.服务描述, value)) { _服务描述 = value; OnPropertyChanged(__.服务描述); } }
        }

        private String _地址;
        /// <summary></summary>
        [DisplayName("地址")]
        [Description("")]
        [DataObjectField(false, false, true, 254)]
        [BindColumn(5, "地址", "", null, "nvarchar(254)", 0, 0, true)]
        public virtual String 地址
        {
            get { return _地址; }
            set { if (OnPropertyChanging(__.地址, value)) { _地址 = value; OnPropertyChanged(__.地址); } }
        }

        private Int32 _状态;
        /// <summary></summary>
        [DisplayName("状态")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(6, "状态", "", null, "int", 10, 0, false)]
        public virtual Int32 状态
        {
            get { return _状态; }
            set { if (OnPropertyChanging(__.状态, value)) { _状态 = value; OnPropertyChanged(__.状态); } }
        }

        private Int32 _类型;
        /// <summary></summary>
        [DisplayName("类型")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(7, "类型", "", null, "int", 10, 0, false)]
        public virtual Int32 类型
        {
            get { return _类型; }
            set { if (OnPropertyChanging(__.类型, value)) { _类型 = value; OnPropertyChanged(__.类型); } }
        }

        private String _方法名称;
        /// <summary></summary>
        [DisplayName("方法名称")]
        [Description("")]
        [DataObjectField(false, false, true, 254)]
        [BindColumn(8, "方法名称", "", null, "nvarchar(254)", 0, 0, true)]
        public virtual String 方法名称
        {
            get { return _方法名称; }
            set { if (OnPropertyChanging(__.方法名称, value)) { _方法名称 = value; OnPropertyChanged(__.方法名称); } }
        }

        private String _调用约束范例;
        /// <summary></summary>
        [DisplayName("调用约束范例")]
        [Description("")]
        [DataObjectField(false, false, true, 4000)]
        [BindColumn(9, "调用约束范例", "", null, "nvarchar(4000)", 0, 0, true)]
        public virtual String 调用约束范例
        {
            get { return _调用约束范例; }
            set { if (OnPropertyChanging(__.调用约束范例, value)) { _调用约束范例 = value; OnPropertyChanged(__.调用约束范例); } }
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
                    case __.业务系统 : return _业务系统;
                    case __.业务系统描述 : return _业务系统描述;
                    case __.服务名称 : return _服务名称;
                    case __.服务描述 : return _服务描述;
                    case __.地址 : return _地址;
                    case __.状态 : return _状态;
                    case __.类型 : return _类型;
                    case __.方法名称 : return _方法名称;
                    case __.调用约束范例 : return _调用约束范例;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.业务系统 : _业务系统 = Convert.ToString(value); break;
                    case __.业务系统描述 : _业务系统描述 = Convert.ToString(value); break;
                    case __.服务名称 : _服务名称 = Convert.ToString(value); break;
                    case __.服务描述 : _服务描述 = Convert.ToString(value); break;
                    case __.地址 : _地址 = Convert.ToString(value); break;
                    case __.状态 : _状态 = Convert.ToInt32(value); break;
                    case __.类型 : _类型 = Convert.ToInt32(value); break;
                    case __.方法名称 : _方法名称 = Convert.ToString(value); break;
                    case __.调用约束范例 : _调用约束范例 = Convert.ToString(value); break;
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
            public static readonly Field 业务系统 = FindByName(__.业务系统);

            ///<summary></summary>
            public static readonly Field 业务系统描述 = FindByName(__.业务系统描述);

            ///<summary></summary>
            public static readonly Field 服务名称 = FindByName(__.服务名称);

            ///<summary></summary>
            public static readonly Field 服务描述 = FindByName(__.服务描述);

            ///<summary></summary>
            public static readonly Field 地址 = FindByName(__.地址);

            ///<summary></summary>
            public static readonly Field 状态 = FindByName(__.状态);

            ///<summary></summary>
            public static readonly Field 类型 = FindByName(__.类型);

            ///<summary></summary>
            public static readonly Field 方法名称 = FindByName(__.方法名称);

            ///<summary></summary>
            public static readonly Field 调用约束范例 = FindByName(__.调用约束范例);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String 业务系统 = "业务系统";

            ///<summary></summary>
            public const String 业务系统描述 = "业务系统描述";

            ///<summary></summary>
            public const String 服务名称 = "服务名称";

            ///<summary></summary>
            public const String 服务描述 = "服务描述";

            ///<summary></summary>
            public const String 地址 = "地址";

            ///<summary></summary>
            public const String 状态 = "状态";

            ///<summary></summary>
            public const String 类型 = "类型";

            ///<summary></summary>
            public const String 方法名称 = "方法名称";

            ///<summary></summary>
            public const String 调用约束范例 = "调用约束范例";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IEsbView_UDDI
    {
        #region 属性
        /// <summary></summary>
        String 业务系统 { get; set; }

        /// <summary></summary>
        String 业务系统描述 { get; set; }

        /// <summary></summary>
        String 服务名称 { get; set; }

        /// <summary></summary>
        String 服务描述 { get; set; }

        /// <summary></summary>
        String 地址 { get; set; }

        /// <summary></summary>
        Int32 状态 { get; set; }

        /// <summary></summary>
        Int32 类型 { get; set; }

        /// <summary></summary>
        String 方法名称 { get; set; }

        /// <summary></summary>
        String 调用约束范例 { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}