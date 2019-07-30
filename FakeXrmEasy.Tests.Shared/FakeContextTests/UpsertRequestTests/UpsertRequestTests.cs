using Crm;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.UpsertRequestTests
{
#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013 && !FAKE_XRM_EASY_2015
    public class UpsertRequestTests
    {
        [Fact]
        public void Upsert_Creates_Record_When_It_Does_Not_Exist()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetOrganizationService();

            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FirstName = "FakeXrm",
                LastName = "Easy"
            };

            var request = new UpsertRequest()
            {
                Target = contact
            };

            var response = (UpsertResponse)service.Execute(request);

            var contactCreated = context.CreateQuery<Contact>().FirstOrDefault();

            Assert.Equal(true, response.RecordCreated);
            Assert.NotNull(contactCreated);
        }

        [Fact]
        public void Upsert_Updates_Record_When_It_Exists()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetOrganizationService();

            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FirstName = "FakeXrm"
            };
            context.Initialize(new[] { contact });

            contact = new Contact()
            {
                Id = contact.Id,
                FirstName = "FakeXrm2",
                LastName = "Easy"
            };

            var request = new UpsertRequest()
            {
                Target = contact
            };


            var response = (UpsertResponse)service.Execute(request);
            var contactUpdated = context.CreateQuery<Contact>().FirstOrDefault();

            Assert.Equal(false, response.RecordCreated);
            Assert.Equal("FakeXrm2", contactUpdated.FirstName);
        }

        [Fact]
        public void Upsert_Creates_Record_When_It_Does_Not_Exist_Using_Alternate_Key()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            context.InitializeMetadata(Assembly.GetExecutingAssembly());
            var service = context.GetOrganizationService();

            var metadata = context.GetEntityMetadataByName("contact");
            metadata.SetFieldValue("_keys", new EntityKeyMetadata[]
            {
                new EntityKeyMetadata()
                {
                    KeyAttributes = new string[]{"firstname"}
                }
            });
            context.SetEntityMetadata(metadata);
            var contact = new Contact()
            {
                FirstName = "FakeXrm",
                LastName = "Easy"
            };
            contact.KeyAttributes.Add("firstname", contact.FirstName);

            var request = new UpsertRequest()
            {
                Target = contact
            };

            var response = (UpsertResponse)service.Execute(request);

            Assert.Equal(true, response.RecordCreated);
        }

        [Fact]
        public void Upsert_Updates_Record_When_It_Exists_Using_Alternate_Key()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            context.InitializeMetadata(Assembly.GetExecutingAssembly());
            var service = context.GetOrganizationService();


            var metadata = context.GetEntityMetadataByName("contact");
            metadata.SetFieldValue("_keys", new EntityKeyMetadata[]
            {
                new EntityKeyMetadata()
                {
                    KeyAttributes = new string[]{"firstname"}
                }
            });
            context.SetEntityMetadata(metadata);

            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FirstName = "FakeXrm",
                LastName = "Easy"
            };
            context.Initialize(new[] { contact });

            contact = new Contact()
            {
                FirstName = "FakeXrm2",
                LastName = "Easy2"
            };

            contact.KeyAttributes.Add("firstname", "FakeXrm");

            var request = new UpsertRequest()
            {
                Target = contact
            };

            var response = (UpsertResponse)service.Execute(request);

            Assert.Equal(false, response.RecordCreated);
        }
    }
#endif
}
