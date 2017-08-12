using Crm;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.TranslateQueryExpressionTests.OperatorTests.DateTimes
{
    public class DateTimeOperatorsTests
    {
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
