using System;
using System.Linq;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk.Query;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using System.ServiceModel;

namespace FakeXrmEasy.Tests
{
    public class FakeXrmEasyTestDelete
    {
        [Fact]
        public void When_delete_is_invoked_with_an_empty_logical_name_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Delete(null,Guid.Empty));
            Assert.Equal(ex.Message, "The entity logical name must not be null or empty.");

            ex = Assert.Throws<InvalidOperationException>(() => service.Delete("", Guid.Empty));
            Assert.Equal(ex.Message, "The entity logical name must not be null or empty.");

            ex = Assert.Throws<InvalidOperationException>(() => service.Delete("     ", Guid.Empty));
            Assert.Equal(ex.Message, "The entity logical name must not be null or empty.");
        }

        [Fact]
        public void When_delete_is_invoked_with_an_empty_guid_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Delete("account", Guid.Empty));
            Assert.Equal(ex.Message, "The id must not be empty.");
        }

        [Fact]
        public void When_retrieve_is_invoked_with_non_existing_entity_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();

            //Initialize the context with a single entity
            var guid = Guid.NewGuid();
            var nonExistingGuid = Guid.NewGuid();
            var data = new List<Entity>() {
                new Entity("account") { Id = guid }
            }.AsQueryable();

            context.Initialize(data);

            var service = context.GetFakedOrganizationService();

            var ex = Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Delete("account", nonExistingGuid));
            Assert.Equal(ex.Message, string.Format("account with Id {0} Does Not Exist", nonExistingGuid));
        }

        [Fact]
        public void When_delete_is_invoked_with_an_existing_entity_that_entity_is_delete_from_the_context()
        {
            var context = new XrmFakedContext();

            //Initialize the context with a single entity
            var guid = Guid.NewGuid();
            var data = new List<Entity>() {
                new Entity("account") { Id = guid }
            }.AsQueryable();

            context.Initialize(data);

            var service = context.GetFakedOrganizationService();

            service.Delete("account", guid);
            Assert.True(context.Data["account"].Count == 0);
        }

        
    }
}
