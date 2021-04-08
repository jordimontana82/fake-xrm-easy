#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
#if FAKE_XRM_EASY_DOTNETCORE
using FakeXrmEasy.DotNetCore;
#else
using System.ServiceModel;
#endif
using Microsoft.Crm.Sdk.Messages;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class AddUserToRecordTeamRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is AddUserToRecordTeamRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            AddUserToRecordTeamRequest addReq = (AddUserToRecordTeamRequest)request;

            EntityReference target = addReq.Record;
            Guid systemuserId = addReq.SystemUserId;
            Guid teamTemplateId = addReq.TeamTemplateId;

            if (target == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not add to team without target");
            }

            if (systemuserId == Guid.Empty)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not add to team without user");
            }

            if (teamTemplateId == Guid.Empty)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not add to team without team");
            }

            IOrganizationService service = ctx.GetOrganizationService();

            Entity teamTemplate = ctx.CreateQuery("teamtemplate").FirstOrDefault(p => p.Id == teamTemplateId);
            if (teamTemplate == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Team template with id=" + teamTemplateId + " does not exist");
            }

            Entity user = ctx.CreateQuery("systemuser").FirstOrDefault(p => p.Id == systemuserId);
            if (user == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "User with id=" + teamTemplateId + " does not exist");
            }


            Entity team = ctx.CreateQuery("team").FirstOrDefault(p => ((EntityReference)p["teamtemplateid"]).Id == teamTemplateId);
            if (team == null)
            {
                team = new Entity("team")
                {
                    ["teamtemplateid"] = new EntityReference("teamtemplate", teamTemplateId)
                };
                team.Id = service.Create(team);
            }

            Entity tm = new Entity("teammembership")
            {
                ["systemuserid"] = systemuserId,
                ["teamid"] = team.Id
            };
            tm.Id = service.Create(tm);

            Entity poa = new Entity("principalobjectaccess")
            {
                ["objectid"] = target.Id,
                ["principalid"] = team.Id,
                ["accessrightsmask"] = teamTemplate.Contains("defaultaccessrightsmask") ? teamTemplate["defaultaccessrightsmask"] : 0
            };
            poa.Id = service.Create(poa);

            ctx.AccessRightsRepository.GrantAccessTo(target, new PrincipalAccess
            {
                Principal = user.ToEntityReference(),
                AccessMask = (AccessRights)poa["accessrightsmask"]
            });
            
            return new AddUserToRecordTeamResponse
            {
                ResponseName = "AddUserToRecordTeam"
            };
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(AddUserToRecordTeamRequest);
        }
    }
}
#endif