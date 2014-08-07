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
    [BindIndex("PK_BindingTemplate", false, "TemplateID")]
    [BindTable("BindingTemplate", Description = "", ConnName = "ServiceDirectoryDB", DbType = DatabaseType.SqlServer)]
    public partial class BindingTemplate<TEntity> : IBindingTemplate
    {
        #region 属性
        private Guid _TemplateID;
        /// <summary></summary>
        [DisplayName("TemplateID")]
        [Description("")]
        [DataObjectField(true, false, false, 16)]
        [BindColumn(1, "TemplateID", "", "newid()", "uniqueidentifier", 0, 0, false)]
        public virtual Guid TemplateID
        {
            get { return _TemplateID; }
            set { if (OnPropertyChanging(__.TemplateID, value)) { _TemplateID = value; OnPropertyChanged(__.TemplateID); } }
        }

        private Guid _ServiceID;
        /// <summary></summary>
        [DisplayName("ServiceID")]
        [Description("")]
        [DataObjectField(false, false, true, 16)]
        [BindColumn(2, "ServiceID", "", null, "uniqueidentifier", 0, 0, false)]
        public virtual Guid ServiceID
        {
            get { return _ServiceID; }
            set { if (OnPropertyChanging(__.ServiceID, value)) { _ServiceID = value; OnPropertyChanged(__.ServiceID); } }
        }

        private String _Address;
        /// <summary></summary>
        [DisplayName("Address")]
        [Description("")]
        [DataObjectField(false, false, true, 254)]
        [BindColumn(3, "Address", "", null, "nvarchar(254)", 0, 0, true)]
        public virtual String Address
        {
            get { return _Address; }
            set { if (OnPropertyChanging(__.Address, value)) { _Address = value; OnPropertyChanged(__.Address); } }
        }

        private String _Description;
        /// <summary></summary>
        [DisplayName("Description")]
        [Description("")]
        [DataObjectField(false, false, true, 254)]
        [BindColumn(4, "Description", "", null, "nvarchar(254)", 0, 0, true)]
        public virtual String Description
        {
            get { return _Description; }
            set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } }
        }

        private Int32 _BindingStatus;
        /// <summary></summary>
        [DisplayName("BindingStatus")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(5, "BindingStatus", "", null, "int", 10, 0, false)]
        public virtual Int32 BindingStatus
        {
            get { return _BindingStatus; }
            set { if (OnPropertyChanging(__.BindingStatus, value)) { _BindingStatus = value; OnPropertyChanged(__.BindingStatus); } }
        }

        private Int32 _BindingType;
        /// <summary></summary>
        [DisplayName("BindingType")]
        [Description("")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(6, "BindingType", "", null, "int", 10, 0, false)]
        public virtual Int32 BindingType
        {
            get { return _BindingType; }
            set { if (OnPropertyChanging(__.BindingType, value)) { _BindingType = value; OnPropertyChanged(__.BindingType); } }
        }

        private String _MethodName;
        /// <summary></summary>
        [DisplayName("MethodName")]
        [Description("")]
        [DataObjectField(false, false, true, 254)]
        [BindColumn(7, "MethodName", "", null, "nvarchar(254)", 0, 0, true)]
        public virtual String MethodName
        {
            get { return _MethodName; }
            set { if (OnPropertyChanging(__.MethodName, value)) { _MethodName = value; OnPropertyChanged(__.MethodName); } }
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
                    case __.TemplateID : return _TemplateID;
                    case __.ServiceID : return _ServiceID;
                    case __.Address : return _Address;
                    case __.Description : return _Description;
                    case __.BindingStatus : return _BindingStatus;
                    case __.BindingType : return _BindingType;
                    case __.MethodName : return _MethodName;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.TemplateID : _TemplateID = (Guid)value; break;
                    case __.ServiceID : _ServiceID = (Guid)value; break;
                    case __.Address : _Address = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    case __.BindingStatus : _BindingStatus = Convert.ToInt32(value); break;
                    case __.BindingType : _BindingType = Convert.ToInt32(value); break;
                    case __.MethodName : _MethodName = Convert.ToString(value); break;
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
            public static readonly Field TemplateID = FindByName(__.TemplateID);

            ///<summary></summary>
            public static readonly Field ServiceID = FindByName(__.ServiceID);

            ///<summary></summary>
            public static readonly Field Address = FindByName(__.Address);

            ///<summary></summary>
            public static readonly Field Description = FindByName(__.Description);

            ///<summary></summary>
            public static readonly Field BindingStatus = FindByName(__.BindingStatus);

            ///<summary></summary>
            public static readonly Field BindingType = FindByName(__.BindingType);

            ///<summary></summary>
            public static readonly Field MethodName = FindByName(__.MethodName);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String TemplateID = "TemplateID";

            ///<summary></summary>
            public const String ServiceID = "ServiceID";

            ///<summary></summary>
            public const String Address = "Address";

            ///<summary></summary>
            public const String Description = "Description";

            ///<summary></summary>
            public const String BindingStatus = "BindingStatus";

            ///<summary></summary>
            public const String BindingType = "BindingType";

            ///<summary></summary>
            public const String MethodName = "MethodName";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IBindingTemplate
    {
        #region 属性
        /// <summary></summary>
        Guid TemplateID { get; set; }

        /// <summary></summary>
        Guid ServiceID { get; set; }

        /// <summary></summary>
        String Address { get; set; }

        /// <summary></summary>
        String Description { get; set; }

        /// <summary></summary>
        Int32 BindingStatus { get; set; }

        /// <summary></summary>
        Int32 BindingType { get; set; }

        /// <summary></summary>
        String MethodName { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}