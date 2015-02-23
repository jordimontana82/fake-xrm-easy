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


namespace FakeXrmEasy.Tests
{
    public class FakeContextTestLinqQueries
    {
        [Fact]
        public void When_doing_a_crm_linq_query_a_retrievemultiple_with_a_queryexpression_is_called()
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
                               where c.FirstName.Equals("Jordi")
                               select c).FirstOrDefault();


            }
            A.CallTo(() => service.Execute(A<OrganizationRequest>.That.Matches(x => x is RetrieveMultipleRequest && ((RetrieveMultipleRequest)x).Query is QueryExpression))).MustHaveHappened();
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_an_equals_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, FirstName = "Jordi" },
                new Contact() { Id = guid2, FirstName = "Other" }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName.Equals("Jordi")
                               select c).ToList();

                Assert.True(matches.Count == 1);
                Assert.True(matches[0].FirstName.Equals("Jordi"));

                matches = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName == "Jordi" //Using now equality operator
                               select c).ToList();

                Assert.True(matches.Count == 1);
                Assert.True(matches[0].FirstName.Equals("Jordi"));
            }
            
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_an_equals_operator_and_nulls_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, FirstName = "Jordi" },
                new Contact() { Id = guid2, FirstName = null }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName.Equals("Jordi")
                               select c).ToList();

                Assert.True(matches.Count == 1);
                Assert.True(matches[0].FirstName.Equals("Jordi"));

                matches = (from c in ctx.CreateQuery<Contact>()
                           where c.FirstName == "Jordi" //Using now equality operator
                           select c).ToList();

                Assert.True(matches.Count == 1);
                Assert.True(matches[0].FirstName.Equals("Jordi"));
            }

        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_starts_with_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, FirstName = "Jordi 1" },
                new Contact() { Id = guid2, FirstName = "Jordi 2" },
                new Contact() { Id = guid3, FirstName = "Other" }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName.StartsWith("Jordi")
                               select c).ToList();

                Assert.True(matches.Count == 2);
                Assert.True(matches[0].FirstName.Equals("Jordi 1"));
                Assert.True(matches[1].FirstName.Equals("Jordi 2"));
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_not_equals_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, FirstName = "Jordi 1" },
                new Contact() { Id = guid2, FirstName = "Jordi 2" },
                new Contact() { Id = guid3, FirstName = "Other" }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName != "Jordi 1"
                               select c).ToList();

                Assert.True(matches.Count == 2);
                Assert.True(matches[0].FirstName.Equals("Jordi 2"));
                Assert.True(matches[1].FirstName.Equals("Other"));
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_not_starts_with_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, FirstName = "Jordi 1" },
                new Contact() { Id = guid2, FirstName = "Jordi 2" },
                new Contact() { Id = guid3, FirstName = "Other" }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where !c.FirstName.StartsWith("Jordi")
                               select c).ToList();

                Assert.True(matches.Count == 1);
                Assert.True(matches[0].FirstName.Equals("Other"));
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_contains_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, FirstName = "Jordi Garcia" },
                new Contact() { Id = guid2, FirstName = "Javi Garcia" },
                new Contact() { Id = guid3, FirstName = "Other" }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName.Contains("Garcia")
                               select c).ToList();

                Assert.True(matches.Count == 2);
                Assert.True(matches[0].FirstName.Equals("Jordi Garcia"));
                Assert.True(matches[1].FirstName.Equals("Javi Garcia"));
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_not_contains_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, FirstName = "Jordi Garcia" },
                new Contact() { Id = guid2, FirstName = "Javi Garcia" },
                new Contact() { Id = guid3, FirstName = "Other" }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where !c.FirstName.Contains("Garcia")
                               select c).ToList();

                Assert.True(matches.Count == 1);
                Assert.True(matches[0].FirstName.Equals("Other"));
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_null_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, FirstName = null },
                new Contact() { Id = guid2 }, //FirstName attribute omitted
                new Contact() { Id = guid3, FirstName = "Other" }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName == null
                               select c).ToList();

                Assert.True(matches.Count == 2);
                Assert.True(matches[0].FirstName == null);
                Assert.True(matches[1].FirstName == null);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_not_null_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, FirstName = null },
                new Contact() { Id = guid2 }, //FirstName attribute omitted
                new Contact() { Id = guid3, FirstName = "Other" }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName != null
                               select c).ToList();

                Assert.True(matches.Count == 1);
                Assert.True(matches[0].FirstName == "Other");
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_greater_than_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, NumberOfChildren = 3 },
                new Contact() { Id = guid2, NumberOfChildren = 1 }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.NumberOfChildren.Value > 2
                               select c).ToList();

                Assert.True(matches.Count == 1);
                Assert.True(matches[0].Id == guid1);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_greater_than_or_equal_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, NumberOfChildren = 3 },
                new Contact() { Id = guid2, NumberOfChildren = 1 },
                new Contact() { Id = guid3, NumberOfChildren = 2 }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.NumberOfChildren.Value >= 2
                               select c).ToList();

                Assert.True(matches.Count == 2);
                Assert.True(matches[0].Id == guid1);
                Assert.True(matches[1].Id == guid3);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_less_than_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, NumberOfChildren = 3 },
                new Contact() { Id = guid2, NumberOfChildren = 1 },
                new Contact() { Id = guid3, NumberOfChildren = 2 }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.NumberOfChildren.Value < 3
                               select c).ToList();

                Assert.True(matches.Count == 2);
                Assert.True(matches[0].Id == guid2);
                Assert.True(matches[1].Id == guid3);
            }
        }
        [Fact]
        public void When_doing_a_crm_linq_query_with_a_less_than_or_equal_operator_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, NumberOfChildren = 3 },
                new Contact() { Id = guid2, NumberOfChildren = 1 },
                new Contact() { Id = guid3, NumberOfChildren = 2 }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.NumberOfChildren.Value <= 3
                               select c).ToList();

                Assert.True(matches.Count == 3);
            }
        }
        [Fact]
        public void When_doing_a_crm_linq_query_with_an_entity_reference_in_where_filter_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Account() { Id = accountId },
                new Contact() { Id = contactId, 
                                ParentCustomerId = new EntityReference(Account.EntityLogicalName, accountId) },
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.ParentCustomerId.Id == accountId
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }
        [Fact]
        public void When_doing_a_crm_linq_query_with_an_entity_reference_with_nulls_in_where_filter_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Account() { Id = accountId },
                new Contact() { Id = contactId, 
                                ParentCustomerId = new EntityReference(Account.EntityLogicalName, accountId) },
                new Contact() { Id = Guid.NewGuid(), 
                                ParentCustomerId = null }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.ParentCustomerId.Id == accountId
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }
        [Fact]
        public void When_doing_a_crm_linq_query_with_an_entity_reference_with_nulls_against_nulls_in_where_filter_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Account() { Id = accountId },
                new Contact() { Id = Guid.NewGuid(), 
                                ParentCustomerId = null }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.ParentCustomerId == null
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }
        [Fact]
        public void When_doing_a_crm_linq_query_with_an_optionset_in_where_filter_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = contactId, StatusCode = new OptionSetValue(1) },
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.StatusCode.Value == 1
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }
        [Fact]
        public void When_doing_a_crm_linq_query_with_an_optionset_with_nulls_in_where_filter_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = contactId, StatusCode = new OptionSetValue(1) },
                new Contact() { Id = Guid.NewGuid(), StatusCode = null },
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.StatusCode.Value == 1
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }
        [Fact]
        public void When_doing_a_crm_linq_query_with_an_optionset_with_nulls_against_nulls_in_where_filter_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = contactId, StatusCode = new OptionSetValue(1) },
                new Contact() { Id = Guid.NewGuid(), StatusCode = null },
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.StatusCode == null
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }
    }
}
