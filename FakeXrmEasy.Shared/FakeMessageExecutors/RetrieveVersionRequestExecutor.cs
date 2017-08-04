using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using System.Reflection;
using System.Diagnostics;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveVersionRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveVersionRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var assemblyPath = Assembly.GetAssembly(typeof(RetrieveVersionRequest)).Location;
            var versionInfo = FileVersionInfo.GetVersionInfo(assemblyPath);
            var version = versionInfo.FileVersion;

            return new RetrieveVersionResponse
            {
                Results = new ParameterCollection
                {
                    { "Version", version }
                }
            };
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveVersionRequest);
        }
    }
}
