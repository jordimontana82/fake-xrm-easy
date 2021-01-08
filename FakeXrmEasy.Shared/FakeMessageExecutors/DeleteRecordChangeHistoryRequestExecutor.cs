using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using System;
using System.ServiceModel;
namespace FakeXrmEasy.FakeMessageExecutors
{
    public class DeleteRecordChangeHistoryRequestExecutor : IFakeMessageExecutor
    {

        public bool CanExecute(OrganizationRequest request)
        {
            return request is DeleteRecordChangeHistoryRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as DeleteRecordChangeHistoryRequest;

            var res = new DeleteRecordChangeHistoryResponse();

            return res;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(DeleteRecordChangeHistoryRequest);
        }
    }

}
