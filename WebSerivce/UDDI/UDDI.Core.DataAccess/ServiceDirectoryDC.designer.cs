﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JN.ESB.UDDI.Core.DataAccess
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Runtime.Serialization;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="ServiceDirectoryDb")]
	public partial class ServiceDirectoryDCDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void Insert业务实体(业务实体 instance);
    partial void Update业务实体(业务实体 instance);
    partial void Delete业务实体(业务实体 instance);
    partial void Insert服务(服务 instance);
    partial void Update服务(服务 instance);
    partial void Delete服务(服务 instance);
    partial void Insert服务约束(服务约束 instance);
    partial void Update服务约束(服务约束 instance);
    partial void Delete服务约束(服务约束 instance);
    partial void Insert服务地址(服务地址 instance);
    partial void Update服务地址(服务地址 instance);
    partial void Delete服务地址(服务地址 instance);
    partial void Insert个人(个人 instance);
    partial void Update个人(个人 instance);
    partial void Delete个人(个人 instance);
    #endregion
		
		
		public ServiceDirectoryDCDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ServiceDirectoryDCDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ServiceDirectoryDCDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ServiceDirectoryDCDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<业务实体> 业务实体
		{
			get
			{
				return this.GetTable<业务实体>();
			}
		}
		
		public System.Data.Linq.Table<服务> 服务
		{
			get
			{
				return this.GetTable<服务>();
			}
		}
		
		public System.Data.Linq.Table<服务约束> 服务约束
		{
			get
			{
				return this.GetTable<服务约束>();
			}
		}
		
		public System.Data.Linq.Table<服务地址> 服务地址
		{
			get
			{
				return this.GetTable<服务地址>();
			}
		}
		
		public System.Data.Linq.Table<个人> 个人
		{
			get
			{
				return this.GetTable<个人>();
			}
		}
		
		public System.Data.Linq.Table<服务视图> 服务视图
		{
			get
			{
				return this.GetTable<服务视图>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.BusinessEntity")]
	[global::System.Runtime.Serialization.DataContractAttribute()]
	public partial class 业务实体 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _BuinessID;
		
		private string _BuinessName;
		
		private string _Description;
		
		private EntitySet<服务> _服务;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void On业务编码Changing(System.Guid value);
    partial void On业务编码Changed();
    partial void On业务名称Changing(string value);
    partial void On业务名称Changed();
    partial void On描述Changing(string value);
    partial void On描述Changed();
    #endregion
		
		public 业务实体()
		{
			this.Initialize();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="BusinessID", Storage="_BuinessID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=1)]
		public System.Guid 业务编码
		{
			get
			{
				return this._BuinessID;
			}
			set
			{
				if ((this._BuinessID != value))
				{
					this.On业务编码Changing(value);
					this.SendPropertyChanging();
					this._BuinessID = value;
					this.SendPropertyChanged("业务编码");
					this.On业务编码Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="BusinessName", Storage="_BuinessName", DbType="NVarChar(50)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=2)]
		public string 业务名称
		{
			get
			{
				return this._BuinessName;
			}
			set
			{
				if ((this._BuinessName != value))
				{
					this.On业务名称Changing(value);
					this.SendPropertyChanging();
					this._BuinessName = value;
					this.SendPropertyChanged("业务名称");
					this.On业务名称Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Description", Storage="_Description", DbType="NVarChar(200)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=3)]
		public string 描述
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.On描述Changing(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("描述");
					this.On描述Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="业务实体_服务", Storage="_服务", ThisKey="业务编码", OtherKey="业务编码")]
		internal EntitySet<服务> 服务
		{
			get
			{
				return this._服务;
			}
			set
			{
				this._服务.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_服务(服务 entity)
		{
			this.SendPropertyChanging();
			entity.业务实体 = this;
		}
		
		private void detach_服务(服务 entity)
		{
			this.SendPropertyChanging();
			entity.业务实体 = null;
		}
		
		private void Initialize()
		{
			this._服务 = new EntitySet<服务>(new Action<服务>(this.attach_服务), new Action<服务>(this.detach_服务));
			OnCreated();
		}
		
		[global::System.Runtime.Serialization.OnDeserializingAttribute()]
		[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.BusinessService")]
	[global::System.Runtime.Serialization.DataContractAttribute()]
	public partial class 服务 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _ServiceID;
		
		private System.Nullable<System.Guid> _PersoanlID;
		
		private System.Nullable<System.Guid> _BusinessID;
		
		private string _ServiceName;
		
		private string _Description;
		
		private string _Category;
		
		private EntitySet<服务地址> _服务地址;
		
		private EntityRef<个人> _个人;
		
		private EntityRef<业务实体> _业务实体;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void On服务编码Changing(System.Guid value);
    partial void On服务编码Changed();
    partial void On个人编码Changing(System.Nullable<System.Guid> value);
    partial void On个人编码Changed();
    partial void On业务编码Changing(System.Nullable<System.Guid> value);
    partial void On业务编码Changed();
    partial void On服务名称Changing(string value);
    partial void On服务名称Changed();
    partial void On描述Changing(string value);
    partial void On描述Changed();
    partial void On服务种类Changing(string value);
    partial void On服务种类Changed();
    #endregion
		
		public 服务()
		{
			this.Initialize();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ServiceID", Storage="_ServiceID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=1)]
		public System.Guid 服务编码
		{
			get
			{
				return this._ServiceID;
			}
			set
			{
				if ((this._ServiceID != value))
				{
					this.On服务编码Changing(value);
					this.SendPropertyChanging();
					this._ServiceID = value;
					this.SendPropertyChanged("服务编码");
					this.On服务编码Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PersonalID", Storage="_PersoanlID", DbType="UniqueIdentifier")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=2)]
		public System.Nullable<System.Guid> 个人编码
		{
			get
			{
				return this._PersoanlID;
			}
			set
			{
				if ((this._PersoanlID != value))
				{
					this.On个人编码Changing(value);
					this.SendPropertyChanging();
					this._PersoanlID = value;
					this.SendPropertyChanged("个人编码");
					this.On个人编码Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="BusinessID", Storage="_BusinessID", DbType="UniqueIdentifier")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=3)]
		public System.Nullable<System.Guid> 业务编码
		{
			get
			{
				return this._BusinessID;
			}
			set
			{
				if ((this._BusinessID != value))
				{
					this.On业务编码Changing(value);
					this.SendPropertyChanging();
					this._BusinessID = value;
					this.SendPropertyChanged("业务编码");
					this.On业务编码Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ServiceName", Storage="_ServiceName", DbType="NVarChar(50)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=4)]
		public string 服务名称
		{
			get
			{
				return this._ServiceName;
			}
			set
			{
				if ((this._ServiceName != value))
				{
					this.On服务名称Changing(value);
					this.SendPropertyChanging();
					this._ServiceName = value;
					this.SendPropertyChanged("服务名称");
					this.On服务名称Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Description", Storage="_Description", DbType="NVarChar(254)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=5)]
		public string 描述
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.On描述Changing(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("描述");
					this.On描述Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Category", Storage="_Category", DbType="NVarChar(50)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=6)]
		public string 服务种类
		{
			get
			{
				return this._Category;
			}
			set
			{
				if ((this._Category != value))
				{
					this.On服务种类Changing(value);
					this.SendPropertyChanging();
					this._Category = value;
					this.SendPropertyChanged("服务种类");
					this.On服务种类Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="服务_服务地址", Storage="_服务地址", ThisKey="服务编码", OtherKey="服务编码")]
		internal EntitySet<服务地址> 服务地址
		{
			get
			{
				return this._服务地址;
			}
			set
			{
				this._服务地址.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="个人_服务", Storage="_个人", ThisKey="个人编码", OtherKey="个人编码", IsForeignKey=true)]
		internal 个人 个人
		{
			get
			{
				return this._个人.Entity;
			}
			set
			{
				个人 previousValue = this._个人.Entity;
				if (((previousValue != value) 
							|| (this._个人.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._个人.Entity = null;
						previousValue.服务.Remove(this);
					}
					this._个人.Entity = value;
					if ((value != null))
					{
						value.服务.Add(this);
						this._PersoanlID = value.个人编码;
					}
					else
					{
						this._PersoanlID = default(Nullable<System.Guid>);
					}
					this.SendPropertyChanged("个人");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="业务实体_服务", Storage="_业务实体", ThisKey="业务编码", OtherKey="业务编码", IsForeignKey=true)]
		internal 业务实体 业务实体
		{
			get
			{
				return this._业务实体.Entity;
			}
			set
			{
				业务实体 previousValue = this._业务实体.Entity;
				if (((previousValue != value) 
							|| (this._业务实体.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._业务实体.Entity = null;
						previousValue.服务.Remove(this);
					}
					this._业务实体.Entity = value;
					if ((value != null))
					{
						value.服务.Add(this);
						this._BusinessID = value.业务编码;
					}
					else
					{
						this._BusinessID = default(Nullable<System.Guid>);
					}
					this.SendPropertyChanged("业务实体");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_服务地址(服务地址 entity)
		{
			this.SendPropertyChanging();
			entity.服务 = this;
		}
		
		private void detach_服务地址(服务地址 entity)
		{
			this.SendPropertyChanging();
			entity.服务 = null;
		}
		
		private void Initialize()
		{
			this._服务地址 = new EntitySet<服务地址>(new Action<服务地址>(this.attach_服务地址), new Action<服务地址>(this.detach_服务地址));
			this._个人 = default(EntityRef<个人>);
			this._业务实体 = default(EntityRef<业务实体>);
			OnCreated();
		}
		
		[global::System.Runtime.Serialization.OnDeserializingAttribute()]
		[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TModel")]
	[global::System.Runtime.Serialization.DataContractAttribute()]
	public partial class 服务约束 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _ModelID;
		
		private System.Nullable<System.Guid> _TemplateID;
		
		private string _Description;
		
		private string _Example;
		
		private EntityRef<服务地址> _服务地址;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void On约束编码Changing(System.Guid value);
    partial void On约束编码Changed();
    partial void On服务地址编码Changing(System.Nullable<System.Guid> value);
    partial void On服务地址编码Changed();
    partial void On描述Changing(string value);
    partial void On描述Changed();
    partial void On示例Changing(string value);
    partial void On示例Changed();
    #endregion
		
		public 服务约束()
		{
			this.Initialize();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ModelID", Storage="_ModelID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=1)]
		public System.Guid 约束编码
		{
			get
			{
				return this._ModelID;
			}
			set
			{
				if ((this._ModelID != value))
				{
					this.On约束编码Changing(value);
					this.SendPropertyChanging();
					this._ModelID = value;
					this.SendPropertyChanged("约束编码");
					this.On约束编码Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TemplateID", Storage="_TemplateID", DbType="UniqueIdentifier")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=2)]
		public System.Nullable<System.Guid> 服务地址编码
		{
			get
			{
				return this._TemplateID;
			}
			set
			{
				if ((this._TemplateID != value))
				{
					this.On服务地址编码Changing(value);
					this.SendPropertyChanging();
					this._TemplateID = value;
					this.SendPropertyChanged("服务地址编码");
					this.On服务地址编码Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Description", Storage="_Description", DbType="NVarChar(254)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=3)]
		public string 描述
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.On描述Changing(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("描述");
					this.On描述Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Example", Storage="_Example", DbType="NVarChar(4000)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=4)]
		public string 示例
		{
			get
			{
				return this._Example;
			}
			set
			{
				if ((this._Example != value))
				{
					this.On示例Changing(value);
					this.SendPropertyChanging();
					this._Example = value;
					this.SendPropertyChanged("示例");
					this.On示例Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="服务地址_服务约束", Storage="_服务地址", ThisKey="服务地址编码", OtherKey="服务地址编码", IsForeignKey=true)]
		internal 服务地址 服务地址
		{
			get
			{
				return this._服务地址.Entity;
			}
			set
			{
				服务地址 previousValue = this._服务地址.Entity;
				if (((previousValue != value) 
							|| (this._服务地址.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._服务地址.Entity = null;
						previousValue.服务约束.Remove(this);
					}
					this._服务地址.Entity = value;
					if ((value != null))
					{
						value.服务约束.Add(this);
						this._TemplateID = value.服务地址编码;
					}
					else
					{
						this._TemplateID = default(Nullable<System.Guid>);
					}
					this.SendPropertyChanged("服务地址");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void Initialize()
		{
			this._服务地址 = default(EntityRef<服务地址>);
			OnCreated();
		}
		
		[global::System.Runtime.Serialization.OnDeserializingAttribute()]
		[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.BindingTemplate")]
	[global::System.Runtime.Serialization.DataContractAttribute()]
	public partial class 服务地址 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _服务地址编码;
		
		private System.Nullable<System.Guid> _服务编码;
		
		private string _访问地址;
		
		private string _描述;
		
		private System.Nullable<int> _状态;
		
		private System.Nullable<int> _绑定类型;
		
		private string _方法名称;
		
		private EntitySet<服务约束> _服务约束;
		
		private EntityRef<服务> _服务;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void On服务地址编码Changing(System.Guid value);
    partial void On服务地址编码Changed();
    partial void On服务编码Changing(System.Nullable<System.Guid> value);
    partial void On服务编码Changed();
    partial void On访问地址Changing(string value);
    partial void On访问地址Changed();
    partial void On描述Changing(string value);
    partial void On描述Changed();
    partial void On状态Changing(System.Nullable<int> value);
    partial void On状态Changed();
    partial void On绑定类型Changing(System.Nullable<int> value);
    partial void On绑定类型Changed();
    partial void On方法名称Changing(string value);
    partial void On方法名称Changed();
    #endregion
		
		public 服务地址()
		{
			this.Initialize();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TemplateID", Storage="_服务地址编码", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=1)]
		public System.Guid 服务地址编码
		{
			get
			{
				return this._服务地址编码;
			}
			set
			{
				if ((this._服务地址编码 != value))
				{
					this.On服务地址编码Changing(value);
					this.SendPropertyChanging();
					this._服务地址编码 = value;
					this.SendPropertyChanged("服务地址编码");
					this.On服务地址编码Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ServiceID", Storage="_服务编码", DbType="UniqueIdentifier")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=2)]
		public System.Nullable<System.Guid> 服务编码
		{
			get
			{
				return this._服务编码;
			}
			set
			{
				if ((this._服务编码 != value))
				{
					this.On服务编码Changing(value);
					this.SendPropertyChanging();
					this._服务编码 = value;
					this.SendPropertyChanged("服务编码");
					this.On服务编码Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Address", Storage="_访问地址", DbType="NVarChar(254)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=3)]
		public string 访问地址
		{
			get
			{
				return this._访问地址;
			}
			set
			{
				if ((this._访问地址 != value))
				{
					this.On访问地址Changing(value);
					this.SendPropertyChanging();
					this._访问地址 = value;
					this.SendPropertyChanged("访问地址");
					this.On访问地址Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Description", Storage="_描述", DbType="NVarChar(254)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=4)]
		public string 描述
		{
			get
			{
				return this._描述;
			}
			set
			{
				if ((this._描述 != value))
				{
					this.On描述Changing(value);
					this.SendPropertyChanging();
					this._描述 = value;
					this.SendPropertyChanged("描述");
					this.On描述Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="BindingStatus", Storage="_状态", DbType="Int")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=5)]
		public System.Nullable<int> 状态
		{
			get
			{
				return this._状态;
			}
			set
			{
				if ((this._状态 != value))
				{
					this.On状态Changing(value);
					this.SendPropertyChanging();
					this._状态 = value;
					this.SendPropertyChanged("状态");
					this.On状态Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="BindingType", Storage="_绑定类型", DbType="Int")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=6)]
		public System.Nullable<int> 绑定类型
		{
			get
			{
				return this._绑定类型;
			}
			set
			{
				if ((this._绑定类型 != value))
				{
					this.On绑定类型Changing(value);
					this.SendPropertyChanging();
					this._绑定类型 = value;
					this.SendPropertyChanged("绑定类型");
					this.On绑定类型Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="MethodName", Storage="_方法名称", DbType="NVarChar(254)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=7)]
		public string 方法名称
		{
			get
			{
				return this._方法名称;
			}
			set
			{
				if ((this._方法名称 != value))
				{
					this.On方法名称Changing(value);
					this.SendPropertyChanging();
					this._方法名称 = value;
					this.SendPropertyChanged("方法名称");
					this.On方法名称Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="服务地址_服务约束", Storage="_服务约束", ThisKey="服务地址编码", OtherKey="服务地址编码")]
		internal EntitySet<服务约束> 服务约束
		{
			get
			{
				return this._服务约束;
			}
			set
			{
				this._服务约束.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="服务_服务地址", Storage="_服务", ThisKey="服务编码", OtherKey="服务编码", IsForeignKey=true)]
		internal 服务 服务
		{
			get
			{
				return this._服务.Entity;
			}
			set
			{
				服务 previousValue = this._服务.Entity;
				if (((previousValue != value) 
							|| (this._服务.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._服务.Entity = null;
						previousValue.服务地址.Remove(this);
					}
					this._服务.Entity = value;
					if ((value != null))
					{
						value.服务地址.Add(this);
						this._服务编码 = value.服务编码;
					}
					else
					{
						this._服务编码 = default(Nullable<System.Guid>);
					}
					this.SendPropertyChanged("服务");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_服务约束(服务约束 entity)
		{
			this.SendPropertyChanging();
			entity.服务地址 = this;
		}
		
		private void detach_服务约束(服务约束 entity)
		{
			this.SendPropertyChanging();
			entity.服务地址 = null;
		}
		
		private void Initialize()
		{
			this._服务约束 = new EntitySet<服务约束>(new Action<服务约束>(this.attach_服务约束), new Action<服务约束>(this.detach_服务约束));
			this._服务 = default(EntityRef<服务>);
			OnCreated();
		}
		
		[global::System.Runtime.Serialization.OnDeserializingAttribute()]
		[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Personal")]
	[global::System.Runtime.Serialization.DataContractAttribute()]
	public partial class 个人 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _个人编码;
		
		private string _姓名;
		
		private string _电话;
		
		private string _邮件地址;
		
		private int _权限;
		
		private string _帐号;
		
		private EntitySet<服务> _服务;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void On个人编码Changing(System.Guid value);
    partial void On个人编码Changed();
    partial void On姓名Changing(string value);
    partial void On姓名Changed();
    partial void On电话Changing(string value);
    partial void On电话Changed();
    partial void On邮件地址Changing(string value);
    partial void On邮件地址Changed();
    partial void On权限Changing(int value);
    partial void On权限Changed();
    partial void On帐号Changing(string value);
    partial void On帐号Changed();
    #endregion
		
		public 个人()
		{
			this.Initialize();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PersonalID", Storage="_个人编码", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=1)]
		public System.Guid 个人编码
		{
			get
			{
				return this._个人编码;
			}
			set
			{
				if ((this._个人编码 != value))
				{
					this.On个人编码Changing(value);
					this.SendPropertyChanging();
					this._个人编码 = value;
					this.SendPropertyChanged("个人编码");
					this.On个人编码Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PersonalName", Storage="_姓名", DbType="NVarChar(50)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=2)]
		public string 姓名
		{
			get
			{
				return this._姓名;
			}
			set
			{
				if ((this._姓名 != value))
				{
					this.On姓名Changing(value);
					this.SendPropertyChanging();
					this._姓名 = value;
					this.SendPropertyChanged("姓名");
					this.On姓名Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Phone", Storage="_电话", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=3)]
		public string 电话
		{
			get
			{
				return this._电话;
			}
			set
			{
				if ((this._电话 != value))
				{
					this.On电话Changing(value);
					this.SendPropertyChanging();
					this._电话 = value;
					this.SendPropertyChanged("电话");
					this.On电话Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Mail", Storage="_邮件地址", DbType="NVarChar(50)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=4)]
		public string 邮件地址
		{
			get
			{
				return this._邮件地址;
			}
			set
			{
				if ((this._邮件地址 != value))
				{
					this.On邮件地址Changing(value);
					this.SendPropertyChanging();
					this._邮件地址 = value;
					this.SendPropertyChanged("邮件地址");
					this.On邮件地址Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="permission", Storage="_权限", DbType="Int NOT NULL")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=5)]
		public int 权限
		{
			get
			{
				return this._权限;
			}
			set
			{
				if ((this._权限 != value))
				{
					this.On权限Changing(value);
					this.SendPropertyChanging();
					this._权限 = value;
					this.SendPropertyChanged("权限");
					this.On权限Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PersonalAccount", Storage="_帐号", CanBeNull=false)]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=6)]
		public string 帐号
		{
			get
			{
				return this._帐号;
			}
			set
			{
				if ((this._帐号 != value))
				{
					this.On帐号Changing(value);
					this.SendPropertyChanging();
					this._帐号 = value;
					this.SendPropertyChanged("帐号");
					this.On帐号Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="个人_服务", Storage="_服务", ThisKey="个人编码", OtherKey="个人编码")]
		internal EntitySet<服务> 服务
		{
			get
			{
				return this._服务;
			}
			set
			{
				this._服务.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_服务(服务 entity)
		{
			this.SendPropertyChanging();
			entity.个人 = this;
		}
		
		private void detach_服务(服务 entity)
		{
			this.SendPropertyChanging();
			entity.个人 = null;
		}
		
		private void Initialize()
		{
			this._服务 = new EntitySet<服务>(new Action<服务>(this.attach_服务), new Action<服务>(this.detach_服务));
			OnCreated();
		}
		
		[global::System.Runtime.Serialization.OnDeserializingAttribute()]
		[global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.EsbView_UDDI")]
	[global::System.Runtime.Serialization.DataContractAttribute()]
	public partial class 服务视图
	{
		
		private string _业务系统;
		
		private string _业务系统描述;
		
		private string _服务名称;
		
		private string _服务描述;
		
		private string _地址;
		
		private System.Nullable<int> _状态;
		
		private System.Nullable<int> _类型;
		
		private string _方法名称;
		
		private string _调用约束范例;
		
		public 服务视图()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_业务系统", DbType="NVarChar(50)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=1)]
		public string 业务系统
		{
			get
			{
				return this._业务系统;
			}
			set
			{
				if ((this._业务系统 != value))
				{
					this._业务系统 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_业务系统描述", DbType="NVarChar(200)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=2)]
		public string 业务系统描述
		{
			get
			{
				return this._业务系统描述;
			}
			set
			{
				if ((this._业务系统描述 != value))
				{
					this._业务系统描述 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_服务名称", DbType="NVarChar(50)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=3)]
		public string 服务名称
		{
			get
			{
				return this._服务名称;
			}
			set
			{
				if ((this._服务名称 != value))
				{
					this._服务名称 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_服务描述", DbType="NVarChar(254)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=4)]
		public string 服务描述
		{
			get
			{
				return this._服务描述;
			}
			set
			{
				if ((this._服务描述 != value))
				{
					this._服务描述 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_地址", DbType="NVarChar(254)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=5)]
		public string 地址
		{
			get
			{
				return this._地址;
			}
			set
			{
				if ((this._地址 != value))
				{
					this._地址 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_状态", DbType="Int")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=6)]
		public System.Nullable<int> 状态
		{
			get
			{
				return this._状态;
			}
			set
			{
				if ((this._状态 != value))
				{
					this._状态 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_类型", DbType="Int")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=7)]
		public System.Nullable<int> 类型
		{
			get
			{
				return this._类型;
			}
			set
			{
				if ((this._类型 != value))
				{
					this._类型 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_方法名称", DbType="NVarChar(254)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=8)]
		public string 方法名称
		{
			get
			{
				return this._方法名称;
			}
			set
			{
				if ((this._方法名称 != value))
				{
					this._方法名称 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_调用约束范例", DbType="NVarChar(4000)")]
		[global::System.Runtime.Serialization.DataMemberAttribute(Order=9)]
		public string 调用约束范例
		{
			get
			{
				return this._调用约束范例;
			}
			set
			{
				if ((this._调用约束范例 != value))
				{
					this._调用约束范例 = value;
				}
			}
		}
	}
}
#pragma warning restore 1591