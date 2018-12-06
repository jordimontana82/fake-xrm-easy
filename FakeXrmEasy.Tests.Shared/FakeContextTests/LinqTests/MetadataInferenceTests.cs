using Crm;
using FakeItEasy;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;  //TypedEntities generated code for testing
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.LinqTests
{
    public class MetadataInferenceTests
    {
        [Fact]
        public static void When_using_proxy_types_assembly_the_entity_metadata_is_inferred_from_the_proxy_types_assembly()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            //Empty contecxt (no Initialize), but we should be able to query any typed entity without an entity not found exception

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName.Equals("Anything!")
                               select c).ToList();

                Assert.True(contact.Count == 0);
            }
        }

        [Fact]
        public static void When_using_proxy_types_assembly_the_attribute_metadata_is_inferred_from_the_proxy_types_assembly()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["fullname"] = "Contact 1"; contact1["firstname"] = "First 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["fullname"] = "Contact 2"; contact2["firstname"] = "First 2";

            fakedContext.Initialize(new List<Entity>() { contact1, contact2 });

            var guid = Guid.NewGuid();

            //Empty contecxt (no Initialize), but we should be able to query any typed entity without an entity not found exception

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c.FirstName.Equals("First 1")
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public static void When_using_proxy_types_assembly_the_attribute_metadata_is_inferred_from_injected_metadata_as_a_fallback()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["injectedAttribute"] = "Contact 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["injectedAttribute"] = "Contact 2";

            fakedContext.Initialize(new List<Entity>() { contact1, contact2 });

            var contactMetadata = new EntityMetadata()
            {
                LogicalName = "contact"
            };

            var injectedAttribute = new StringAttributeMetadata()
            {
                LogicalName = "injectedAttribute"
            };

            contactMetadata.SetAttribute(injectedAttribute);
            fakedContext.InitializeMetadata(contactMetadata);

            var guid = Guid.NewGuid();

            //Empty contecxt (no Initialize), but we should be able to query any typed entity without an entity not found exception

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c["injectedAttribute"].Equals("Contact 1")
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

#if FAKE_XRM_EASY_9
        [Fact]
        public static void When_using_proxy_types_assembly_the_optionset_metadata_is_inferred_from_injected_metadata_as_a_fallback()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["injectedAttribute"] = new OptionSetValue(10001);
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["injectedAttribute"] = new OptionSetValue(10002);

            fakedContext.Initialize(new List<Entity>() { contact1, contact2 });

            var contactMetadata = new EntityMetadata()
            {
                LogicalName = "contact"
            };

            var injectedAttribute = new PicklistAttributeMetadata()
            {
                LogicalName = "injectedAttribute"
            };

            contactMetadata.SetAttribute(injectedAttribute);
            fakedContext.InitializeMetadata(contactMetadata);

            var guid = Guid.NewGuid();

            //Empty contecxt (no Initialize), but we should be able to query any typed entity without an entity not found exception

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                var contact = (from c in ctx.CreateQuery<Contact>()
                               where c["injectedAttribute"].Equals(new OptionSetValue(10002))
                               select c).ToList();

                Assert.True(contact.Count == 1);
            }
        }

        [Fact]
        public static void When_using_proxy_types_assembly_multi_select_option_set_metadata_is_inferred_from_injected_metadata_as_a_fallback()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var record1 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["new_injectedmultiselectoptionset"] = new OptionSetValueCollection(
                    new[]
                    {
                        new OptionSetValue(100001),
                        new OptionSetValue(100002)
                    })
            };

            var record2 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["new_injectedmultiselectoptionset"] = new OptionSetValueCollection(
                    new[]
                    {
                        new OptionSetValue(100002),
                        new OptionSetValue(100003)
                    })
            };

            fakedContext.Initialize(new List<Entity>() { record1, record2 });

            var entityMetadata = new EntityMetadata()
            {
                LogicalName = "contact"
            };

            var injectedAttribute = new MultiSelectPicklistAttributeMetadata()
            {
                LogicalName = "new_injectedmultiselectoptionset"
            };

            entityMetadata.SetAttribute(injectedAttribute);
            fakedContext.InitializeMetadata(entityMetadata);

            var guid = Guid.NewGuid();

            //Empty context (no Initialize), but we should be able to query any typed entity without an entity not found exception

            var service = fakedContext.GetOrganizationService();

            var contacts = service.RetrieveMultiple(new QueryExpression(Contact.EntityLogicalName)
            {
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression("new_injectedmultiselectoptionset" , ConditionOperator.In, new[] { 100002, 100003 })
                    }
                }
            });

            Assert.True(contacts.Entities.Count == 1);
        }
#endif

        [Fact]
        public static void When_using_proxy_types_assembly_the_finding_attribute_metadata_fails_if_neither_proxy_type_or_injected_metadata_exist()
        {
            var fakedContext = new XrmFakedContext();
            fakedContext.ProxyTypesAssembly = Assembly.GetExecutingAssembly();

            var contact1 = new Entity("contact") { Id = Guid.NewGuid() }; contact1["injectedAttribute"] = "Contact 1";
            var contact2 = new Entity("contact") { Id = Guid.NewGuid() }; contact2["injectedAttribute"] = "Contact 2";

            fakedContext.Initialize(new List<Entity>() { contact1, contact2 });

            var guid = Guid.NewGuid();

            var service = fakedContext.GetOrganizationService();

            using (XrmServiceContext ctx = new XrmServiceContext(service))
            {
                Assert.Throws<Exception>(() => (from c in ctx.CreateQuery<Contact>()
                               where c["injectedAttribute"].Equals("Contact 1")
                               select c).ToList());
            }
        }
    }
}