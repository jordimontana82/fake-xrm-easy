using Crm;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.Tests.FakeContextTests.PermissionsTests
{
    public class PermissionsTests
    {
        [Fact]
        public void Entity_Granted_Access_Has_Access()
        {
            var context = new XrmFakedContext();
            var contact = new Contact { Id = Guid.NewGuid() };
            var user = new SystemUser { Id = Guid.NewGuid() };

            context.Initialize(new List<Entity>
            {
                contact, user
            });

            var service = context.GetFakedOrganizationService();

            GrantAccessRequest gar = new GrantAccessRequest
            {
                PrincipalAccess = new PrincipalAccess
                {
                    AccessMask = AccessRights.ReadAccess,
                    Principal = user.ToEntityReference()
                },
                Target = contact.ToEntityReference()
            };
            service.Execute(gar);

            RetrievePrincipalAccessRequest rpar = new RetrievePrincipalAccessRequest
            {
                Target = contact.ToEntityReference(),
                Principal = user.ToEntityReference()
            };

            RetrievePrincipalAccessResponse rpaResp = (RetrievePrincipalAccessResponse)service.Execute(rpar);
            Assert.NotEqual(AccessRights.None, rpaResp.AccessRights);
            Assert.True(rpaResp.AccessRights.HasFlag(AccessRights.ReadAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.AppendAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.AppendToAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.AssignAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.CreateAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.DeleteAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.ShareAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.WriteAccess));
        }

        [Fact]
        public void Entity_Granted_Multiple_Access_Has_Access()
        {
            var context = new XrmFakedContext();
            var contact = new Contact { Id = Guid.NewGuid() };
            var user = new SystemUser { Id = Guid.NewGuid() };

            context.Initialize(new List<Entity>
            {
                contact, user
            });

            var service = context.GetFakedOrganizationService();

            GrantAccessRequest gar = new GrantAccessRequest
            {
                PrincipalAccess = new PrincipalAccess
                {
                    AccessMask = AccessRights.ReadAccess | AccessRights.WriteAccess | AccessRights.DeleteAccess | AccessRights.CreateAccess,
                    Principal = user.ToEntityReference()
                },
                Target = contact.ToEntityReference()
            };
            service.Execute(gar);

            RetrievePrincipalAccessRequest rpar = new RetrievePrincipalAccessRequest
            {
                Target = contact.ToEntityReference(),
                Principal = user.ToEntityReference()
            };

            RetrievePrincipalAccessResponse rpaResp = (RetrievePrincipalAccessResponse)service.Execute(rpar);
            Assert.NotEqual(AccessRights.None, rpaResp.AccessRights);
            Assert.True(rpaResp.AccessRights.HasFlag(AccessRights.ReadAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.AppendAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.AppendToAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.AssignAccess));
            Assert.True(rpaResp.AccessRights.HasFlag(AccessRights.CreateAccess));
            Assert.True(rpaResp.AccessRights.HasFlag(AccessRights.DeleteAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.ShareAccess));
            Assert.True(rpaResp.AccessRights.HasFlag(AccessRights.WriteAccess));
        }

        [Fact]
        public void Entity_Not_Granted_Access_Does_Not_Have_Access()
        {
            var context = new XrmFakedContext();
            var contact = new Contact { Id = Guid.NewGuid() };
            var user = new SystemUser { Id = Guid.NewGuid() };

            context.Initialize(new List<Entity>
            {
                contact, user
            });

            var service = context.GetFakedOrganizationService();

            RetrievePrincipalAccessRequest rpar = new RetrievePrincipalAccessRequest
            {
                Target = contact.ToEntityReference(),
                Principal = user.ToEntityReference()
            };

            RetrievePrincipalAccessResponse rpaResp = (RetrievePrincipalAccessResponse)service.Execute(rpar);
            Assert.Equal(AccessRights.None, rpaResp.AccessRights);
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.ReadAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.AppendAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.AppendToAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.AssignAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.CreateAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.DeleteAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.ShareAccess));
            Assert.False(rpaResp.AccessRights.HasFlag(AccessRights.WriteAccess));
        }
    }
}
