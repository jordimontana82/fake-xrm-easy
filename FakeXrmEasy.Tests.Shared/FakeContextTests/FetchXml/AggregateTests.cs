using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.FetchXml
{
    public class AggregateTests
    {
        [Fact]
        public void FetchXml_Aggregate_Count()
        {


            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='contactid' alias='count.contacts' aggregate='count' />
                                    <attribute name='lastname' alias='group.lastname' groupby='true' />
                                  </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            ctx.Initialize(new[] {
                new Contact() { Id = Guid.NewGuid(), LastName = "Smith", FirstName = "John" },
                new Contact() { Id = Guid.NewGuid(), LastName = "Smith", FirstName = "Jane" },
                new Contact() { Id = Guid.NewGuid(), LastName = "Wood", FirstName = "Sam" },
                new Contact() { Id = Guid.NewGuid(), LastName = "Grant", FirstName = "John" },
            });

            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);

            // Make sure we only have the expected properties
            foreach(var e in collection.Entities)
            {
                Assert.Equal(new[] { "count.contacts", "group.lastname" }, e.Attributes.Keys.OrderBy(x => x));
            }

            var smithGroup = collection.Entities.SingleOrDefault(x => "Smith".Equals(x.GetAttributeValue<AliasedValue>("group.lastname").Value));
            Assert.Equal(2, smithGroup.GetAttributeValue<AliasedValue>("count.contacts").Value);

            var woodGroup = collection.Entities.SingleOrDefault(x => "Wood".Equals(x.GetAttributeValue<AliasedValue>("group.lastname").Value));
            Assert.Equal(1, woodGroup.GetAttributeValue<AliasedValue>("count.contacts").Value);

            var grantGroup = collection.Entities.SingleOrDefault(x => "Grant".Equals(x.GetAttributeValue<AliasedValue>("group.lastname").Value));
            Assert.Equal(1, grantGroup.GetAttributeValue<AliasedValue>("count.contacts").Value);
        }
    }
}
