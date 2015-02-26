using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using FakeXrmEasy;
using Xunit;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using Crm;
using Microsoft.Xrm.Sdk.Messages;
using System.Reflection;  //TypedEntities generated code for testing


namespace FakeXrmEasy.Tests.FakeContextTests.LinqTests
{
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

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName == "Jordi"
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

            var service = fakedContext.GetFakedOrganizationService();

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
        public void When_executing_a_linq_query_with_equals_between_2_integers_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid = Guid.NewGuid();
            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid, NumberOfChildren = 2},
                new Contact() { Id = Guid.NewGuid()}  //To test also nulls
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.NumberOfChildren != null && c.NumberOfChildren.Value == 2
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

            var service = fakedContext.GetFakedOrganizationService();

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

            var service = fakedContext.GetFakedOrganizationService();

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

            var service = fakedContext.GetFakedOrganizationService();

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

            var service = fakedContext.GetFakedOrganizationService();

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

            var service = fakedContext.GetFakedOrganizationService();

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

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from s in ctx.CreateQuery<SalesOrderDetail>()
                               where s.ProductId == new EntityReference(Product.EntityLogicalName, productId)
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

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from a in ctx.CreateQuery<Account>()
                               where a.StatusCode == new OptionSetValue(1)
                               select a).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        
    }
    
}
