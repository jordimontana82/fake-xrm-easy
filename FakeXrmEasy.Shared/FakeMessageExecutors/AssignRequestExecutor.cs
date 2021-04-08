using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
#if !FAKE_XRM_EASY_DOTNETCORE
using System.ServiceModel;
#else
using FakeXrmEasy.DotNetCore;
#endif

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

            KeyValuePair<string, object> owningX = new KeyValuePair<string, object>();
            if (assignee.LogicalName == "systemuser")
                owningX = new KeyValuePair<string, object>("owninguser", assignee);
            else if (assignee.LogicalName == "team")
                owningX = new KeyValuePair<string, object>("owningteam", assignee);

            var assignment = new Entity
            {
                LogicalName = target.LogicalName,
                Id = target.Id,
                Attributes = new AttributeCollection
                {
                    { "ownerid", assignee },
                    owningX
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