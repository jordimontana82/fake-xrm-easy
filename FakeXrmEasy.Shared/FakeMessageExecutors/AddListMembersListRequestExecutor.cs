using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using System.ServiceModel;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class AddListMembersListRequestExecutor : IFakeMessageExecutor
    {
        public enum ListCreatedFromCode
        {
            Account = 1,
            Contact = 2,
            Lead = 4
        }

        public bool CanExecute(OrganizationRequest request)
        {
            return request is AddListMembersListRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = (AddListMembersListRequest)request;

            if (req.ListId == null || req.ListId == Guid.Empty)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "ListId parameter is required");
            }

            if (req.MemberIds == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "MemberIds parameter is required");
            }

            var service = ctx.GetOrganizationService();

            //Find the list
            var list = ctx.CreateQuery("list")
                        .Where(e => e.Id == req.ListId)
                        .FirstOrDefault();

            if (list == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("List with Id {0} wasn't found", req.ListId.ToString()));
            }

            //Find the member
            if (!list.Attributes.ContainsKey("createdfromcode"))
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("List with Id {0} must have a CreatedFromCode attribute defined and it has to be an option set value.", req.ListId.ToString()));
            }

            if (list["createdfromcode"] != null && !(list["createdfromcode"] is OptionSetValue))
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("List with Id {0} must have a CreatedFromCode attribute defined and it has to be an option set value.", req.ListId.ToString()));
            }

            var createdFromCodeValue = (list["createdfromcode"] as OptionSetValue).Value;
            string memberEntityName = "";
            switch (createdFromCodeValue)
            {
                case (int)ListCreatedFromCode.Account:
                    memberEntityName = "account";
                    break;

                case (int)ListCreatedFromCode.Contact:
                    memberEntityName = "contact";
                    break;

                case (int)ListCreatedFromCode.Lead:
                    memberEntityName = "lead";
                    break;

                default:
                    throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("List with Id {0} must have a supported CreatedFromCode value (Account, Contact or Lead).", req.ListId.ToString()));
            }

            foreach (var memberId in req.MemberIds)
            {
                var member = ctx.CreateQuery(memberEntityName)
            .Where(e => e.Id == memberId)
            .FirstOrDefault();

                if (member == null)
                {
                    throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), string.Format("Member of type {0} with Id {1} wasn't found", memberEntityName, memberId.ToString()));
                }

                //create member list
                var listmember = new Entity("listmember");
                listmember["listid"] = new EntityReference("list", req.ListId);
                listmember["entityid"] = new EntityReference(memberEntityName, memberId);

                service.Create(listmember);
            }

            return new AddListMembersListResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(AddListMembersListRequest);
        }
    }
}