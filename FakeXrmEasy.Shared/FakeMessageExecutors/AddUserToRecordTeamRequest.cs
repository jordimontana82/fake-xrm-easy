using System;
using Microsoft.Xrm.Sdk;
using System.ServiceModel;
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

            AddUserToRecordTeamRequest addRequest = new AddUserToRecordTeamRequest
            {
                SystemUserId = systemuserId,
                Record = target,
                TeamTemplateId = teamTemplateId
            };

            AddUserToRecordTeamResponse response = (AddUserToRecordTeamResponse)service.Execute(addRequest);

            return response;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(AddUserToRecordTeamRequestExecutor);
        }
    }
}
