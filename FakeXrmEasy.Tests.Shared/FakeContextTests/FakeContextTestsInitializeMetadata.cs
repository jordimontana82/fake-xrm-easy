using System;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using System.Linq;
using System.Threading;
using FakeXrmEasy.Tests.PluginsForTesting;
using Crm;
using System.Reflection;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace FakeXrmEasy.Tests
{
    public class FakeContextTestsInitializeMetadata
    {
        [Fact]
        public void When_an_earlyboundentity_has_constant_PrimaryNameAttribute_then_metadata_PrimaryNameAttribute_populated()
        {
            var context = new XrmFakedContext();
            context.InitializeMetadata(typeof(AccountTest).Assembly);

            var req = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Attributes,
                RetrieveAsIfPublished = true,
                LogicalName = AccountTest.EntityLogicalName,
            };

            var metadata = ((RetrieveEntityResponse)context.GetOrganizationService().Execute(req)).EntityMetadata;

            Assert.Equal("name", metadata.PrimaryNameAttribute);
        }

        [Fact]
        public void When_an_earlyboundentity_lookup_then_target_populated()
        {
            var lookupAttributeLogicalName = "primarycontactid";
            var context = new XrmFakedContext();
            context.InitializeMetadata(typeof(Account).Assembly);

            var req = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Attributes,
                RetrieveAsIfPublished = true,
                LogicalName = Account.EntityLogicalName,
            };

            var metadata = ((RetrieveEntityResponse)context.GetOrganizationService().Execute(req)).EntityMetadata;
            var attribute = metadata.Attributes.Where(a => a.LogicalName.Equals(lookupAttributeLogicalName)).FirstOrDefault() as LookupAttributeMetadata;

            Assert.Contains("contact", attribute.Targets);
        }


        /// <summary>
        /// Business that represents a customer or potential customer. The company that is billed in business transactions.
        /// </summary>
        [System.Runtime.Serialization.DataContractAttribute()]
        [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("accounttest")]
        private partial class AccountTest : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
        {

            /// <summary>
            /// Default Constructor.
            /// </summary>
            [System.Diagnostics.DebuggerNonUserCode()]
            public AccountTest() :
                    base(EntityLogicalName)
            {
            }

            public const string EntityLogicalName = "accounttest";

            public const string EntitySchemaName = "Account";

            public const string PrimaryIdAttribute = "accountid";

            public const string PrimaryNameAttribute = "name";

            public const string EntityLogicalCollectionName = "accounts";

            public const string EntitySetName = "accounts";

            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

            public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

            [System.Diagnostics.DebuggerNonUserCode()]
            private void OnPropertyChanged(string propertyName)
            {
                if ((this.PropertyChanged != null))
                {
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
                }
            }

            [System.Diagnostics.DebuggerNonUserCode()]
            private void OnPropertyChanging(string propertyName)
            {
                if ((this.PropertyChanging != null))
                {
                    this.PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
                }
            }


            /// <summary>
            /// Type the company or business name.
            /// </summary>
            [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("name")]
            public string Name
            {
                [System.Diagnostics.DebuggerNonUserCode()]
                get
                {
                    return this.GetAttributeValue<string>("name");
                }
                [System.Diagnostics.DebuggerNonUserCode()]
                set
                {
                    this.OnPropertyChanging("Name");
                    this.SetAttributeValue("name", value);
                    this.OnPropertyChanged("Name");
                }
            }
        }
    }
}