using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using FakeXrmEasy;
using FakeXrmEasy.OrganizationFaults;
using Xunit;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using Crm;
using Microsoft.Xrm.Sdk.Messages;
using System.ServiceModel;  //TypedEntities generated code for testing

namespace FakeXrmEasy.Tests
{
    public class FakeContextTestTranslateQueryExpression
    {
        [Fact]
        public void When_translating_a_null_query_expression_the_linq_query_is_also_null()
        {
            var context = new XrmFakedContext();
            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, null);
            Assert.True(result == null);
        }

        [Fact]
        public void When_translating_a_query_from_a_non_existing_entity_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            IQueryable<Entity> data = new List<Entity>() {
                new Entity("account") { Id = guid1 },
                new Entity("account") { Id = guid2 },
                new Entity("contact") { Id = guid3 }
            }.AsQueryable();

            context.Initialize(data);
            var qe = new QueryExpression() { EntityName = "nonexistingentityname"};
            Assert.Throws<Exception>(() => XrmFakedContext.TranslateQueryExpressionToLinq(context, qe));
        }
        
        [Fact]
        public void When_executing_a_query_expression_with_a_simple_join_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2";
            
            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            context.Initialize(new List<Entity>() { account, contact1, contact2});

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner,
                    Columns = new ColumnSet(new string[] { "name" })
                }
            );
            qe.ColumnSet = new ColumnSet(new string[] { "fullname", "parentcustomerid" });

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 2); //2 Contacts related to the same account
            var firstContact = result.FirstOrDefault();
            var secondContact = result.LastOrDefault();

            Assert.True(firstContact.Attributes.Count == 3);
            Assert.True(secondContact.Attributes.Count == 3);

            Assert.True(firstContact["fullname"].Equals(contact1["fullname"]));
            Assert.True((firstContact["account.name"] as AliasedValue).Value.Equals(account["name"]));

            Assert.True(secondContact["fullname"].Equals(contact2["fullname"]));
            Assert.True((secondContact["account.name"] as AliasedValue).Value.Equals(account["name"]));

        }
        [Fact]
        public void When_executing_a_query_expression_with_a_left_join_all_left_hand_side_elements_are_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            //Contact3 doesnt have a parent customer, but must be returned anyway (left outer)

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.LeftOuter,
                    Columns = new ColumnSet(new string[] { "name" })
                }
            );
            qe.ColumnSet = new ColumnSet(new string[] { "fullname", "parentcustomerid" });

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 3); //2 Contacts related to the same account + 1 contact without parent account
            var firstContact = result.FirstOrDefault();
            var lastContact = result.LastOrDefault();

            Assert.True(firstContact["fullname"].Equals(contact1["fullname"]));
            Assert.True(lastContact["fullname"].Equals(contact3["fullname"]));
        }

        [Fact]
        public void When_executing_a_query_expression_join_with_orphans_these_are_not_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            //Contact3 doesnt have a parent customer, but must be returned anyway (left outer)

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner,
                    Columns = new ColumnSet(new string[] { "name" })
                }
            );
            qe.ColumnSet = new ColumnSet(new string[] { "fullname", "parentcustomerid" });

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 2); 
            var firstContact = result.FirstOrDefault();
            var lastContact = result.LastOrDefault();

            Assert.True(firstContact["fullname"].Equals(contact1["fullname"]));
            Assert.True(lastContact["fullname"].Equals(contact2["fullname"]));
        }

        [Fact]
        public void When_executing_a_query_expression_only_the_selected_columns_in_the_columnset_are_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3"; contact3["firstname"] = "First 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner,
                    Columns = new ColumnSet(new string[] { "name" })
                }
            );

            //We only select fullname and parentcustomerid, firstname should not be included
            qe.ColumnSet = new ColumnSet(new string[] { "fullname", "parentcustomerid" });

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 2);
            var firstContact = result.FirstOrDefault();
            var lastContact = result.LastOrDefault();

            Assert.False(firstContact.Attributes.ContainsKey("firstname"));
            Assert.False(lastContact.Attributes.ContainsKey("firstname"));
        }

        [Fact]
        public void When_executing_a_query_expression_with_an_attribute_in_columnset_that_doesnt_exists_descriptive_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account

            context.Initialize(new List<Entity>() { account, contact1 });

            var qe = new QueryExpression() { EntityName = "contact" };

            //We only select fullname and parentcustomerid, firstname should not be included
            qe.ColumnSet = new ColumnSet(new string[] { "this attribute doesnt exists!" });

            var exception = Assert.Throws<FaultException<OrganizationServiceFault>>(() => XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList());
            Assert.Equal(exception.Detail.ErrorCode, OrganizationServiceFaultQueryBuilderNoAttributeException.ErrorCode);
        }

        [Fact]
        public void When_executing_a_query_expression_with_an_attribute_in_columnset_in_a_linked_entity_that_doesnt_exists_descriptive_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3"; contact3["firstname"] = "First 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner,
                    Columns = new ColumnSet(new string[] { "this attribute does not exists" })
                }
            );

            //We only select fullname and parentcustomerid, firstname should not be included
            qe.ColumnSet = new ColumnSet(new string[] { "this attribute doesnt exists!" });

            var exception = Assert.Throws<FaultException<OrganizationServiceFault>>(() => XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList());
            Assert.Equal(exception.Detail.ErrorCode, OrganizationServiceFaultQueryBuilderNoAttributeException.ErrorCode); 
        }

        [Fact]
        public void When_executing_a_query_expression_with_all_attributes_all_of_them_are_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3"; contact3["firstname"] = "First 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner
                }
            );

            //We only select fullname and parentcustomerid, firstname should not be included
            qe.ColumnSet = new ColumnSet(true);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 2);
            var firstContact = result.FirstOrDefault();
            var lastContact = result.LastOrDefault();

            //Contact 1 attributes = 3 + 5 (the extra five are the CreatedOn, ModifiedOn, CreatedBy, ModifiedBy + StateCode attributes generated automatically
            //+ Attributes from the join(account) = 1 + 5

            Assert.True(firstContact.Attributes.Count == 3 + 1 + 5 * 2); 
            Assert.True(lastContact.Attributes.Count == 3 + 1 + 5 * 2);  //Contact 2
        }

        [Fact]
        public void When_executing_a_query_expression_without_columnset_no_attributes_are_returned()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3"; contact3["firstname"] = "First 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner
                }
            );

            qe.ColumnSet = new ColumnSet(false);
            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 2);
            var firstContact = result.FirstOrDefault();
            var lastContact = result.LastOrDefault();

            Assert.True(firstContact.Attributes.Count == 0); //Contact 1
            Assert.True(lastContact.Attributes.Count == 0);  //Contact 2
        }

        [Fact]
        public void When_executing_a_query_expression_with_a_columnset_in_a_linkedentity_attribute_is_returned_with_a_prefix()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3"; contact3["firstname"] = "First 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner,
                    Columns = new ColumnSet(new string[] { "name" } )
                }
            );

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 2);
            var firstContact = result.FirstOrDefault();
            var lastContact = result.LastOrDefault();

            Assert.True(firstContact.Attributes.Count == 1); //Contact 1
            Assert.True(lastContact.Attributes.Count == 1);  //Contact 2

            Assert.True(firstContact.Attributes.ContainsKey("account.name")); //Contact 1
            Assert.True(lastContact.Attributes.ContainsKey("account.name"));  //Contact 2

            Assert.True(firstContact.Attributes["account.name"] is AliasedValue); //Contact 1
            Assert.True(lastContact.Attributes["account.name"] is AliasedValue);  //Contact 2
        }

        [Fact]
        public void When_executing_a_query_expression_with_a_columnset_in_a_linkedentity_attribute_is_returned_with_an_alias()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3"; contact3["firstname"] = "First 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner,
                    Columns = new ColumnSet(new string[] { "name" }),
                    EntityAlias = "myCoolAlias"
                }
            );

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 2);
            var firstContact = result.FirstOrDefault();
            var lastContact = result.LastOrDefault();

            Assert.True(firstContact.Attributes.Count == 1); //Contact 1
            Assert.True(lastContact.Attributes.Count == 1);  //Contact 2

            Assert.True(firstContact.Attributes.ContainsKey("myCoolAlias.name")); //Contact 1
            Assert.True(lastContact.Attributes.ContainsKey("myCoolAlias.name"));  //Contact 2

            Assert.True(firstContact.Attributes["myCoolAlias.name"] is AliasedValue); //Contact 1
            Assert.True(lastContact.Attributes["myCoolAlias.name"] is AliasedValue);  //Contact 2
        }


        #region Filters
        [Fact]
        public void When_executing_a_query_expression_with_simple_equals_condition_expression_returns_right_number_of_results()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3"; contact3["firstname"] = "First 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner
                }
            );
            qe.ColumnSet = new ColumnSet(true);
            //Filter criteria
            qe.Criteria = new FilterExpression()
            {
                FilterOperator = LogicalOperator.And,
                Conditions =
                {
                    new ConditionExpression("fullname", ConditionOperator.Equal, "Contact 1")
                }
            };

            var service = context.GetFakedOrganizationService();
            var result = service.Execute(new RetrieveMultipleRequest()
            {
                Query = qe
            }) as RetrieveMultipleResponse;

            Assert.True(result.EntityCollection.Entities.Count == 1);
            var firstContact = result.EntityCollection.Entities.FirstOrDefault();
            Assert.True(firstContact["fullname"].Equals("Contact 1"));
        }

        [Fact]
        public void When_executing_a_query_expression_with_simple_equals_condition_expression_returns_right_number_of_results_with_execute_request()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3"; contact3["firstname"] = "First 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner
                }
            );
            qe.ColumnSet = new ColumnSet(true);
            //Filter criteria
            qe.Criteria = new FilterExpression()
            {
                FilterOperator = LogicalOperator.And,
                Conditions =
                {
                    new ConditionExpression("fullname", ConditionOperator.Equal, "Contact 1")
                }
            };

            var service = context.GetFakedOrganizationService();
            var result = service.Execute(new RetrieveMultipleRequest()
            {
                Query = qe
            }) as RetrieveMultipleResponse;

            Assert.True(result.EntityCollection.Entities.Count == 1);
            var firstContact = result.EntityCollection.Entities.FirstOrDefault();
            Assert.True(firstContact["fullname"].Equals("Contact 1"));
        }

        [Fact]
        public void When_executing_a_query_expression_with_simple_equals_condition_expression_returns_right_number_of_results_with_retrieve_multiple()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";
            var contact3 = new Entity("contact") { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3"; contact3["firstname"] = "First 3";

            var account = new Entity("account") { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account
            contact2["parentcustomerid"] = account.ToEntityReference();

            context.Initialize(new List<Entity>() { account, contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.LinkEntities.Add(
                new LinkEntity()
                {
                    LinkFromEntityName = "contact",
                    LinkToEntityName = "account",
                    LinkFromAttributeName = "parentcustomerid",
                    LinkToAttributeName = "accountid",
                    JoinOperator = JoinOperator.Inner
                }
            );
            qe.ColumnSet = new ColumnSet(true);
            //Filter criteria
            qe.Criteria = new FilterExpression()
            {
                FilterOperator = LogicalOperator.And,
                Conditions =
                {
                    new ConditionExpression("fullname", ConditionOperator.Equal, "Contact 1")
                }
            };

            var service = context.GetFakedOrganizationService();
            var result = service.RetrieveMultiple(qe);

            Assert.True(result.Entities.Count == 1);
            var firstContact = result.Entities.FirstOrDefault();
            Assert.True(firstContact["fullname"].Equals("Contact 1"));
        }
        #endregion

        [Fact]
        public void When_using_joins_attribute_projection_shouldnt_affect_column_sets()
        {
            var context = new XrmFakedContext();

            Entity invoice_entity = new Entity("invoice");
            invoice_entity.Id = Guid.NewGuid();

            //create the onlinePayment structure
            int opstatus_completed = 108550002;
            int opstatus_new = 108550000;

            Entity op_entity = new Entity("new_onlinepayment");
            op_entity.Id = Guid.NewGuid();
            //op_entity.Attributes.Add("new_ps", new OptionSetValue(opstatus_new));
            op_entity.Attributes.Add("statuscode", new OptionSetValue(1));
            op_entity.Attributes.Add("new_amount", null);
            op_entity.Attributes.Add("new_invoiceid", new EntityReference("invoice", invoice_entity.Id));
            op_entity.Attributes.Add("new_pproviderid", new EntityReference("new_paymentprovider", Guid.NewGuid()));

            //create pmr
            Entity pmr_entity = new Entity("new_invoicepaymentmethod");
            pmr_entity.Id = Guid.NewGuid();
            pmr_entity.Attributes.Add("new_paymentvalue", new Money(100));
            pmr_entity.Attributes.Add("statuscode", new OptionSetValue(1));
            pmr_entity.Attributes.Add("statecode", new OptionSetValue(0));
            pmr_entity.Attributes.Add("new_invoicepaymentmethodid", pmr_entity.Id);
            pmr_entity.Attributes.Add("new_invoiceid", new EntityReference("invoice", invoice_entity.Id));

            //create joining entity
            Entity opi_entity = new Entity("new_onlinepaymentitem");
            opi_entity.Id = Guid.NewGuid();
            opi_entity.Attributes.Add("new_onlinepaymentid", new EntityReference("new_onlinepayment", op_entity.Id));
            opi_entity.Attributes.Add("new_invoicepaymentmethodid", new EntityReference("new_invoicepaymentmethod", pmr_entity.Id));
            opi_entity.Attributes.Add("statuscode", new OptionSetValue(1));
            opi_entity.Attributes.Add("statecode", new OptionSetValue(0));

            //create these objects in crm
            context.Initialize(new List<Entity>() { invoice_entity, pmr_entity, op_entity, opi_entity });

            //create the mock service
            IOrganizationService service = context.GetFakedOrganizationService();

            QueryExpression query = new QueryExpression("new_onlinepaymentitem");
            query.Criteria.AddCondition("new_onlinepaymentid", ConditionOperator.Equal, op_entity.Id);

            LinkEntity LinkPayments = new LinkEntity(
                "new_onlinepaymentitem", "new_invoicepaymentmethod",
                "new_invoicepaymentmethodid", "new_invoicepaymentmethodid",
                JoinOperator.Inner);
            LinkPayments.LinkCriteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            LinkPayments.LinkCriteria.AddCondition("statuscode", ConditionOperator.Equal, 1);
            query.LinkEntities.Add(LinkPayments);

            query.ColumnSet = new ColumnSet("new_invoicepaymentmethodid");

            EntityCollection paymentitems = service.RetrieveMultiple(query);
            Assert.True(paymentitems.Entities.Count == 1);
        }

    }
}
