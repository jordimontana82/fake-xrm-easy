using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using Xunit;

#if FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365
using Xunit.Sdk;
#endif

using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using Crm;
using System.ServiceModel;

namespace FakeXrmEasy.Tests.FakeContextTests.AddListMembersListRequestTests
{
    public class Tests
    {
        public enum ListCreatedFromCode
        {
            Account = 1,
            Contact = 2,
            Lead = 4
        }

        [Fact]
        public void When_a_member_is_added_to_a_non_existing_list_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            AddListMembersListRequest addListMembersListRequest = new AddListMembersListRequest
            {
                MemberIds = new[]
                {
                    Guid.NewGuid()
                },
                ListId = Guid.NewGuid()
            };

            // Execute the request.
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(addListMembersListRequest));
        }

        [Fact]
        public void When_a_request_is_called_with_an_empty_listid_parameter_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            AddListMembersListRequest addListMembersListRequest = new AddListMembersListRequest
            {
                MemberIds = new[]
                {
                    Guid.NewGuid()
                },
                ListId = Guid.Empty
            };

            // Execute the request.
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(addListMembersListRequest));
        }

        [Fact]
        public void When_a_request_is_called_with_an_empty_memberid_parameter_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            AddListMembersListRequest addListMembersListRequest = new AddListMembersListRequest
            {
                MemberIds = new[]
                {
                    Guid.Empty
                },
                ListId = Guid.NewGuid()
            };

            // Execute the request.
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(addListMembersListRequest));
        }

        [Fact]
        public void When_a_member_is_added_to_an_existing_list_without_membercode_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var list = new List
            {
                Id = Guid.NewGuid(),
                ListName = "Some list"
            };

            ctx.Initialize(new List<Entity>
            {
                list
            });

            AddListMembersListRequest addListMembersListRequest = new AddListMembersListRequest
            {
                MemberIds = new[]
                {
                    Guid.NewGuid()
                },
                ListId = list.ToEntityReference().Id
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(addListMembersListRequest));
        }

        [Fact]
        public void When_a_non_existing_member_is_added_to_an_existing_list_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var list = new List
            {
                Id = Guid.NewGuid(),
                ListName = "Some list",
                CreatedFromCode = new OptionSetValue((int)ListCreatedFromCode.Account)
            };

            ctx.Initialize(new List<Entity>
            {
                list
            });

            AddListMembersListRequest addListMembersListRequest = new AddListMembersListRequest
            {
                MemberIds = new[]
                {
                    Guid.NewGuid()
                },
                ListId = list.ToEntityReference().Id
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(addListMembersListRequest));
        }

        [Fact]
        public void When_different_membertypes_are_added_to_an_existing_list_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var list = new List
            {
                Id = Guid.NewGuid(),
                ListName = "Some list",
                CreatedFromCode = new OptionSetValue((int)ListCreatedFromCode.Account)
            };

            var account = new Account
            {
                Id = Guid.NewGuid()
            };

            var contact = new Contact
            {
                Id = Guid.NewGuid()
            };

            ctx.Initialize(new List<Entity>
            {
                list,
                account,
                contact
            });

            AddListMembersListRequest addListMembersListRequest = new AddListMembersListRequest
            {
                MemberIds = new[]
                {
                    account.Id,
                    contact.Id
                },
                ListId = list.ToEntityReference().Id
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(addListMembersListRequest));
        }

        [Fact]
        public void When_a_member_is_added_to_an_existing_list_member_is_added_successfully_account()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var list = new List
            {
                Id = Guid.NewGuid(),
                ListName = "Some list",
                CreatedFromCode = new OptionSetValue((int)ListCreatedFromCode.Account)
            };

            var account = new Account()
            {
                Id = Guid.NewGuid()
            };
            ctx.Initialize(new List<Entity>
            {
                list,
                account
            });

            AddListMembersListRequest addListMembersListRequest = new AddListMembersListRequest
            {
                MemberIds = new[]
                {
                    account.Id
                },
                ListId = list.ToEntityReference().Id
            };

            service.Execute(addListMembersListRequest);

            using (var context = new XrmServiceContext(service))
            {
                var member = (from lm in context.CreateQuery<ListMember>()
                              join l in context.CreateQuery<List>() on lm.ListId.Id equals l.ListId.Value
                              join a in context.CreateQuery<Account>() on lm.EntityId.Id equals a.AccountId.Value
                              where lm.EntityId.Id == account.Id
                              where lm.ListId.Id == list.Id
                              select lm
                              ).FirstOrDefault();

                Assert.NotNull(member);
            }
        }

        [Fact]
        public void When_a_member_is_added_to_an_existing_list_member_is_added_successfully_contact()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var list = new List
            {
                Id = Guid.NewGuid(),
                ListName = "Some list",
                CreatedFromCode = new OptionSetValue((int)ListCreatedFromCode.Contact)
            };

            var contact = new Contact()
            {
                Id = Guid.NewGuid()
            };
            ctx.Initialize(new List<Entity>
            {
                list,
                contact
            });

            AddListMembersListRequest addListMembersListRequest = new AddListMembersListRequest
            {
                MemberIds = new[]
                {
                    contact.Id
                },
                ListId = list.ToEntityReference().Id
            };

            service.Execute(addListMembersListRequest);

            using (var context = new XrmServiceContext(service))
            {
                var member = (from lm in context.CreateQuery<ListMember>()
                              join l in context.CreateQuery<Crm.List>() on lm.ListId.Id equals l.ListId.Value
                              join c in context.CreateQuery<Contact>() on lm.EntityId.Id equals c.ContactId.Value
                              where lm.EntityId.Id == contact.Id
                              where lm.ListId.Id == list.Id
                              select lm
                              ).FirstOrDefault();

                Assert.NotNull(member);
            }
        }

        [Fact]
        public void When_a_member_is_added_to_an_existing_list_member_is_added_successfully_lead()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var list = new List()
            {
                Id = Guid.NewGuid(),
                ListName = "Some list",
                CreatedFromCode = new OptionSetValue((int)ListCreatedFromCode.Lead)
            };

            var lead = new Lead()
            {
                Id = Guid.NewGuid()
            };
            ctx.Initialize(new List<Entity>
            {
                list,
                lead
            });

            AddListMembersListRequest addListMembersListRequest = new AddListMembersListRequest
            {
                MemberIds = new[]
                {
                    lead.Id
                },
                ListId = list.ToEntityReference().Id
            };

            service.Execute(addListMembersListRequest);

            using (var context = new XrmServiceContext(service))
            {
                var member = (from lm in context.CreateQuery<ListMember>()
                              join l in context.CreateQuery<Crm.List>() on lm.ListId.Id equals l.ListId.Value
                              join le in context.CreateQuery<Lead>() on lm.EntityId.Id equals le.LeadId.Value
                              where lm.EntityId.Id == lead.Id
                              where lm.ListId.Id == list.Id
                              select lm
                              ).FirstOrDefault();

                Assert.NotNull(member);
            }
        }
    }
}