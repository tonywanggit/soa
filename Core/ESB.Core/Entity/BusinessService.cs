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
    [BindIndex("PK_BusinessService", true, "ServiceID")]
    [BindIndex("IX_BusinessService_PersonalID", false, "PersonalID")]
    [BindRelation("PersonalID", false, "Personal", "PersonalID")]
    [BindTable("BusinessService", Description = "", ConnName = "EsbServiceDirectoryDB", DbType = DatabaseType.SqlServer)]
    public partial class BusinessService<TEntity> : IBusinessService
    {
        #region 属性
        private String _ServiceID;
        /// <summary></summary>
        [DisplayName("ServiceID")]
        [Description("")]
        [DataObjectField(true, false, false, 50)]
        [BindColumn(1, "ServiceID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceID
        {
            get { return _ServiceID; }
            set { if (OnPropertyChanging(__.ServiceID, value)) { _ServiceID = value; OnPropertyChanged(__.ServiceID); } }
        }

        private String _PersonalID;
        /// <summary></summary>
        [DisplayName("PersonalID")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(2, "PersonalID", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String PersonalID
        {
            get { return _PersonalID; }
            set { if (OnPropertyChanging(__.PersonalID, value)) { _PersonalID = value; OnPropertyChanged(__.PersonalID); } }
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

        private String _ServiceName;
        /// <summary></summary>
        [DisplayName("ServiceName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(4, "ServiceName", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ServiceName
        {
            get { return _ServiceName; }
            set { if (OnPropertyChanging(__.ServiceName, value)) { _ServiceName = value; OnPropertyChanged(__.ServiceName); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, true, 254)]
        [BindColumn(5, "Description", "", null, "nvarchar(254)", 0, 0, true)]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private String _Category;
        /// <summary></summary>
        [DisplayName("Category")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(6, "Category", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Category
        {
            get { return _Category; }
            set { if (OnPropertyChanging(__.Category, value)) { _Category = value; OnPropertyChanged(__.Category); } }
        }

        private Int32 _DefaultVersion;
        /// <summary>默认使用的大版本号</summary>
        [DisplayName("默认使用的大版本号")]
        [Description("默认使用的大版本号")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(7, "DefaultVersion", "默认使用的大版本号", "0", "int", 10, 0, false)]
        public virtual Int32 DefaultVersion
        {
            get { return _DefaultVersion; }
            set { if (OnPropertyChanging(__.DefaultVersion, value)) { _DefaultVersion = value; OnPropertyChanged(__.DefaultVersion); } }
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
                    case __.ServiceID : return _ServiceID;
                    case __.PersonalID : return _PersonalID;
                    case __.BusinessID : return _BusinessID;
                    case __.ServiceName : return _ServiceName;
                    case __.Description : return _Description;
                    case __.Category : return _Category;
                    case __.DefaultVersion : return _DefaultVersion;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ServiceID : _ServiceID = Convert.ToString(value); break;
                    case __.PersonalID : _PersonalID = Convert.ToString(value); break;
                    case __.BusinessID : _BusinessID = Convert.ToString(value); break;
                    case __.ServiceName : _ServiceName = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.Category : _Category = Convert.ToString(value); break;
                    case __.DefaultVersion : _DefaultVersion = Convert.ToInt32(value); break;
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
            public static readonly Field ServiceID = FindByName(__.ServiceID);

            ///<summary></summary>
            public static readonly Field PersonalID = FindByName(__.PersonalID);

            ///<summary></summary>
            public static readonly Field BusinessID = FindByName(__.BusinessID);

            ///<summary></summary>
            public static readonly Field ServiceName = FindByName(__.ServiceName);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field Category = FindByName(__.Category);

            ///<summary>默认使用的大版本号</summary>
            public static readonly Field DefaultVersion = FindByName(__.DefaultVersion);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String ServiceID = "ServiceID";

            ///<summary></summary>
            public const String PersonalID = "PersonalID";

            ///<summary></summary>
            public const String BusinessID = "BusinessID";

            ///<summary></summary>
            public const String ServiceName = "ServiceName";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String Category = "Category";

            ///<summary>默认使用的大版本号</summary>
            public const String DefaultVersion = "DefaultVersion";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IBusinessService
    {
        #region 属性
        /// <summary></summary>
        String ServiceID { get; set; }

        /// <summary></summary>
        String PersonalID { get; set; }

        /// <summary></summary>
        String BusinessID { get; set; }

        /// <summary></summary>
        String ServiceName { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        String Category { get; set; }

        /// <summary>默认使用的大版本号</summary>
        Int32 DefaultVersion { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}