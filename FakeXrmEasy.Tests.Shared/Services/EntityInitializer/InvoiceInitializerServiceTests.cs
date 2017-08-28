using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FakeXrmEasy.Services;

namespace FakeXrmEasy.Tests.Services.EntityInitializer
{
    public class InvoiceInitializerServiceTests
    {
        [Fact]
        public void TestPopulateFields()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            initialEntities.Add(invoice);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve("invoice", invoice.Id, new ColumnSet(true));
            Assert.NotNull(testPostCreate["invoicenumber"]);
        }

        [Fact]
        public void When_InvoiceNumberSet_DoesNot_Overridde_It()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            invoice["invoicenumber"] = "TEST";
            initialEntities.Add(invoice);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve("invoice", invoice.Id, new ColumnSet(true));
            Assert.NotNull(testPostCreate["invoicenumber"]);
            Assert.Equal("TEST", testPostCreate["invoicenumber"]);
        }
    }
}
