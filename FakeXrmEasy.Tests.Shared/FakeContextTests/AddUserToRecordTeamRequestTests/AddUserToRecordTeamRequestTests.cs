#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using Crm;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.AddUserToRecordTeamRequestTests
{
    public class AddUserToRecordTeamRequestTests
    {
        [Fact]
        public void When_can_execute_is_called_with_an_invalid_request_result_is_false()
        {
            var executor = new AddUserToRecordTeamRequestExecutor();
            var anotherRequest = new AddToQueueRequest();
            Assert.False(executor.CanExecute(anotherRequest));
        }

        [Fact]
        public void When_a_request_is_called_User_Is_Added_To_Record_Team()
        {
            var context = new XrmFakedContext();

            var teamTemplate = new TeamTemplate
            {
                Id = Guid.NewGuid(),
                DefaultAccessRightsMask = (int)AccessRights.ReadAccess
            };

            var user = new SystemUser
            {
                Id = Guid.NewGuid()
            };

            var account = new Account
            {
                Id = Guid.NewGuid()
            };

            context.Initialize(new Entity[]
            {
                teamTemplate, user, account
            });

            var executor = new AddUserToRecordTeamRequestExecutor();

            var req = new AddUserToRecordTeamRequest
            {
                Record = account.ToEntityReference(),
                SystemUserId = user.Id,
                TeamTemplateId = teamTemplate.Id
            };

            executor.Execute(req, context);

            var team = context.CreateQuery<Team>().FirstOrDefault(p => p.TeamTemplateId.Id == teamTemplate.Id);
            Assert.NotNull(team);

            var teamMembership = context.CreateQuery<TeamMembership>().FirstOrDefault(p => p.SystemUserId == user.Id && p.TeamId == team.Id);
            Assert.NotNull(teamMembership);

            var poa = context.CreateQuery("principalobjectaccess").FirstOrDefault(p => (Guid)p["objectid"] == account.Id && 
                                                                                       (Guid)p["principalid"] == team.Id);
            Assert.NotNull(poa);

            var response = context.AccessRightsRepository.RetrievePrincipalAccess(account.ToEntityReference(),
                user.ToEntityReference());
            Assert.Equal((AccessRights)teamTemplate.DefaultAccessRightsMask, response.AccessRights);

        }
    }
}
#endif