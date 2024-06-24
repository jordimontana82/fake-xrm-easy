using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;  //TypedEntities generated code for testing
using Xunit;

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
        public void When_translating_a_query_from_a_non_existing_entity_an_empty_list_is_returned_when_using_dynamic_entities()
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
            var qe = new QueryExpression() { EntityName = "nonexistingentityname" };
            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);
            Assert.Equal(0, result.Count());
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

            context.Initialize(new List<Entity>() { account, contact1, contact2 });

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
            Assert.True((firstContact["account1.name"] as AliasedValue).Value.Equals(account["name"]));

            Assert.True(secondContact["fullname"].Equals(contact2["fullname"]));
            Assert.True((secondContact["account1.name"] as AliasedValue).Value.Equals(account["name"]));
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
        public void When_executing_a_query_expression_with_an_attribute_in_columnset_that_doesnt_exists_no_value_is_returned_with_dynamic_entities()
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

            XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            var list = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.False(list[0].Attributes.ContainsKey("this attribute doesnt exists!"));
        }

        [Fact]
        public void When_executing_a_query_expression_with_an_attribute_in_columnset_that_doesnt_exists_exception_is_raised_with_early_bound_entities()
        {
            var context = new XrmFakedContext();
            var contact1 = new Contact() { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";

            var account = new Account() { Id = Guid.NewGuid() };
            account["name"] = "Account 1";

            contact1["parentcustomerid"] = account.ToEntityReference(); //Both contacts are related to the same account

            context.Initialize(new List<Entity>() { account, contact1 });

            var qe = new QueryExpression() { EntityName = "contact" };

            //We only select fullname and parentcustomerid, firstname should not be included
            qe.ColumnSet = new ColumnSet(new string[] { "this attribute doesnt exists!" });

            var exception = Assert.Throws<FaultException<OrganizationServiceFault>>(() => XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList());
            Assert.Equal(exception.Detail.ErrorCode, (int)ErrorCodes.QueryBuilderNoAttribute);
        }

        [Fact]
        public void When_executing_a_query_expression_with_an_attribute_in_columnset_in_a_linked_entity_that_doesnt_exists_descriptive_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var contact1 = new Contact() { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Contact() { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";
            var contact3 = new Contact() { Id = Guid.NewGuid() }; contact3["fullname"] = "Contact 3"; contact3["firstname"] = "First 3";

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
            Assert.Equal(exception.Detail.ErrorCode, (int)ErrorCodes.QueryBuilderNoAttribute);
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

            //Contact 1 attributes = 4 + 6 (the extra six are the CreatedOn, ModifiedOn, CreatedBy, ModifiedBy, OwnerId + StateCode attributes generated automatically
            //+ Attributes from the join(account) = 2 + 6

            Assert.True(firstContact.Attributes.Count == 18);
            Assert.True(lastContact.Attributes.Count == 18);
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
                    JoinOperator = JoinOperator.Inner,
                    Columns = new ColumnSet(false)
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
                    Columns = new ColumnSet(new string[] { "name" })
                }
            );

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe);

            Assert.True(result.Count() == 2);
            var firstContact = result.FirstOrDefault();
            var lastContact = result.LastOrDefault();

            Assert.True(firstContact.Attributes.Count == 1); //Contact 1
            Assert.True(lastContact.Attributes.Count == 1);  //Contact 2

            Assert.True(firstContact.Attributes.ContainsKey("account1.name")); //Contact 1
            Assert.True(lastContact.Attributes.ContainsKey("account1.name"));  //Contact 2

            Assert.True(firstContact.Attributes["account1.name"] is AliasedValue); //Contact 1
            Assert.True(lastContact.Attributes["account1.name"] is AliasedValue);  //Contact 2
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

            var service = context.GetOrganizationService();
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

            var service = context.GetOrganizationService();
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

            var service = context.GetOrganizationService();
            var result = service.RetrieveMultiple(qe);

            Assert.True(result.Entities.Count == 1);
            var firstContact = result.Entities.FirstOrDefault();
            Assert.True(firstContact["fullname"].Equals("Contact 1"));
        }

        #endregion Filters

        [Fact]
        public void When_using_joins_attribute_projection_shouldnt_affect_column_sets()
        {
            var context = new XrmFakedContext();

            Entity invoice_entity = new Entity("invoice");
            invoice_entity.Id = Guid.NewGuid();

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
            IOrganizationService service = context.GetOrganizationService();

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

        [Fact]
        public void When_querying_nested_link_entities_with_dynamic_entities_right_result_is_returned()
        {
            // create a contact
            var contactId = Guid.NewGuid();
            var contact = new Entity
            {
                LogicalName = "contact",
                Id = contactId,
                Attributes = new AttributeCollection { { "contactid", contactId } }
            };

            // link a child to the contact
            var childId = Guid.NewGuid();
            var child = new Entity
            {
                LogicalName = "child",
                Id = childId,
                Attributes = new AttributeCollection {
                    { "childid", childId },
                    { "contactid", new EntityReference("contact", contact.Id) }
                }
            };

            // link a pet to the child
            var petId = Guid.NewGuid();
            var pet = new Entity
            {
                LogicalName = "pet",
                Id = petId,
                Attributes = new AttributeCollection {
                    { "petid", petId },
                    { "childid", new EntityReference("child", child.Id) }
                }
            };

            // initialise
            var context = new XrmFakedContext();
            context.Initialize(new[] { contact, child, pet });
            var service = context.GetOrganizationService();

            // 1st Query: join contact and child
            //var query1 = new QueryExpression("contact");
            //var link1 = query1.AddLink("child", "contactid", "contactid", JoinOperator.Inner);

            //var count1 = service.RetrieveMultiple(query1).Entities.Count;
            //Console.WriteLine(count1); // returns 1 record (expected)

            // 2nd Query: join contact and child and pet
            var query2 = new QueryExpression("contact");
            var link2 = query2.AddLink("child", "contactid", "contactid", JoinOperator.Inner);
            var link22 = link2.AddLink("pet", "childid", "childid", JoinOperator.Inner);

            var count2 = service.RetrieveMultiple(query2).Entities.Count;
            Assert.Equal(1, count2); // returns 0 records (unexpected?)
        }

        [Fact]
        public void When_querying_early_bound_entities_attribute_not_initialised_returns_null_in_joins()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var role = new Role() { Id = Guid.NewGuid() };
            var parentRole = new Role() { Id = Guid.NewGuid() };

            context.Initialize(new[] { role, parentRole });

            using (var ctx = new XrmServiceContext(service))
            {
                var roleResult = (from r in ctx.CreateQuery<Role>()
                                  join parent in ctx.CreateQuery<Role>() on r.ParentRoleId.Id equals parent.RoleId.Value
                                  select r).FirstOrDefault();

                Assert.Equal(roleResult, null);
            }
        }

        [Fact]
        public void When_querying_early_bound_entities_unexisting_attribute_raises_exception_when_selected()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var role = new Role() { Id = Guid.NewGuid() };
            var parentRole = new Role() { Id = Guid.NewGuid() };

            context.Initialize(new[] { role, parentRole });

            using (var ctx = new XrmServiceContext(service))
            {
                var qe = new QueryExpression() { EntityName = "role" };
                qe.LinkEntities.Add(
                    new LinkEntity()
                    {
                        LinkFromEntityName = "role",
                        LinkToEntityName = "role",
                        LinkFromAttributeName = "parentroleid",
                        LinkToAttributeName = "thisAttributeDoesntExists",
                        JoinOperator = JoinOperator.Inner,
                        Columns = new ColumnSet(new string[] { "thisAttributeDoesntExists" })
                    }
                );

                Assert.Throws<FaultException<OrganizationServiceFault>>(() => XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList());
            }
        }

        [Fact]
        public void When_querying_early_bound_entities_unexisting_attribute_raises_exception_when_linked_to()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var role = new Role() { Id = Guid.NewGuid() };
            var parentRole = new Role() { Id = Guid.NewGuid() };

            context.Initialize(new[] { role, parentRole });

            using (var ctx = new XrmServiceContext(service))
            {
                var qe = new QueryExpression() { EntityName = "role" };
                qe.LinkEntities.Add(
                    new LinkEntity()
                    {
                        LinkFromEntityName = "role",
                        LinkToEntityName = "role",
                        LinkFromAttributeName = "parentroleid",
                        LinkToAttributeName = "thisAttributeDoesntExists",
                        JoinOperator = JoinOperator.Inner,
                        Columns = new ColumnSet(new string[] { "roleid" })
                    }
                );

                Assert.Throws<FaultException<OrganizationServiceFault>>(() => XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList());
            }
        }

        [Fact]
        public void When_retrieve_multiple_is_invoked_with_a_service_created_entity_that_entity_is_returned_with_logical_name()
        {
            var context = new XrmFakedContext();

            var service = context.GetOrganizationService();

            Entity account1 = new Entity("account");
            account1.Id = Guid.NewGuid();
            account1.Attributes.Add("name", "Account1");

            Entity account2 = new Entity("account");
            account2.Id = Guid.NewGuid();
            account2.Attributes.Add("name", "Account2");

            service.Create(account1);
            service.Create(account2);

            QueryExpression query = new QueryExpression { EntityName = "account", ColumnSet = new ColumnSet(true) };

            var result = service.RetrieveMultiple(query);

            foreach (var item in result.Entities)
            {
                Assert.NotNull(item.LogicalName);
            }
        }

        [Fact]
        public static void Should_Not_Fail_On_Conditions_In_Link_Entities_Multiple()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            fakedContext.AddRelationship("new_invoicepaymentmethod_invoicedetail",
                new XrmFakedRelationship("new_invoicepaymentmethod_invoicedetail",
                            "invoicedetailid", "new_invoicepaymentmethodid",
                            "invoicedetail",
                            "new_invoicepaymentmethod"));

            Entity product01 = new Entity("product");
            product01.Id = Guid.NewGuid();
            product01.Attributes.Add("name", "Test Product");

            Entity invoicedetail01 = new Entity("invoicedetail");
            invoicedetail01.Id = Guid.NewGuid();
            invoicedetail01.Attributes.Add("invoicedetailid", invoicedetail01.Id);
            invoicedetail01.Attributes.Add("new_productid", new EntityReference("product", product01.Id));

            Entity pmr01 = new Entity("new_invoicepaymentmethod");
            pmr01.Id = Guid.NewGuid();
            pmr01.Attributes.Add("new_invoicepaymentmethodid", pmr01.Id);
            pmr01.Attributes.Add("new_name", "PMR0000000001");

            Entity invoicedetail02 = new Entity("invoicedetail");
            invoicedetail02.Id = Guid.NewGuid();
            invoicedetail02.Attributes.Add("invoicedetailid", invoicedetail02.Id);
            invoicedetail02.Attributes.Add("new_productid", new EntityReference("product", product01.Id));

            Entity pmr02 = new Entity("new_invoicepaymentmethod");
            pmr02.Id = Guid.NewGuid();
            pmr02.Attributes.Add("new_invoicepaymentmethodid", pmr02.Id);
            pmr02.Attributes.Add("new_name", "PMR0000000002");

            fakedService.Create(product01);

            fakedService.Create(invoicedetail01);
            fakedService.Create(invoicedetail02);
            fakedService.Create(pmr01);
            fakedService.Create(pmr02);

            fakedService.Associate("invoicedetail", invoicedetail01.Id, new Relationship("new_invoicepaymentmethod_invoicedetail"), new EntityReferenceCollection() { pmr01.ToEntityReference() });
            fakedService.Associate("invoicedetail", invoicedetail02.Id, new Relationship("new_invoicepaymentmethod_invoicedetail"), new EntityReferenceCollection() { pmr02.ToEntityReference() });

            EntityCollection invoiceDetails = new EntityCollection();

            QueryExpression query = new QueryExpression("invoicedetail");
            query.ColumnSet = new ColumnSet(true);
            LinkEntity link1 = new LinkEntity();
            link1.JoinOperator = JoinOperator.Natural;
            link1.LinkFromEntityName = "invoicedetail";
            link1.LinkFromAttributeName = "invoicedetailid";
            link1.LinkToEntityName = "new_invoicepaymentmethod_invoicedetail";
            link1.LinkToAttributeName = "invoicedetailid";

            LinkEntity link2 = new LinkEntity();
            link2.JoinOperator = JoinOperator.Natural;
            link2.LinkFromEntityName = "new_invoicepaymentmethod_invoicedetail";
            link2.LinkFromAttributeName = "new_invoicepaymentmethodid";
            link2.LinkToEntityName = "new_invoicepaymentmethod";
            link2.LinkToAttributeName = "new_invoicepaymentmethodid";
            link2.LinkCriteria = new FilterExpression(LogicalOperator.And);

            ConditionExpression condition1 = new ConditionExpression("new_invoicepaymentmethodid", ConditionOperator.Equal, pmr02.Id);

            link2.LinkCriteria.Conditions.Add(condition1);
            link1.LinkEntities.Add(link2);
            query.LinkEntities.Add(link1);

            invoiceDetails = fakedService.RetrieveMultiple(query);

            Assert.Equal(1, invoiceDetails.Entities.Count);
            Assert.Equal(invoicedetail02.Id, invoiceDetails.Entities[0].Id);
        }
    }
}