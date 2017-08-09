using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using Xunit;

namespace FakeXrmEasy.Tests
{
    public class FakeXrmEasyTestRetrieve
    {
        [Fact]
        public void When_retrieve_is_invoked_with_an_empty_logical_name_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Retrieve(null, Guid.Empty, new ColumnSet()));
            Assert.Equal(ex.Message, "The entity logical name must not be null or empty.");

            ex = Assert.Throws<InvalidOperationException>(() => service.Retrieve("", Guid.Empty, new ColumnSet()));
            Assert.Equal(ex.Message, "The entity logical name must not be null or empty.");

            ex = Assert.Throws<InvalidOperationException>(() => service.Retrieve("     ", Guid.Empty, new ColumnSet()));
            Assert.Equal(ex.Message, "The entity logical name must not be null or empty.");
        }

        [Fact]
        public void When_retrieve_is_invoked_with_an_empty_guid_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Retrieve("account", Guid.Empty, new ColumnSet()));
            Assert.Equal(ex.Message, "The id must not be empty.");
        }

        [Fact]
        public void When_retrieve_is_invoked_with_a_null_columnset_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Retrieve("account", Guid.NewGuid(), null));
            Assert.Equal(ex.Message, "The columnset parameter must not be null.");
        }

        [Fact]
        public void When_retrieve_is_invoked_with_a_non_existing_logical_name_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();

            var service = context.GetFakedOrganizationService();

            var ex = Assert.Throws<InvalidOperationException>(() => service.Retrieve("account", Guid.NewGuid(), null));
            Assert.Equal(ex.Message, "The columnset parameter must not be null.");
        }

        [Fact]
        public void When_retrieve_is_invoked_with_non_existing_entity_null_is_returned()
        {
            var context = new XrmFakedContext();

            //Initialize the context with a single entity
            var guid = Guid.NewGuid();
            var data = new List<Entity>() {
                new Entity("account") { Id = guid }
            }.AsQueryable();

            context.Initialize(data);

            var service = context.GetFakedOrganizationService();

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Retrieve("account", Guid.NewGuid(), new ColumnSet()));
        }

        [Fact]
        public void When_retrieve_is_invoked_with_an_existing_entity_that_entity_is_returned()
        {
            var context = new XrmFakedContext();

            //Initialize the context with a single entity
            var guid = Guid.NewGuid();
            var data = new List<Entity>() {
                new Entity("account") { Id = guid }
            }.AsQueryable();

            context.Initialize(data);

            var service = context.GetFakedOrganizationService();

            var result = service.Retrieve("account", guid, new ColumnSet());
            Assert.Equal(result.Id, data.FirstOrDefault().Id);
        }

        [Fact]
        public void When_retrieve_is_invoked_with_an_existing_entity_and_all_columns_all_the_attributes_are_returned()
        {
            var context = new XrmFakedContext();

            //Initialize the context with a single entity
            var guid = Guid.NewGuid();
            var data = new List<Entity>() {
                new Entity("account") { Id = guid }
            }.AsQueryable();

            context.Initialize(data);

            var service = context.GetFakedOrganizationService();

            var result = service.Retrieve("account", guid, new ColumnSet(true));
            Assert.Equal(result.Id, data.FirstOrDefault().Id);
            Assert.Equal(result.Attributes.Count, 7);
        }

        [Fact]
        public void When_retrieve_is_invoked_with_an_existing_entity_and_only_one_column_only_that_one_is_retrieved()
        {
            var context = new XrmFakedContext();

            //Initialize the context with a single entity
            var guid = Guid.NewGuid();
            var entity = new Entity("account") { Id = guid };
            entity["name"] = "Test account";
            entity["createdon"] = DateTime.UtcNow;

            var data = new List<Entity>() { entity }.AsQueryable();
            context.Initialize(data);

            var service = context.GetFakedOrganizationService();

            var result = service.Retrieve("account", guid, new ColumnSet(new string[] { "name" }));
            Assert.Equal(result.Id, data.FirstOrDefault().Id);
            Assert.True(result.Attributes.Count == 1);
            Assert.Equal(result["name"], "Test account");
        }

        [Fact]
        public void When_retrieve_is_invoked_with_an_existing_entity_and_proxy_types_the_returned_entity_must_be_of_the_appropiate_subclass()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            //Initialize the context with a single entity
            var guid = Guid.NewGuid();
            var account = new Account() { Id = guid };
            account.Name = "Test account";

            var data = new List<Entity>() { account }.AsQueryable();
            context.Initialize(data);

            var service = context.GetFakedOrganizationService();

            var result = service.Retrieve("account", guid, new ColumnSet(new string[] { "name" }));

            Assert.True(result is Account);
        }

        [Fact]
        public void When_retrieving_entity_that_does_not_exist_with_proxy_types_entity_name_should_be_known()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Account));

            var service = context.GetFakedOrganizationService();
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Retrieve("account", Guid.NewGuid(), new ColumnSet(true)));
        }

        [Fact]
        public void Should_Not_Fail_On_Retrieving_Entity_With_Entity_Collection_Attributes()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetFakedOrganizationService();

            var party = new ActivityParty
            {
                PartyId = new EntityReference("systemuser", Guid.NewGuid())
            };

            var email = new Email
            {
                Id = Guid.NewGuid(),
                To = new[] { party }
            };

            service.Create(email);

            var ex = Record.Exception(() => service.Retrieve(email.LogicalName, email.Id, new ColumnSet(true)));
            Assert.Null(ex);
        }
    }
}