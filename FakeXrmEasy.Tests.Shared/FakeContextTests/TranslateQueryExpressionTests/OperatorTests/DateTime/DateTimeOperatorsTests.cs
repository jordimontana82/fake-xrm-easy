using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.TranslateQueryExpressionTests.OperatorTests.DateTimes
{
    public class DateTimeOperatorsTests
    {
        [Fact]
        public void When_executing_a_query_expression_with_on_operator_time_part_is_ignored()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact") {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 28, 10, 10, 10)
            };
            var contact2 = new Entity("contact") {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 28, 0, 0, 0)
            };
            var contact3 = new Entity("contact") {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 29, 0, 0, 0)  //Not included
            };

            context.Initialize(new List<Entity>() { contact1, contact2, contact3 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("anniversary", ConditionOperator.On, new DateTime(2017, 07, 28, 0, 0, 0));
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 2);
            Assert.True(result[0].Id == contact1.Id);
            Assert.True(result[1].Id == contact2.Id);
        }

        [Fact]
        public void When_executing_a_query_expression_with_on_or_after_operator_time_part_is_ignored()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 28, 10, 10, 10)
            };
            var contact2 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 28, 0, 0, 0)
            };
            var contact3 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 29, 0, 0, 0)  
            };
            var contact4 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 27, 0, 0, 0)  //Not included
            };

            context.Initialize(new List<Entity>() { contact1, contact2, contact3, contact4 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("anniversary", ConditionOperator.OnOrAfter, new DateTime(2017, 07, 28, 0, 0, 0));
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 3);
            Assert.True(result[0].Id == contact1.Id);
            Assert.True(result[1].Id == contact2.Id);
            Assert.True(result[2].Id == contact3.Id);
        }
        [Fact]
        public void When_executing_a_query_expression_with_on_or_before_operator_time_part_is_ignored()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 28, 10, 10, 10)
            };
            var contact2 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 28, 0, 0, 0)
            };
            var contact3 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 29, 0, 0, 0) //Not included
            };
            var contact4 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = new DateTime(2017, 07, 27, 0, 0, 0)  
            };

            context.Initialize(new List<Entity>() { contact1, contact2, contact3, contact4 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("anniversary", ConditionOperator.OnOrBefore, new DateTime(2017, 07, 28, 0, 0, 0));
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 3);
            Assert.True(result[0].Id == contact1.Id);
            Assert.True(result[1].Id == contact2.Id);
            Assert.True(result[2].Id == contact4.Id);
        }

        [Fact]
        public void When_executing_a_query_expression_with_today_operator_time_part_is_ignored()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = DateTime.Today.AddSeconds(3)
            };
            var contact2 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = DateTime.Today.AddDays(1).AddSeconds(3)
            };

            context.Initialize(new List<Entity>() { contact1, contact2 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("anniversary", ConditionOperator.Today, new DateTime(2017, 07, 28, 0, 0, 0));
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 1);
            Assert.True(result[0].Id == contact1.Id);
        }

        [Fact]
        public void When_executing_a_query_expression_with_yesterday_operator_time_part_is_ignored()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = DateTime.Today.AddSeconds(3)
            };
            var contact2 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = DateTime.Today.AddDays(-1).AddSeconds(3)
            };

            context.Initialize(new List<Entity>() { contact1, contact2 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("anniversary", ConditionOperator.Yesterday, new DateTime(2017, 07, 28, 0, 0, 0));
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 1);
            Assert.True(result[0].Id == contact2.Id);
        }

        [Fact]
        public void When_executing_a_query_expression_with_tomorrow_operator_time_part_is_ignored()
        {
            var context = new XrmFakedContext();
            var contact1 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = DateTime.Today.AddSeconds(3)
            };
            var contact2 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["anniversary"] = DateTime.Today.AddDays(1).AddSeconds(3)
            };

            context.Initialize(new List<Entity>() { contact1, contact2 });

            var qe = new QueryExpression() { EntityName = "contact" };
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria = new FilterExpression(LogicalOperator.And);
            var condition = new ConditionExpression("anniversary", ConditionOperator.Tomorrow, new DateTime(2017, 07, 28, 0, 0, 0));
            qe.Criteria.AddCondition(condition);

            var result = XrmFakedContext.TranslateQueryExpressionToLinq(context, qe).ToList();

            Assert.True(result.Count() == 1);
            Assert.True(result[0].Id == contact2.Id);
        }

        [Fact]
        public void When_executing_a_query_expression_with_older_than_x_months_and_null_right_result_is_returned()
        {
            var ctx = new XrmFakedContext();
            var contact1 = new Contact
            {
                Id = Guid.NewGuid()
            };  //birthdate null

            var contact2 = new Contact
            {
                Id = Guid.NewGuid(),
                BirthDate = new DateTime(2017, 3, 7)  //Older than 3 months
            };

            var contact3 = new Contact
            {
                Id = Guid.NewGuid(),
                BirthDate = DateTime.Now
            };

            var fetchXml = @"
                    <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' page='1'>
                        <entity name='contact'>
                            <attribute name='contactid' />
                            <attribute name='birthdate' />
                            <filter type='and' >
                                <condition attribute='birthdate' operator='olderthan-x-months' value='3' />
                            </filter>
                        </entity>
                    </fetch>";

            
            ctx.Initialize(new[] { contact1, contact2, contact3 });
            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            Assert.Equal(contact2.Id, collection.Entities[0].Id);
        }


    }
}
