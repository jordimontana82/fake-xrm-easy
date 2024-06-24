using Crm;
using FakeItEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;  //TypedEntities generated code for testing
using System.ServiceModel;
using Xunit;

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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
        public void When_doing_a_crm_linq_query_and_proxy_types_projection_must_be_applied_after_where_clause()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = guid1, FirstName = "Jordi", LastName = "Montana" },
                new Contact() { Id = guid2, FirstName = "Other" }
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.LastName == "Montana"  //Should be able to filter by a non-selected attribute
                               select new
                               {
                                   FirstName = c.FirstName
                               }).ToList();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.StatusCode == null
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }

#if FAKE_XRM_EASY_9
        [Fact]
        public void When_doing_a_crm_linq_query_with_an_optionsetvaluecollection_in_where_filter_exception_is_thrown()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = contactId, new_MultiSelectAttribute = new OptionSetValueCollection(new[] { new OptionSetValue(1) }) },
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var query = from c in ctx.CreateQuery<Contact>()
                            where c.new_MultiSelectAttribute.Contains(new OptionSetValue(1))
                            select c;

                Assert.Throws<FaultException<OrganizationServiceFault>>(() => query.ToList());
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_an_optionsetvaluecollection_with_nulls_against_nulls_in_where_filter_record_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();

            fakedContext.Initialize(new List<Entity>() {
                new Contact() { Id = contactId, new_MultiSelectAttribute = new OptionSetValueCollection(new[] { new OptionSetValue(1) }) },
                new Contact() { Id = Guid.NewGuid(), new_MultiSelectAttribute = null },
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               where c.new_MultiSelectAttribute == null
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }
#endif

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

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               join a in ctx.CreateQuery<Account>() on c.ParentCustomerId.Id equals a.AccountId
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_an_intersect_entity_and_joins_right_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var user = new SystemUser() { Id = Guid.NewGuid() };
            var systemRole = new SystemUserRoles() { Id = Guid.NewGuid() };
            var role = new Role() { Id = Guid.NewGuid() };

            systemRole["systemuserid"] = user.ToEntityReference();
            systemRole["roleid"] = role.ToEntityReference();

            fakedContext.Initialize(new List<Entity>() {
                user, systemRole, role
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from sr in ctx.CreateQuery<SystemUserRoles>()
                               join r in ctx.CreateQuery<Role>() on sr.RoleId equals r.RoleId
                               join u in ctx.CreateQuery<SystemUser>() on sr.SystemUserId equals u.SystemUserId
                               select sr).ToList();

                Assert.True(matches.Count == 1);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_aliases_with_uppercase_chars_right_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contact = new Contact() { Id = Guid.NewGuid(), FirstName = "Chuck" };
            var parentAccount = new Account()
            {
                Id = Guid.NewGuid(),
                PrimaryContactId = contact.ToEntityReference()
            };
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                ParentAccountId = parentAccount.ToEntityReference()
            };

            fakedContext.Initialize(new List<Entity>() {
                contact, parentAccount, account
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from childAccount in ctx.CreateQuery<Account>()
                               join childsParentAccount in ctx.CreateQuery<Account>() on childAccount.ParentAccountId.Id equals childsParentAccount.AccountId
                               join primaryContact in ctx.CreateQuery<Contact>() on childsParentAccount.PrimaryContactId.Id equals primaryContact.ContactId
                               select new
                               {
                                   Name = primaryContact.FirstName
                               }).ToList();

                Assert.True(matches.Count == 1);
                Assert.Equal(matches[0].Name, "Chuck");
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_and_selecting_an_entire_object_all_attributes_are_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contact = new Contact() { Id = Guid.NewGuid(), FirstName = "Chuck" };
            var parentAccount = new Account()
            {
                Id = Guid.NewGuid(),
                PrimaryContactId = contact.ToEntityReference()
            };
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                ParentAccountId = parentAccount.ToEntityReference()
            };

            fakedContext.Initialize(new List<Entity>() {
                contact, parentAccount, account
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from childAccount in ctx.CreateQuery<Account>()
                               join childsParentAccount in ctx.CreateQuery<Account>() on childAccount.ParentAccountId.Id equals childsParentAccount.AccountId
                               join primaryContact in ctx.CreateQuery<Contact>() on childsParentAccount.PrimaryContactId.Id equals primaryContact.ContactId
                               select new
                               {
                                   Contact = primaryContact
                               }).ToList();

                Assert.True(matches.Count == 1);
                Assert.Equal(matches[0].Contact.Attributes.Count, 7 + 1);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_and_selecting_an_entire_object_and_a_subset_of_the_attributes_all_attributes_are_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contact = new Contact() { Id = Guid.NewGuid(), FirstName = "Chuck" };
            var parentAccount = new Account()
            {
                Id = Guid.NewGuid(),
                PrimaryContactId = contact.ToEntityReference()
            };
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                ParentAccountId = parentAccount.ToEntityReference()
            };

            fakedContext.Initialize(new List<Entity>() {
                contact, parentAccount, account
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from childAccount in ctx.CreateQuery<Account>()
                               join childsParentAccount in ctx.CreateQuery<Account>() on childAccount.ParentAccountId.Id equals childsParentAccount.AccountId
                               join primaryContact in ctx.CreateQuery<Contact>() on childsParentAccount.PrimaryContactId.Id equals primaryContact.ContactId
                               select new
                               {
                                   Name = primaryContact.FirstName,
                                   Contact = primaryContact
                               }).ToList();

                Assert.True(matches.Count == 1);
                Assert.Equal(matches[0].Contact.Attributes.Count, 7 + 1);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_and_selecting_an_entire_object_between_joins_all_attributes_are_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contact = new Contact() { Id = Guid.NewGuid(), FirstName = "Chuck" };
            var parentAccount = new Account()
            {
                Id = Guid.NewGuid(),
                PrimaryContactId = contact.ToEntityReference(),
                Name = "Parent Account",
                Address1_Name = "Address1",
                Address2_Name = "Address2"
            };
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                ParentAccountId = parentAccount.ToEntityReference(),
            };

            fakedContext.Initialize(new List<Entity>() {
                contact, parentAccount, account
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from childAccount in ctx.CreateQuery<Account>()
                               join childsParentAccount in ctx.CreateQuery<Account>() on childAccount.ParentAccountId.Id equals childsParentAccount.AccountId
                               join primaryContact in ctx.CreateQuery<Contact>() on childsParentAccount.PrimaryContactId.Id equals primaryContact.ContactId
                               select new
                               {
                                   Name = account.Name,
                                   Account = childsParentAccount
                               }).ToList();

                Assert.True(matches.Count == 1);
                Assert.Equal(matches[0].Account.Attributes.Count, 7 + 4); //7 = default attributes
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_and_selecting_an_entire_object_between_joins_all_attributes_are_returned_2()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contact = new Contact() { Id = Guid.NewGuid(), FirstName = "Chuck" };
            var parentAccount = new Account()
            {
                Id = Guid.NewGuid(),
                PrimaryContactId = contact.ToEntityReference(),
                Name = "Parent Account",
                Address1_Name = "Address1",
                Address2_Name = "Address2"
            };
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                Name = "Child Account",
                ParentAccountId = parentAccount.ToEntityReference(),
            };

            fakedContext.Initialize(new List<Entity>() {
                contact, parentAccount, account
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from childAccount in ctx.CreateQuery<Account>()
                               join childsParentAccount in ctx.CreateQuery<Account>() on childAccount.ParentAccountId.Id equals childsParentAccount.AccountId
                               join primaryContact in ctx.CreateQuery<Contact>() on childsParentAccount.PrimaryContactId.Id equals primaryContact.ContactId
                               select new
                               {
                                   Name = childAccount.Name,
                                   ParentAccountName = childsParentAccount.Name,
                                   Account = childsParentAccount
                               }).ToList();

                Assert.True(matches.Count == 1);
                Assert.Equal(matches[0].Account.Attributes.Count, 7 + 4); //7 = default attributes
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_and_selecting_an_entire_object_plus_some_attributes_of_the_same_object_between_joins_all_attributes_are_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contact = new Contact() { Id = Guid.NewGuid(), FirstName = "Chuck" };
            var parentAccount = new Account()
            {
                Id = Guid.NewGuid(),
                PrimaryContactId = contact.ToEntityReference(),
                Name = "Child Account",
                Address1_Name = "Address1",
                Address2_Name = "Address2"
            };
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                ParentAccountId = parentAccount.ToEntityReference(),
            };

            fakedContext.Initialize(new List<Entity>() {
                contact, parentAccount, account
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from childAccount in ctx.CreateQuery<Account>()
                               join childsParentAccount in ctx.CreateQuery<Account>() on childAccount.ParentAccountId.Id equals childsParentAccount.AccountId
                               join primaryContact in ctx.CreateQuery<Contact>() on childsParentAccount.PrimaryContactId.Id equals primaryContact.ContactId
                               select new
                               {
                                   Name = childsParentAccount.Name,
                                   Account = childsParentAccount
                               }).ToList();

                Assert.True(matches.Count == 1);
                Assert.Equal(matches[0].Account.Attributes.Count, 7 + 4); //6 = default attributes
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_an_intersect_entity_and_joins_and_where_clauses_right_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var user = new SystemUser() { Id = Guid.NewGuid(), FirstName = "Jordi" };
            var systemRole = new SystemUserRoles() { Id = Guid.NewGuid() };
            var role = new Role() { Id = Guid.NewGuid(), Name = "System Administrator" };

            systemRole["systemuserid"] = user.ToEntityReference();
            systemRole["roleid"] = role.ToEntityReference();

            var anotherUser = new SystemUser() { Id = Guid.NewGuid(), FirstName = "FakeUser" };
            var anotherSystemRole = new SystemUserRoles() { Id = Guid.NewGuid() };
            var anotherRole = new Role() { Id = Guid.NewGuid(), Name = "Basic Access" };

            anotherSystemRole["systemuserid"] = anotherUser.ToEntityReference();
            anotherSystemRole["roleid"] = anotherRole.ToEntityReference();

            fakedContext.Initialize(new List<Entity>() {
                user, systemRole, role,
                anotherUser, anotherSystemRole, anotherRole
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from sr in ctx.CreateQuery<SystemUserRoles>()
                               join r in ctx.CreateQuery<Role>() on sr.RoleId equals r.RoleId
                               join u in ctx.CreateQuery<SystemUser>() on sr.SystemUserId equals u.SystemUserId
                               where u.FirstName == "Jordi"
                               where r.Name == "System Administrator"
                               select sr).ToList();

                Assert.True(matches.Count == 1);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_an_intersect_entity_and_2_levels_of_joins_and_where_clauses_right_result_is_returned()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var parentRole = new Role() { Id = Guid.NewGuid(), Name = "System Administrator" };
            var role = new Role() { Id = Guid.NewGuid(), Name = "Sys Admin" };
            var user = new SystemUser() { Id = Guid.NewGuid(), FirstName = "Jordi" };
            var systemRole = new SystemUserRoles() { Id = Guid.NewGuid() };

            role["parentroleid"] = parentRole.ToEntityReference();
            systemRole["systemuserid"] = user.ToEntityReference();
            systemRole["roleid"] = role.ToEntityReference();

            fakedContext.Initialize(new List<Entity>() {
                user, systemRole, role, parentRole
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from sr in ctx.CreateQuery<SystemUserRoles>()
                               join r in ctx.CreateQuery<Role>() on sr.RoleId equals r.RoleId
                               join u in ctx.CreateQuery<SystemUser>() on sr.SystemUserId equals u.SystemUserId

                               where u.FirstName == "Jordi"
                               where r.Name == "Sys Admin"
                               select sr).ToList();

                Assert.True(matches.Count == 1);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_an_intersect_entity_and_2_levels_of_joins_the_order_of_where_clauses_is_irrelevant_between_linked_entities()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var parentRole = new Role() { Id = Guid.NewGuid(), Name = "System Administrator" };
            var role = new Role() { Id = Guid.NewGuid(), Name = "Sys Admin" };
            var user = new SystemUser() { Id = Guid.NewGuid(), FirstName = "Jordi" };
            var systemRole = new SystemUserRoles() { Id = Guid.NewGuid() };

            role["parentroleid"] = parentRole.ToEntityReference();
            systemRole["systemuserid"] = user.ToEntityReference();
            systemRole["roleid"] = role.ToEntityReference();

            fakedContext.Initialize(new List<Entity>() {
                user, systemRole, role, parentRole
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from sr in ctx.CreateQuery<SystemUserRoles>()
                               join r in ctx.CreateQuery<Role>() on sr.RoleId equals r.RoleId
                               join u in ctx.CreateQuery<SystemUser>() on sr.SystemUserId equals u.SystemUserId

                               where u.FirstName == "Jordi"
                               where r.Name == "Sys Admin"
                               select sr).ToList();

                Assert.True(matches.Count == 1);
            }

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from sr in ctx.CreateQuery<SystemUserRoles>()
                               join r in ctx.CreateQuery<Role>() on sr.RoleId equals r.RoleId
                               join u in ctx.CreateQuery<SystemUser>() on sr.SystemUserId equals u.SystemUserId

                               where r.Name == "Sys Admin"
                               where u.FirstName == "Jordi"

                               select sr).ToList();

                Assert.True(matches.Count == 1);
            }
        }

        [Fact]
        public void When_doing_a_crm_linq_query_with_an_intersect_entity_and_2_levels_of_joins_the_order_of_where_clauses_is_irrelevant_between_main_and_linked_entity()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var account = new Account() { Id = Guid.NewGuid(), Name = "Barcelona" };
            var contact1 = new Contact()
            {
                Id = Guid.NewGuid(),
                ParentCustomerId = account.ToEntityReference(),
                AccountRoleCode = new OptionSetValue(1)
            };
            var contact2 = new Contact()
            {
                Id = Guid.NewGuid(),
                ParentCustomerId = account.ToEntityReference(),
                AccountRoleCode = new OptionSetValue(1)
            };

            fakedContext.Initialize(new List<Entity>() {
                account, contact1, contact2
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               join a in ctx.CreateQuery<Account>() on c.ParentCustomerId.Id equals a.AccountId

                               where c.AccountRoleCode != null
                               where a.Name == "Barcelona"
                               select c.Address1_Name).ToList();

                Assert.Equal(2, matches.Count);
            }

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               join a in ctx.CreateQuery<Account>() on c.ParentCustomerId.Id equals a.AccountId

                               where a.Name == "Barcelona"
                               where c.AccountRoleCode != null
                               select c.Address1_Name).ToList();

                Assert.True(matches.Count == 2);
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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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
            var accountId2 = Guid.NewGuid();

            var lead = new Lead() { Id = Guid.NewGuid() };

            //Contact is related to first account, but because first account is not related to itself then the query must return 0 records
            fakedContext.Initialize(new List<Entity>() {
                lead,
                new Account() { Id = accountId , Name = "Testing" },
                new Account() { Id = accountId2,
                            ParentAccountId = new EntityReference(Account.EntityLogicalName, accountId),
                            PrimaryContactId = new EntityReference(Contact.EntityLogicalName, contactId) },
                new Contact() { Id = contactId, NumberOfChildren = 2, OriginatingLeadId = lead.ToEntityReference()}
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                //var matches = (from c in ctx.ContactSet
                //               join a in ctx.AccountSet on c.ContactId equals a.PrimaryContactId.Id
                //               join a2 in ctx.AccountSet on a.ParentAccountId.Id equals a2.AccountId
                //               where c.NumberOfChildren == 2
                //               where a2.Name == "Testing"
                //               select a2.Name).ToList();

                var matches = (from a in ctx.AccountSet
                               join c in ctx.ContactSet on a.PrimaryContactId.Id equals c.ContactId
                               join l in ctx.LeadSet on c.OriginatingLeadId.Id equals l.LeadId
                               select a).ToList();

                Assert.True(matches.Count == 1);
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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

            var service = fakedContext.GetOrganizationService();

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

        [Fact(DisplayName = "When_doing_a_join_with_filter_then_can_filter_by_the_joined_entity_attributes")]
        public void When_doing_a_join_with_filter_then_can_filter_by_the_joined_entity_attributes()
        {
            //REVIEW: Different implementations of the ConditionExpression class in Microsoft.Xrm.Sdk (which has EntityName property for versions >= 2013)

            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contactId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var accountId2 = Guid.NewGuid();

            //Contact is related to first account, but because first account is not related to itself then the query must return 0 records
            fakedContext.Initialize(new List<Entity>() {
                new Account() { Id = accountId, Name="Account1" },
                new Account() { Id = accountId2, Name = "Account2" },
                new Contact() { Id = contactId, ParentCustomerId = new EntityReference(Account.EntityLogicalName, accountId),
                                                NumberOfChildren = 2, FirstName = "Contact" },
                new Contact() {Id = Guid.NewGuid(), ParentCustomerId =  new EntityReference(Account.EntityLogicalName, accountId2) }
            });

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var matches = (from c in ctx.CreateQuery<Contact>()
                               join account in ctx.CreateQuery<Account>() on c.ParentCustomerId.Id equals account.AccountId
                               where account.Name == "Account1"
                               select c).ToList();

                Assert.True(matches.Count == 1);
            }
        }
    }
}