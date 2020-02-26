namespace Crm
{
    [System.Runtime.Serialization.DataContractAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "8.1.0.7711")]
    public enum Gbp_customaddressState
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Active = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Inactive = 1,
    }

    /// <summary>
    ///
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("gbp_customaddress")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "8.1.0.7711")]
    public partial class gbp_customaddress : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public gbp_customaddress() :
                base(EntityLogicalName)
        {
        }

        public const string EntityLogicalName = "gbp_customaddress";

        public const int EntityTypeCode = 10020;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

        private void OnPropertyChanged(string propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnPropertyChanging(string propertyName)
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Unique identifier of the user who created the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdby");
            }
        }

        /// <summary>
        /// Date and time when the record was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdon")]
        public System.Nullable<System.DateTime> CreatedOn
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>("createdon");
            }
        }

        /// <summary>
        /// Unique identifier of the delegate user who created the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdonbehalfby");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_account")]
        public Microsoft.Xrm.Sdk.EntityReference gbp_account
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("gbp_account");
            }
            set
            {
                this.OnPropertyChanging("gbp_account");
                this.SetAttributeValue("gbp_account", value);
                this.OnPropertyChanged("gbp_account");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_addresstype")]
        public Microsoft.Xrm.Sdk.OptionSetValue gbp_addresstype
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("gbp_addresstype");
            }
            set
            {
                this.OnPropertyChanging("gbp_addresstype");
                this.SetAttributeValue("gbp_addresstype", value);
                this.OnPropertyChanged("gbp_addresstype");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_addrline1")]
        public string gbp_addrline1
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_addrline1");
            }
            set
            {
                this.OnPropertyChanging("gbp_addrline1");
                this.SetAttributeValue("gbp_addrline1", value);
                this.OnPropertyChanged("gbp_addrline1");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_addrline2")]
        public string gbp_addrline2
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_addrline2");
            }
            set
            {
                this.OnPropertyChanging("gbp_addrline2");
                this.SetAttributeValue("gbp_addrline2", value);
                this.OnPropertyChanged("gbp_addrline2");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_addrline3")]
        public string gbp_addrline3
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_addrline3");
            }
            set
            {
                this.OnPropertyChanging("gbp_addrline3");
                this.SetAttributeValue("gbp_addrline3", value);
                this.OnPropertyChanged("gbp_addrline3");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_alternativeorgs")]
        public string gbp_alternativeorgs
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_alternativeorgs");
            }
            set
            {
                this.OnPropertyChanging("gbp_alternativeorgs");
                this.SetAttributeValue("gbp_alternativeorgs", value);
                this.OnPropertyChanged("gbp_alternativeorgs");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_city")]
        public string gbp_city
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_city");
            }
            set
            {
                this.OnPropertyChanging("gbp_city");
                this.SetAttributeValue("gbp_city", value);
                this.OnPropertyChanged("gbp_city");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_contact")]
        public Microsoft.Xrm.Sdk.EntityReference gbp_contact
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("gbp_contact");
            }
            set
            {
                this.OnPropertyChanging("gbp_contact");
                this.SetAttributeValue("gbp_contact", value);
                this.OnPropertyChanged("gbp_contact");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_country")]
        public Microsoft.Xrm.Sdk.EntityReference gbp_country
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("gbp_country");
            }
            set
            {
                this.OnPropertyChanging("gbp_country");
                this.SetAttributeValue("gbp_country", value);
                this.OnPropertyChanged("gbp_country");
            }
        }

        /// <summary>
        /// Unique identifier for entity instances
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_customaddressid")]
        public System.Nullable<System.Guid> gbp_customaddressId
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>("gbp_customaddressid");
            }
            set
            {
                this.OnPropertyChanging("gbp_customaddressId");
                this.SetAttributeValue("gbp_customaddressid", value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged("gbp_customaddressId");
            }
        }

        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_customaddressid")]
        public override System.Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                this.gbp_customaddressId = value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_departmentname")]
        public string gbp_departmentname
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_departmentname");
            }
            set
            {
                this.OnPropertyChanging("gbp_departmentname");
                this.SetAttributeValue("gbp_departmentname", value);
                this.OnPropertyChanged("gbp_departmentname");
            }
        }

        /// <summary>
        /// The name of the custom entity.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_name")]
        public string gbp_name
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_name");
            }
            set
            {
                this.OnPropertyChanging("gbp_name");
                this.SetAttributeValue("gbp_name", value);
                this.OnPropertyChanged("gbp_name");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_noswitchboard")]
        public string gbp_noswitchboard
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_noswitchboard");
            }
            set
            {
                this.OnPropertyChanging("gbp_noswitchboard");
                this.SetAttributeValue("gbp_noswitchboard", value);
                this.OnPropertyChanged("gbp_noswitchboard");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_postalcode")]
        public string gbp_postalcode
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_postalcode");
            }
            set
            {
                this.OnPropertyChanging("gbp_postalcode");
                this.SetAttributeValue("gbp_postalcode", value);
                this.OnPropertyChanged("gbp_postalcode");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_region")]
        public Microsoft.Xrm.Sdk.EntityReference gbp_region
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("gbp_region");
            }
            set
            {
                this.OnPropertyChanging("gbp_region");
                this.SetAttributeValue("gbp_region", value);
                this.OnPropertyChanged("gbp_region");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_status")]
        public Microsoft.Xrm.Sdk.OptionSetValue gbp_status
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("gbp_status");
            }
            set
            {
                this.OnPropertyChanging("gbp_status");
                this.SetAttributeValue("gbp_status", value);
                this.OnPropertyChanged("gbp_status");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_usmailstop")]
        public string gbp_usmailstop
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_usmailstop");
            }
            set
            {
                this.OnPropertyChanging("gbp_usmailstop");
                this.SetAttributeValue("gbp_usmailstop", value);
                this.OnPropertyChanged("gbp_usmailstop");
            }
        }

        /// <summary>
        /// Sequence number of the import that created this record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importsequencenumber")]
        public System.Nullable<int> ImportSequenceNumber
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>("importsequencenumber");
            }
            set
            {
                this.OnPropertyChanging("ImportSequenceNumber");
                this.SetAttributeValue("importsequencenumber", value);
                this.OnPropertyChanged("ImportSequenceNumber");
            }
        }

        /// <summary>
        /// Unique identifier of the user who modified the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedby");
            }
        }

        /// <summary>
        /// Date and time when the record was modified.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedon")]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>("modifiedon");
            }
        }

        /// <summary>
        /// Unique identifier of the delegate user who modified the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedonbehalfby");
            }
        }

        /// <summary>
        /// Date and time that the record was migrated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overriddencreatedon")]
        public System.Nullable<System.DateTime> OverriddenCreatedOn
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>("overriddencreatedon");
            }
            set
            {
                this.OnPropertyChanging("OverriddenCreatedOn");
                this.SetAttributeValue("overriddencreatedon", value);
                this.OnPropertyChanged("OverriddenCreatedOn");
            }
        }

        /// <summary>
        /// Owner Id
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ownerid")]
        public Microsoft.Xrm.Sdk.EntityReference OwnerId
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("ownerid");
            }
            set
            {
                this.OnPropertyChanging("OwnerId");
                this.SetAttributeValue("ownerid", value);
                this.OnPropertyChanged("OwnerId");
            }
        }

        /// <summary>
        /// Unique identifier for the business unit that owns the record
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningbusinessunit")]
        public Microsoft.Xrm.Sdk.EntityReference OwningBusinessUnit
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owningbusinessunit");
            }
        }

        /// <summary>
        /// Unique identifier for the team that owns the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningteam")]
        public Microsoft.Xrm.Sdk.EntityReference OwningTeam
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owningteam");
            }
        }

        /// <summary>
        /// Unique identifier for the user that owns the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owninguser")]
        public Microsoft.Xrm.Sdk.EntityReference OwningUser
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owninguser");
            }
        }

        /// <summary>
        /// Status of the Custom Address
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecode")]
        public System.Nullable<Gbp_customaddressState> statecode
        {
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statecode");
                if ((optionSet != null))
                {
                    return ((Gbp_customaddressState)(System.Enum.ToObject(typeof(Gbp_customaddressState), optionSet.Value)));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnPropertyChanging("statecode");
                if ((value == null))
                {
                    this.SetAttributeValue("statecode", null);
                }
                else
                {
                    this.SetAttributeValue("statecode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged("statecode");
            }
        }

        /// <summary>
        /// Reason for the status of the Custom Address
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscode")]
        public Microsoft.Xrm.Sdk.OptionSetValue statuscode
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statuscode");
            }
            set
            {
                this.OnPropertyChanging("statuscode");
                this.SetAttributeValue("statuscode", value);
                this.OnPropertyChanged("statuscode");
            }
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("timezoneruleversionnumber")]
        public System.Nullable<int> TimeZoneRuleVersionNumber
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>("timezoneruleversionnumber");
            }
            set
            {
                this.OnPropertyChanging("TimeZoneRuleVersionNumber");
                this.SetAttributeValue("timezoneruleversionnumber", value);
                this.OnPropertyChanged("TimeZoneRuleVersionNumber");
            }
        }

        /// <summary>
        /// Time zone code that was in use when the record was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("utcconversiontimezonecode")]
        public System.Nullable<int> UTCConversionTimeZoneCode
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>("utcconversiontimezonecode");
            }
            set
            {
                this.OnPropertyChanging("UTCConversionTimeZoneCode");
                this.SetAttributeValue("utcconversiontimezonecode", value);
                this.OnPropertyChanged("UTCConversionTimeZoneCode");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
        public System.Nullable<long> VersionNumber
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>("versionnumber");
            }
        }

        /// <summary>
        /// 1:N gbp_customaddress_ActivityPointers
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_customaddress_ActivityPointers")]
        public System.Collections.Generic.IEnumerable<ActivityPointer> gbp_customaddress_ActivityPointers
        {
            get
            {
                return this.GetRelatedEntities<ActivityPointer>("gbp_customaddress_ActivityPointers", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_customaddress_ActivityPointers");
                this.SetRelatedEntities<ActivityPointer>("gbp_customaddress_ActivityPointers", null, value);
                this.OnPropertyChanged("gbp_customaddress_ActivityPointers");
            }
        }

        /// <summary>
        /// 1:N gbp_customaddress_Annotations
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_customaddress_Annotations")]
        public System.Collections.Generic.IEnumerable<Annotation> gbp_customaddress_Annotations
        {
            get
            {
                return this.GetRelatedEntities<Annotation>("gbp_customaddress_Annotations", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_customaddress_Annotations");
                this.SetRelatedEntities<Annotation>("gbp_customaddress_Annotations", null, value);
                this.OnPropertyChanged("gbp_customaddress_Annotations");
            }
        }

        /// <summary>
        /// 1:N gbp_customaddress_connections1
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_customaddress_connections1")]
        public System.Collections.Generic.IEnumerable<Connection> gbp_customaddress_connections1
        {
            get
            {
                return this.GetRelatedEntities<Connection>("gbp_customaddress_connections1", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_customaddress_connections1");
                this.SetRelatedEntities<Connection>("gbp_customaddress_connections1", null, value);
                this.OnPropertyChanged("gbp_customaddress_connections1");
            }
        }

        /// <summary>
        /// 1:N gbp_customaddress_connections2
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_customaddress_connections2")]
        public System.Collections.Generic.IEnumerable<Connection> gbp_customaddress_connections2
        {
            get
            {
                return this.GetRelatedEntities<Connection>("gbp_customaddress_connections2", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_customaddress_connections2");
                this.SetRelatedEntities<Connection>("gbp_customaddress_connections2", null, value);
                this.OnPropertyChanged("gbp_customaddress_connections2");
            }
        }

        /// <summary>
        /// 1:N gbp_customaddress_Emails
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_customaddress_Emails")]
        public System.Collections.Generic.IEnumerable<Email> gbp_customaddress_Emails
        {
            get
            {
                return this.GetRelatedEntities<Email>("gbp_customaddress_Emails", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_customaddress_Emails");
                this.SetRelatedEntities<Email>("gbp_customaddress_Emails", null, value);
                this.OnPropertyChanged("gbp_customaddress_Emails");
            }
        }

        /// <summary>
        /// 1:N gbp_customaddress_gbp_globenotes
        /// </summary>
        //[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_customaddress_gbp_globenotes")]
        //public System.Collections.Generic.IEnumerable<gbp_globenote> gbp_customaddress_gbp_globenotes
        //{
        //    get
        //    {
        //        return this.GetRelatedEntities<gbp_globenote>("gbp_customaddress_gbp_globenotes", null);
        //    }
        //    set
        //    {
        //        this.OnPropertyChanging("gbp_customaddress_gbp_globenotes");
        //        this.SetRelatedEntities<gbp_globenote>("gbp_customaddress_gbp_globenotes", null, value);
        //        this.OnPropertyChanged("gbp_customaddress_gbp_globenotes");
        //    }
        //}

        /// <summary>
        /// 1:N gbp_customaddress_PhoneCalls
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_customaddress_PhoneCalls")]
        public System.Collections.Generic.IEnumerable<PhoneCall> gbp_customaddress_PhoneCalls
        {
            get
            {
                return this.GetRelatedEntities<PhoneCall>("gbp_customaddress_PhoneCalls", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_customaddress_PhoneCalls");
                this.SetRelatedEntities<PhoneCall>("gbp_customaddress_PhoneCalls", null, value);
                this.OnPropertyChanged("gbp_customaddress_PhoneCalls");
            }
        }

        /// <summary>
        /// 1:N gbp_customaddress_ProcessSession
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_customaddress_ProcessSession")]
        public System.Collections.Generic.IEnumerable<ProcessSession> gbp_customaddress_ProcessSession
        {
            get
            {
                return this.GetRelatedEntities<ProcessSession>("gbp_customaddress_ProcessSession", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_customaddress_ProcessSession");
                this.SetRelatedEntities<ProcessSession>("gbp_customaddress_ProcessSession", null, value);
                this.OnPropertyChanged("gbp_customaddress_ProcessSession");
            }
        }

        /// <summary>
        /// 1:N gbp_customaddress_Tasks
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_customaddress_Tasks")]
        public System.Collections.Generic.IEnumerable<Task> gbp_customaddress_Tasks
        {
            get
            {
                return this.GetRelatedEntities<Task>("gbp_customaddress_Tasks", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_customaddress_Tasks");
                this.SetRelatedEntities<Task>("gbp_customaddress_Tasks", null, value);
                this.OnPropertyChanged("gbp_customaddress_Tasks");
            }
        }

        /// <summary>
        /// N:N gbp_gbp_customaddress_contact
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_gbp_customaddress_contact")]
        public System.Collections.Generic.IEnumerable<Contact> gbp_gbp_customaddress_contact
        {
            get
            {
                return this.GetRelatedEntities<Contact>("gbp_gbp_customaddress_contact", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_gbp_customaddress_contact");
                this.SetRelatedEntities<Contact>("gbp_gbp_customaddress_contact", null, value);
                this.OnPropertyChanged("gbp_gbp_customaddress_contact");
            }
        }

        /// <summary>
        /// N:1 gbp_account_gbp_customaddress_Account
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_account")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_account_gbp_customaddress_Account")]
        public Account gbp_account_gbp_customaddress_Account
        {
            get
            {
                return this.GetRelatedEntity<Account>("gbp_account_gbp_customaddress_Account", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_account_gbp_customaddress_Account");
                this.SetRelatedEntity<Account>("gbp_account_gbp_customaddress_Account", null, value);
                this.OnPropertyChanged("gbp_account_gbp_customaddress_Account");
            }
        }

        /// <summary>
        /// N:1 gbp_contact_gbp_customaddress_contact
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_contact")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_contact_gbp_customaddress_contact")]
        public Contact gbp_contact_gbp_customaddress_contact
        {
            get
            {
                return this.GetRelatedEntity<Contact>("gbp_contact_gbp_customaddress_contact", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_contact_gbp_customaddress_contact");
                this.SetRelatedEntity<Contact>("gbp_contact_gbp_customaddress_contact", null, value);
                this.OnPropertyChanged("gbp_contact_gbp_customaddress_contact");
            }
        }

        /// <summary>
        /// N:1 gbp_gbp_globecountry_gbp_customaddress_country
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_country")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_gbp_globecountry_gbp_customaddress_country")]
        public gbp_globecountry gbp_gbp_globecountry_gbp_customaddress_country
        {
            get
            {
                return this.GetRelatedEntity<gbp_globecountry>("gbp_gbp_globecountry_gbp_customaddress_country", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_gbp_globecountry_gbp_customaddress_country");
                this.SetRelatedEntity<gbp_globecountry>("gbp_gbp_globecountry_gbp_customaddress_country", null, value);
                this.OnPropertyChanged("gbp_gbp_globecountry_gbp_customaddress_country");
            }
        }

        /// <summary>
        /// N:1 gbp_gbp_globeregion_gbp_customaddress_region
        /// </summary>
        //[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_region")]
        //[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_gbp_globeregion_gbp_customaddress_region")]
        //public gbp_globeregion gbp_gbp_globeregion_gbp_customaddress_region
        //{
        //    get
        //    {
        //        return this.GetRelatedEntity<gbp_globeregion>("gbp_gbp_globeregion_gbp_customaddress_region", null);
        //    }
        //    set
        //    {
        //        this.OnPropertyChanging("gbp_gbp_globeregion_gbp_customaddress_region");
        //        this.SetRelatedEntity<gbp_globeregion>("gbp_gbp_globeregion_gbp_customaddress_region", null, value);
        //        this.OnPropertyChanged("gbp_gbp_globeregion_gbp_customaddress_region");
        //    }
        //}

        /// <summary>
        /// N:1 lk_gbp_customaddress_createdby
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_gbp_customaddress_createdby")]
        public SystemUser lk_gbp_customaddress_createdby
        {
            get
            {
                return this.GetRelatedEntity<SystemUser>("lk_gbp_customaddress_createdby", null);
            }
        }

        /// <summary>
        /// N:1 lk_gbp_customaddress_createdonbehalfby
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_gbp_customaddress_createdonbehalfby")]
        public SystemUser lk_gbp_customaddress_createdonbehalfby
        {
            get
            {
                return this.GetRelatedEntity<SystemUser>("lk_gbp_customaddress_createdonbehalfby", null);
            }
        }

        /// <summary>
        /// N:1 lk_gbp_customaddress_modifiedby
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_gbp_customaddress_modifiedby")]
        public SystemUser lk_gbp_customaddress_modifiedby
        {
            get
            {
                return this.GetRelatedEntity<SystemUser>("lk_gbp_customaddress_modifiedby", null);
            }
        }

        /// <summary>
        /// N:1 lk_gbp_customaddress_modifiedonbehalfby
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_gbp_customaddress_modifiedonbehalfby")]
        public SystemUser lk_gbp_customaddress_modifiedonbehalfby
        {
            get
            {
                return this.GetRelatedEntity<SystemUser>("lk_gbp_customaddress_modifiedonbehalfby", null);
            }
        }

        /// <summary>
        /// N:1 team_gbp_customaddress
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningteam")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_gbp_customaddress")]
        public Team team_gbp_customaddress
        {
            get
            {
                return this.GetRelatedEntity<Team>("team_gbp_customaddress", null);
            }
        }

        /// <summary>
        /// N:1 user_gbp_customaddress
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owninguser")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_gbp_customaddress")]
        public SystemUser user_gbp_customaddress
        {
            get
            {
                return this.GetRelatedEntity<SystemUser>("user_gbp_customaddress", null);
            }
        }
    }

    [System.Runtime.Serialization.DataContractAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "8.1.0.7711")]
    public enum Gbp_globecountryState
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Active = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Inactive = 1,
    }

    /// <summary>
    ///
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("gbp_globecountry")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "8.1.0.7711")]
    public partial class gbp_globecountry : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public gbp_globecountry() :
                base(EntityLogicalName)
        {
        }

        public const string EntityLogicalName = "gbp_globecountry";

        public const int EntityTypeCode = 10025;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

        private void OnPropertyChanged(string propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnPropertyChanging(string propertyName)
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Unique identifier of the user who created the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
        public Microsoft.Xrm.Sdk.EntityReference CreatedBy
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdby");
            }
        }

        /// <summary>
        /// Date and time when the record was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdon")]
        public System.Nullable<System.DateTime> CreatedOn
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>("createdon");
            }
        }

        /// <summary>
        /// Unique identifier of the delegate user who created the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
        public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdonbehalfby");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_code")]
        public string gbp_code
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_code");
            }
            set
            {
                this.OnPropertyChanging("gbp_code");
                this.SetAttributeValue("gbp_code", value);
                this.OnPropertyChanged("gbp_code");
            }
        }

        /// <summary>
        /// Unique identifier for entity instances
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_globecountryid")]
        public System.Nullable<System.Guid> gbp_globecountryId
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>("gbp_globecountryid");
            }
            set
            {
                this.OnPropertyChanging("gbp_globecountryId");
                this.SetAttributeValue("gbp_globecountryid", value);
                if (value.HasValue)
                {
                    base.Id = value.Value;
                }
                else
                {
                    base.Id = System.Guid.Empty;
                }
                this.OnPropertyChanged("gbp_globecountryId");
            }
        }

        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_globecountryid")]
        public override System.Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                this.gbp_globecountryId = value;
            }
        }

        /// <summary>
        /// The name of the custom entity.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_name")]
        public string gbp_name
        {
            get
            {
                return this.GetAttributeValue<string>("gbp_name");
            }
            set
            {
                this.OnPropertyChanging("gbp_name");
                this.SetAttributeValue("gbp_name", value);
                this.OnPropertyChanged("gbp_name");
            }
        }

        /// <summary>
        /// Sequence number of the import that created this record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importsequencenumber")]
        public System.Nullable<int> ImportSequenceNumber
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>("importsequencenumber");
            }
            set
            {
                this.OnPropertyChanging("ImportSequenceNumber");
                this.SetAttributeValue("importsequencenumber", value);
                this.OnPropertyChanged("ImportSequenceNumber");
            }
        }

        /// <summary>
        /// Unique identifier of the user who modified the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedby");
            }
        }

        /// <summary>
        /// Date and time when the record was modified.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedon")]
        public System.Nullable<System.DateTime> ModifiedOn
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>("modifiedon");
            }
        }

        /// <summary>
        /// Unique identifier of the delegate user who modified the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
        public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedonbehalfby");
            }
        }

        /// <summary>
        /// Date and time that the record was migrated.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overriddencreatedon")]
        public System.Nullable<System.DateTime> OverriddenCreatedOn
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.DateTime>>("overriddencreatedon");
            }
            set
            {
                this.OnPropertyChanging("OverriddenCreatedOn");
                this.SetAttributeValue("overriddencreatedon", value);
                this.OnPropertyChanged("OverriddenCreatedOn");
            }
        }

        /// <summary>
        /// Owner Id
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ownerid")]
        public Microsoft.Xrm.Sdk.EntityReference OwnerId
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("ownerid");
            }
            set
            {
                this.OnPropertyChanging("OwnerId");
                this.SetAttributeValue("ownerid", value);
                this.OnPropertyChanged("OwnerId");
            }
        }

        /// <summary>
        /// Unique identifier for the business unit that owns the record
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningbusinessunit")]
        public Microsoft.Xrm.Sdk.EntityReference OwningBusinessUnit
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owningbusinessunit");
            }
        }

        /// <summary>
        /// Unique identifier for the team that owns the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningteam")]
        public Microsoft.Xrm.Sdk.EntityReference OwningTeam
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owningteam");
            }
        }

        /// <summary>
        /// Unique identifier for the user that owns the record.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owninguser")]
        public Microsoft.Xrm.Sdk.EntityReference OwningUser
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("owninguser");
            }
        }

        /// <summary>
        /// Status of the GlobeCountry
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecode")]
        public System.Nullable<Gbp_globecountryState> statecode
        {
            get
            {
                Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statecode");
                if ((optionSet != null))
                {
                    return ((Gbp_globecountryState)(System.Enum.ToObject(typeof(Gbp_globecountryState), optionSet.Value)));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnPropertyChanging("statecode");
                if ((value == null))
                {
                    this.SetAttributeValue("statecode", null);
                }
                else
                {
                    this.SetAttributeValue("statecode", new Microsoft.Xrm.Sdk.OptionSetValue(((int)(value))));
                }
                this.OnPropertyChanged("statecode");
            }
        }

        /// <summary>
        /// Reason for the status of the GlobeCountry
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscode")]
        public Microsoft.Xrm.Sdk.OptionSetValue statuscode
        {
            get
            {
                return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statuscode");
            }
            set
            {
                this.OnPropertyChanging("statuscode");
                this.SetAttributeValue("statuscode", value);
                this.OnPropertyChanged("statuscode");
            }
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("timezoneruleversionnumber")]
        public System.Nullable<int> TimeZoneRuleVersionNumber
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>("timezoneruleversionnumber");
            }
            set
            {
                this.OnPropertyChanging("TimeZoneRuleVersionNumber");
                this.SetAttributeValue("timezoneruleversionnumber", value);
                this.OnPropertyChanged("TimeZoneRuleVersionNumber");
            }
        }

        /// <summary>
        /// Time zone code that was in use when the record was created.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("utcconversiontimezonecode")]
        public System.Nullable<int> UTCConversionTimeZoneCode
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<int>>("utcconversiontimezonecode");
            }
            set
            {
                this.OnPropertyChanging("UTCConversionTimeZoneCode");
                this.SetAttributeValue("utcconversiontimezonecode", value);
                this.OnPropertyChanged("UTCConversionTimeZoneCode");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
        public System.Nullable<long> VersionNumber
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>("versionnumber");
            }
        }

        /// <summary>
        /// 1:N gbp_gbp_globecountry_gbp_customaddress_country
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_gbp_globecountry_gbp_customaddress_country")]
        public System.Collections.Generic.IEnumerable<gbp_customaddress> gbp_gbp_globecountry_gbp_customaddress_country
        {
            get
            {
                return this.GetRelatedEntities<gbp_customaddress>("gbp_gbp_globecountry_gbp_customaddress_country", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_gbp_globecountry_gbp_customaddress_country");
                this.SetRelatedEntities<gbp_customaddress>("gbp_gbp_globecountry_gbp_customaddress_country", null, value);
                this.OnPropertyChanged("gbp_gbp_globecountry_gbp_customaddress_country");
            }
        }

        /// <summary>
        /// 1:N gbp_gbp_globecountry_gbp_event_eventcountry
        /// </summary>
        //[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_gbp_globecountry_gbp_event_eventcountry")]
        //public System.Collections.Generic.IEnumerable<gbp_event> gbp_gbp_globecountry_gbp_event_eventcountry
        //{
        //    get
        //    {
        //        return this.GetRelatedEntities<gbp_event>("gbp_gbp_globecountry_gbp_event_eventcountry", null);
        //    }
        //    set
        //    {
        //        this.OnPropertyChanging("gbp_gbp_globecountry_gbp_event_eventcountry");
        //        this.SetRelatedEntities<gbp_event>("gbp_gbp_globecountry_gbp_event_eventcountry", null, value);
        //        this.OnPropertyChanged("gbp_gbp_globecountry_gbp_event_eventcountry");
        //    }
        //}

        /// <summary>
        /// 1:N gbp_gbp_globecountry_gbp_globeregion_countrycode
        /// </summary>
        //[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_gbp_globecountry_gbp_globeregion_countrycode")]
        //public System.Collections.Generic.IEnumerable<gbp_globeregion> gbp_gbp_globecountry_gbp_globeregion_countrycode
        //{
        //    get
        //    {
        //        return this.GetRelatedEntities<gbp_globeregion>("gbp_gbp_globecountry_gbp_globeregion_countrycode", null);
        //    }
        //    set
        //    {
        //        this.OnPropertyChanging("gbp_gbp_globecountry_gbp_globeregion_countrycode");
        //        this.SetRelatedEntities<gbp_globeregion>("gbp_gbp_globecountry_gbp_globeregion_countrycode", null, value);
        //        this.OnPropertyChanged("gbp_gbp_globecountry_gbp_globeregion_countrycode");
        //    }
        //}

        /// <summary>
        /// 1:N gbp_gbp_globecountry_gbp_purchasetax_countrycode
        /// </summary>
        //[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_gbp_globecountry_gbp_purchasetax_countrycode")]
        //public System.Collections.Generic.IEnumerable<gbp_purchasetax> gbp_gbp_globecountry_gbp_purchasetax_countrycode
        //{
        //    get
        //    {
        //        return this.GetRelatedEntities<gbp_purchasetax>("gbp_gbp_globecountry_gbp_purchasetax_countrycode", null);
        //    }
        //    set
        //    {
        //        this.OnPropertyChanging("gbp_gbp_globecountry_gbp_purchasetax_countrycode");
        //        this.SetRelatedEntities<gbp_purchasetax>("gbp_gbp_globecountry_gbp_purchasetax_countrycode", null, value);
        //        this.OnPropertyChanged("gbp_gbp_globecountry_gbp_purchasetax_countrycode");
        //    }
        //}

        /// <summary>
        /// 1:N gbp_gbp_globecountry_lead_country
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_gbp_globecountry_lead_country")]
        public System.Collections.Generic.IEnumerable<Lead> gbp_gbp_globecountry_lead_country
        {
            get
            {
                return this.GetRelatedEntities<Lead>("gbp_gbp_globecountry_lead_country", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_gbp_globecountry_lead_country");
                this.SetRelatedEntities<Lead>("gbp_gbp_globecountry_lead_country", null, value);
                this.OnPropertyChanged("gbp_gbp_globecountry_lead_country");
            }
        }

        /// <summary>
        /// 1:N gbp_globecountry_ActivityPointers
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_globecountry_ActivityPointers")]
        public System.Collections.Generic.IEnumerable<ActivityPointer> gbp_globecountry_ActivityPointers
        {
            get
            {
                return this.GetRelatedEntities<ActivityPointer>("gbp_globecountry_ActivityPointers", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_globecountry_ActivityPointers");
                this.SetRelatedEntities<ActivityPointer>("gbp_globecountry_ActivityPointers", null, value);
                this.OnPropertyChanged("gbp_globecountry_ActivityPointers");
            }
        }

        /// <summary>
        /// 1:N gbp_globecountry_Annotations
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_globecountry_Annotations")]
        public System.Collections.Generic.IEnumerable<Annotation> gbp_globecountry_Annotations
        {
            get
            {
                return this.GetRelatedEntities<Annotation>("gbp_globecountry_Annotations", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_globecountry_Annotations");
                this.SetRelatedEntities<Annotation>("gbp_globecountry_Annotations", null, value);
                this.OnPropertyChanged("gbp_globecountry_Annotations");
            }
        }

        /// <summary>
        /// 1:N gbp_globecountry_connections1
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_globecountry_connections1")]
        public System.Collections.Generic.IEnumerable<Connection> gbp_globecountry_connections1
        {
            get
            {
                return this.GetRelatedEntities<Connection>("gbp_globecountry_connections1", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_globecountry_connections1");
                this.SetRelatedEntities<Connection>("gbp_globecountry_connections1", null, value);
                this.OnPropertyChanged("gbp_globecountry_connections1");
            }
        }

        /// <summary>
        /// 1:N gbp_globecountry_connections2
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_globecountry_connections2")]
        public System.Collections.Generic.IEnumerable<Connection> gbp_globecountry_connections2
        {
            get
            {
                return this.GetRelatedEntities<Connection>("gbp_globecountry_connections2", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_globecountry_connections2");
                this.SetRelatedEntities<Connection>("gbp_globecountry_connections2", null, value);
                this.OnPropertyChanged("gbp_globecountry_connections2");
            }
        }

        /// <summary>
        /// 1:N gbp_globecountry_Emails
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_globecountry_Emails")]
        public System.Collections.Generic.IEnumerable<Email> gbp_globecountry_Emails
        {
            get
            {
                return this.GetRelatedEntities<Email>("gbp_globecountry_Emails", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_globecountry_Emails");
                this.SetRelatedEntities<Email>("gbp_globecountry_Emails", null, value);
                this.OnPropertyChanged("gbp_globecountry_Emails");
            }
        }

        /// <summary>
        /// 1:N gbp_globecountry_gbp_globenotes
        /// </summary>
        //[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_globecountry_gbp_globenotes")]
        //public System.Collections.Generic.IEnumerable<gbp_globenote> gbp_globecountry_gbp_globenotes
        //{
        //    get
        //    {
        //        return this.GetRelatedEntities<gbp_globenote>("gbp_globecountry_gbp_globenotes", null);
        //    }
        //    set
        //    {
        //        this.OnPropertyChanging("gbp_globecountry_gbp_globenotes");
        //        this.SetRelatedEntities<gbp_globenote>("gbp_globecountry_gbp_globenotes", null, value);
        //        this.OnPropertyChanged("gbp_globecountry_gbp_globenotes");
        //    }
        //}

        /// <summary>
        /// 1:N gbp_globecountry_PhoneCalls
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_globecountry_PhoneCalls")]
        public System.Collections.Generic.IEnumerable<PhoneCall> gbp_globecountry_PhoneCalls
        {
            get
            {
                return this.GetRelatedEntities<PhoneCall>("gbp_globecountry_PhoneCalls", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_globecountry_PhoneCalls");
                this.SetRelatedEntities<PhoneCall>("gbp_globecountry_PhoneCalls", null, value);
                this.OnPropertyChanged("gbp_globecountry_PhoneCalls");
            }
        }

        /// <summary>
        /// 1:N gbp_globecountry_ProcessSession
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_globecountry_ProcessSession")]
        public System.Collections.Generic.IEnumerable<ProcessSession> gbp_globecountry_ProcessSession
        {
            get
            {
                return this.GetRelatedEntities<ProcessSession>("gbp_globecountry_ProcessSession", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_globecountry_ProcessSession");
                this.SetRelatedEntities<ProcessSession>("gbp_globecountry_ProcessSession", null, value);
                this.OnPropertyChanged("gbp_globecountry_ProcessSession");
            }
        }

        /// <summary>
        /// 1:N gbp_globecountry_Tasks
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_globecountry_Tasks")]
        public System.Collections.Generic.IEnumerable<Task> gbp_globecountry_Tasks
        {
            get
            {
                return this.GetRelatedEntities<Task>("gbp_globecountry_Tasks", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_globecountry_Tasks");
                this.SetRelatedEntities<Task>("gbp_globecountry_Tasks", null, value);
                this.OnPropertyChanged("gbp_globecountry_Tasks");
            }
        }

        /// <summary>
        /// N:1 lk_gbp_globecountry_createdby
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_gbp_globecountry_createdby")]
        public SystemUser lk_gbp_globecountry_createdby
        {
            get
            {
                return this.GetRelatedEntity<SystemUser>("lk_gbp_globecountry_createdby", null);
            }
        }

        /// <summary>
        /// N:1 lk_gbp_globecountry_createdonbehalfby
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_gbp_globecountry_createdonbehalfby")]
        public SystemUser lk_gbp_globecountry_createdonbehalfby
        {
            get
            {
                return this.GetRelatedEntity<SystemUser>("lk_gbp_globecountry_createdonbehalfby", null);
            }
        }

        /// <summary>
        /// N:1 lk_gbp_globecountry_modifiedby
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_gbp_globecountry_modifiedby")]
        public SystemUser lk_gbp_globecountry_modifiedby
        {
            get
            {
                return this.GetRelatedEntity<SystemUser>("lk_gbp_globecountry_modifiedby", null);
            }
        }

        /// <summary>
        /// N:1 lk_gbp_globecountry_modifiedonbehalfby
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_gbp_globecountry_modifiedonbehalfby")]
        public SystemUser lk_gbp_globecountry_modifiedonbehalfby
        {
            get
            {
                return this.GetRelatedEntity<SystemUser>("lk_gbp_globecountry_modifiedonbehalfby", null);
            }
        }

        /// <summary>
        /// N:1 team_gbp_globecountry
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owningteam")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_gbp_globecountry")]
        public Team team_gbp_globecountry
        {
            get
            {
                return this.GetRelatedEntity<Team>("team_gbp_globecountry", null);
            }
        }

        /// <summary>
        /// N:1 user_gbp_globecountry
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("owninguser")]
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_gbp_globecountry")]
        public SystemUser user_gbp_globecountry
        {
            get
            {
                return this.GetRelatedEntity<SystemUser>("user_gbp_globecountry", null);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    [System.Runtime.Serialization.DataContractAttribute()]
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("gbp_gbp_customaddress_contact")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "8.1.0.7711")]
    public partial class gbp_gbp_customaddress_contact : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public gbp_gbp_customaddress_contact() :
                base(EntityLogicalName)
        {
        }

        public const string EntityLogicalName = "gbp_gbp_customaddress_contact";

        public const int EntityTypeCode = 10055;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

        private void OnPropertyChanged(string propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnPropertyChanging(string propertyName)
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("contactid")]
        public System.Nullable<System.Guid> contactid
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>("contactid");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_customaddressid")]
        public System.Nullable<System.Guid> gbp_customaddressid
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>("gbp_customaddressid");
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_gbp_customaddress_contactid")]
        public System.Nullable<System.Guid> gbp_gbp_customaddress_contactId
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<System.Guid>>("gbp_gbp_customaddress_contactid");
            }
        }

        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("gbp_gbp_customaddress_contactid")]
        public override System.Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
        public System.Nullable<long> VersionNumber
        {
            get
            {
                return this.GetAttributeValue<System.Nullable<long>>("versionnumber");
            }
        }

        /// <summary>
        /// N:N gbp_gbp_customaddress_contact
        /// </summary>
        [Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_gbp_customaddress_contact")]
        public System.Collections.Generic.IEnumerable<gbp_customaddress> gbp_gbp_customaddress_contact1
        {
            get
            {
                return this.GetRelatedEntities<gbp_customaddress>("gbp_gbp_customaddress_contact", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_gbp_customaddress_contact1");
                this.SetRelatedEntities<gbp_customaddress>("gbp_gbp_customaddress_contact", null, value);
                this.OnPropertyChanged("gbp_gbp_customaddress_contact1");
            }
        }
    }
}