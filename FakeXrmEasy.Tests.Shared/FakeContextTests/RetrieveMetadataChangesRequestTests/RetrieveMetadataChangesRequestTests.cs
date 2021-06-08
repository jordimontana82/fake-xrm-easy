using Crm;
using FakeXrmEasy.FakeMessageExecutors;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;
using System.Reflection;
using Xunit;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Metadata.Query;

namespace FakeXrmEasy.Tests.FakeContextTests.RetrieveMetadataChangesRequestTests
{
    public class RetrieveMetadataChangesRequestTests
    {
        private IOrganizationService Arrange()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var accountMetadata = new EntityMetadata()
            {
                LogicalName = Account.EntityLogicalName,
                IsCustomizable = new BooleanManagedProperty(true)
            };
            var stringMetadata = new StringAttributeMetadata()
            {
                LogicalName = "name",
                MaxLength = 200,
            };
            stringMetadata.SetSealedPropertyValue("IsValidForCreate", new Nullable<bool>(true));
            accountMetadata.SetAttributeCollection(new List<AttributeMetadata>() { stringMetadata });

            var contactMetadata = new EntityMetadata()
            {
                LogicalName = Contact.EntityLogicalName
            };
            var intMetadata = new IntegerAttributeMetadata()
            {
                LogicalName = "numberofchildren",
                MinValue = 0,
                MaxValue = 100
            };
            contactMetadata.SetAttributeCollection(new List<AttributeMetadata>() { intMetadata });
            ctx.InitializeMetadata(new[] { accountMetadata, contactMetadata });

            return service;
        }

        [Fact]
        public void When_calling_RetrieveMetadataChanges_with_a_filter_only_the_requested_entity_is_returned()
        {
            var service = Arrange();

            var request = new RetrieveMetadataChangesRequest()
            {
                Query = new EntityQueryExpression
                {
                    Criteria = new MetadataFilterExpression
                    {
                        Conditions =
                        {
                            new MetadataConditionExpression(nameof(EntityMetadata.LogicalName), MetadataConditionOperator.Equals, Account.EntityLogicalName)
                        }
                    }
                }
            };

            var response = service.Execute(request);
            var metadata = Assert.IsType<RetrieveMetadataChangesResponse>(response);

            var names = metadata.EntityMetadata.Select(e => e.LogicalName).ToArray();
            Assert.Equal(new[] { Account.EntityLogicalName }, names);
        }

        [Fact]
        public void When_calling_RetrieveMetadataChanges_with_an_attribute_filter_only_the_requested_attributes_are_returned()
        {
            var service = Arrange();

            var request = new RetrieveMetadataChangesRequest()
            {
                Query = new EntityQueryExpression
                {
                    AttributeQuery = new AttributeQueryExpression
                    {
                        Criteria = new MetadataFilterExpression
                        {
                            Conditions =
                            {
                                new MetadataConditionExpression(nameof(AttributeMetadata.LogicalName), MetadataConditionOperator.Equals, "name")
                            }
                        },
                        Properties = new MetadataPropertiesExpression(nameof(StringAttributeMetadata.MaxLength))
                    }
                }
            };

            var response = service.Execute(request);
            var metadata = Assert.IsType<RetrieveMetadataChangesResponse>(response);

            var attributes = metadata.EntityMetadata.SelectMany(e => e.Attributes).ToArray();
            Assert.Equal(1, attributes.Length);
            var stringAttr = Assert.IsType<StringAttributeMetadata>(attributes.Single());
            Assert.Equal(200, stringAttr.MaxLength);
        }

        [Fact]
        public void When_calling_RetrieveMetadataChanges_and_get_attributes_of_different_types_properties_that_only_exist_in_one_type_are_returned()
        {
            var service = Arrange();

            var request = new RetrieveMetadataChangesRequest()
            {
                Query = new EntityQueryExpression
                {
                    AttributeQuery = new AttributeQueryExpression
                    {
                        Properties = new MetadataPropertiesExpression(nameof(StringAttributeMetadata.MaxLength), nameof(IntegerAttributeMetadata.MaxValue))
                    }
                }
            };

            var response = service.Execute(request);
            var metadata = Assert.IsType<RetrieveMetadataChangesResponse>(response);

            var attributes = metadata.EntityMetadata.SelectMany(e => e.Attributes).ToArray();
            Assert.Equal(2, attributes.Length);
            var stringAttr = attributes.OfType<StringAttributeMetadata>().Single();
            Assert.Equal(200, stringAttr.MaxLength);
            var intAttr = attributes.OfType<IntegerAttributeMetadata>().Single();
            Assert.Equal(100, intAttr.MaxValue);
        }
    }
}