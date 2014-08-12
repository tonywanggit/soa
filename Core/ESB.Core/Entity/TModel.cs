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
    [BindIndex("PK_TModel", false, "ModelID")]
    [BindTable("TModel", Description = "", ConnName = "EsbServiceDirectoryDB", DbType = DatabaseType.SqlServer)]
    public partial class TModel<TEntity> : ITModel
    {
        #region 属性
        private Guid _ModelID;
        /// <summary></summary>
        [DisplayName("ModelID")]
        [Description("")]
        [DataObjectField(true, false, false, 16)]
        [BindColumn(1, "ModelID", "", null, "uniqueidentifier", 0, 0, false)]
        public virtual Guid ModelID
        {
            get { return _ModelID; }
            set { if (OnPropertyChanging(__.ModelID, value)) { _ModelID = value; OnPropertyChanged(__.ModelID); } }
        }

        private Guid _TemplateID;
        /// <summary></summary>
        [DisplayName("TemplateID")]
        [Description("")]
        [DataObjectField(false, false, true, 16)]
        [BindColumn(2, "TemplateID", "", null, "uniqueidentifier", 0, 0, false)]
        public virtual Guid TemplateID
        {
            get { return _TemplateID; }
            set { if (OnPropertyChanging(__.TemplateID, value)) { _TemplateID = value; OnPropertyChanged(__.TemplateID); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, true, 254)]
        [BindColumn(3, "Description", "", null, "nvarchar(254)", 0, 0, true)]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private String _Example;
        /// <summary></summary>
        [DisplayName("Example")]
        [Description("")]
        [DataObjectField(false, false, true, 4000)]
        [BindColumn(4, "Example", "", null, "nvarchar(4000)", 0, 0, true)]
        public virtual String Example
        {
            get { return _Example; }
            set { if (OnPropertyChanging(__.Example, value)) { _Example = value; OnPropertyChanged(__.Example); } }
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
                    case __.ModelID : return _ModelID;
                    case __.TemplateID : return _TemplateID;
                    case __.Description : return _Description;
                    case __.Example : return _Example;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ModelID : _ModelID = (Guid)value; break;
                    case __.TemplateID : _TemplateID = (Guid)value; break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.Example : _Example = Convert.ToString(value); break;
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
            public static readonly Field ModelID = FindByName(__.ModelID);

            ///<summary></summary>
            public static readonly Field TemplateID = FindByName(__.TemplateID);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field Example = FindByName(__.Example);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String ModelID = "ModelID";

            ///<summary></summary>
            public const String TemplateID = "TemplateID";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String Example = "Example";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface ITModel
    {
        #region 属性
        /// <summary></summary>
        Guid ModelID { get; set; }

        /// <summary></summary>
        Guid TemplateID { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        String Example { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}