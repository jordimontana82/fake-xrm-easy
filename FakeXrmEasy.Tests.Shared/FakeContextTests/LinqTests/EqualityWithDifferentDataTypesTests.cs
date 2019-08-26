using Crm;
using FakeItEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;  //TypedEntities generated code for testing
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.LinqTests
{
    /// <summary>
    /// Test suite to check that all the different CRM types in the SDK are supported:
    /// https://msdn.microsoft.com/en-us/library/gg328507%28v=crm.6%29.aspx
    /// </summary>
    public class EqualityWithDifferentDataTypesTests
    {
        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_strings_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid, FirstName = "Jordi" }
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName == "Jordi"
                               select c).FirstOrDefault();

                Assert.True(contact != null);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_strings_with_date_format_right_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid, FirstName = "11.1" }
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName == "11.1"
                               select c).FirstOrDefault();

                Assert.True(contact != null);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_booleans_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid, IsBackofficeCustomer = true},
                new Contact() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.IsBackofficeCustomer != null && c.IsBackofficeCustomer.Value == true
                               where c.IsBackofficeCustomer.Value
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_boolean_managed_properties_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new Report() { Id = guid, IsCustomizable = new BooleanManagedProperty(true) },
                new Report() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Report>()
                               where c.IsCustomizable.Value == true
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_integers_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid, NumberOfChildren = 2},
                new Contact() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.NumberOfChildren != null && c.NumberOfChildren.Value == 2
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_longs_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid },
                new Contact() { Id = Guid.NewGuid()}  //To test also nulls
            });

            fakedContext.Data["contact"][guid]["versionnumber"] = long.MaxValue; //Couldn´t be set by the Proxy types but set here just for testing long data types

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.VersionNumber == long.MaxValue
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_dates_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid, BirthDate = DateTime.Today },
                new Contact() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.BirthDate != null && c.BirthDate == DateTime.Today
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_date_times_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid, BirthDate = new DateTime(2015,02,26,3,42,59) },
                new Contact() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.BirthDate != null && c.BirthDate == new DateTime(2015, 02, 26, 3, 42, 59)
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_decimals_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new SalesOrderDetail() { Id = guid, Quantity = 1.1M },
                new SalesOrderDetail() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<SalesOrderDetail>()
                               where c.Quantity == 1.1M
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_doubles_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            var barcelonaLatitude = 41.387128;
            fakedContext.Initialize(new List<Entity>() {
                new Account() { Id = guid, Address1_Latitude = barcelonaLatitude  },
                new Account() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from a in ctx.CreateQuery<Account>()
                               where a.Address1_Latitude == barcelonaLatitude
                               select a).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_moneys_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new SalesOrderDetail() { Id = guid, BaseAmount = new Money(1.1M) },
                new SalesOrderDetail() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<SalesOrderDetail>()
                               where c.BaseAmount == new Money(1.1M)
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_entityreferences_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var productId = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new SalesOrderDetail() { Id = Guid.NewGuid(), ProductId = new EntityReference(Product.EntityLogicalName, productId) },
                new SalesOrderDetail() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from s in ctx.CreateQuery<SalesOrderDetail>()
                               where s.ProductId == new EntityReference(Product.EntityLogicalName, productId)
                               select s).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_guids_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var productId = Guid.NewGuid();
            var salesOrderDetailId = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new SalesOrderDetail() { Id = salesOrderDetailId, ProductId = new EntityReference(Product.EntityLogicalName, productId) },
                new SalesOrderDetail() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from s in ctx.CreateQuery<SalesOrderDetail>()
                               where s.SalesOrderDetailId == salesOrderDetailId
                               select s).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_optionsets_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var productId = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new Account() { Id = Guid.NewGuid(), StatusCode = new OptionSetValue(1) },
                new Account() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from a in ctx.CreateQuery<Account>()
                               where a.StatusCode == new OptionSetValue(1)
                               select a).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public void When_executing_a_linq_query_with_equals_between_2_activityparties_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();
            var activityId = Guid.NewGuid();

            var partyRecord = new ActivityParty()
            {
                Id = Guid.NewGuid(),
                ActivityId = new EntityReference(Email.EntityLogicalName, activityId),
                PartyId = new EntityReference(Contact.EntityLogicalName, contactId)
            };

            fakedContext.Initialize(new List<Entity>() {
                new Email() { Id = activityId, ActivityId = activityId, Subject = "Test email"},
                new ActivityPointer () { Id = Guid.NewGuid(), ActivityId = activityId },
                partyRecord,
                new ActivityPointer() { Id = Guid.NewGuid()},  //To test also nulls
                new ActivityParty() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var activities = (from pointer in ctx.CreateQuery<Email>()
                                  join party in ctx.CreateQuery<ActivityParty>() on pointer.ActivityId.Value equals party.ActivityId.Id
                                  // from party in ctx.CreateQuery<ActivityParty>() //on pointer.ActivityId.Value equals party.ActivityId.Id
                                  where party.PartyId.Id == contactId
                                  select pointer).ToList();

                Assert.True(activities.Count == 1);
            }
        }

        [Fact]
        public void When_querying_option_sets_with_string_values_right_result_is_returned()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            ctx.Initialize(new List<Entity>()
            {
                new Account() { Id = Guid.NewGuid(), IndustryCode = new OptionSetValue(23) },
                new Account() { Id = Guid.NewGuid(), IndustryCode = new OptionSetValue(69) }
            });

            QueryExpression query = new QueryExpression
            {
                EntityName = "account",
                ColumnSet = new ColumnSet(new string[] { "accountid", "industrycode" }),
            };
            query.Criteria.AddCondition("industrycode", ConditionOperator.Equal, "23");
            var result = service.RetrieveMultiple(query);

            Assert.Equal(1, result.Entities.Count);
            Assert.Equal(23, (result.Entities[0] as Account).IndustryCode.Value);
        }

        //[Fact]
        //public void When_querying_enums_with_string_values_right_result_is_returned()
        //{
        //    var ctx = new XrmFakedContext();
        //    var service = ctx.GetOrganizationService();

        //    var inactiveAccount = new Account() { Id = Guid.NewGuid() };
        //    inactiveAccount["statecode"] = new OptionSetValue((int)AccountState.Inactive);

        //    ctx.Initialize(new List<Entity>()
        //    {
        //        new Account() { Id = Guid.NewGuid() }, //Active by default
        //        inactiveAccount
        //    });

        //    QueryExpression query = new QueryExpression
        //    {
        //        EntityName = "account",
        //        ColumnSet = new ColumnSet(new string[] { "accountid", "statecode" }),
        //    };
        //    query.Criteria.AddCondition("statecode", ConditionOperator.Equal, "inactive");
        //    var result = service.RetrieveMultiple(query);

        //    Assert.Equal(1, result.Entities.Count);
        //    Assert.Equal(AccountState.Inactive, (result.Entities[0] as Account).StateCode.Value);
        //}
    }
}