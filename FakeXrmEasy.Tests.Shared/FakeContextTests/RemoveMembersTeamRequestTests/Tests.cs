using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using Xunit;

#if FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
using Xunit.Sdk;
#endif

using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using Crm;
using System.ServiceModel;

namespace FakeXrmEasy.Tests.FakeContextTests.RemoveMembersTeamRequestTests
{
    public class Tests
    {
        [Fact]
        public void When_a_member_is_added_to_a_non_existing_team_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            RemoveMembersTeamRequest removeMembersTeamRequest = new RemoveMembersTeamRequest
            {
                MemberIds = new[]
                {
                    Guid.NewGuid()
                },
                TeamId = Guid.NewGuid()
            };

            // Execute the request.
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(removeMembersTeamRequest));
        }

        [Fact]
        public void When_a_request_is_called_with_an_empty_teamid_parameter_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            RemoveMembersTeamRequest removeMembersTeamRequest = new RemoveMembersTeamRequest
            {
                MemberIds = new[]
                {
                    Guid.NewGuid()
                },
                TeamId = Guid.Empty
            };

            // Execute the request.
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(removeMembersTeamRequest));
        }

        [Fact]
        public void When_a_request_is_called_with_a_null_memberid_parameter_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            RemoveMembersTeamRequest removeMembersTeamRequest = new RemoveMembersTeamRequest
            {
                MemberIds = null,
                TeamId = Guid.NewGuid()
            };

            // Execute the request.
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(removeMembersTeamRequest));
        }

        [Fact]
        public void When_a_request_is_called_with_an_empty_memberid_parameter_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            RemoveMembersTeamRequest addMembersTeamRequest = new RemoveMembersTeamRequest
            {
                MemberIds = new[]
                {
                    Guid.Empty
                },
                TeamId = Guid.NewGuid()
            };

            // Execute the request.
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(addMembersTeamRequest));
        }

        [Fact]
        public void When_a_non_existing_member_is_removed_from_an_existing_list_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var team = new Team
            {
                Id = Guid.NewGuid(),
                Name = "Some team"
            };

            
            var teammembership = new Entity("teammembership")
            {
                Id = Guid.NewGuid()
            };

            // We use this way, because the TeamId and SystemUserId properties are readonly
            teammembership["teamid"] = team.Id;
            teammembership["systemuserid"] = Guid.NewGuid();

            ctx.Initialize(new List<Entity>
            {
                team,
                teammembership
            });

            RemoveMembersTeamRequest removeMembersTeamRequest = new RemoveMembersTeamRequest
            {
                MemberIds = new[]
                {
                    Guid.NewGuid()
                },
                TeamId = team.ToEntityReference().Id
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(removeMembersTeamRequest));
        }

        [Fact]
        public void When_a_member_is_removed_from_an_existing_list_member_is_removed_successfully()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetOrganizationService();

            var team = new Team
            {
                Id = Guid.NewGuid(),
                Name = "Some team"
            };

            var systemuser = new SystemUser
            {
                Id = Guid.NewGuid()
            };

            
            var teammembership = new Entity("teammembership")
            {
                Id = Guid.NewGuid()
            };

            // We use this way, because the TeamId and SystemUserId properties are readonly
            teammembership["teamid"] = team.Id;
            teammembership["systemuserid"] = systemuser.Id;

            ctx.Initialize(new List<Entity>
            {
                team,
                systemuser,
                teammembership
            });

            RemoveMembersTeamRequest removeMembersTeamRequest = new RemoveMembersTeamRequest
            {
                MemberIds = new[]
                {
                    systemuser.Id
                },
                TeamId = team.ToEntityReference().Id
            };

            service.Execute(removeMembersTeamRequest);

            using (var context = new XrmServiceContext(service))
            {
                var member = context.CreateQuery<TeamMembership>().FirstOrDefault(tm => tm.TeamId == team.Id && tm.SystemUserId == systemuser.Id);

                Assert.Null(member);
            }
        }
    }
}