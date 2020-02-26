using Crm;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.AddMemberListRequestTests
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

            AddMemberListRequest marketingList = new AddMemberListRequest(); // Set the properties of the request object.
            marketingList.EntityId = Guid.NewGuid();
            marketingList.ListId = Guid.NewGuid();

            // Execute the request.
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(marketingList));
        }

        [Fact]
        public void When_a_request_is_called_with_an_empty_listid_parameter_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            AddMemberListRequest marketingList = new AddMemberListRequest(); // Set the properties of the request object.
            marketingList.EntityId = Guid.NewGuid();
            marketingList.ListId = Guid.Empty;

            // Execute the request.
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(marketingList));
        }

        [Fact]
        public void When_a_request_is_called_with_an_empty_entityid_parameter_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            AddMemberListRequest marketingList = new AddMemberListRequest(); // Set the properties of the request object.
            marketingList.EntityId = Guid.Empty;
            marketingList.ListId = Guid.NewGuid();

            // Execute the request.
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(marketingList));
        }

        [Fact]
        public void When_a_member_is_added_to_an_existing_list_without_membercode_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var list = new Crm.List()
            {
                Id = Guid.NewGuid(),
                ListName = "Some list"
            };

            ctx.Initialize(new List<Entity>
            {
                list
            });

            AddMemberListRequest marketingList = new AddMemberListRequest();
            marketingList.EntityId = Guid.NewGuid();
            marketingList.ListId = list.ToEntityReference().Id;

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(marketingList));
        }

        [Fact]
        public void When_a_non_existing_member_is_added_to_an_existing_list_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var list = new Crm.List()
            {
                Id = Guid.NewGuid(),
                ListName = "Some list",
                CreatedFromCode = new OptionSetValue((int)ListCreatedFromCode.Account)
            };

            ctx.Initialize(new List<Entity>
            {
                list
            });

            AddMemberListRequest request = new AddMemberListRequest();
            request.EntityId = Guid.NewGuid();
            request.ListId = list.ToEntityReference().Id;

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(request));
        }

        [Fact]
        public void When_a_member_is_added_to_an_existing_list_member_is_added_successfully_account()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var list = new Crm.List()
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
                list, account
            });

            AddMemberListRequest request = new AddMemberListRequest();
            request.EntityId = account.Id;
            request.ListId = list.ToEntityReference().Id;

            service.Execute(request);

            using (var context = new XrmServiceContext(service))
            {
                var member = (from lm in context.CreateQuery<ListMember>()
                              join l in context.CreateQuery<Crm.List>() on lm.ListId.Id equals l.ListId.Value
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

            var list = new Crm.List()
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
                list, contact
            });

            AddMemberListRequest request = new AddMemberListRequest();
            request.EntityId = contact.Id;
            request.ListId = list.ToEntityReference().Id;

            service.Execute(request);

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

            var list = new Crm.List()
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
                list, lead
            });

            AddMemberListRequest request = new AddMemberListRequest();
            request.EntityId = lead.Id;
            request.ListId = list.ToEntityReference().Id;

            service.Execute(request);

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