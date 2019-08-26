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

            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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
        public void FetchXml_Aggregate_Group_EntityReference_Count()
        {
            var fetchXml = @"<fetch no-lock='true' aggregate='true'> <entity name='account'> <attribute name='parentaccountid' alias='pa' groupby='true' /> <attribute name='accountid' alias='Qt' aggregate='countcolumn' /> <order alias='Qt' descending='true' /> </entity> </fetch>";

            EntityReference parentId = new EntityReference("account", Guid.NewGuid());

            var ctx = new XrmFakedContext();
            ctx.Initialize(new[] {
                new Account() { Id = Guid.NewGuid(), ParentAccountId=parentId },
                new Account() { Id = Guid.NewGuid(), ParentAccountId=parentId },
                new Account() { Id = Guid.NewGuid(), ParentAccountId=new EntityReference("account",Guid.NewGuid()) },
                new Account() { Id = Guid.NewGuid()}
            });

            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);

            var biggestGroup = collection.Entities.Where(x => x.Attributes.ContainsKey("pa")).SingleOrDefault(x => parentId.Id.Equals((x.GetAttributeValue<AliasedValue>("pa")?.Value as EntityReference)?.Id));
            Assert.Equal(2, biggestGroup.GetAttributeValue<AliasedValue>("Qt").Value);
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

            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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

            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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

            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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

            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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

            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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

            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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

            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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
            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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
            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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
            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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
            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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
            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

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
            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            Assert.Equal(1, collection.Entities.First().Attributes.Count);
            Assert.True(collection.Entities.First().Contains("avg"));
        }

        [Fact]
        public void FetchXml_Aggregate_Min_Date()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity e = new Entity("entity");
            e.Id = Guid.NewGuid();
            e["value"] = new DateTime(2011, 01, 01);
            initialEntities.Add(e);

            Entity e2 = new Entity("entity");
            e2.Id = Guid.NewGuid();
            e2["value"] = new DateTime(2011, 01, 02);
            initialEntities.Add(e2);

            Entity e3 = new Entity("entity");
            e3.Id = Guid.NewGuid();
            e3["value"] = new DateTime(2011, 01, 03);
            initialEntities.Add(e3);

            context.Initialize(initialEntities);

            FetchExpression query = new FetchExpression($@"
                <fetch aggregate='true' >
                  <entity name='entity' >
                    <attribute name='value' alias='minvalue' aggregate='min' />
                  </entity>
                </fetch>
                ");

            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(1, result.Entities.Count);
            Assert.Equal(new DateTime(2011, 01, 01), result.Entities.Single().GetAttributeValue<AliasedValue>("minvalue").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Min_Date_With_Nulls()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity e = new Entity("entity");
            e.Id = Guid.NewGuid();
            e["value"] = new DateTime(2011, 01, 01);
            initialEntities.Add(e);

            Entity e2 = new Entity("entity");
            e2.Id = Guid.NewGuid();
            e2["value"] = null;
            initialEntities.Add(e2);

            Entity e3 = new Entity("entity");
            e3.Id = Guid.NewGuid();
            e3["value"] = new DateTime(2011, 01, 03);
            initialEntities.Add(e3);

            context.Initialize(initialEntities);

            FetchExpression query = new FetchExpression($@"
                <fetch aggregate='true' >
                  <entity name='entity' >
                    <attribute name='value' alias='minvalue' aggregate='min' />
                  </entity>
                </fetch>
                ");

            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(1, result.Entities.Count);
            Assert.Equal(new DateTime(2011, 01, 01), result.Entities.Single().GetAttributeValue<AliasedValue>("minvalue").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Max_Date()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity e = new Entity("entity");
            e.Id = Guid.NewGuid();
            e["value"] = new DateTime(2011, 01, 01);
            initialEntities.Add(e);

            Entity e2 = new Entity("entity");
            e2.Id = Guid.NewGuid();
            e2["value"] = new DateTime(2011, 01, 02);
            initialEntities.Add(e2);

            Entity e3 = new Entity("entity");
            e3.Id = Guid.NewGuid();
            e3["value"] = new DateTime(2011, 01, 03);
            initialEntities.Add(e3);

            context.Initialize(initialEntities);

            FetchExpression query = new FetchExpression($@"
                <fetch aggregate='true' >
                  <entity name='entity' >
                    <attribute name='value' alias='maxvalue' aggregate='max' />
                  </entity>
                </fetch>
                ");

            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(1, result.Entities.Count);
            Assert.Equal(new DateTime(2011, 01, 03), result.Entities.Single().GetAttributeValue<AliasedValue>("maxvalue").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Max_Date_With_Nulls()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity e = new Entity("entity");
            e.Id = Guid.NewGuid();
            e["value"] = new DateTime(2011, 01, 01);
            initialEntities.Add(e);

            Entity e2 = new Entity("entity");
            e2.Id = Guid.NewGuid();
            e2["value"] = null;
            initialEntities.Add(e2);

            Entity e3 = new Entity("entity");
            e3.Id = Guid.NewGuid();
            e3["value"] = new DateTime(2011, 01, 03);
            initialEntities.Add(e3);

            context.Initialize(initialEntities);

            FetchExpression query = new FetchExpression($@"
                <fetch aggregate='true' >
                  <entity name='entity' >
                    <attribute name='value' alias='maxvalue' aggregate='max' />
                  </entity>
                </fetch>
                ");

            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(1, result.Entities.Count);
            Assert.Equal(new DateTime(2011, 01, 03), result.Entities.Single().GetAttributeValue<AliasedValue>("maxvalue").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Min_With_Nulls()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity e = new Entity("entity");
            e.Id = Guid.NewGuid();
            e["value"] = 1;
            initialEntities.Add(e);

            Entity e2 = new Entity("entity");
            e2.Id = Guid.NewGuid();
            e2["value"] = 2;
            initialEntities.Add(e2);

            Entity e3 = new Entity("entity");
            e3.Id = Guid.NewGuid();
            e3["value"] = null;
            initialEntities.Add(e3);

            context.Initialize(initialEntities);

            FetchExpression query = new FetchExpression($@"
                <fetch aggregate='true' >
                  <entity name='entity' >
                    <attribute name='value' alias='minvalue' aggregate='min' />
                  </entity>
                </fetch>
                ");

            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(1, result.Entities.Count);
            Assert.Equal(1, result.Entities.Single().GetAttributeValue<AliasedValue>("minvalue").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Max_With_Nulls()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity e = new Entity("entity");
            e.Id = Guid.NewGuid();
            e["value"] = -0.5m;
            initialEntities.Add(e);

            Entity e2 = new Entity("entity");
            e2.Id = Guid.NewGuid();
            e2["value"] = -2m;
            initialEntities.Add(e2);

            Entity e3 = new Entity("entity");
            e3.Id = Guid.NewGuid();
            e3["value"] = null;
            initialEntities.Add(e3);

            context.Initialize(initialEntities);

            FetchExpression query = new FetchExpression($@"
                <fetch aggregate='true' >
                  <entity name='entity' >
                    <attribute name='value' alias='maxvalue' aggregate='max' />
                  </entity>
                </fetch>
                ");

            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(1, result.Entities.Count);
            Assert.Equal(-0.5m, result.Entities.Single().GetAttributeValue<AliasedValue>("maxvalue").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Avg_With_Nulls()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity e = new Entity("entity");
            e.Id = Guid.NewGuid();
            e["value"] = -1m;
            initialEntities.Add(e);

            Entity e2 = new Entity("entity");
            e2.Id = Guid.NewGuid();
            e2["value"] = -2m;
            initialEntities.Add(e2);

            Entity e3 = new Entity("entity");
            e3.Id = Guid.NewGuid();
            e3["value"] = null;
            initialEntities.Add(e3);

            context.Initialize(initialEntities);

            FetchExpression query = new FetchExpression($@"
                <fetch aggregate='true' >
                  <entity name='entity' >
                    <attribute name='value' alias='avgvalue' aggregate='avg' />
                  </entity>
                </fetch>
                ");

            EntityCollection result = service.RetrieveMultiple(query);
            Assert.Equal(1, result.Entities.Count);
            Assert.Equal(-1.5m, result.Entities.Single().GetAttributeValue<AliasedValue>("avgvalue").Value);
        }

        [Fact]
        public void FetchXml_Aggregate_Sum_With_Linked_Entity()
        {
            var context = new XrmFakedContext();
            var contact = new Contact() { Id = Guid.NewGuid() };
            var sale1 = new SalesOrder() { Id = Guid.NewGuid() };
            sale1.CustomerId = contact.ToEntityReference();
            sale1.TotalAmount = new Money(10m);
            sale1.DateFulfilled = new DateTime(2019, 1, 1);
            context.Initialize(new Entity[] { contact, sale1 });

            EntityCollection result = context.GetOrganizationService().RetrieveMultiple(new FetchExpression(@"
<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true' aggregate='true'>
  <entity name='contact'>
	<attribute name='contactid' groupby='true' alias='agg_contactid' />	
    <link-entity name='salesorder' from='customerid' to='contactid' alias='sales12m' link-type='inner'>
		<attribute name='totalamount' aggregate='sum' alias='TotalAmount' />
        <filter type='and'>
	      <condition attribute='datefulfilled' operator='on-or-before' value='2019-02-01' />
	    </filter>
	</link-entity>
  </entity>
</fetch>"));


            Assert.Equal(1, result.Entities.Count);
            var value = result.Entities.First().GetAttributeValue<AliasedValue>("sales12m.TotalAmount");
            Assert.NotNull(value);
            Assert.Equal(10m, ((Money)value.Value).Value);
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