using System;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using System.Linq;

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

        [Fact]
        public void When_initializing_the_context_with_an_entity_the_context_has_that_entity()
        {
            var context = new XrmFakedContext();
            var guid = Guid.NewGuid();

            IQueryable<Entity> data = new List<Entity>() {
                new Entity("account") { Id = guid }
            }.AsQueryable();

            context.Initialize(data);
            Assert.True(context.Data.Count == 1);
            Assert.True(context.Data["account"].Count == 1);
            Assert.Equal(context.Data["account"][guid], data.FirstOrDefault());
        }
        [Fact]
        public void When_initializing_with_two_entities_with_the_same_guid_only_the_latest_will_be_in_the_context()
        {
            var context = new XrmFakedContext();
            var guid = Guid.NewGuid();

            IQueryable<Entity> data = new List<Entity>() {
                new Entity("account") { Id = guid },
                new Entity("account") { Id = guid }
            }.AsQueryable();

            context.Initialize(data);
            Assert.True(context.Data.Count == 1);
            Assert.True(context.Data["account"].Count == 1);
            Assert.Equal(context.Data["account"][guid], data.LastOrDefault());
        }

        [Fact]
        public void When_initializing_with_two_entities_with_two_different_guids_the_context_will_have_both()
        {
            var context = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            IQueryable<Entity> data = new List<Entity>() {
                new Entity("account") { Id = guid1 },
                new Entity("account") { Id = guid2 }
            }.AsQueryable();

            context.Initialize(data);
            Assert.True(context.Data.Count == 1);
            Assert.True(context.Data["account"].Count == 2);
            Assert.Equal(context.Data["account"][guid1], data.FirstOrDefault());
            Assert.Equal(context.Data["account"][guid2], data.LastOrDefault());
        }
        [Fact]
        public void When_initializing_with_two_entities_of_same_logical_name_and_another_one_the_context_will_have_all_three()
        {
            var context = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();

            IQueryable<Entity> data = new List<Entity>() {
                new Entity("account") { Id = guid1 },
                new Entity("account") { Id = guid2 },
                new Entity("contact") { Id = guid3 }
            }.AsQueryable();

            context.Initialize(data);
            Assert.True(context.Data.Count == 2);
            Assert.True(context.Data["account"].Count == 2);
            Assert.True(context.Data["contact"].Count == 1);
            Assert.Equal(context.Data["account"][guid1], data.FirstOrDefault());
            Assert.Equal(context.Data["contact"][guid3], data.LastOrDefault());

        }

        
        

    }
}
