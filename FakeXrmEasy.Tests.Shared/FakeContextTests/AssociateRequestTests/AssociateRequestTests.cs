using Crm;
using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.AssociateRequestTests
{
    public class AssociateRequestTests
    {
        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new AssociateRequestExecutor();
            var anotherRequest = new RetrieveMultipleRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }

        [Fact]
        public void When_execute_is_called_with_a_null_request_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var executor = new AssociateRequestExecutor();
            AssociateRequest req = null;
            Assert.Throws<Exception>(() => executor.Execute(req, context));
        }

        [Fact]
        public void When_execute_is_called_with_a_null_target_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var executor = new AssociateRequestExecutor();
            var req = new AssociateRequest() { Target = null, Relationship = new Relationship("fakeRelationship") };
            context.AddRelationship("fakeRelationship", new XrmFakedRelationship());
            Assert.Throws<Exception>(() => executor.Execute(req, context));
        }

        [Fact]
        public void When_execute_is_called_with_reversed_target_and_Related()
        {
            var context = new XrmFakedContext();

            var userId = Guid.NewGuid();
            var teamId = Guid.NewGuid();
            var user2Id = Guid.NewGuid();
            context.Initialize(new List<Entity> {
                new SystemUser
                {
                    Id = userId
                },
                new SystemUser
                {
                    Id = user2Id
                },
                new Team
                {
                    Id = teamId
                }
            });
            
            context.AddRelationship("teammembership", new XrmFakedRelationship()
            {
                RelationshipType = XrmFakedRelationship.enmFakeRelationshipType.ManyToMany,
                IntersectEntity = "teammembership",
                Entity1Attribute = "systemuserid",
                Entity1LogicalName = "systemuser",
                Entity2Attribute = "teamid",
                Entity2LogicalName = "team"
            });

            var orgSvc = context.GetFakedOrganizationService();
            orgSvc.Associate("team", teamId, new Relationship("teammembership"),
                new EntityReferenceCollection(new List<EntityReference> { new EntityReference("systemuser", userId) }));

            orgSvc.Associate("systemuser", user2Id, new Relationship("teammembership"),
                new EntityReferenceCollection(new List<EntityReference> { new EntityReference("team", teamId) }));

            using (Crm.XrmServiceContext ctx = new XrmServiceContext(orgSvc))
            {
                var firstAssociation = (from tu in ctx.TeamMembershipSet
                                        where tu.TeamId == teamId
                                        && tu.SystemUserId == userId
                                        select tu).FirstOrDefault();
                Assert.NotNull(firstAssociation);

                var secondAssociation = (from tu in ctx.TeamMembershipSet
                                        where tu.TeamId == teamId
                                        && tu.SystemUserId == user2Id
                                        select tu).FirstOrDefault();
                Assert.NotNull(secondAssociation);
            }

        }
    }
}
