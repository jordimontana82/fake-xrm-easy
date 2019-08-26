using System;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using System.Linq;
using System.Threading;
using FakeXrmEasy.Tests.PluginsForTesting;
using Crm;
using System.Reflection;

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
        public void When_initializing_the_context_with_a_null_list_of_entities_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var ex = Assert.Throws<InvalidOperationException>(() => context.Initialize(entities: null));
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
            Assert.Equal(ex.Message, "The LogicalName property must not be empty");
        }

        [Fact]
        public void When_initializing_the_context_with_an_entity_with_an_empty_guid_an_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            IEnumerable<Entity> data = new List<Entity>() {
                new Entity("account") { Id = Guid.Empty }
            };

            var ex = Assert.Throws<InvalidOperationException>(() => context.Initialize(data));
            Assert.Equal(ex.Message, "The Id property must not be empty");
        }

        [Fact]
        public void When_initializing_the_context_with_a_dynamic_entity_without_a_primary_key_but_id_entity_is_added()
        {
            var context = new XrmFakedContext();
            IEnumerable<Entity> data = new List<Entity>() {
                new Entity("account") { Id = Guid.NewGuid() }
            };

            context.Initialize(data);
            Assert.True(context.Data.Count == 1);
            Assert.True(context.Data["account"].Count == 1);
        }


        [Fact]
        public void When_initializing_the_context_with_a_dynamic_entity_with_a_primary_key_is_added_to_the_context()
        {
            var context = new XrmFakedContext();
            IEnumerable<Entity> data = new List<Entity>() {
                new Entity("account") { Id = Guid.NewGuid(), Attributes = new AttributeCollection { { "accountid", Guid.NewGuid() } }  }
            };

            context.Initialize(data);
            Assert.True(context.Data.Count == 1);
            Assert.True(context.Data["account"].Count == 1);
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
            Assert.Equal(context.Data["account"][guid].Id, data.FirstOrDefault().Id);
        }

        [Fact]
        public void When_initializing_the_context_with_the_single_entity_overload_the_context_has_that_entity()
        {
            var context = new XrmFakedContext();
            var guid = Guid.NewGuid();

            context.Initialize(new Entity("account") { Id = guid });
            Assert.True(context.Data.Count == 1);
            Assert.True(context.Data["account"].Count == 1);
            Assert.Equal(context.Data["account"][guid].Id, guid);
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
            Assert.Equal(context.Data["account"][guid].Id, data.LastOrDefault().Id);
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
            Assert.Equal(context.Data["account"][guid1].Id, data.FirstOrDefault().Id);
            Assert.Equal(context.Data["account"][guid2].Id, data.LastOrDefault().Id);
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
            Assert.Equal(context.Data["account"][guid1].Id, data.FirstOrDefault().Id);
            Assert.Equal(context.Data["contact"][guid3].Id, data.LastOrDefault().Id);

        }


        [Fact]
        public void When_initializing_the_context_with_an_entity_it_should_have_default_createdon_createdby_modifiedon_and_modifiedby_attributes()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var e = new Entity("account") { Id = Guid.NewGuid() };
            context.Initialize(new List<Entity>() { e });

            var createdEntity = context.CreateQuery("account").FirstOrDefault();
            Assert.True(createdEntity.Attributes.ContainsKey("createdon"));
            Assert.True(createdEntity.Attributes.ContainsKey("modifiedon"));
            Assert.True(createdEntity.Attributes.ContainsKey("createdby"));
            Assert.True(createdEntity.Attributes.ContainsKey("modifiedby"));
        }

        [Fact]
        public void When_updating_an_entity_Modified_On_Should_Also_Be_Updated()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var e = new Entity("account") { Id = Guid.NewGuid() };
            context.Initialize(new List<Entity>() { e });

            var oldModifiedOn = context.CreateQuery<Account>()
                                        .FirstOrDefault()
                                        .ModifiedOn;

            Thread.Sleep(1000);

            service.Update(e);
            var newModifiedOn = context.CreateQuery<Account>()
                                        .FirstOrDefault()
                                        .ModifiedOn;

            Assert.NotEqual(oldModifiedOn, newModifiedOn);
        }

        [Fact]
        public void When_using_typed_entities_ProxyTypesAssembly_is_not_mandatory()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var c = new Contact() { Id = Guid.NewGuid(), FirstName = "Jordi" };
            context.Initialize(new List<Entity>() { c });

            //Linq 2 Query Expression
            using (var ctx = new XrmServiceContext(service))
            {
                var contacts = (from con in ctx.CreateQuery<Contact>()
                                select con).ToList();

                Assert.Equal(contacts.Count, 1);
            }

            //Query faked context directly
            var ex = Record.Exception(() => context.CreateQuery<Contact>());
            Assert.Null(ex);

        }

        [Fact]
        public void When_initializing_the_entities_a_proxy_types_assembly_is_not_mandatory()
        {
            //This will make tests much more simple as we won't need to specificy the ProxyTypesAssembly every single time if 
            //we use early bound entities

            var assembly = Assembly.GetAssembly(typeof(Contact));

            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var c = new Contact() { Id = Guid.NewGuid(), FirstName = "Jordi" };
            context.Initialize(new List<Entity>() { c });

            Assert.Equal(assembly, context.ProxyTypesAssembly);
        }

        [Fact]
        public void When_using_proxy_types_entity_names_are_validated()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var c = new Contact() { Id = Guid.NewGuid(), FirstName = "Jordi" };
            context.Initialize(new List<Entity>() { c });

            Assert.Throws<Exception>(() => service.Create(new Entity("thisDoesntExist")));
        }

        [Fact]
        public void When_initialising_the_context_once_exception_is_not_thrown()
        {
            var context = new XrmFakedContext();
            var c = new Contact() { Id = Guid.NewGuid(), FirstName = "Lionel" };
            var ex = Record.Exception(() => context.Initialize(new List<Entity>() { c }));
            Assert.Null(ex);
        }

        [Fact]
        public void When_initialising_the_context_twice_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            var c = new Contact() { Id = Guid.NewGuid(), FirstName = "Lionel" };
            var ex = Record.Exception(() => context.Initialize(new List<Entity>() { c }));
            Assert.Null(ex);
            Assert.Throws<Exception>(() => context.Initialize(new List<Entity>() { c }));
        }

        [Fact]
        public void When_getting_a_fake_service_reference_it_uses_a_singleton_pattern()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();
            var service2 = context.GetOrganizationService();

            Assert.Equal(service, service2);
        }

        [Fact]
        public void When_enabling_proxy_types_exception_is_not_be_thrown() {

            var assembly = typeof(Crm.Account).Assembly;

            var context = new XrmFakedContext();
            var ex = Record.Exception(() => context.EnableProxyTypes(assembly));
            Assert.Null(ex);
        }

        [Fact]
        public void When_enabling_proxy_types_twice_for_same_assembly_an_exception_is_thrown() {

            var assembly = typeof(Crm.Account).Assembly;

            var context = new XrmFakedContext();
            context.EnableProxyTypes(assembly);
            Assert.Throws<InvalidOperationException>(() => context.EnableProxyTypes(assembly));
        }

        [Fact]
        public void When_initialising_the_context_after_enabling_proxy_types_exception_is_not_thrown()
        {
            var assembly = typeof(Crm.Account).Assembly;

            var context = new XrmFakedContext();
            context.EnableProxyTypes(assembly);
            var c = new Contact() { Id = Guid.NewGuid(), FirstName = "Lionel" };
            var ex = Record.Exception(() => context.Initialize(new List<Entity>() { c }));
            Assert.Null(ex);
        }

    }
}
