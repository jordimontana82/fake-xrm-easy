#if FAKE_XRM_EASY_9
using FakeXrmEasy.Tests.PluginsForTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Xunit;

namespace FakeXrmEasy.Tests.Features
{
    public class RetrieveMultipleDataProviderTesting
    {
        [Fact]
        public void IServiceProvider_should_has_IEntityDataSourceRetrieverService_in_v9()
        {
            var context = new XrmFakedContext();
            context.EntityDataSourceRetriever = new Entity("abc_customdatasource")
            {
                ["abc_crmurl"] = "https://...",
                ["abc_username"] = "abcd",
                ["abc_password"] = "1234"
            };
            var pluginContext = context.GetDefaultPluginContext();
            var entity = new Entity();
            var query = new QueryExpression();
            pluginContext.InputParameters["Query"] = query;

            context.ExecutePluginWithConfigurations<RetrieveMultipleDataProvider>(pluginContext, null, null);

            var outputParameters = pluginContext.OutputParameters["BusinessEntityCollection"] as EntityCollection;
            Assert.Equal(2, outputParameters.Entities.Count);
            Assert.Equal("abc_dataprovider", outputParameters.EntityName);
        }
    }
}
#endif