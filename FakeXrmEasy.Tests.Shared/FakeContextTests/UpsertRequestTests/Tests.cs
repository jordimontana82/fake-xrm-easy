using Crm;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.UpsertRequestTests
{
#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013 && !FAKE_XRM_EASY_2015
    public class Tests
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

            Assert.Equal(true, response.RecordCreated);
        }

        [Fact]
        public void Upsert_Updates_Record_When_It_Exists()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetOrganizationService();

            var contact = new Contact()
            {
                Id = Guid.NewGuid()
            };
            context.Initialize(new[] { contact });

            contact = new Contact()
            {
                Id = contact.Id,
                FirstName = "FakeXrm",
                LastName = "Easy"
            };

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
