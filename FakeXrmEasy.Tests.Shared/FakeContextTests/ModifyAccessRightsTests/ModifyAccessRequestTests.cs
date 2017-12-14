using System;
using System.Collections.Generic;
using System.Text;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using Xunit;
using Microsoft.Crm.Sdk.Messages;

namespace FakeXrmEasy.Tests.FakeContextTests.ModifyAccessRightsTests
{
    public class ModifyAccessRequestTests
    {
        /// <summary>
        /// Test that if permissions already exist that they can be modified
        /// </summary>
        [Fact]
        public void Test_That_Existing_Permissions_Can_Be_Modified()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity contact = new Entity("contact");
            contact.Id = Guid.NewGuid();
            initialEntities.Add(contact);

            Entity user = new Entity("systemuser");
            user.Id = Guid.NewGuid();
            initialEntities.Add(user);

            context.Initialize(initialEntities);

            GrantAccessRequest grantRequest = new GrantAccessRequest()
            {
                Target = contact.ToEntityReference(),
                PrincipalAccess = new PrincipalAccess() { Principal = user.ToEntityReference(), AccessMask = AccessRights.ReadAccess }
            };

            service.Execute(grantRequest);

            RetrieveSharedPrincipalsAndAccessRequest getPermissions = new RetrieveSharedPrincipalsAndAccessRequest()
            {
                Target = contact.ToEntityReference(),
            };

            var permissionsResponse = (RetrieveSharedPrincipalsAndAccessResponse)service.Execute(getPermissions);

            // Make sure things are correct before I start changing things
            Assert.Equal(user.Id, permissionsResponse.PrincipalAccesses[0].Principal.Id);
            Assert.Equal(AccessRights.ReadAccess, permissionsResponse.PrincipalAccesses[0].AccessMask);

            ModifyAccessRequest modifyRequest = new ModifyAccessRequest()
            {
                Target = contact.ToEntityReference(),
                PrincipalAccess = new PrincipalAccess() { Principal = user.ToEntityReference(), AccessMask = AccessRights.ReadAccess | AccessRights.DeleteAccess }
            };

            service.Execute(modifyRequest);

            permissionsResponse = (RetrieveSharedPrincipalsAndAccessResponse)service.Execute(getPermissions);

            // Check permissions
            Assert.Equal(user.Id, permissionsResponse.PrincipalAccesses[0].Principal.Id);
            Assert.Equal(AccessRights.ReadAccess | AccessRights.DeleteAccess, permissionsResponse.PrincipalAccesses[0].AccessMask);
        }

        /// <summary>
        /// If permssions haven't been set ModifyAccessRequest actually creates the permissions
        /// </summary>
        [Fact]
        public void Test_If_Permissions_Missing_Permissions_Are_Added()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();
            List<Entity> initialEntities = new List<Entity>();

            Entity contact = new Entity("contact");
            contact.Id = Guid.NewGuid();
            initialEntities.Add(contact);

            Entity user = new Entity("systemuser");
            user.Id = Guid.NewGuid();
            initialEntities.Add(user);

            context.Initialize(initialEntities);

            ModifyAccessRequest modifyRequest = new ModifyAccessRequest()
            {
                Target = contact.ToEntityReference(),
                PrincipalAccess = new PrincipalAccess() { Principal = user.ToEntityReference(), AccessMask = AccessRights.ReadAccess | AccessRights.DeleteAccess }
            };

            service.Execute(modifyRequest);


            RetrieveSharedPrincipalsAndAccessRequest getPermissions = new RetrieveSharedPrincipalsAndAccessRequest()
            {
                Target = contact.ToEntityReference(),
            };

            var permissionsResponse = (RetrieveSharedPrincipalsAndAccessResponse)service.Execute(getPermissions);

            // Check permissions
            Assert.Equal(user.Id, permissionsResponse.PrincipalAccesses[0].Principal.Id);
            Assert.Equal(AccessRights.ReadAccess | AccessRights.DeleteAccess, permissionsResponse.PrincipalAccesses[0].AccessMask);
        }
    }
}
