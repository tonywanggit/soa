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
    [BindIndex("PK_ExceptionType", true, "TypeId")]
    [BindTable("ExceptionType", Description = "", ConnName = "EsbExceptionDb", DbType = DatabaseType.SqlServer)]
    public partial class ExceptionType<TEntity> : IExceptionType
    {
        #region 属性
        private String _Desception;
        /// <summary></summary>
        [DisplayName("Desception")]
        [Description("")]
        [DataObjectField(false, false, true, 4000)]
        [BindColumn(1, "Desception", "", null, "varchar(4000)", 0, 0, false)]
        public virtual String Desception
        {
            get { return _Desception; }
            set { if (OnPropertyChanging(__.Desception, value)) { _Desception = value; OnPropertyChanged(__.Desception); } }
        }

        private Int32 _ExceptionLevel;
        /// <summary></summary>
        [DisplayName("ExceptionLevel")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(2, "ExceptionLevel", "", null, "int", 10, 0, false)]
        public virtual Int32 ExceptionLevel
        {
            get { return _ExceptionLevel; }
            set { if (OnPropertyChanging(__.ExceptionLevel, value)) { _ExceptionLevel = value; OnPropertyChanged(__.ExceptionLevel); } }
        }

        private String _LevelDesception;
        /// <summary></summary>
        [DisplayName("LevelDesception")]
        [Description("")]
        [DataObjectField(false, false, true, 4000)]
        [BindColumn(3, "LevelDesception", "", null, "nvarchar(4000)", 0, 0, true)]
        public virtual String LevelDesception
        {
            get { return _LevelDesception; }
            set { if (OnPropertyChanging(__.LevelDesception, value)) { _LevelDesception = value; OnPropertyChanged(__.LevelDesception); } }
        }

        private Int32 _TypeId;
        /// <summary></summary>
        [DisplayName("TypeId")]
        [Description("")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn(4, "TypeId", "", null, "int", 10, 0, false)]
        public virtual Int32 TypeId
        {
            get { return _TypeId; }
            set { if (OnPropertyChanging(__.TypeId, value)) { _TypeId = value; OnPropertyChanged(__.TypeId); } }
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
                    case __.Desception : return _Desception;
                    case __.ExceptionLevel : return _ExceptionLevel;
                    case __.LevelDesception : return _LevelDesception;
                    case __.TypeId : return _TypeId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.Desception : _Desception = Convert.ToString(value); break;
                    case __.ExceptionLevel : _ExceptionLevel = Convert.ToInt32(value); break;
                    case __.LevelDesception : _LevelDesception = Convert.ToString(value); break;
                    case __.TypeId : _TypeId = Convert.ToInt32(value); break;
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
            public static readonly Field Desception = FindByName(__.Desception);

            ///<summary></summary>
            public static readonly Field ExceptionLevel = FindByName(__.ExceptionLevel);

            ///<summary></summary>
            public static readonly Field LevelDesception = FindByName(__.LevelDesception);

            ///<summary></summary>
            public static readonly Field TypeId = FindByName(__.TypeId);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String Desception = "Desception";

            ///<summary></summary>
            public const String ExceptionLevel = "ExceptionLevel";

            ///<summary></summary>
            public const String LevelDesception = "LevelDesception";

            ///<summary></summary>
            public const String TypeId = "TypeId";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IExceptionType
    {
        #region 属性
        /// <summary></summary>
        String Desception { get; set; }

        /// <summary></summary>
        Int32 ExceptionLevel { get; set; }

        /// <summary></summary>
        String LevelDesception { get; set; }

        /// <summary></summary>
        Int32 TypeId { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}