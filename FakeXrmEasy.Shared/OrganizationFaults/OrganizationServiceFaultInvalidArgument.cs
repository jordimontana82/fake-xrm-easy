using Microsoft.Xrm.Sdk;
using System.ServiceModel;

namespace FakeXrmEasy.OrganizationFaults
{
    public class OrganizationServiceFaultInvalidArgument
    {
        public static int ErrorCode = -2147220989;

        public static void Throw(string message)
        {
            throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()
            {
                ErrorCode = ErrorCode,
                Message = message
            },
                new FaultReason(message));
        }
    }
}