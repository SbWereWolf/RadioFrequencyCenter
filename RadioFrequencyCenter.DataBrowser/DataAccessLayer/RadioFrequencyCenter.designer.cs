﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RadioFrequencyCenter.DataBrowser.DataAccessLayer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="RadioFrequencyCenter")]
	public partial class RadioFrequencyCenterDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertBroadcastStations(BroadcastStations instance);
    partial void UpdateBroadcastStations(BroadcastStations instance);
    partial void DeleteBroadcastStations(BroadcastStations instance);
    partial void InsertBroadcastFrequencies(BroadcastFrequencies instance);
    partial void UpdateBroadcastFrequencies(BroadcastFrequencies instance);
    partial void DeleteBroadcastFrequencies(BroadcastFrequencies instance);
    #endregion
		
		public RadioFrequencyCenterDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["RadioFrequencyCenterConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public RadioFrequencyCenterDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RadioFrequencyCenterDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RadioFrequencyCenterDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RadioFrequencyCenterDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<BroadcastStations> BroadcastStations
		{
			get
			{
				return this.GetTable<BroadcastStations>();
			}
		}
		
		public System.Data.Linq.Table<BroadcastFrequencies> BroadcastFrequencies
		{
			get
			{
				return this.GetTable<BroadcastFrequencies>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.BroadcastStations")]
	public partial class BroadcastStations : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _ID_RES;
		
		private string _ZAV_NUM;
		
		private string _NUM_SVID;
		
		private System.Nullable<System.DateTime> _DATE_SVID;
		
		private System.Nullable<System.DateTime> _SROK_SVID;
		
		private System.Nullable<double> _REGION;
		
		private string _LAT;
		
		private string _LONG;
		
		private string _IDS;
		
		private string _MAC;
		
		private System.Nullable<System.DateTimeOffset> _DEL_DATE;
		
		private System.Nullable<System.DateTimeOffset> _UPDATE_DATE;
		
		private EntitySet<BroadcastFrequencies> _BroadcastFrequencies;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnID_RESChanging(long value);
    partial void OnID_RESChanged();
    partial void OnZAV_NUMChanging(string value);
    partial void OnZAV_NUMChanged();
    partial void OnNUM_SVIDChanging(string value);
    partial void OnNUM_SVIDChanged();
    partial void OnDATE_SVIDChanging(System.Nullable<System.DateTime> value);
    partial void OnDATE_SVIDChanged();
    partial void OnSROK_SVIDChanging(System.Nullable<System.DateTime> value);
    partial void OnSROK_SVIDChanged();
    partial void OnREGIONChanging(System.Nullable<double> value);
    partial void OnREGIONChanged();
    partial void OnLATChanging(string value);
    partial void OnLATChanged();
    partial void OnLONGChanging(string value);
    partial void OnLONGChanged();
    partial void OnIDSChanging(string value);
    partial void OnIDSChanged();
    partial void OnMACChanging(string value);
    partial void OnMACChanged();
    partial void OnDEL_DATEChanging(System.Nullable<System.DateTimeOffset> value);
    partial void OnDEL_DATEChanged();
    partial void OnUPDATE_DATEChanging(System.Nullable<System.DateTimeOffset> value);
    partial void OnUPDATE_DATEChanged();
    #endregion
		
		public BroadcastStations()
		{
			this._BroadcastFrequencies = new EntitySet<BroadcastFrequencies>(new Action<BroadcastFrequencies>(this.attach_BroadcastFrequencies), new Action<BroadcastFrequencies>(this.detach_BroadcastFrequencies));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_RES", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long ID_RES
		{
			get
			{
				return this._ID_RES;
			}
			set
			{
				if ((this._ID_RES != value))
				{
					this.OnID_RESChanging(value);
					this.SendPropertyChanging();
					this._ID_RES = value;
					this.SendPropertyChanged("ID_RES");
					this.OnID_RESChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ZAV_NUM", DbType="VarChar(256)")]
		public string ZAV_NUM
		{
			get
			{
				return this._ZAV_NUM;
			}
			set
			{
				if ((this._ZAV_NUM != value))
				{
					this.OnZAV_NUMChanging(value);
					this.SendPropertyChanging();
					this._ZAV_NUM = value;
					this.SendPropertyChanged("ZAV_NUM");
					this.OnZAV_NUMChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NUM_SVID", DbType="VarChar(32)")]
		public string NUM_SVID
		{
			get
			{
				return this._NUM_SVID;
			}
			set
			{
				if ((this._NUM_SVID != value))
				{
					this.OnNUM_SVIDChanging(value);
					this.SendPropertyChanging();
					this._NUM_SVID = value;
					this.SendPropertyChanged("NUM_SVID");
					this.OnNUM_SVIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DATE_SVID", DbType="Date")]
		public System.Nullable<System.DateTime> DATE_SVID
		{
			get
			{
				return this._DATE_SVID;
			}
			set
			{
				if ((this._DATE_SVID != value))
				{
					this.OnDATE_SVIDChanging(value);
					this.SendPropertyChanging();
					this._DATE_SVID = value;
					this.SendPropertyChanged("DATE_SVID");
					this.OnDATE_SVIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SROK_SVID", DbType="Date")]
		public System.Nullable<System.DateTime> SROK_SVID
		{
			get
			{
				return this._SROK_SVID;
			}
			set
			{
				if ((this._SROK_SVID != value))
				{
					this.OnSROK_SVIDChanging(value);
					this.SendPropertyChanging();
					this._SROK_SVID = value;
					this.SendPropertyChanged("SROK_SVID");
					this.OnSROK_SVIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_REGION", DbType="Real")]
		public System.Nullable<double> REGION
		{
			get
			{
				return this._REGION;
			}
			set
			{
				if ((this._REGION != value))
				{
					this.OnREGIONChanging(value);
					this.SendPropertyChanging();
					this._REGION = value;
					this.SendPropertyChanged("REGION");
					this.OnREGIONChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LAT", DbType="VarChar(8)")]
		public string LAT
		{
			get
			{
				return this._LAT;
			}
			set
			{
				if ((this._LAT != value))
				{
					this.OnLATChanging(value);
					this.SendPropertyChanging();
					this._LAT = value;
					this.SendPropertyChanged("LAT");
					this.OnLATChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LONG", DbType="VarChar(8)")]
		public string LONG
		{
			get
			{
				return this._LONG;
			}
			set
			{
				if ((this._LONG != value))
				{
					this.OnLONGChanging(value);
					this.SendPropertyChanging();
					this._LONG = value;
					this.SendPropertyChanged("LONG");
					this.OnLONGChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IDS", DbType="VarChar(512)")]
		public string IDS
		{
			get
			{
				return this._IDS;
			}
			set
			{
				if ((this._IDS != value))
				{
					this.OnIDSChanging(value);
					this.SendPropertyChanging();
					this._IDS = value;
					this.SendPropertyChanged("IDS");
					this.OnIDSChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MAC", DbType="VarChar(255)")]
		public string MAC
		{
			get
			{
				return this._MAC;
			}
			set
			{
				if ((this._MAC != value))
				{
					this.OnMACChanging(value);
					this.SendPropertyChanging();
					this._MAC = value;
					this.SendPropertyChanged("MAC");
					this.OnMACChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DEL_DATE", DbType="DateTimeOffset")]
		public System.Nullable<System.DateTimeOffset> DEL_DATE
		{
			get
			{
				return this._DEL_DATE;
			}
			set
			{
				if ((this._DEL_DATE != value))
				{
					this.OnDEL_DATEChanging(value);
					this.SendPropertyChanging();
					this._DEL_DATE = value;
					this.SendPropertyChanged("DEL_DATE");
					this.OnDEL_DATEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UPDATE_DATE", DbType="DateTimeOffset")]
		public System.Nullable<System.DateTimeOffset> UPDATE_DATE
		{
			get
			{
				return this._UPDATE_DATE;
			}
			set
			{
				if ((this._UPDATE_DATE != value))
				{
					this.OnUPDATE_DATEChanging(value);
					this.SendPropertyChanging();
					this._UPDATE_DATE = value;
					this.SendPropertyChanged("UPDATE_DATE");
					this.OnUPDATE_DATEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="BroadcastStations_BroadcastFrequencies", Storage="_BroadcastFrequencies", ThisKey="ID_RES", OtherKey="RES")]
		public EntitySet<BroadcastFrequencies> BroadcastFrequencies
		{
			get
			{
				return this._BroadcastFrequencies;
			}
			set
			{
				this._BroadcastFrequencies.Assign(value);
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
		
		private void attach_BroadcastFrequencies(BroadcastFrequencies entity)
		{
			this.SendPropertyChanging();
			entity.BroadcastStations = this;
		}
		
		private void detach_BroadcastFrequencies(BroadcastFrequencies entity)
		{
			this.SendPropertyChanging();
			entity.BroadcastStations = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.BroadcastFrequencies")]
	public partial class BroadcastFrequencies : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _ID_F;
		
		private long _RES;
		
		private System.Nullable<double> _TN;
		
		private System.Nullable<double> _RN;
		
		private EntityRef<BroadcastStations> _BroadcastStation;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnID_FChanging(long value);
    partial void OnID_FChanged();
    partial void OnRESChanging(long value);
    partial void OnRESChanged();
    partial void OnTNChanging(System.Nullable<double> value);
    partial void OnTNChanged();
    partial void OnRNChanging(System.Nullable<double> value);
    partial void OnRNChanged();
    #endregion
		
		public BroadcastFrequencies()
		{
			this._BroadcastStation = default(EntityRef<BroadcastStations>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID_F", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long ID_F
		{
			get
			{
				return this._ID_F;
			}
			set
			{
				if ((this._ID_F != value))
				{
					this.OnID_FChanging(value);
					this.SendPropertyChanging();
					this._ID_F = value;
					this.SendPropertyChanged("ID_F");
					this.OnID_FChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RES", DbType="BigInt NOT NULL")]
		public long RES
		{
			get
			{
				return this._RES;
			}
			set
			{
				if ((this._RES != value))
				{
					if (this._BroadcastStation.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRESChanging(value);
					this.SendPropertyChanging();
					this._RES = value;
					this.SendPropertyChanged("RES");
					this.OnRESChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TN", DbType="Real")]
		public System.Nullable<double> TN
		{
			get
			{
				return this._TN;
			}
			set
			{
				if ((this._TN != value))
				{
					this.OnTNChanging(value);
					this.SendPropertyChanging();
					this._TN = value;
					this.SendPropertyChanged("TN");
					this.OnTNChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RN", DbType="Real")]
		public System.Nullable<double> RN
		{
			get
			{
				return this._RN;
			}
			set
			{
				if ((this._RN != value))
				{
					this.OnRNChanging(value);
					this.SendPropertyChanging();
					this._RN = value;
					this.SendPropertyChanged("RN");
					this.OnRNChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="BroadcastStations_BroadcastFrequencies", Storage="_BroadcastStation", ThisKey="RES", OtherKey="ID_RES", IsForeignKey=true)]
		public BroadcastStations BroadcastStations
		{
			get
			{
				return this._BroadcastStation.Entity;
			}
			set
			{
				BroadcastStations previousValue = this._BroadcastStation.Entity;
				if (((previousValue != value) 
							|| (this._BroadcastStation.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._BroadcastStation.Entity = null;
						previousValue.BroadcastFrequencies.Remove(this);
					}
					this._BroadcastStation.Entity = value;
					if ((value != null))
					{
						value.BroadcastFrequencies.Add(this);
						this._RES = value.ID_RES;
					}
					else
					{
						this._RES = default(long);
					}
					this.SendPropertyChanged("BroadcastStations");
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
	}
}
#pragma warning restore 1591
