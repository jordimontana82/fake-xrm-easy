using Microsoft.Xrm.Sdk;
using System;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class PreAccountCreate : IPlugin
    {
        /// <summary>
        /// A plug-in that auto generates an account number when an
        /// account is created.
        /// </summary>
        /// <remarks>Register this plug-in on the Create message, account entity,
        /// and pre-operation stage.
        /// </remarks>
        //<snippetAccountNumberPlugin2>
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (context == null)
                throw new InvalidPluginExecutionException("Initialize IPluginExecutionContext fail.");

            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            if (serviceFactory == null)
                throw new InvalidPluginExecutionException("Initialize IOrganizationServiceFactory fail.");

            var service = serviceFactory.CreateOrganizationService(context.UserId);
            if (service == null)
                throw new InvalidPluginExecutionException("Initialize IOrganizationService fail.");

            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            if (tracing == null)
                throw new InvalidPluginExecutionException("Initialize ITracingService fail.");

            if (context.PrimaryEntityName.ToLower() != "Account".ToLower())
                throw new InvalidPluginExecutionException("PrimaryEntityName does not equals Account");

            if (context.MessageName.ToLower() != "Create".ToLower())
                throw new InvalidPluginExecutionException("MessageName does not equals Create");

            if (context.Stage != 20)
                throw new InvalidPluginExecutionException("Stage does not equals PreOperation");
        }
    }
}