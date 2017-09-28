using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.FetchXml
{
    public class AggregateTests
    {
        [Fact]
        public void FetchXml_Aggregate_Group_Count()
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
            foreach (var e in collection.Entities)
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
        public void FetchXml_Aggregate_CountDistinct()
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='lastname' alias='count' aggregate='countcolumn' distinct='true'/>
                                  </entity>
                            </fetch>";
            var ctx = new XrmFakedContext();
            ctx.Initialize(new[] {
                new Contact() { Id = Guid.NewGuid(), LastName = "A" },
                new Contact() { Id = Guid.NewGuid(), LastName = "A" },
                new Contact() { Id = Guid.NewGuid(), LastName = "A" },

                new Contact() { Id = Guid.NewGuid(), LastName = "B" },
                new Contact() { Id = Guid.NewGuid(), LastName = "B" },

                new Contact() { Id = Guid.NewGuid(), LastName = "C" },
            });

            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            var ent = collection.Entities[0];

            Assert.Equal(3, ent.GetAttributeValue<AliasedValue>("count")?.Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Sum_Int()
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='numberofchildren' alias='sum' aggregate='sum'/>
                                  </entity>
                            </fetch>";
            var ctx = new XrmFakedContext();
            ctx.Initialize(new[] {
                new Contact() { Id = Guid.NewGuid(), NumberOfChildren = 1 },
                new Contact() { Id = Guid.NewGuid(), NumberOfChildren = 2 },
                new Contact() { Id = Guid.NewGuid(),  }, /* attribute missing */
                new Contact() { Id = Guid.NewGuid(), NumberOfChildren = null },
            });

            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            var ent = collection.Entities[0];

            Assert.Equal(3, ent.GetAttributeValue<AliasedValue>("sum")?.Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Sum_Money()
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' aggregate='true'>
                              <entity name='salesorderdetail'>
                                    <attribute name='priceperunit' alias='sum' aggregate='sum'/>
                                  </entity>
                            </fetch>";
            var ctx = new XrmFakedContext();
            ctx.Initialize(new[] {
                new SalesOrderDetail() { Id = Guid.NewGuid(), PricePerUnit = new Money(100m) },
                new SalesOrderDetail() { Id = Guid.NewGuid(), PricePerUnit = new Money(100m)},
                new SalesOrderDetail() { Id = Guid.NewGuid(),  }, /* attribute missing */
                new SalesOrderDetail() { Id = Guid.NewGuid(), PricePerUnit = null },
            });

            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            var ent = collection.Entities[0];

            Assert.IsType(typeof(Money), ent.GetAttributeValue<AliasedValue>("sum")?.Value);
            Assert.Equal(200m, (ent.GetAttributeValue<AliasedValue>("sum")?.Value as Money)?.Value);
        }

        private Contact[] BirthdateContacts = new[]
        {
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1980, 1, 1), NumberOfChildren = 1 },
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1980, 2, 1), NumberOfChildren = 2 },
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1981, 1, 2) },
                new Contact() { Id = Guid.NewGuid(), BirthDate = new DateTime(1981, 5, 2) },
        };

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
            ctx.Initialize(BirthdateContacts);

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
            ctx.Initialize(BirthdateContacts);

            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);
            var byMonth = collection.Entities.ToDictionary(x => x.GetAttributeValue<AliasedValue>("group.dob").Value as int?);
            Assert.Equal(new int?[] { 1, 2, 5 }, byMonth.Keys.OrderBy(x => x));

            Assert.Equal(2, byMonth[1].GetAttributeValue<AliasedValue>("count.contacts").Value);
            Assert.Equal(1, byMonth[2].GetAttributeValue<AliasedValue>("count.contacts").Value);
            Assert.Equal(1, byMonth[5].GetAttributeValue<AliasedValue>("count.contacts").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Dategroup_Quarter()
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='contactid' alias='count.contacts' aggregate='count' />
                                    <attribute name='birthdate' alias='group.dob' groupby='true' dategrouping='quarter' />
                                  </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            ctx.Initialize(BirthdateContacts);

            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(2, collection.Entities.Count);
            var byQuarter = collection.Entities.ToDictionary(x => x.GetAttributeValue<AliasedValue>("group.dob").Value as int?);
            Assert.Equal(new int?[] { 1, 2 }, byQuarter.Keys.OrderBy(x => x));

            Assert.Equal(3, byQuarter[1].GetAttributeValue<AliasedValue>("count.contacts").Value);
            Assert.Equal(1, byQuarter[2].GetAttributeValue<AliasedValue>("count.contacts").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Dategroup_Day()
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='contactid' alias='count.contacts' aggregate='count' />
                                    <attribute name='birthdate' alias='group.dob' groupby='true' dategrouping='day' />
                                  </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            ctx.Initialize(BirthdateContacts);

            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(2, collection.Entities.Count);
            var byDay = collection.Entities.ToDictionary(x => x.GetAttributeValue<AliasedValue>("group.dob").Value as int?);
            Assert.Equal(new int?[] { 1, 2 }, byDay.Keys.OrderBy(x => x));

            Assert.Equal(2, byDay[1].GetAttributeValue<AliasedValue>("count.contacts").Value);
            Assert.Equal(2, byDay[2].GetAttributeValue<AliasedValue>("count.contacts").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_OrderByGroup()
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='contactid' alias='count' aggregate='count' />
                                    <attribute name='birthdate' alias='month' groupby='true' dategrouping='month'  />
                                    <order alias='month' />
                               </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            ctx.Initialize(BirthdateContacts);
            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);
            Assert.Equal(new int?[] { 1, 2, 5 }, collection.Entities.Select(x => x.GetAttributeValue<AliasedValue>("month")?.Value as int?));
        }

        [Fact]
        public void FetchXml_Aggregate_OrderByGroupDescending()
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='contactid' alias='count' aggregate='count' />
                                    <attribute name='birthdate' alias='month' groupby='true' dategrouping='month'  />
                                    <order alias='month' descending='true' />
                               </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            ctx.Initialize(BirthdateContacts);
            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);
            Assert.Equal(new int?[] { 5, 2, 1 }, collection.Entities.Select(x => x.GetAttributeValue<AliasedValue>("month")?.Value as int?));
        }

        [Fact]
        public void FetchXml_Aggregate_OrderByAggregate()
        {
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='contactid' alias='count' aggregate='count' />
                                    <attribute name='birthdate' alias='month' groupby='true' dategrouping='month'  />
                                    <order alias='count' />
                               </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            ctx.Initialize(BirthdateContacts);
            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);
            Assert.Equal(new int?[] { 1, 1, 2 }, collection.Entities.Select(x => x.GetAttributeValue<AliasedValue>("count")?.Value as int?));
        }

        [Fact]
        public void FetchXml_Aggregate_NoRows_NoGroups_Count()
        {
            // When there are no groupings and no matching rows, a count should return a single entity with the count alias set to 0

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='contactid' alias='count.contacts' aggregate='count' />
                               </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            Assert.Equal(0, collection.Entities.First().GetAttributeValue<AliasedValue>("count.contacts")?.Value);
        }

        [Fact]
        public void FetchXml_Aggregate_NoRows_NoGroups_Sum()
        {
            // When there are no groupings and no matching rows, a sum returns a single entity, with no attributes set

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='numberofchildren' alias='sum' aggregate='sum' />
                               </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            Assert.Equal(1, collection.Entities.First().Attributes.Count);
            Assert.True(collection.Entities.First().Contains("sum"));
        }

        [Fact]
        public void FetchXml_Aggregate_NoRows_NoGroups_Avg()
        {
            // When there are no groupings and no matching rows, an avg returns a single entity, with no attributes set

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' aggregate='true'>
                              <entity name='contact'>
                                    <attribute name='numberofchildren' alias='avg' aggregate='avg' />
                               </entity>
                            </fetch>";

            var ctx = new XrmFakedContext();
            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            Assert.Equal(1, collection.Entities.First().Attributes.Count);
            Assert.True(collection.Entities.First().Contains("avg"));
        }

        [Fact]
        public void Query_Should_Return_QuoteProduct_Counts()
        {
            var context = new XrmFakedContext();
            var quoteId = Guid.NewGuid();

            context.Initialize(new List<Entity>()
            {
                new QuoteDetail()
                {
                    Id = Guid.NewGuid(),
                    ProductId = new EntityReference("product", Guid.NewGuid()),
                    Quantity = 4M,
                    QuoteId = new EntityReference("quote", quoteId)
                }
            });

            var service = context.GetOrganizationService();

            string fetchXml =
                $@"<fetch aggregate='true' >
                <entity name='quotedetail' >
                <attribute name='productid' alias='ProductCount' aggregate='count' />
                <filter>
                    <condition attribute='quoteid' operator='eq' value='{quoteId}' />
                </filter>
                </entity>
            </fetch>";

            EntityCollection collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
        }
    }
}