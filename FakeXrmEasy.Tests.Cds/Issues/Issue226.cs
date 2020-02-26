using Crm;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue226
    {
        [Fact]
        public void FetchXml_Operator_Older_Than_X_Months_WithoutDateAttribute_Execution()
        {
            var contact1 = new Contact
            {
                Id = Guid.NewGuid(),
                //remove it or make it null then exception "The given key was not presented in the dictionary" will be thrown
                //BirthDate = new DateTime(2017, 8, 7)
            };

            var contact2 = new Contact
            {
                Id = Guid.NewGuid(),
                BirthDate = new DateTime(2017, 3, 7)
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

            var ctx = new XrmFakedContext();
            ctx.Initialize(new[] { contact1, contact2 });
            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
        }
    }
}
