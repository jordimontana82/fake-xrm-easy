using Microsoft.Xrm.Sdk;
using System.ServiceModel;

namespace FakeXrmEasy.OrganizationFaults
{
    public class OrganizationServiceFaultOperatorIsNotValidException
    {
        public static int ErrorCode = -2147187691;

        public static void Throw()
        {
            throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()
            {
                ErrorCode = ErrorCode,
                Message = "The operator is not valid or it is not supported."
            },
                new FaultReason("The operator is not valid or it is not supported."));
        }
    }
}