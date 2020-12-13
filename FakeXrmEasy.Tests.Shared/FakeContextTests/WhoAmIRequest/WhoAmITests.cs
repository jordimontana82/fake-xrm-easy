using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.WhoAmIRequestTests
{
    public class WhoAmITests
    {
        [Fact]
        public static void When_a_who_am_i_request_is_invoked_the_caller_id_is_returned()
        {
            var context = new XrmFakedContext();
            context.CallerId = new EntityReference() { Id = Guid.NewGuid(), Name = "Super Faked User" };

            var service = context.GetOrganizationService();
            WhoAmIRequest req = new WhoAmIRequest();

            var response = service.Execute(req) as WhoAmIResponse;
            Assert.Equal(response.UserId, context.CallerId.Id);
        }

        [Fact]
        public void OrganizationIsReturnedWhenUserBelongsToOrganization() {

            var user = new Entity("systemuser") {
              Id = Guid.NewGuid(),
              ["organizationid"] = (Guid?)organization_.Id
            };

            var dbContent = new List<Entity> {
              user,
              organization_
            };

            var context = new XrmFakedContext() {
              CallerId = user.ToEntityReference()
            };
            context.Initialize(dbContent);

            var service = context.GetOrganizationService();

            var req = new WhoAmIRequest();
            var response = service.Execute(req) as WhoAmIResponse;

            Assert.Equal(user.Id, response.UserId);
            Assert.Equal(organization_.Id, response.OrganizationId);
        }

        [Fact]
        public void BusinessUnitIsReturnedWhenUserBelongsToBusinessUnit() {

          var businessUnit = new Entity("businessunit") {
            Id = Guid.NewGuid()
          };

          var user = new Entity("systemuser") {
              Id = Guid.NewGuid(),
              ["businessunitid"] = businessUnit.ToEntityReference()
            };

            var dbContent = new List<Entity> {
              user,
              businessUnit
            };

            var context = new XrmFakedContext() {
              CallerId = user.ToEntityReference()
            };
            context.Initialize(dbContent);

            var service = context.GetOrganizationService();

            var req = new WhoAmIRequest();
            var response = service.Execute(req) as WhoAmIResponse;

            Assert.Equal(user.Id, response.UserId);
            Assert.Equal(businessUnit.Id, response.BusinessUnitId);
        }

        [Fact]
        public void BuAndOrgAreReturnedWhenUserBelongsToBuAndOrg() {

            var businessUnit = new Entity("businessunit") {
              Id = Guid.NewGuid()
            };

            var user = new Entity("systemuser") {
              Id = Guid.NewGuid(),
              ["businessunitid"] = businessUnit.ToEntityReference(),
              ["organizationid"] = (Guid?)organization_.Id
            };

            var dbContent = new List<Entity> {
              user,
              businessUnit,
              organization_
            };

            var context = new XrmFakedContext() {
              CallerId = user.ToEntityReference()
            };
            context.Initialize(dbContent);

            var service = context.GetOrganizationService();

            var req = new WhoAmIRequest();
            var response = service.Execute(req) as WhoAmIResponse;

            Assert.Equal(user.Id, response.UserId);
            Assert.Equal(businessUnit.Id, response.BusinessUnitId);
            Assert.Equal(organization_.Id, response.OrganizationId);
        }

        [Fact]
        public void BuAndOrgAreReturnedWhenUserBelongsToBuAndBuHasOrg() {

            var businessUnit = new Entity("businessunit") {
              Id = Guid.NewGuid(),
              ["organizationid"] = organization_.ToEntityReference()
            };

            var user = new Entity("systemuser") {
              Id = Guid.NewGuid(),
              ["businessunitid"] = businessUnit.ToEntityReference(),
            };

            var dbContent = new List<Entity> {
              user,
              businessUnit,
              organization_
            };

            var context = new XrmFakedContext() {
              CallerId = user.ToEntityReference()
            };
            context.Initialize(dbContent);

            var service = context.GetOrganizationService();

            var req = new WhoAmIRequest();
            var response = service.Execute(req) as WhoAmIResponse;

            Assert.Equal(user.Id, response.UserId);
            Assert.Equal(businessUnit.Id, response.BusinessUnitId);
            Assert.Equal(organization_.Id, response.OrganizationId);
        }

        private readonly Entity organization_ = new Entity("organization") {
          Id = Guid.NewGuid()
        };

    }
}