using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

using Crm;
using Microsoft.Xrm.Sdk.Messages;
using System.Collections.ObjectModel;

namespace FakeXrmEasy.Tests.FakeContextTests.QueryByAttributeTests
{
    public class Tests
    {
        [Fact]
        public static void When_a_query_by_attribute_is_executed_when_one_attribute_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var account = new Account() {Id = Guid.NewGuid(), Name = "Test"};
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
    }
}
