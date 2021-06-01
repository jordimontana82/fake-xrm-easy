using Crm;
using FakeXrmEasy.FakeMessageExecutors;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;
using Xunit;
using System.Collections.Generic;

namespace FakeXrmEasy.Tests.FakeContextTests.RetrieveAllEntitiesRequestTests
{
    public class RetrieveAllEntitiesRequestTests
    {
        [Fact]
        public void When_calling_retrieve_entity_without_specifying_EntityFilter_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();

            var service = ctx.GetOrganizationService();
            var executor = new RetrieveAllEntitiesRequestExecutor();
            ctx.AddFakeMessageExecutor<RetrieveAllEntitiesRequest>(executor);

            var request = new RetrieveAllEntitiesRequest()
            {
            };
            Assert.Throws<Exception>(() => service.Execute(request));
        }
       
        [Fact]
        public void When_calling_retrieve_entities_with_a_fake_entity_metadata_that_one_is_returned()
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

            var request = new RetrieveAllEntitiesRequest()
            {
                EntityFilters = EntityFilters.Entity
            };

            var response = service.Execute(request);
            Assert.IsType<RetrieveAllEntitiesResponse>(response);
            var retrieveEntityResponse = response as RetrieveAllEntitiesResponse;
            Assert.NotEmpty(retrieveEntityResponse.EntityMetadata);
            Assert.True(retrieveEntityResponse.EntityMetadata[0].IsCustomizable.Value);
            Assert.Null(retrieveEntityResponse.EntityMetadata[0].Attributes); //EntityFilters = Entity
            Assert.Null(retrieveEntityResponse.EntityMetadata[0].Privileges); //EntityFilters = Entity
            Assert.Null(retrieveEntityResponse.EntityMetadata[0].OneToManyRelationships); //EntityFilters = Entity
            Assert.Null(retrieveEntityResponse.EntityMetadata[0].ManyToOneRelationships); //EntityFilters = Entity
            Assert.Null(retrieveEntityResponse.EntityMetadata[0].ManyToManyRelationships); //EntityFilters = Entity
        }

        [Fact]
        public void When_calling_retrieve_entities_with_a_fake_attribute_definition_it_is_returned()
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

            var request = new RetrieveAllEntitiesRequest()
            {
                EntityFilters = EntityFilters.Attributes
            };

            var response = service.Execute(request);
            Assert.IsType<RetrieveAllEntitiesResponse>(response);

            var nameAttribute = (response as RetrieveAllEntitiesResponse).EntityMetadata[0].Attributes
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

            var request = new RetrieveAllEntitiesRequest()
            {
                EntityFilters = EntityFilters.Relationships
            };

            var response = service.Execute(request);
            Assert.IsType<RetrieveAllEntitiesResponse>(response);

            var retrieveEntityResponse = response as RetrieveAllEntitiesResponse;

            Assert.NotEmpty(retrieveEntityResponse.EntityMetadata);
            Assert.NotNull(retrieveEntityResponse.EntityMetadata[0].OneToManyRelationships);
            Assert.NotEmpty(retrieveEntityResponse.EntityMetadata[0].OneToManyRelationships
                            .Where(a => a.SchemaName.Equals("account_parent_account")));

        }
    }
}