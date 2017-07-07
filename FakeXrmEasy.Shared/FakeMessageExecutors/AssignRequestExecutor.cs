using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.ServiceModel;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class AssignRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is AssignRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var assignRequest = (AssignRequest)request;

            var target = assignRequest.Target;
            var assignee = assignRequest.Assignee;

            if (target == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not assign without target");
            }

            if (assignee == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Can not assign without assignee");
            }

            var service = ctx.GetOrganizationService();

            var assignment = new Entity
            {
                LogicalName = target.LogicalName,
                Id = target.Id,
                Attributes = new AttributeCollection
                {
                    { "ownerid", assignee }
                }
            };

            service.Update(assignment);

            return new AssignResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(AssignRequest);
        }
    }
}