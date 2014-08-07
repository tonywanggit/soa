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
    [BindIndex("PK_Personal", false, "PersonalID")]
    [BindRelation("PersonalID", false, "BusinessService", "PersonalID")]
    [BindTable("Personal", Description = "", ConnName = "ServiceDirectoryDB", DbType = DatabaseType.SqlServer)]
    public partial class Personal<TEntity> : IPersonal
    {
        #region 属性
        private Guid _PersonalID;
        /// <summary></summary>
        [DisplayName("PersonalID")]
        [Description("")]
        [DataObjectField(true, false, false, 16)]
        [BindColumn(1, "PersonalID", "", "newid()", "uniqueidentifier", 0, 0, false)]
        public virtual Guid PersonalID
        {
            get { return _PersonalID; }
            set { if (OnPropertyChanging(__.PersonalID, value)) { _PersonalID = value; OnPropertyChanged(__.PersonalID); } }
        }

        private String _PersonalName;
        /// <summary></summary>
        [DisplayName("PersonalName")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(2, "PersonalName", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String PersonalName
        {
            get { return _PersonalName; }
            set { if (OnPropertyChanging(__.PersonalName, value)) { _PersonalName = value; OnPropertyChanged(__.PersonalName); } }
        }

        private String _Phone;
        /// <summary></summary>
        [DisplayName("Phone")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(3, "Phone", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Phone
        {
            get { return _Phone; }
            set { if (OnPropertyChanging(__.Phone, value)) { _Phone = value; OnPropertyChanged(__.Phone); } }
        }

        private String _Mail;
        /// <summary></summary>
        [DisplayName("Mail")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(4, "Mail", "", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Mail
        {
            get { return _Mail; }
            set { if (OnPropertyChanging(__.Mail, value)) { _Mail = value; OnPropertyChanged(__.Mail); } }
        }

        private Int32 _permission;
        /// <summary></summary>
        [DisplayName("permission")]
        [Description("")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(5, "permission", "", null, "int", 10, 0, false)]
        public virtual Int32 permission
        {
            get { return _permission; }
            set { if (OnPropertyChanging(__.permission, value)) { _permission = value; OnPropertyChanged(__.permission); } }
        }

        private String _PersonalAccount;
        /// <summary></summary>
        [DisplayName("PersonalAccount")]
        [Description("")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(6, "PersonalAccount", "", "", "nvarchar(50)", 0, 0, true)]
        public virtual String PersonalAccount
        {
            get { return _PersonalAccount; }
            set { if (OnPropertyChanging(__.PersonalAccount, value)) { _PersonalAccount = value; OnPropertyChanged(__.PersonalAccount); } }
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
                    case __.PersonalID : return _PersonalID;
                    case __.PersonalName : return _PersonalName;
                    case __.Phone : return _Phone;
                    case __.Mail : return _Mail;
                    case __.permission : return _permission;
                    case __.PersonalAccount : return _PersonalAccount;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.PersonalID : _PersonalID = (Guid)value; break;
                    case __.PersonalName : _PersonalName = Convert.ToString(value); break;
                    case __.Phone : _Phone = Convert.ToString(value); break;
                    case __.Mail : _Mail = Convert.ToString(value); break;
                    case __.permission : _permission = Convert.ToInt32(value); break;
                    case __.PersonalAccount : _PersonalAccount = Convert.ToString(value); break;
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
            public static readonly Field PersonalID = FindByName(__.PersonalID);

            ///<summary></summary>
            public static readonly Field PersonalName = FindByName(__.PersonalName);

            ///<summary></summary>
            public static readonly Field Phone = FindByName(__.Phone);

            ///<summary></summary>
            public static readonly Field Mail = FindByName(__.Mail);

            ///<summary></summary>
            public static readonly Field permission = FindByName(__.permission);

            ///<summary></summary>
            public static readonly Field PersonalAccount = FindByName(__.PersonalAccount);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary></summary>
            public const String PersonalID = "PersonalID";

            ///<summary></summary>
            public const String PersonalName = "PersonalName";

            ///<summary></summary>
            public const String Phone = "Phone";

            ///<summary></summary>
            public const String Mail = "Mail";

            ///<summary></summary>
            public const String permission = "permission";

            ///<summary></summary>
            public const String PersonalAccount = "PersonalAccount";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface IPersonal
    {
        #region 属性
        /// <summary></summary>
        Guid PersonalID { get; set; }

        /// <summary></summary>
        String PersonalName { get; set; }

        /// <summary></summary>
        String Phone { get; set; }

        /// <summary></summary>
        String Mail { get; set; }

        /// <summary></summary>
        Int32 permission { get; set; }

        /// <summary></summary>
        String PersonalAccount { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}