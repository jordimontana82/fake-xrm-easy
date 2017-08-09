using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using Xunit;

namespace FakeXrmEasy
{
    public class Issue116
    {
        private List<Entity> SetupContactTests()
        {
            var entityList = new List<Entity>();

            var company = new Account() { AccountId = new Guid("0C2C9B96-0DC5-47F4-B9DD-2E6B765823B3"), Name = "Company XYZ" };
            var company2 = new Account() { AccountId = new Guid("190B49D8-119E-4FDC-84B2-0485754CAFE2"), Name = "Company ABC" };

            entityList.Add(new Contact() { Id = new Guid("B6B4B46B-3209-4A8F-8FC8-A16F27CC44F3"), FirstName = "Contact XYZ 1", LastName = "Surname", ParentCustomerId = new EntityReference(company.LogicalName, new Guid("0C2C9B96-0DC5-47F4-B9DD-2E6B765823B3")), EMailAddress1 = "1@xyz.com" });
            entityList.Add(new Contact() { Id = new Guid("57AC385E-1B0D-417D-B030-3307BEB13A2B"), FirstName = "Contact XYZ 2", LastName = "Surname", ParentCustomerId = new EntityReference(company.LogicalName, new Guid("0C2C9B96-0DC5-47F4-B9DD-2E6B765823B3")), EMailAddress1 = "1@xyz.com" });
            entityList.Add(new Contact() { Id = new Guid("E396BEF5-F1A9-4100-A838-02E942AABF56"), FirstName = "Contact XYZ 3", LastName = "Surname", ParentCustomerId = new EntityReference(company.LogicalName, new Guid("0C2C9B96-0DC5-47F4-B9DD-2E6B765823B3")), EMailAddress1 = "1@xyz.com" });

            entityList.Add(new Contact() { Id = new Guid("FC2D1344-3468-4E54-B07F-27DE63BFA899"), FirstName = "Contact ABC 1", LastName = "Surname", ParentCustomerId = new EntityReference(company2.LogicalName, new Guid("190B49D8-119E-4FDC-84B2-0485754CAFE2")), EMailAddress1 = "1@abc.com" });
            entityList.Add(new Contact() { Id = new Guid("BA9BE182-21A0-47F3-8C5C-4A7B36D765C9"), FirstName = "Contact ABC 2", LastName = "Surname", ParentCustomerId = new EntityReference(company2.LogicalName, new Guid("190B49D8-119E-4FDC-84B2-0485754CAFE2")), EMailAddress1 = "1@abc.com" });
            entityList.Add(new Contact() { Id = new Guid("9D78DDC4-46CD-45A3-B806-59ED27E6F91A"), FirstName = "Contact ABC 3", LastName = "Surname", ParentCustomerId = new EntityReference(company2.LogicalName, new Guid("190B49D8-119E-4FDC-84B2-0485754CAFE2")), EMailAddress1 = "1@abc.com" });

            company.PrimaryContactId = new EntityReference(entityList[2].LogicalName, new Guid("B6B4B46B-3209-4A8F-8FC8-A16F27CC44F3"));
            company2.PrimaryContactId = new EntityReference(entityList[2].LogicalName, new Guid("FC2D1344-3468-4E54-B07F-27DE63BFA899"));

            entityList.Add(company);
            entityList.Add(company2);

            return entityList;
        }

