using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
#if !FAKE_XRM_EASY_DOTNETCORE
using System.ServiceModel;
#else
using FakeXrmEasy.DotNetCore;
#endif
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RemoveMembersTeamRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RemoveMembersTeamRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = (RemoveMembersTeamRequest)request;

            if (req.TeamId == null || req.TeamId == Guid.Empty)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "TeamId parameter is required");
            }

            if (req.MemberIds == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "MemberIds parameter is required");
            }

            var service = ctx.GetOrganizationService();

            // Find the list
            var team = ctx.CreateQuery("team").FirstOrDefault(e => e.Id == req.TeamId);

            if (team == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("Team with Id {0} wasn't found", req.TeamId.ToString()));
            }

            foreach (var memberId in req.MemberIds)
            {
                var user = ctx.CreateQuery("systemuser").FirstOrDefault(e => e.Id == memberId);
                if (user == null)
                {
                    throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("SystemUser with Id {0} wasn't found", memberId.ToString()));
                }

                var queryTeamMember = new QueryExpression("teammembership")
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet("teammembershipid"),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression("teamid", ConditionOperator.Equal, req.TeamId),
                            new ConditionExpression("systemuserid", ConditionOperator.Equal, user.Id)
                        }
                    }
                };

                var teamMember = ctx.Service.RetrieveMultiple(queryTeamMember).Entities.FirstOrDefault();

                if (teamMember != null)
                {
                    service.Delete("teammembership", teamMember.Id);
                }
            }

            return new RemoveMembersTeamResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RemoveMembersTeamRequest);
        }
    }
}