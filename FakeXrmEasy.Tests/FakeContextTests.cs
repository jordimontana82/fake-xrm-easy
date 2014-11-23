using System;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;

namespace FakeXrmEasy.Tests
{
    public class FakeXrmEasyTests
    {
        [Fact]
        public void When_a_fake_context_is_created_the_data_is_initialized()
        {
            var context = new FakedContext();
            Assert.True(context.Data != null);
        }
    }
}