        private QueryExpression CreateBrokenTestQuery(string contactId)
        {
            QueryExpression query = new QueryExpression();

            query.PageInfo = new PagingInfo();
            query.PageInfo.Count = 100;
            query.PageInfo.PageNumber = 1;
            query.PageInfo.PagingCookie = null;

            // Setup the query for the contact entity
            query.EntityName = Contact.EntityLogicalName;

            // Specify the columns to retrieve
            query.ColumnSet = new ColumnSet(true);

            query.Criteria = new FilterExpression();
            query.Criteria.FilterOperator = LogicalOperator.And;

            FilterExpression filter = new FilterExpression(LogicalOperator.And);

            // Create the link from the contact to the account entity
            LinkEntity linkAccount = new LinkEntity();
            linkAccount.JoinOperator = JoinOperator.Inner;
            linkAccount.LinkFromEntityName = Contact.EntityLogicalName;
            linkAccount.LinkFromAttributeName = "parentcustomerid";
            linkAccount.LinkToEntityName = Account.EntityLogicalName;
            linkAccount.LinkToAttributeName = "accountid";
            linkAccount.Columns = new ColumnSet(true);
            linkAccount.EntityAlias = "accountItem";

            #region this is the broken additional field condition

            linkAccount.LinkCriteria = new FilterExpression();
            linkAccount.LinkCriteria.FilterOperator = LogicalOperator.And;

            // Create the primary contact condition
            ConditionExpression condition3 = new ConditionExpression();
            condition3.AttributeName = "primarycontactid";
            condition3.Operator = ConditionOperator.Equal;
            condition3.Values.Add(new Guid(contactId));

            linkAccount.LinkCriteria.Conditions.Add(condition3);

            #endregion this is the broken additional field condition

            query.LinkEntities.Add(linkAccount);
            return query;
        }

        private QueryExpression CreateWorkingTestQuery(string contactId)
        {
            QueryExpression query = new QueryExpression();

            query.PageInfo = new PagingInfo();
            query.PageInfo.Count = 100;
            query.PageInfo.PageNumber = 1;
            query.PageInfo.PagingCookie = null;

            // Setup the query for the contact entity
            query.EntityName = Contact.EntityLogicalName;

            // Specify the columns to retrieve
            query.ColumnSet = new ColumnSet(true);

            query.Criteria = new FilterExpression();
            query.Criteria.FilterOperator = LogicalOperator.And;

            FilterExpression filter = new FilterExpression(LogicalOperator.And);

            // Create the link from the contact to the account entity
            LinkEntity linkAccount = new LinkEntity();
            linkAccount.JoinOperator = JoinOperator.Inner;
            linkAccount.LinkFromEntityName = Contact.EntityLogicalName;
            linkAccount.LinkFromAttributeName = "parentcustomerid";
            linkAccount.LinkToEntityName = Account.EntityLogicalName;
            linkAccount.LinkToAttributeName = "accountid";
            linkAccount.Columns = new ColumnSet(true);
            linkAccount.EntityAlias = "accountItem";

            linkAccount.LinkCriteria = new FilterExpression();
            linkAccount.LinkCriteria.FilterOperator = LogicalOperator.And;

            #region this is the working additional field reference

            // Create the primary contact condition
            LinkEntity linkContact = new LinkEntity(Account.EntityLogicalName, Contact.EntityLogicalName, "primarycontactid", "contactid", JoinOperator.Inner);
            linkContact.Columns = new ColumnSet(true);
            linkContact.EntityAlias = "primaryContact";

            linkContact.LinkCriteria = new FilterExpression();
            linkContact.LinkCriteria.FilterOperator = LogicalOperator.And;

            linkContact.LinkCriteria.Conditions.Add(new ConditionExpression("contactid", ConditionOperator.Equal, new Guid(contactId)));
            linkAccount.LinkEntities.Add(linkContact);

            #endregion this is the working additional field reference

            query.LinkEntities.Add(linkAccount);
            return query;
        }

        [Fact]
        public void __QueryExpression_Test_CodeBased__Contact_Account_Contact_Broken()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            ctx.Initialize(SetupContactTests());

            EntityCollection ec = service.RetrieveMultiple(CreateBrokenTestQuery("B6B4B46B-3209-4A8F-8FC8-A16F27CC44F3"));
            Assert.Equal(3, ec.Entities.Count);
        }

        [Fact]
        public void __QueryExpression_Test_CodeBased_Contact_Account_Contact_Working()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            ctx.Initialize(SetupContactTests());

            EntityCollection ec = service.RetrieveMultiple(CreateWorkingTestQuery("B6B4B46B-3209-4A8F-8FC8-A16F27CC44F3"));
            Assert.Equal(3, ec.Entities.Count);
        }
    }
}