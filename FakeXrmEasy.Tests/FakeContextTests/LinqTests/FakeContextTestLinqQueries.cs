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
        public void When_doing_a_crm_linq_query_and_proxy_types_and_a_selected_attribute_returned_projected_entity_is_thesubclass()
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
                               select new
                               {
                                   FirstName = c.FirstName,
                                   CrmRecord = c
                               }).ToList();

                Assert.True(matches.Count == 1);
                Assert.True(matches[0].FirstName.Equals("Jordi"));
                Assert.IsAssignableFrom(typeof(Contact), matches[0].CrmRecord);
                Assert.True(matches[0].CrmRecord.GetType() == typeof(Contact));
               
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

        [Fact]
        public void When_doing_a_crm_linq_query_with_an_innerjoin_right_result_is_returned()
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
                               join a in ctx.CreateQuery<Account>() on c.ParentCustomerId.Id equals a.AccountId
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_leftjoin_right_result_is_returned()
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
                               join a in ctx.CreateQuery<Account>() on c.ParentCustomerId.Id equals a.AccountId into joined
                               from contact in joined.DefaultIfEmpty()
                               select contact).ToList();

                Assert.True(matches.Count == 2);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_leftjoin_with_a_where_expression_right_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Account() { Id = accountId },
                new Contact() { Id = contactId, ParentCustomerId = new EntityReference(Account.EntityLogicalName, accountId),
                                                NumberOfChildren = 2},
                new Contact() { Id = Guid.NewGuid(), ParentCustomerId = null, NumberOfChildren = 2 },
                new Contact() { Id = Guid.NewGuid(), ParentCustomerId = null, NumberOfChildren = 3 }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               join a in ctx.CreateQuery<Account>() on c.ParentCustomerId.Id equals a.AccountId into joinedAccounts
                               from account in joinedAccounts.DefaultIfEmpty()
                               where c.NumberOfChildren == 2
                               select c).ToList();

                Assert.True(matches.Count == 2);
            }
        }
        [Fact]
        public void When_doing_a_crm_linq_query_with_a_2_innerjoins_right_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            //Contact is related to first account, but because first account is not related to itself then the query must return 0 records
            fakedContext.Initialize(new List<Entity>() {
                new Account() { Id = accountId },
                new Account() { Id = Guid.NewGuid(), ParentAccountId = new EntityReference(Account.EntityLogicalName, accountId) },
                new Contact() { Id = contactId, ParentCustomerId = new EntityReference(Account.EntityLogicalName, accountId),
                                                NumberOfChildren = 2}
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               join account in ctx.CreateQuery<Account>() on c.ParentCustomerId.Id equals account.AccountId
                               join parentAccount in ctx.CreateQuery<Account>() on account.ParentAccountId.Id equals parentAccount.AccountId
                               select c).ToList();

                Assert.True(matches.Count == 0);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_a_an_and_filter_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var taskId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            //Contact is related to first account, but because first account is not related to itself then the query must return 0 records
            fakedContext.Initialize(new List<Entity>() {
                new Task() { Id = taskId, StatusCode = new OptionSetValue(2) }, //Completed
                new Task() { Id = Guid.NewGuid() }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from t in ctx.CreateQuery<Task>()
                               where t.StatusCode != null && t.StatusCode.Value == 2
                               select t).ToList();

                Assert.True(matches.Count == 1);
            }
        }
        [Fact]
        public void When_doing_a_crm_linq_query_with_2_and_filters_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var taskId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var euroId = Guid.NewGuid();

            //Contact is related to first account, but because first account is not related to itself then the query must return 0 records
            fakedContext.Initialize(new List<Entity>() {
                new Task() { Id = taskId, StatusCode = new OptionSetValue(1), 
                                          TransactionCurrencyId = new EntityReference(TransactionCurrency.EntityLogicalName, euroId) }, 
                new Task() { Id = Guid.NewGuid()  }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from t in ctx.CreateQuery<Task>()
                               where t.StatusCode != null && t.StatusCode.Value == 1
                               where t.TransactionCurrencyId != null && t.TransactionCurrencyId.Id == euroId
                               select t).ToList();

                Assert.True(matches.Count == 1);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_that_produces_a_filter_expression_plus_condition_expression_at_same_level_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var taskId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var euroId = Guid.NewGuid();

            //Contact is related to first account, but because first account is not related to itself then the query must return 0 records
            fakedContext.Initialize(new List<Entity>() {
                new Task() { Id = taskId, StatusCode = new OptionSetValue(1), 
                                          TransactionCurrencyId = new EntityReference(TransactionCurrency.EntityLogicalName, euroId) }, 
                new Task() { Id = Guid.NewGuid(), StatusCode = new OptionSetValue(2), 
                                          TransactionCurrencyId = new EntityReference(TransactionCurrency.EntityLogicalName, euroId) }, 
                
                new Task() { Id = Guid.NewGuid()  }
            });

            var service = fakedContext.GetFakedOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from t in ctx.CreateQuery<Task>()
                               where t.StatusCode != null && (
                                            t.StatusCode.Value == 1 || t.StatusCode.Value == 2)
                                        //StatusCode != null is converted into a ConditionExpression plus
                                        //t.StatusCode.Value == 1 || t.StatusCode.Value == 2 is converted into a FilterExpression
                               where t.TransactionCurrencyId != null && t.TransactionCurrencyId.Id == euroId
                                        //Second where is converted as FilterExpression, but without a sibling condition
                               select t).ToList();

                Assert.True(matches.Count == 2);
            }
        }
    }
}
