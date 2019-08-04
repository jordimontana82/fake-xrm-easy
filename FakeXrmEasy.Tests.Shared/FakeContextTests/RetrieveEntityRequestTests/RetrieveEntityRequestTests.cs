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

namespace FakeXrmEasy.Tests.FakeContextTests.RetrieveEntityRequestTests
{
    public class RetrieveEntityRequestTests
    {
        [Fact]
        public void When_calling_retrieve_entity_without_proxy_types_assembly_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();

            var service = ctx.GetOrganizationService();
            var executor = new RetrieveEntityRequestExecutor();
            ctx.AddFakeMessageExecutor<RetrieveEntityRequest>(executor);

            var request = new RetrieveEntityRequest()
            {
                LogicalName = Account.EntityLogicalName
            };
            Assert.Throws<Exception>(() => service.Execute(request));
        }

        [Fact]
        public void When_calling_retrieve_entity_with_a_null_or_empty_logicalname_exception_is_thrown()
        {
            var ctx = new XrmFakedContext()
            {
                ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account))
            };

            var service = ctx.GetOrganizationService();
            var executor = new RetrieveEntityRequestExecutor();
            ctx.AddFakeMessageExecutor<RetrieveEntityRequest>(executor);

            var request = new RetrieveEntityRequest()
            {
                LogicalName = ""
            };
            Assert.Throws<Exception>(() => service.Execute(request));
        }

        [Fact]
        public void When_calling_retrieve_entity_without_a_fake_entity_metadata_exception_is_thrown()
        {
            var ctx = new XrmFakedContext()
            {
                ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account))
            };

            var service = ctx.GetOrganizationService();
            var executor = new RetrieveEntityRequestExecutor();
            ctx.AddFakeMessageExecutor<RetrieveEntityRequest>(executor);

            var request = new RetrieveEntityRequest()
            {
                LogicalName = Account.EntityLogicalName
            };
            Assert.Throws<Exception>(() => service.Execute(request));
        }

        [Fact]
        public void When_calling_retrieve_entity_with_a_fake_entity_metadata_that_one_is_returned()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var entityMetadata = new EntityMetadata()
            {
                LogicalName = Account.EntityLogicalName,
                IsCustomizable = new BooleanManagedProperty(true)
            };
            ctx.InitializeMetadata(entityMetadata);

            var request = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Entity,
                LogicalName = Account.EntityLogicalName
            };

            var response = service.Execute(request);
            Assert.IsType<RetrieveEntityResponse>(response);
            var retrieveEntityResponse = response as RetrieveEntityResponse;
            Assert.True(retrieveEntityResponse.EntityMetadata.IsCustomizable.Value);
            Assert.Null(retrieveEntityResponse.EntityMetadata.Attributes); //EntityFilters = Entity
            Assert.Null(retrieveEntityResponse.EntityMetadata.Privileges); //EntityFilters = Entity
            Assert.Null(retrieveEntityResponse.EntityMetadata.OneToManyRelationships); //EntityFilters = Entity
            Assert.Null(retrieveEntityResponse.EntityMetadata.ManyToOneRelationships); //EntityFilters = Entity
            Assert.Null(retrieveEntityResponse.EntityMetadata.ManyToManyRelationships); //EntityFilters = Entity
        }

        [Fact]
        public void When_calling_retrieve_entity_with_a_fake_attribute_definition_it_is_returned()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var entityMetadata = new EntityMetadata()
            {
                LogicalName = Account.EntityLogicalName,
                IsCustomizable = new BooleanManagedProperty(true)
            };
            var stringMetadata = new StringAttributeMetadata()
            {
                SchemaName = "name",
                MaxLength = 200,
            };
            stringMetadata.SetSealedPropertyValue("IsValidForCreate", new Nullable<bool>(true));
            entityMetadata.SetAttributeCollection(new List<AttributeMetadata>() { stringMetadata });
            ctx.InitializeMetadata(entityMetadata);

            var request = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Attributes,
                LogicalName = Account.EntityLogicalName
            };

            var response = service.Execute(request);
            Assert.IsType<RetrieveEntityResponse>(response);

            var nameAttribute = (response as RetrieveEntityResponse).EntityMetadata.Attributes
                                .Where(a => a.SchemaName.Equals("name"))
                                .FirstOrDefault();

            Assert.True(nameAttribute.IsValidForCreate.Value);
        }

        [Fact]
        public void When_calling_retrieve_entity_with_a_fake_relationship_definition_it_is_returned()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var entityMetadata = new EntityMetadata()
            {
                LogicalName = Account.EntityLogicalName,
                IsCustomizable = new BooleanManagedProperty(true)
            };
            var parentAccountMetadata = new OneToManyRelationshipMetadata()
            {
                SchemaName = "account_parent_account",
                ReferencingAttribute = "parentaccountid",
                ReferencingEntity = Account.EntityLogicalName,
                ReferencedAttribute = "accountid",
                ReferencedEntity = Account.EntityLogicalName
            };
            entityMetadata.SetOneToManyRelationshipCollection(new OneToManyRelationshipMetadata[] { parentAccountMetadata });
            ctx.InitializeMetadata(entityMetadata);

            var request = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Relationships,
                LogicalName = Account.EntityLogicalName
            };

            var response = service.Execute(request);
            Assert.IsType<RetrieveEntityResponse>(response);

            var retrieveEntityResponse = response as RetrieveEntityResponse;

            Assert.NotNull(retrieveEntityResponse.EntityMetadata.OneToManyRelationships);
            Assert.NotEmpty(retrieveEntityResponse.EntityMetadata.OneToManyRelationships
                            .Where(a => a.SchemaName.Equals("account_parent_account")));

        }
    }
}