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
using Microsoft.Xrm.Sdk.Messages;  //TypedEntities generated code for testing


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
    }
}
