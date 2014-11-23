using System;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Tests
{
    public class FakeXrmEasyTests
    {
        [Fact]
        public void When_a_fake_context_is_created_the_data_is_initialized()
        {
            var context = new XrmFakedContext();
            Assert.True(context.Data != null);
        }

        [Fact]
        public void When_getting_the_faked_organization_service_the_context_must_not_be_null()
        {
            var context = new XrmFakedContext();
            var ex = Assert.Throws<InvalidOperationException>(() => context.GetFakedOrganizationService(null));
            Assert.Equal(ex.Message, "The faked context must not be null.");
        }

        [Fact]
        public void When_initializing_the_context_with_a_null_list_of_entities_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var ex = Assert.Throws<InvalidOperationException>(() => context.Initialize(null));
            Assert.Equal(ex.Message, "The entities parameter must be not null");
        }

        [Fact]
        public void When_initializing_the_context_with_an_entity_with_an_empty_logical_name_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            IEnumerable<Entity> data = new List<Entity>() {
                new Entity() { Id = Guid.NewGuid()}
            };

            var ex = Assert.Throws<InvalidOperationException>(() => context.Initialize(data));
            Assert.Equal(ex.Message, "An entity must not have a null or empty LogicalName property.");
        }

        [Fact]
        public void When_initializing_the_context_with_an_entity_with_an_empty_guid_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            IEnumerable<Entity> data = new List<Entity>() {
                new Entity("account") { Id = Guid.Empty}
            };

            var ex = Assert.Throws<InvalidOperationException>(() => context.Initialize(data));
            Assert.Equal(ex.Message, "An entity with an empty Id can't be added");
        }

        
        

    }
}
