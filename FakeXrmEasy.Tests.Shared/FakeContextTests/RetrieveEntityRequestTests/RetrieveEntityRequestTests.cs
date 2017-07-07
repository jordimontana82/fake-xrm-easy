using Crm;
using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

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
            var ctx = new XrmFakedContext()
            {
                ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account))
            };

            var service = ctx.GetOrganizationService();
            var executor = new RetrieveEntityRequestExecutor();
            executor.AddFakeEntityMetadata(Account.EntityLogicalName, new EntityMetadata()
            {
                IsCustomizable = new BooleanManagedProperty(true)
            });

            ctx.AddFakeMessageExecutor<RetrieveEntityRequest>(executor);

            var request = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Entity,
                LogicalName = Account.EntityLogicalName
            };

            var response = service.Execute(request);
            Assert.IsType<RetrieveEntityResponse>(response);
            Assert.True((response as RetrieveEntityResponse).EntityMetadata.IsCustomizable.Value);
        }

        [Fact]
        public void When_calling_retrieve_entity_with_a_fake_attribute_definition_it_is_returned()
        {
            var ctx = new XrmFakedContext()
            {
                ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account))
            };

            var service = ctx.GetOrganizationService();

            var executor = new RetrieveEntityRequestExecutor();
            executor.AddFakeEntityMetadata(Account.EntityLogicalName, new EntityMetadata()
            {
                IsCustomizable = new BooleanManagedProperty(true)
            });

            var stringMetadata = new StringAttributeMetadata()
            {
                MaxLength = 200
            };
            stringMetadata.GetType().GetProperty("IsValidForCreate").SetValue(stringMetadata, new Nullable<bool>(true), null);
            executor.AddFakeAttributeMetadata(Account.EntityLogicalName, "name", stringMetadata);

            ctx.AddFakeMessageExecutor<RetrieveEntityRequest>(executor);

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
    }
}