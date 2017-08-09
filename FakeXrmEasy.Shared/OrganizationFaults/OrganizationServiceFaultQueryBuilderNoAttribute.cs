using Microsoft.Xrm.Sdk;
using System.ServiceModel;

namespace FakeXrmEasy.OrganizationFaults
{
    public class OrganizationServiceFaultQueryBuilderNoAttributeException
    {
        public static int ErrorCode = 80041103;

        protected OrganizationServiceFault _fault = null;

        public static void Throw(string sMissingAttributeName)
        {
            throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()
            {
                ErrorCode = ErrorCode,
                Message = string.Format("The attribute {0} does not exist on this entity.", sMissingAttributeName)
            },
                new FaultReason(string.Format("The attribute {0} does not exist on this entity.", sMissingAttributeName)));
        }
    }
}