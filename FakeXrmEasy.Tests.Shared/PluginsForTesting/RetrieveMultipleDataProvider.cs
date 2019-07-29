#if FAKE_XRM_EASY_9
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace FakeXrmEasy.Tests.PluginsForTesting
{
    public class RetrieveMultipleDataProvider : IPlugin
    {
        private readonly string _unsecureString = null;
        private readonly string _secureString = null;

        public RetrieveMultipleDataProvider(string unsecureString, string secureString)
        {
            if (!string.IsNullOrWhiteSpace(unsecureString)) _unsecureString = unsecureString;
            if (!string.IsNullOrWhiteSpace(secureString)) _secureString = secureString;
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(context.UserId);
            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var retriever = serviceProvider.Get<IEntityDataSourceRetrieverService>();
            var dataSource = retriever.RetrieveEntityDataSource();

            var crmUrl = dataSource.GetAttributeValue<string>("abc_crmurl");
            var userName = dataSource.GetAttributeValue<string>("abc_username");
            var password = dataSource.GetAttributeValue<string>("abc_password");

            var query = context.InputParameterOrDefault<QueryExpression>("Query");

            var entities = new EntityCollection
            {
                MoreRecords = false,
                TotalRecordCountLimitExceeded = false,
                TotalRecordCount = 2,
                EntityName = "abc_dataprovider"
            };
            var row1 = "1: ";
            entities.Entities.Add(new Entity("abc_dataprovider", Guid.Parse("{86C8A693-5D26-4AC5-89FF-0606AE2B52DB}"))
            {
                ["abc_dataproviderid"] = Guid.Parse("{86C8A693-5D26-4AC5-89FF-0606AE2B52DB}"),
                ["abc_link"] = row1 + crmUrl,
                ["abc_user"] = row1 + userName,
                ["abc_pass"] = row1 + password
            });
            var row2 = "2: ";
            entities.Entities.Add(new Entity("abc_dataprovider", Guid.Parse("{86C8A693-5D26-4AC5-89FF-0606AE2B52DB}"))
            {
                ["abc_dataproviderid"] = Guid.Parse("{500B71FF-EA05-4064-ABE5-868F93A0E241}"),
                ["abc_link"] = row2 + crmUrl,
                ["abc_user"] = row2 + userName,
                ["abc_pass"] = row2 + password
            });
            context.OutputParameters["BusinessEntityCollection"] = entities;
        }
    }
}
#endif