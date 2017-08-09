using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.QueryByAttributeTests
{
    public class Tests
    {
        [Fact]
        public static void When_a_query_by_attribute_is_executed_with_one_attribute_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var account = new Account() { Id = Guid.NewGuid(), Name = "Test" };
            var account2 = new Account() { Id = Guid.NewGuid(), Name = "Other account!" };
            context.Initialize(new List<Entity>()
            {
                account, account2
            });

            var service = context.GetFakedOrganizationService();

            QueryByAttribute query = new QueryByAttribute();
            query.Attributes.AddRange(new string[] { "name" });
            query.ColumnSet = new ColumnSet(new string[] { "name" });
            query.EntityName = Account.EntityLogicalName;
            query.Values.AddRange(new object[] { "Test" });

            //Execute using a request to test the OOB (XRM) message contracts
            RetrieveMultipleRequest request = new RetrieveMultipleRequest();
            request.Query = query;
            Collection<Entity> entityList = ((RetrieveMultipleResponse)service.Execute(request)).EntityCollection.Entities;

            Assert.True(entityList.Count == 1);
            Assert.Equal(entityList[0]["name"].ToString(), "Test");
        }

        [Fact]
        public static void When_a_query_by_a_boolean_attribute_is_executed_with_one_attribute_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var account = new Account() { Id = Guid.NewGuid(), Name = "Test", DoNotEMail = false };
            var account2 = new Account() { Id = Guid.NewGuid(), Name = "Other account!", DoNotEMail = true };
            context.Initialize(new List<Entity>()
            {
                account, account2
            });

            var service = context.GetFakedOrganizationService();

            var query = new QueryByAttribute
            {
                EntityName = Account.EntityLogicalName,
                ColumnSet = new ColumnSet("name")
            };

            query.AddAttributeValue("donotemail", false);

            //Execute using a request to test the OOB (XRM) message contracts
            RetrieveMultipleRequest request = new RetrieveMultipleRequest();
            request.Query = query;
            Collection<Entity> entityList = ((RetrieveMultipleResponse)service.Execute(request)).EntityCollection.Entities;

            Assert.True(entityList.Count == 1);
            Assert.Equal(entityList[0]["name"].ToString(), "Test");
        }

        [Fact]
        public static void When_a_query_by_attribute_is_executed_with_one_null_attribute_it_is_not_returned()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetFakedOrganizationService();

            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = null,
                LastName = "asdf"
            };

            fakedContext.Initialize(new List<Entity> { contact });

            QueryByAttribute query = new QueryByAttribute("contact");
            query.ColumnSet = new ColumnSet("firstname", "lastname");
            var results = fakedService.RetrieveMultiple(query);

            Assert.True(results.Entities[0].Attributes.ContainsKey("lastname"));
            Assert.False(results.Entities[0].Attributes.ContainsKey("firstname"));
        }

        [Fact]
        public static void When_a_query_by_attribute_is_executed_with_non_existing_attribute_it_is_not_returned()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetFakedOrganizationService();

            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                LastName = "asdf"
            };

            fakedContext.Initialize(new List<Entity> { contact });

            QueryByAttribute query = new QueryByAttribute("contact");
            query.ColumnSet = new ColumnSet("firstname", "lastname");
            var results = fakedService.RetrieveMultiple(query);

            Assert.True(results.Entities[0].Attributes.ContainsKey("lastname"));
            Assert.False(results.Entities[0].Attributes.ContainsKey("firstname"));
        }
    }
}