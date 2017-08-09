using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Xunit;

namespace FakeXrmEasy.Tests.Extensions
{
    public class EntityExtensionsTests
    {
        [Fact]
        public void SetValueIfEmpty_should_not_override_existing_values()
        {
            var e = new Entity("account");
            e["name"] = "Some name";

            e.SetValueIfEmpty("name", "another name");
            Assert.Equal(e["name"].ToString(), "Some name");
        }

        [Fact]
        public void SetValueIfEmpty_should_override_if_null()
        {
            var e = new Entity("account");
            e["name"] = null;

            e.SetValueIfEmpty("name", "new name");
            Assert.Equal(e["name"].ToString(), "new name");
        }

        [Fact]
        public void SetValueIfEmpty_should_override_if_doesnt_contains_key()
        {
            var e = new Entity("account");

            e.SetValueIfEmpty("name", "new name");
            Assert.Equal(e["name"].ToString(), "new name");
        }
    }
}