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

        [Fact]
        public void FetchXml_Aggregate_Dategroup_Year()
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='contactid' alias='count.contacts' aggregate='count' />
                                    <attribute name='birthdate' alias='group.dob' groupby='true' dategrouping='year' />
                                  </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            ctx.Initialize(new[] {
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1980, 1, 1)  },
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1980, 2, 1) },
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1981, 1, 1) },
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1981, 5, 1) },
            });

            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(2, collection.Entities.Count);
            var byYear = collection.Entities.ToDictionary(x => x.GetAttributeValue<AliasedValue>("group.dob").Value as int?);
            Assert.Equal(new int?[] { 1980, 1981 }, byYear.Keys.OrderBy(x => x));

            Assert.Equal(2, byYear[1980].GetAttributeValue<AliasedValue>("count.contacts").Value);
            Assert.Equal(2, byYear[1981].GetAttributeValue<AliasedValue>("count.contacts").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Dategroup_Month()
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='contactid' alias='count.contacts' aggregate='count' />
                                    <attribute name='birthdate' alias='group.dob' groupby='true' dategrouping='month' />
                                  </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            ctx.Initialize(new[] {
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1980, 1, 1)  },
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1980, 2, 1) },
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1981, 1, 1) },
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1981, 5, 1) },
            });

            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);
            var byMonth = collection.Entities.ToDictionary(x => x.GetAttributeValue<AliasedValue>("group.dob").Value as int?);
            Assert.Equal(new int?[] { 1, 2, 5 }, byMonth.Keys.OrderBy(x => x));

            Assert.Equal(2, byMonth[1].GetAttributeValue<AliasedValue>("count.contacts").Value);
            Assert.Equal(1, byMonth[2].GetAttributeValue<AliasedValue>("count.contacts").Value);
            Assert.Equal(1, byMonth[5].GetAttributeValue<AliasedValue>("count.contacts").Value);
        }
    }
}
