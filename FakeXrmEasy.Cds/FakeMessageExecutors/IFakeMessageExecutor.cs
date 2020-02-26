using Microsoft.Xrm.Sdk;
using System;

namespace FakeXrmEasy.FakeMessageExecutors
{
    /// <summary>
    /// An interface to delegate custom messages to be executed via a IOrganizationService.Execute method.
    /// Each executor is in charge of encapsulating a single request and declare which requests can handle via de CanExecute method
    /// </summary>
    public interface IFakeMessageExecutor
    {
        bool CanExecute(OrganizationRequest request);

        Type GetResponsibleRequestType();

        OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx);
    }
}