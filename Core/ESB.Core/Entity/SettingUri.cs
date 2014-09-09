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
    [BindIndex("PK_SettingUri", true, "OID")]
    [BindTable("SettingUri", Description = "", ConnName = "EsbServiceDirectoryDB", DbType = DatabaseType.SqlServer)]
    public partial class SettingUri<TEntity> : ISettingUri
    {
        #region 属性
        private String _OID;
        /// <summary>主键</summary>
        [DisplayName("主键")]
        [Description("主键")]
        [DataObjectField(true, false, false, 50)]
        [BindColumn(1, "OID", "主键", "", "nvarchar(50)", 0, 0, true)]
        public virtual String OID
        {
            get { return _OID; }
            set { if (OnPropertyChanging(__.OID, value)) { _OID = value; OnPropertyChanged(__.OID); } }
        }

        private Int32 _UriType;
        /// <summary>0：注册中心，2：监控中心，3：调用中心</summary>
        [DisplayName("0：注册中心，2：监控中心，3：调用中心")]
        [Description("0：注册中心，2：监控中心，3：调用中心")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(2, "UriType", "0：注册中心，2：监控中心，3：调用中心", "0", "int", 10, 0, false)]
        public virtual Int32 UriType
        {
            get { return _UriType; }
            set { if (OnPropertyChanging(__.UriType, value)) { _UriType = value; OnPropertyChanged(__.UriType); } }
        }

        private String _Uri;
        /// <summary>地址</summary>
        [DisplayName("地址")]
        [Description("地址")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn(3, "Uri", "地址", "", "nvarchar(50)", 0, 0, true)]
        public virtual String Uri
        {
            get { return _Uri; }
            set { if (OnPropertyChanging(__.Uri, value)) { _Uri = value; OnPropertyChanged(__.Uri); } }
        }

        private Int32 _Port;
        /// <summary>端口号</summary>
        [DisplayName("端口号")]
        [Description("端口号")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(4, "Port", "端口号", "-1", "int", 10, 0, false)]
        public virtual Int32 Port
        {
            get { return _Port; }
            set { if (OnPropertyChanging(__.Port, value)) { _Port = value; OnPropertyChanged(__.Port); } }
        }

        private String _UserName;
        /// <summary>用户名</summary>
        [DisplayName("用户名")]
        [Description("用户名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(5, "UserName", "用户名", null, "nvarchar(50)", 0, 0, true)]
        public virtual String UserName
        {
            get { return _UserName; }
            set { if (OnPropertyChanging(__.UserName, value)) { _UserName = value; OnPropertyChanged(__.UserName); } }
        }

        private String _PassWord;
        /// <summary>密码</summary>
        [DisplayName("密码")]
        [Description("密码")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(6, "PassWord", "密码", null, "nvarchar(50)", 0, 0, true)]
        public virtual String PassWord
        {
            get { return _PassWord; }
            set { if (OnPropertyChanging(__.PassWord, value)) { _PassWord = value; OnPropertyChanged(__.PassWord); } }
        }

        private Int32 _Status;
        /// <summary>1：启用，0：停用</summary>
        [DisplayName("1：启用，0：停用")]
        [Description("1：启用，0：停用")]
        [DataObjectField(false, false, false, 10)]
        [BindColumn(7, "Status", "1：启用，0：停用", "1", "int", 10, 0, false)]
        public virtual Int32 Status
        {
            get { return _Status; }
            set { if (OnPropertyChanging(__.Status, value)) { _Status = value; OnPropertyChanged(__.Status); } }
        }

        private DateTime _CreateDateTime;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, false, 3)]
        [BindColumn(8, "CreateDateTime", "创建时间", "getdate()", "datetime", 3, 0, false)]
        public virtual DateTime CreateDateTime
        {
            get { return _CreateDateTime; }
            set { if (OnPropertyChanging(__.CreateDateTime, value)) { _CreateDateTime = value; OnPropertyChanged(__.CreateDateTime); } }
        }

        private String _Remark;
        /// <summary>备注</summary>
        [DisplayName("备注")]
        [Description("备注")]
        [DataObjectField(false, false, false, 200)]
        [BindColumn(9, "Remark", "备注", "", "nvarchar(200)", 0, 0, true)]
        public virtual String Remark
        {
            get { return _Remark; }
            set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } }
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
                    case __.OID : return _OID;
                    case __.UriType : return _UriType;
                    case __.Uri : return _Uri;
                    case __.Port : return _Port;
                    case __.UserName : return _UserName;
                    case __.PassWord : return _PassWord;
                    case __.Status : return _Status;
                    case __.CreateDateTime : return _CreateDateTime;
                    case __.Remark : return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.OID : _OID = Convert.ToString(value); break;
                    case __.UriType : _UriType = Convert.ToInt32(value); break;
                    case __.Uri : _Uri = Convert.ToString(value); break;
                    case __.Port : _Port = Convert.ToInt32(value); break;
                    case __.UserName : _UserName = Convert.ToString(value); break;
                    case __.PassWord : _PassWord = Convert.ToString(value); break;
                    case __.Status : _Status = Convert.ToInt32(value); break;
                    case __.CreateDateTime : _CreateDateTime = Convert.ToDateTime(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得字段信息的快捷方式</summary>
        public class _
        {
            ///<summary>主键</summary>
            public static readonly Field OID = FindByName(__.OID);

            ///<summary>0：注册中心，2：监控中心，3：调用中心</summary>
            public static readonly Field UriType = FindByName(__.UriType);

            ///<summary>地址</summary>
            public static readonly Field Uri = FindByName(__.Uri);

            ///<summary>端口号</summary>
            public static readonly Field Port = FindByName(__.Port);

            ///<summary>用户名</summary>
            public static readonly Field UserName = FindByName(__.UserName);

            ///<summary>密码</summary>
            public static readonly Field PassWord = FindByName(__.PassWord);

            ///<summary>1：启用，0：停用</summary>
            public static readonly Field Status = FindByName(__.Status);

            ///<summary>创建时间</summary>
            public static readonly Field CreateDateTime = FindByName(__.CreateDateTime);

            ///<summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得字段名称的快捷方式</summary>
        class __
        {
            ///<summary>主键</summary>
            public const String OID = "OID";

            ///<summary>0：注册中心，2：监控中心，3：调用中心</summary>
            public const String UriType = "UriType";

            ///<summary>地址</summary>
            public const String Uri = "Uri";

            ///<summary>端口号</summary>
            public const String Port = "Port";

            ///<summary>用户名</summary>
            public const String UserName = "UserName";

            ///<summary>密码</summary>
            public const String PassWord = "PassWord";

            ///<summary>1：启用，0：停用</summary>
            public const String Status = "Status";

            ///<summary>创建时间</summary>
            public const String CreateDateTime = "CreateDateTime";

            ///<summary>备注</summary>
            public const String Remark = "Remark";

        }
        #endregion
    }

    /// <summary>接口</summary>
    public partial interface ISettingUri
    {
        #region 属性
        /// <summary>主键</summary>
        String OID { get; set; }

        /// <summary>0：注册中心，2：监控中心，3：调用中心</summary>
        Int32 UriType { get; set; }

        /// <summary>地址</summary>
        String Uri { get; set; }

        /// <summary>端口号</summary>
        Int32 Port { get; set; }

        /// <summary>用户名</summary>
        String UserName { get; set; }

        /// <summary>密码</summary>
        String PassWord { get; set; }

        /// <summary>1：启用，0：停用</summary>
        Int32 Status { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateDateTime { get; set; }

        /// <summary>备注</summary>
        String Remark { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}