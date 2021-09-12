#if FAKE_XRM_EASY_9
using FakeItEasy;
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

            var logger = context.GetLogger();

            A.CallTo(() => logger.LogInformation("Test")).MustHaveHappened(1, Times.Exactly);
        }
    }
}
#endif