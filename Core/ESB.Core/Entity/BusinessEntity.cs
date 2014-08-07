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
    [BindIndex("PK_Buiness", false, "BusinessID")]
    [BindTable("BusinessEntity", Description = "", ConnName = "ServiceDirectoryDB", DbType = DatabaseType.SqlServer)]
    public partial class BusinessEntity<TEntity> : IBusinessEntity
    {
        #region 属性
        private Guid _BusinessID;
        /// <summary></summary>
        [DisplayName("BusinessID")]
        [Description("")]
        [DataObjectField(true, false, false, 16)]
        [BindColumn(1, "BusinessID", "", "newid()", "uniqueidentifier", 0, 0, false)]
        public virtual Guid BusinessID
        {
            get { return _BusinessID; }
            set { if (OnPropertyChanging(__.BusinessID, value)) { _BusinessID = value; OnPropertyChanged(__.BusinessID); } }
        }

        private String _BusinessName;
        /// <summary></summary>
        [DisplayName("BusinessName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(2, "BusinessName", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String BusinessName
        {
            get { return _BusinessName; }
            set { if (OnPropertyChanging(__.BusinessName, value)) { _BusinessName = value; OnPropertyChanged(__.BusinessName); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn(3, "Description", "", null, "nvarchar(200)", 0, 0, true)]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
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
                    case __.BusinessID : return _BusinessID;
                    case __.BusinessName : return _BusinessName;
                    case __.Description : return _Description;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.BusinessID : _BusinessID = (Guid)value; break;
                    case __.BusinessName : _BusinessName = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
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
            public static readonly Field BusinessID = FindByName(__.BusinessID);

            ///<summary></summary>
            public static readonly Field BusinessName = FindByName(__.BusinessName);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String BusinessID = "BusinessID";

            ///<summary></summary>
            public const String BusinessName = "BusinessName";

            ///<summary></summary>
            public const String Description = "Description";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IBusinessEntity
    {
        #region 属性
        /// <summary></summary>
        Guid BusinessID { get; set; }

        /// <summary></summary>
        String BusinessName { get; set; }

        /// <summary></summary>
        String Description { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}