using System;
using System.Linq;

using Xunit;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk.Query;

using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Crm;
using System.Reflection;

namespace FakeXrmEasy.Tests
{
    public class FakeContextTestCreateQuery
    {

        [Fact]
        public void After_querying_the_context_with_an_invalid_entity_name_exception_is_thrown()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var service = context.GetFakedOrganizationService();

            var guid = Guid.NewGuid();
            var data = new List<Entity>() {
                new Contact() { Id = guid }
            }.AsQueryable();

            context.Initialize(data);

            Assert.Throws<Exception>(() => {
                var query = (from c in context.CreateQuery("    ")
                             select c);
            });
        }

        [Fact]
        public void After_adding_a_contact_the_create_query_returns_it()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var service = context.GetFakedOrganizationService();

            var guid = Guid.NewGuid();
            var data = new List<Entity>() {
                new Contact() { Id = guid }
            }.AsQueryable();

            context.Initialize(data);
            
            //Find the contact
            var contact = (from c in context.CreateQuery<Contact>()
                           where c.ContactId == guid
                           select c).FirstOrDefault();

            
            Assert.False(contact == null);
            Assert.Equal(guid, contact.Id);
        }

        [Fact]
        public void Querying_an_early_bound_entity_not_present_in_the_context_should_return_no_records()
        {

            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var service = context.GetFakedOrganizationService();

            //Find the contact
            var contact = (from c in context.CreateQuery<Contact>()
                           select c).ToList();

            Assert.Equal(contact.Count, 0);
        }

        [Fact]
        public void Querying_a_dynamic_entity_not_present_in_the_context_should_return_no_records()
        {
            var context = new XrmFakedContext();

            var service = context.GetFakedOrganizationService();

            //Find the contact
            var contact = (from c in context.CreateQuery("contact")
                           select c).ToList();

            Assert.Equal(contact.Count, 0);
        }

        [Fact]
        public void Querying_a_dynamic_using_type_should_use_the_entity_entity_logical_name_attribute()
        {
            var context = new XrmFakedContext();

            var service = context.GetFakedOrganizationService();

            //Find the contact
            var contact = (from c in context.CreateQuery("contact")
                           select c).ToList();

            Assert.Equal(contact.Count, 0);
        }

    }
}
