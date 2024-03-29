// 
// Code generated by a template.
//
using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;


namespace Shared.Proxies 
{
	
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "<Pending>")]
	[EntityLogicalName("businessunit")]
	public partial class BusinessUnit : CCLLC.CDS.Sdk.EarlyBound.EntityProxy
	{
		public const string EntityLogicalName = "businessunit";
		public const string PrimaryIdAttribute = "businessunitid";
		public const string PrimaryNameAttribute = "name";

		public BusinessUnit()
			: base("businessunit") {}

		#region Local OptionSet Enumerations

		#pragma warning disable IDE1006 // Naming Styles
		public enum eAddress1AddressTypeCode { DefaultValue=1, }
		public enum eAddress1ShippingMethodCode { DefaultValue=1, }
		public enum eAddress2AddressTypeCode { DefaultValue=1, }
		public enum eAddress2ShippingMethodCode { DefaultValue=1, }
		#pragma warning restore IDE1006 // Naming Styles

		#endregion Local OptionSet Enumerations

		#region Late Bound Field Constants

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
		public static class Fields
		{			
			public const string Id = "businessunitid";
			public const string Address1AddressId = "address1_addressid";
			public const string Address1AddressTypeCode = "address1_addresstypecode";
			public const string Address1AddressTypeCodeName = "address1_addresstypecodename";
			public const string Address1City = "address1_city";
			public const string Address1Country = "address1_country";
			public const string Address1County = "address1_county";
			public const string Address1Fax = "address1_fax";
			public const string Address1Latitude = "address1_latitude";
			public const string Address1Line1 = "address1_line1";
			public const string Address1Line2 = "address1_line2";
			public const string Address1Line3 = "address1_line3";
			public const string Address1Longitude = "address1_longitude";
			public const string Address1Name = "address1_name";
			public const string Address1PostalCode = "address1_postalcode";
			public const string Address1PostOfficeBox = "address1_postofficebox";
			public const string Address1ShippingMethodCode = "address1_shippingmethodcode";
			public const string Address1ShippingMethodCodeName = "address1_shippingmethodcodename";
			public const string Address1StateOrProvince = "address1_stateorprovince";
			public const string Address1Telephone1 = "address1_telephone1";
			public const string Address1Telephone2 = "address1_telephone2";
			public const string Address1Telephone3 = "address1_telephone3";
			public const string Address1UPSZone = "address1_upszone";
			public const string Address1UTCOffset = "address1_utcoffset";
			public const string Address2AddressId = "address2_addressid";
			public const string Address2AddressTypeCode = "address2_addresstypecode";
			public const string Address2AddressTypeCodeName = "address2_addresstypecodename";
			public const string Address2City = "address2_city";
			public const string Address2Country = "address2_country";
			public const string Address2County = "address2_county";
			public const string Address2Fax = "address2_fax";
			public const string Address2Latitude = "address2_latitude";
			public const string Address2Line1 = "address2_line1";
			public const string Address2Line2 = "address2_line2";
			public const string Address2Line3 = "address2_line3";
			public const string Address2Longitude = "address2_longitude";
			public const string Address2Name = "address2_name";
			public const string Address2PostalCode = "address2_postalcode";
			public const string Address2PostOfficeBox = "address2_postofficebox";
			public const string Address2ShippingMethodCode = "address2_shippingmethodcode";
			public const string Address2ShippingMethodCodeName = "address2_shippingmethodcodename";
			public const string Address2StateOrProvince = "address2_stateorprovince";
			public const string Address2Telephone1 = "address2_telephone1";
			public const string Address2Telephone2 = "address2_telephone2";
			public const string Address2Telephone3 = "address2_telephone3";
			public const string Address2UPSZone = "address2_upszone";
			public const string Address2UTCOffset = "address2_utcoffset";
			public const string BusinessUnitId = "businessunitid";
			public const string CalendarId = "calendarid";
			public const string CostCenter = "costcenter";
			public const string CreatedBy = "createdby";
			public const string CreatedByName = "createdbyname";
			public const string CreatedByYomiName = "createdbyyominame";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CreatedOnBehalfByName = "createdonbehalfbyname";
			public const string CreatedOnBehalfByYomiName = "createdonbehalfbyyominame";
			public const string CreditLimit = "creditlimit";
			public const string Description = "description";
			public const string DisabledReason = "disabledreason";
			public const string DivisionName = "divisionname";
			public const string EMailAddress = "emailaddress";
			public const string ExchangeRate = "exchangerate";
			public const string FileAsName = "fileasname";
			public const string FtpSiteUrl = "ftpsiteurl";
			public const string ImportSequenceNumber = "importsequencenumber";
			public const string InheritanceMask = "inheritancemask";
			public const string IsDisabled = "isdisabled";
			public const string IsDisabledName = "isdisabledname";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedByName = "modifiedbyname";
			public const string ModifiedByYomiName = "modifiedbyyominame";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string ModifiedOnBehalfByName = "modifiedonbehalfbyname";
			public const string ModifiedOnBehalfByYomiName = "modifiedonbehalfbyyominame";
			public const string Name = "name";
			public const string OrganizationId = "organizationid";
			public const string OrganizationIdName = "organizationidname";
			public const string OverriddenCreatedOn = "overriddencreatedon";
			public const string ParentBusinessUnitId = "parentbusinessunitid";
			public const string ParentBusinessUnitIdName = "parentbusinessunitidname";
			public const string Picture = "picture";
			public const string StockExchange = "stockexchange";
			public const string TickerSymbol = "tickersymbol";
			public const string TransactionCurrencyId = "transactioncurrencyid";
			public const string TransactionCurrencyIdName = "transactioncurrencyidname";
			public const string UserGroupId = "usergroupid";
			public const string UTCOffset = "utcoffset";
			public const string VersionNumber = "versionnumber";
			public const string WebSiteUrl = "websiteurl";
			public const string WorkflowSuspended = "workflowsuspended";
			public const string WorkflowSuspendedName = "workflowsuspendedname";
		}

		#endregion Late Bound Field Constants

		[AttributeLogicalName("businessunitid")]
		public override Guid Id
		{
			get => base.Id; 
			set 
			{
				BusinessUnitId = value;
				base.Id = value;
			}
		}

		[AttributeLogicalName("address1_addressid")]
		public virtual Guid Address1AddressId
		{
			get => GetPropertyValue<Guid>("address1_addressid"); 
			set => SetPropertyValue("address1_addressid", value); 
		}

		[AttributeLogicalName("address1_addresstypecode")]
		public virtual eAddress1AddressTypeCode? Address1AddressTypeCode
		{
			get => (eAddress1AddressTypeCode?)GetPropertyValue<OptionSetValue>("address1_addresstypecode")?.Value;
			set => SetPropertyValue("address1_addresstypecode", value is null ? null : new OptionSetValue((int)value)); 
		}

		[AttributeLogicalName("address1_addresstypecodename")]
		public virtual string Address1AddressTypeCodeName
		{
			get => GetPropertyValue<string>("address1_addresstypecodename"); 
		}

		[AttributeLogicalName("address1_city")]
		public virtual string Address1City
		{
			get => GetPropertyValue<string>("address1_city"); 
			set => SetPropertyValue("address1_city", value); 
		}

		[AttributeLogicalName("address1_country")]
		public virtual string Address1Country
		{
			get => GetPropertyValue<string>("address1_country"); 
			set => SetPropertyValue("address1_country", value); 
		}

		[AttributeLogicalName("address1_county")]
		public virtual string Address1County
		{
			get => GetPropertyValue<string>("address1_county"); 
			set => SetPropertyValue("address1_county", value); 
		}

		[AttributeLogicalName("address1_fax")]
		public virtual string Address1Fax
		{
			get => GetPropertyValue<string>("address1_fax"); 
			set => SetPropertyValue("address1_fax", value); 
		}

		[AttributeLogicalName("address1_latitude")]
		public virtual double? Address1Latitude
		{
			get => GetPropertyValue<double?>("address1_latitude"); 
			set => SetPropertyValue("address1_latitude", value); 
		}

		[AttributeLogicalName("address1_line1")]
		public virtual string Address1Line1
		{
			get => GetPropertyValue<string>("address1_line1"); 
			set => SetPropertyValue("address1_line1", value); 
		}

		[AttributeLogicalName("address1_line2")]
		public virtual string Address1Line2
		{
			get => GetPropertyValue<string>("address1_line2"); 
			set => SetPropertyValue("address1_line2", value); 
		}

		[AttributeLogicalName("address1_line3")]
		public virtual string Address1Line3
		{
			get => GetPropertyValue<string>("address1_line3"); 
			set => SetPropertyValue("address1_line3", value); 
		}

		[AttributeLogicalName("address1_longitude")]
		public virtual double? Address1Longitude
		{
			get => GetPropertyValue<double?>("address1_longitude"); 
			set => SetPropertyValue("address1_longitude", value); 
		}

		[AttributeLogicalName("address1_name")]
		public virtual string Address1Name
		{
			get => GetPropertyValue<string>("address1_name"); 
			set => SetPropertyValue("address1_name", value); 
		}

		[AttributeLogicalName("address1_postalcode")]
		public virtual string Address1PostalCode
		{
			get => GetPropertyValue<string>("address1_postalcode"); 
			set => SetPropertyValue("address1_postalcode", value); 
		}

		[AttributeLogicalName("address1_postofficebox")]
		public virtual string Address1PostOfficeBox
		{
			get => GetPropertyValue<string>("address1_postofficebox"); 
			set => SetPropertyValue("address1_postofficebox", value); 
		}

		[AttributeLogicalName("address1_shippingmethodcode")]
		public virtual eAddress1ShippingMethodCode? Address1ShippingMethodCode
		{
			get => (eAddress1ShippingMethodCode?)GetPropertyValue<OptionSetValue>("address1_shippingmethodcode")?.Value;
			set => SetPropertyValue("address1_shippingmethodcode", value is null ? null : new OptionSetValue((int)value)); 
		}

		[AttributeLogicalName("address1_shippingmethodcodename")]
		public virtual string Address1ShippingMethodCodeName
		{
			get => GetPropertyValue<string>("address1_shippingmethodcodename"); 
		}

		[AttributeLogicalName("address1_stateorprovince")]
		public virtual string Address1StateOrProvince
		{
			get => GetPropertyValue<string>("address1_stateorprovince"); 
			set => SetPropertyValue("address1_stateorprovince", value); 
		}

		[AttributeLogicalName("address1_telephone1")]
		public virtual string Address1Telephone1
		{
			get => GetPropertyValue<string>("address1_telephone1"); 
			set => SetPropertyValue("address1_telephone1", value); 
		}

		[AttributeLogicalName("address1_telephone2")]
		public virtual string Address1Telephone2
		{
			get => GetPropertyValue<string>("address1_telephone2"); 
			set => SetPropertyValue("address1_telephone2", value); 
		}

		[AttributeLogicalName("address1_telephone3")]
		public virtual string Address1Telephone3
		{
			get => GetPropertyValue<string>("address1_telephone3"); 
			set => SetPropertyValue("address1_telephone3", value); 
		}

		[AttributeLogicalName("address1_upszone")]
		public virtual string Address1UPSZone
		{
			get => GetPropertyValue<string>("address1_upszone"); 
			set => SetPropertyValue("address1_upszone", value); 
		}

		[AttributeLogicalName("address1_utcoffset")]
		public virtual int? Address1UTCOffset
		{
			get => GetPropertyValue<int?>("address1_utcoffset"); 
			set => SetPropertyValue("address1_utcoffset", value); 
		}

		[AttributeLogicalName("address2_addressid")]
		public virtual Guid Address2AddressId
		{
			get => GetPropertyValue<Guid>("address2_addressid"); 
			set => SetPropertyValue("address2_addressid", value); 
		}

		[AttributeLogicalName("address2_addresstypecode")]
		public virtual eAddress2AddressTypeCode? Address2AddressTypeCode
		{
			get => (eAddress2AddressTypeCode?)GetPropertyValue<OptionSetValue>("address2_addresstypecode")?.Value;
			set => SetPropertyValue("address2_addresstypecode", value is null ? null : new OptionSetValue((int)value)); 
		}

		[AttributeLogicalName("address2_addresstypecodename")]
		public virtual string Address2AddressTypeCodeName
		{
			get => GetPropertyValue<string>("address2_addresstypecodename"); 
		}

		[AttributeLogicalName("address2_city")]
		public virtual string Address2City
		{
			get => GetPropertyValue<string>("address2_city"); 
			set => SetPropertyValue("address2_city", value); 
		}

		[AttributeLogicalName("address2_country")]
		public virtual string Address2Country
		{
			get => GetPropertyValue<string>("address2_country"); 
			set => SetPropertyValue("address2_country", value); 
		}

		[AttributeLogicalName("address2_county")]
		public virtual string Address2County
		{
			get => GetPropertyValue<string>("address2_county"); 
			set => SetPropertyValue("address2_county", value); 
		}

		[AttributeLogicalName("address2_fax")]
		public virtual string Address2Fax
		{
			get => GetPropertyValue<string>("address2_fax"); 
			set => SetPropertyValue("address2_fax", value); 
		}

		[AttributeLogicalName("address2_latitude")]
		public virtual double? Address2Latitude
		{
			get => GetPropertyValue<double?>("address2_latitude"); 
			set => SetPropertyValue("address2_latitude", value); 
		}

		[AttributeLogicalName("address2_line1")]
		public virtual string Address2Line1
		{
			get => GetPropertyValue<string>("address2_line1"); 
			set => SetPropertyValue("address2_line1", value); 
		}

		[AttributeLogicalName("address2_line2")]
		public virtual string Address2Line2
		{
			get => GetPropertyValue<string>("address2_line2"); 
			set => SetPropertyValue("address2_line2", value); 
		}

		[AttributeLogicalName("address2_line3")]
		public virtual string Address2Line3
		{
			get => GetPropertyValue<string>("address2_line3"); 
			set => SetPropertyValue("address2_line3", value); 
		}

		[AttributeLogicalName("address2_longitude")]
		public virtual double? Address2Longitude
		{
			get => GetPropertyValue<double?>("address2_longitude"); 
			set => SetPropertyValue("address2_longitude", value); 
		}

		[AttributeLogicalName("address2_name")]
		public virtual string Address2Name
		{
			get => GetPropertyValue<string>("address2_name"); 
			set => SetPropertyValue("address2_name", value); 
		}

		[AttributeLogicalName("address2_postalcode")]
		public virtual string Address2PostalCode
		{
			get => GetPropertyValue<string>("address2_postalcode"); 
			set => SetPropertyValue("address2_postalcode", value); 
		}

		[AttributeLogicalName("address2_postofficebox")]
		public virtual string Address2PostOfficeBox
		{
			get => GetPropertyValue<string>("address2_postofficebox"); 
			set => SetPropertyValue("address2_postofficebox", value); 
		}

		[AttributeLogicalName("address2_shippingmethodcode")]
		public virtual eAddress2ShippingMethodCode? Address2ShippingMethodCode
		{
			get => (eAddress2ShippingMethodCode?)GetPropertyValue<OptionSetValue>("address2_shippingmethodcode")?.Value;
			set => SetPropertyValue("address2_shippingmethodcode", value is null ? null : new OptionSetValue((int)value)); 
		}

		[AttributeLogicalName("address2_shippingmethodcodename")]
		public virtual string Address2ShippingMethodCodeName
		{
			get => GetPropertyValue<string>("address2_shippingmethodcodename"); 
		}

		[AttributeLogicalName("address2_stateorprovince")]
		public virtual string Address2StateOrProvince
		{
			get => GetPropertyValue<string>("address2_stateorprovince"); 
			set => SetPropertyValue("address2_stateorprovince", value); 
		}

		[AttributeLogicalName("address2_telephone1")]
		public virtual string Address2Telephone1
		{
			get => GetPropertyValue<string>("address2_telephone1"); 
			set => SetPropertyValue("address2_telephone1", value); 
		}

		[AttributeLogicalName("address2_telephone2")]
		public virtual string Address2Telephone2
		{
			get => GetPropertyValue<string>("address2_telephone2"); 
			set => SetPropertyValue("address2_telephone2", value); 
		}

		[AttributeLogicalName("address2_telephone3")]
		public virtual string Address2Telephone3
		{
			get => GetPropertyValue<string>("address2_telephone3"); 
			set => SetPropertyValue("address2_telephone3", value); 
		}

		[AttributeLogicalName("address2_upszone")]
		public virtual string Address2UPSZone
		{
			get => GetPropertyValue<string>("address2_upszone"); 
			set => SetPropertyValue("address2_upszone", value); 
		}

		[AttributeLogicalName("address2_utcoffset")]
		public virtual int? Address2UTCOffset
		{
			get => GetPropertyValue<int?>("address2_utcoffset"); 
			set => SetPropertyValue("address2_utcoffset", value); 
		}

		[AttributeLogicalName("businessunitid")]
		public virtual Guid BusinessUnitId
		{
			get => GetPropertyValue<Guid>("businessunitid"); 
			set => SetPropertyValue("businessunitid", value); 
		}

		[AttributeLogicalName("calendarid")]
		public virtual EntityReference CalendarId
		{
			get => GetPropertyValue<EntityReference>("calendarid"); 
			set => SetPropertyValue("calendarid", value); 
		}

		[AttributeLogicalName("costcenter")]
		public virtual string CostCenter
		{
			get => GetPropertyValue<string>("costcenter"); 
			set => SetPropertyValue("costcenter", value); 
		}

		[AttributeLogicalName("createdby")]
		public virtual EntityReference CreatedBy
		{
			get => GetPropertyValue<EntityReference>("createdby"); 
		}

		[AttributeLogicalName("createdbyname")]
		public virtual string CreatedByName
		{
			get => GetPropertyValue<string>("createdbyname"); 
		}

		[AttributeLogicalName("createdbyyominame")]
		public virtual string CreatedByYomiName
		{
			get => GetPropertyValue<string>("createdbyyominame"); 
		}

		[AttributeLogicalName("createdon")]
		public virtual DateTime? CreatedOn
		{
			get => GetPropertyValue<DateTime?>("createdon"); 
		}

		[AttributeLogicalName("createdonbehalfby")]
		public virtual EntityReference CreatedOnBehalfBy
		{
			get => GetPropertyValue<EntityReference>("createdonbehalfby"); 
		}

		[AttributeLogicalName("createdonbehalfbyname")]
		public virtual string CreatedOnBehalfByName
		{
			get => GetPropertyValue<string>("createdonbehalfbyname"); 
		}

		[AttributeLogicalName("createdonbehalfbyyominame")]
		public virtual string CreatedOnBehalfByYomiName
		{
			get => GetPropertyValue<string>("createdonbehalfbyyominame"); 
		}

		[AttributeLogicalName("creditlimit")]
		public virtual double? CreditLimit
		{
			get => GetPropertyValue<double?>("creditlimit"); 
			set => SetPropertyValue("creditlimit", value); 
		}

		[AttributeLogicalName("description")]
		public virtual string Description
		{
			get => GetPropertyValue<string>("description"); 
			set => SetPropertyValue("description", value); 
		}

		[AttributeLogicalName("disabledreason")]
		public virtual string DisabledReason
		{
			get => GetPropertyValue<string>("disabledreason"); 
		}

		[AttributeLogicalName("divisionname")]
		public virtual string DivisionName
		{
			get => GetPropertyValue<string>("divisionname"); 
			set => SetPropertyValue("divisionname", value); 
		}

		[AttributeLogicalName("emailaddress")]
		public virtual string EMailAddress
		{
			get => GetPropertyValue<string>("emailaddress"); 
			set => SetPropertyValue("emailaddress", value); 
		}

		[AttributeLogicalName("exchangerate")]
		public virtual decimal? ExchangeRate
		{
			get => GetPropertyValue<decimal?>("exchangerate"); 
		}

		[AttributeLogicalName("fileasname")]
		public virtual string FileAsName
		{
			get => GetPropertyValue<string>("fileasname"); 
			set => SetPropertyValue("fileasname", value); 
		}

		[AttributeLogicalName("ftpsiteurl")]
		public virtual string FtpSiteUrl
		{
			get => GetPropertyValue<string>("ftpsiteurl"); 
			set => SetPropertyValue("ftpsiteurl", value); 
		}

		[AttributeLogicalName("importsequencenumber")]
		public virtual int? ImportSequenceNumber
		{
			get => GetPropertyValue<int?>("importsequencenumber"); 
			set => SetPropertyValue("importsequencenumber", value); 
		}

		[AttributeLogicalName("inheritancemask")]
		public virtual int? InheritanceMask
		{
			get => GetPropertyValue<int?>("inheritancemask"); 
			set => SetPropertyValue("inheritancemask", value); 
		}

		[AttributeLogicalName("isdisabled")]
		public virtual bool? IsDisabled
		{
			get => GetPropertyValue<bool?>("isdisabled"); 
			set => SetPropertyValue("isdisabled", value); 
		}

		[AttributeLogicalName("isdisabledname")]
		public virtual string IsDisabledName
		{
			get => GetPropertyValue<string>("isdisabledname"); 
		}

		[AttributeLogicalName("modifiedby")]
		public virtual EntityReference ModifiedBy
		{
			get => GetPropertyValue<EntityReference>("modifiedby"); 
		}

		[AttributeLogicalName("modifiedbyname")]
		public virtual string ModifiedByName
		{
			get => GetPropertyValue<string>("modifiedbyname"); 
		}

		[AttributeLogicalName("modifiedbyyominame")]
		public virtual string ModifiedByYomiName
		{
			get => GetPropertyValue<string>("modifiedbyyominame"); 
		}

		[AttributeLogicalName("modifiedon")]
		public virtual DateTime? ModifiedOn
		{
			get => GetPropertyValue<DateTime?>("modifiedon"); 
		}

		[AttributeLogicalName("modifiedonbehalfby")]
		public virtual EntityReference ModifiedOnBehalfBy
		{
			get => GetPropertyValue<EntityReference>("modifiedonbehalfby"); 
		}

		[AttributeLogicalName("modifiedonbehalfbyname")]
		public virtual string ModifiedOnBehalfByName
		{
			get => GetPropertyValue<string>("modifiedonbehalfbyname"); 
		}

		[AttributeLogicalName("modifiedonbehalfbyyominame")]
		public virtual string ModifiedOnBehalfByYomiName
		{
			get => GetPropertyValue<string>("modifiedonbehalfbyyominame"); 
		}

		[AttributeLogicalName("name")]
		public virtual string Name
		{
			get => GetPropertyValue<string>("name"); 
			set => SetPropertyValue("name", value); 
		}

		[AttributeLogicalName("organizationid")]
		public virtual EntityReference OrganizationId
		{
			get => GetPropertyValue<EntityReference>("organizationid"); 
		}

		[AttributeLogicalName("organizationidname")]
		public virtual string OrganizationIdName
		{
			get => GetPropertyValue<string>("organizationidname"); 
		}

		[AttributeLogicalName("overriddencreatedon")]
		public virtual DateTime? OverriddenCreatedOn
		{
			get => GetPropertyValue<DateTime?>("overriddencreatedon"); 
			set => SetPropertyValue("overriddencreatedon", value); 
		}

		[AttributeLogicalName("parentbusinessunitid")]
		public virtual EntityReference ParentBusinessUnitId
		{
			get => GetPropertyValue<EntityReference>("parentbusinessunitid"); 
			set => SetPropertyValue("parentbusinessunitid", value); 
		}

		[AttributeLogicalName("parentbusinessunitidname")]
		public virtual string ParentBusinessUnitIdName
		{
			get => GetPropertyValue<string>("parentbusinessunitidname"); 
		}

		[AttributeLogicalName("picture")]
		public virtual string Picture
		{
			get => GetPropertyValue<string>("picture"); 
			set => SetPropertyValue("picture", value); 
		}

		[AttributeLogicalName("stockexchange")]
		public virtual string StockExchange
		{
			get => GetPropertyValue<string>("stockexchange"); 
			set => SetPropertyValue("stockexchange", value); 
		}

		[AttributeLogicalName("tickersymbol")]
		public virtual string TickerSymbol
		{
			get => GetPropertyValue<string>("tickersymbol"); 
			set => SetPropertyValue("tickersymbol", value); 
		}

		[AttributeLogicalName("transactioncurrencyid")]
		public virtual EntityReference TransactionCurrencyId
		{
			get => GetPropertyValue<EntityReference>("transactioncurrencyid"); 
			set => SetPropertyValue("transactioncurrencyid", value); 
		}

		[AttributeLogicalName("transactioncurrencyidname")]
		public virtual string TransactionCurrencyIdName
		{
			get => GetPropertyValue<string>("transactioncurrencyidname"); 
		}

		[AttributeLogicalName("usergroupid")]
		public virtual Guid UserGroupId
		{
			get => GetPropertyValue<Guid>("usergroupid"); 
		}

		[AttributeLogicalName("utcoffset")]
		public virtual int? UTCOffset
		{
			get => GetPropertyValue<int?>("utcoffset"); 
			set => SetPropertyValue("utcoffset", value); 
		}

		[AttributeLogicalName("versionnumber")]
		public virtual int? VersionNumber
		{
			get => GetPropertyValue<int?>("versionnumber"); 
		}

		[AttributeLogicalName("websiteurl")]
		public virtual string WebSiteUrl
		{
			get => GetPropertyValue<string>("websiteurl"); 
			set => SetPropertyValue("websiteurl", value); 
		}

		[AttributeLogicalName("workflowsuspended")]
		public virtual bool? WorkflowSuspended
		{
			get => GetPropertyValue<bool?>("workflowsuspended"); 
			set => SetPropertyValue("workflowsuspended", value); 
		}

		[AttributeLogicalName("workflowsuspendedname")]
		public virtual string WorkflowSuspendedName
		{
			get => GetPropertyValue<string>("workflowsuspendedname"); 
		}

	

	
	
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
	[AttributeLogicalName("parentbusinessunitid")]
	[RelationshipSchemaName("business_unit_parent_business_unit")]
	public BusinessUnit business_unit_parent_business_unit
	{
		get { return this.GetRelatedEntity<BusinessUnit>("business_unit_parent_business_unit",null); }
	}


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
	[AttributeLogicalName("createdby")]
	[RelationshipSchemaName("lk_businessunitbase_createdby")]
	public SystemUser lk_businessunitbase_createdby
	{
		get { return this.GetRelatedEntity<SystemUser>("lk_businessunitbase_createdby",null); }
	}


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
	[AttributeLogicalName("modifiedby")]
	[RelationshipSchemaName("lk_businessunitbase_modifiedby")]
	public SystemUser lk_businessunitbase_modifiedby
	{
		get { return this.GetRelatedEntity<SystemUser>("lk_businessunitbase_modifiedby",null); }
	}


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
	[AttributeLogicalName("createdonbehalfby")]
	[RelationshipSchemaName("lk_businessunit_createdonbehalfby")]
	public SystemUser lk_businessunit_createdonbehalfby
	{
		get { return this.GetRelatedEntity<SystemUser>("lk_businessunit_createdonbehalfby",null); }
	}


		[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
	[AttributeLogicalName("modifiedonbehalfby")]
	[RelationshipSchemaName("lk_businessunit_modifiedonbehalfby")]
	public SystemUser lk_businessunit_modifiedonbehalfby
	{
		get { return this.GetRelatedEntity<SystemUser>("lk_businessunit_modifiedonbehalfby",null); }
	}


	
	    }
}
