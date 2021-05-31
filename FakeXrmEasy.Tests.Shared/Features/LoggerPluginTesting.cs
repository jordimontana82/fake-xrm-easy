#if FAKE_XRM_EASY_9
using FakeXrmEasy.Tests.PluginsForTesting;
using Xunit;

namespace FakeXrmEasy.Tests.Features
{
    public class LoggerPluginTesting
    {
        [Fact]
        public void IServiceProvider_should_has_ILogger_in_v9()
        {
            var context = new XrmFakedContext();

            context.ExecutePluginWith<LoggerPlugin>();
        }
    }
}
#endif