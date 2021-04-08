using Microsoft.Xrm.Sdk;
#if !FAKE_XRM_EASY_DOTNETCORE
using System.ServiceModel;
#else
using FakeXrmEasy.DotNetCore;
#endif

namespace FakeXrmEasy
{
    public class FakeOrganizationServiceFault
    {
        public static void Throw(ErrorCodes errorCode, string message)
        {
            throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault() { ErrorCode = (int)errorCode, Message = message }, message);
        }
    }
}