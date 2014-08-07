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
    [BindTable("ReturnResponse", Description = "", ConnName = "EsbAuditDB", DbType = DatabaseType.SqlServer)]
    public partial class ReturnResponse<TEntity> : IReturnResponse
    {
        #region 属性
        private String _C;
        /// <summary></summary>
        [DisplayName("C")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(1, "C", "", null, "nchar(10)", 0, 0, true)]
        public virtual String C
        {
            get { return _C; }
            set { if (OnPropertyChanging(__.C, value)) { _C = value; OnPropertyChanged(__.C); } }
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
                    case __.C : return _C;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.C : _C = Convert.ToString(value); break;
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
            public static readonly Field C = FindByName(__.C);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String C = "C";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IReturnResponse
    {
        #region 属性
        /// <summary></summary>
        String C { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}