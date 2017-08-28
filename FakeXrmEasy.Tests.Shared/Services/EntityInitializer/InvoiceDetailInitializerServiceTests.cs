using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FakeItEasy;
using FakeXrmEasy.Services;


namespace FakeXrmEasy.Tests.Services.EntityInitializer
{
    public class InvoiceDetailInitializerServiceTests
    {
        [Fact]
        public void When_using_default_entity_initialization_level_invoice_detail_init_service_is_not_called()
        {
            XrmFakedContext context = new XrmFakedContext(); //By default it is using the default setting
            IOrganizationService service = context.GetOrganizationService();
            var fakeService = A.Fake<IEntityInitializerService>();
            var overridenDefaultInitializer = new DefaultEntityInitializerService();
            overridenDefaultInitializer.InitializerServiceDictionary["invoicedetail"] = fakeService;
            context.EntityInitializerService = overridenDefaultInitializer;

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            context.Initialize(invoiceDetail);

            A.CallTo(() => fakeService.Initialize(A<Entity>._, A<Guid>._, A<XrmFakedContext>._, A<bool>._)).MustNotHaveHappened();
        }
        [Fact]
        public void No_Invoice_And_Price_Overriden_Is_False_Result_0()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(0m), testPostCreate["priceperunit"]);
            Assert.Equal(1m, testPostCreate["quantity"]);
            Assert.Equal(new Money(0m), testPostCreate["amount"]);
            Assert.Equal(new Money(0m), testPostCreate["extendedamount"]);
        }

        [Fact]
        public void Invoice_And_Price_Overriden_Is_False_Result_0()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            initialEntities.Add(invoice);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(0m), testPostCreate["priceperunit"]);
            Assert.Equal(1m, testPostCreate["quantity"]);
            Assert.Equal(new Money(0m), testPostCreate["amount"]);
            Assert.Equal(new Money(0m), testPostCreate["extendedamount"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(0m), testPostInvoiceCreate["totalamount"]);
        }

        [Fact]
        public void Invoice_Product_And_Price_Overriden_Is_False_Result_0()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            initialEntities.Add(invoice);

            Entity product = new Entity("product");
            product.Id = Guid.NewGuid();
            initialEntities.Add(product);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            invoiceDetail["productid"] = product.ToEntityReference();
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(0m), testPostCreate["priceperunit"]);
            Assert.Equal(1m, testPostCreate["quantity"]);
            Assert.Equal(new Money(0m), testPostCreate["amount"]);
            Assert.Equal(new Money(0m), testPostCreate["extendedamount"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(0m), testPostInvoiceCreate["totalamount"]);
        }

        [Fact]
        public void Invoice_Product_Pricelist_And_Price_Overriden_Is_False_Result_0()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity priceLevel = new Entity("pricelevel");
            priceLevel.Id = Guid.NewGuid();
            priceLevel["amount"] = new Money(10m);
            initialEntities.Add(priceLevel);

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            invoice["pricelevelid"] = priceLevel.ToEntityReference();
            initialEntities.Add(invoice);

            Entity product = new Entity("product");
            product.Id = Guid.NewGuid();
            initialEntities.Add(product);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            invoiceDetail["productid"] = product.ToEntityReference();
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(0m), testPostCreate["priceperunit"]);
            Assert.Equal(1m, testPostCreate["quantity"]);
            Assert.Equal(new Money(0m), testPostCreate["amount"]);
            Assert.Equal(new Money(0m), testPostCreate["extendedamount"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(0m), testPostInvoiceCreate["totalamount"]);
        }

        [Fact]
        public void Invoice_Product_Pricelist_UoM_And_Price_Overriden_Is_False_Result_0()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity uom = new Entity("unitofmeasure");
            uom.Id = Guid.NewGuid();
            initialEntities.Add(uom);

            Entity priceLevel = new Entity("pricelevel");
            priceLevel.Id = Guid.NewGuid();
            priceLevel["amount"] = new Money(10m);
            initialEntities.Add(priceLevel);

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            invoice["pricelevelid"] = priceLevel.ToEntityReference();
            initialEntities.Add(invoice);

            Entity product = new Entity("product");
            product.Id = Guid.NewGuid();
            initialEntities.Add(product);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            invoiceDetail["productid"] = product.ToEntityReference();
            invoiceDetail["uomid"] = uom.ToEntityReference();
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(0m), testPostCreate["priceperunit"]);
            Assert.Equal(1m, testPostCreate["quantity"]);
            Assert.Equal(new Money(0m), testPostCreate["amount"]);
            Assert.Equal(new Money(0m), testPostCreate["extendedamount"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(0m), testPostInvoiceCreate["totalamount"]);
        }

        [Fact]
        public void Invoice_Product_Pricelist_UoM_And_Price_Overriden_Is_False_Price_Is_In_ProductPriceLevel()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity uom = new Entity("unitofmeasure");
            uom.Id = Guid.NewGuid();
            initialEntities.Add(uom);

            Entity priceLevel = new Entity("pricelevel");
            priceLevel.Id = Guid.NewGuid();
            priceLevel["amount"] = new Money(10m);
            initialEntities.Add(priceLevel);

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            invoice["pricelevelid"] = priceLevel.ToEntityReference();
            initialEntities.Add(invoice);

            Entity product = new Entity("product");
            product.Id = Guid.NewGuid();
            initialEntities.Add(product);

            Entity productPriceLevel = new Entity("productpricelevel");
            productPriceLevel.Id = Guid.NewGuid();
            productPriceLevel["amount"] = new Money(10m);
            productPriceLevel["pricelevelid"] = priceLevel.ToEntityReference();
            productPriceLevel["productid"] = product.ToEntityReference();
            productPriceLevel["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel);

            //another productpricelevel to ensure that the request takes the correct one
            Entity productPriceLevel2 = new Entity("productpricelevel");
            productPriceLevel2.Id = Guid.NewGuid();
            productPriceLevel2["amount"] = new Money(15m);
            productPriceLevel2["pricelevelid"] = new EntityReference("pricelevel", Guid.NewGuid());
            productPriceLevel2["productid"] = product.ToEntityReference();
            productPriceLevel2["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel2);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            invoiceDetail["productid"] = product.ToEntityReference();
            invoiceDetail["uomid"] = uom.ToEntityReference();
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostCreate["priceperunit"]);
            Assert.Equal(1m, testPostCreate["quantity"]);
            Assert.Equal(new Money(10m), testPostCreate["amount"]);
            Assert.Equal(new Money(10m), testPostCreate["extendedamount"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostInvoiceCreate["totalamount"]);
        }

        [Fact]
        public void Invoice_Product_Pricelist_And_Price_Overriden_Is_False_Price_Is_In_ProductPriceLevel_UoM_In_Product()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity uom = new Entity("unitofmeasure");
            uom.Id = Guid.NewGuid();
            initialEntities.Add(uom);

            Entity priceLevel = new Entity("pricelevel");
            priceLevel.Id = Guid.NewGuid();
            priceLevel["amount"] = new Money(10m);
            initialEntities.Add(priceLevel);

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            invoice["pricelevelid"] = priceLevel.ToEntityReference();
            initialEntities.Add(invoice);

            Entity product = new Entity("product");
            product.Id = Guid.NewGuid();
            product["defaultuomid"] = uom.ToEntityReference();
            initialEntities.Add(product);

            Entity productPriceLevel = new Entity("productpricelevel");
            productPriceLevel.Id = Guid.NewGuid();
            productPriceLevel["amount"] = new Money(10m);
            productPriceLevel["pricelevelid"] = priceLevel.ToEntityReference();
            productPriceLevel["productid"] = product.ToEntityReference();
            productPriceLevel["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel);

            //another productpricelevel to ensure that the request takes the correct one
            Entity productPriceLevel2 = new Entity("productpricelevel");
            productPriceLevel2.Id = Guid.NewGuid();
            productPriceLevel2["amount"] = new Money(15m);
            productPriceLevel2["pricelevelid"] = new EntityReference("pricelevel", Guid.NewGuid());
            productPriceLevel2["productid"] = product.ToEntityReference();
            productPriceLevel2["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel2);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            invoiceDetail["productid"] = product.ToEntityReference();
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostCreate["priceperunit"]);
            Assert.Equal(1m, testPostCreate["quantity"]);
            Assert.Equal(new Money(10m), testPostCreate["amount"]);
            Assert.Equal(new Money(10m), testPostCreate["extendedamount"]);
            Assert.Equal(uom.ToEntityReference(), testPostCreate["uomid"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostInvoiceCreate["totalamount"]);
        }

        [Fact]
        public void Invoice_Product_Pricelist_And_Price_Overriden_Is_False_Price_Is_In_ProductPriceLevel_UoM_In_Product_2()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity uom = new Entity("unitofmeasure");
            uom.Id = Guid.NewGuid();
            initialEntities.Add(uom);

            Entity priceLevel = new Entity("pricelevel");
            priceLevel.Id = Guid.NewGuid();
            priceLevel["amount"] = new Money(10m);
            initialEntities.Add(priceLevel);

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            invoice["pricelevelid"] = priceLevel.ToEntityReference();
            initialEntities.Add(invoice);

            Entity product = new Entity("product");
            product.Id = Guid.NewGuid();
            product["defaultuomscheduleid"] = uom.ToEntityReference();
            initialEntities.Add(product);

            Entity productPriceLevel = new Entity("productpricelevel");
            productPriceLevel.Id = Guid.NewGuid();
            productPriceLevel["amount"] = new Money(10m);
            productPriceLevel["pricelevelid"] = priceLevel.ToEntityReference();
            productPriceLevel["productid"] = product.ToEntityReference();
            productPriceLevel["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel);

            //another productpricelevel to ensure that the request takes the correct one
            Entity productPriceLevel2 = new Entity("productpricelevel");
            productPriceLevel2.Id = Guid.NewGuid();
            productPriceLevel2["amount"] = new Money(15m);
            productPriceLevel2["pricelevelid"] = new EntityReference("pricelevel", Guid.NewGuid());
            productPriceLevel2["productid"] = product.ToEntityReference();
            productPriceLevel2["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel2);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            invoiceDetail["productid"] = product.ToEntityReference();
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostCreate["priceperunit"]);
            Assert.Equal(1m, testPostCreate["quantity"]);
            Assert.Equal(new Money(10m), testPostCreate["amount"]);
            Assert.Equal(new Money(10m), testPostCreate["extendedamount"]);
            Assert.Equal(uom.ToEntityReference(), testPostCreate["uomid"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostInvoiceCreate["totalamount"]);
        }

        [Fact]
        public void Invoice_Product_Pricelist_UoM_And_Price_Overriden_Is_False_Price_Is_In_ProductPriceLevel_Quantity_3()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity uom = new Entity("unitofmeasure");
            uom.Id = Guid.NewGuid();
            initialEntities.Add(uom);

            Entity priceLevel = new Entity("pricelevel");
            priceLevel.Id = Guid.NewGuid();
            priceLevel["amount"] = new Money(10m);
            initialEntities.Add(priceLevel);

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            invoice["pricelevelid"] = priceLevel.ToEntityReference();
            initialEntities.Add(invoice);

            Entity product = new Entity("product");
            product.Id = Guid.NewGuid();
            initialEntities.Add(product);

            Entity productPriceLevel = new Entity("productpricelevel");
            productPriceLevel.Id = Guid.NewGuid();
            productPriceLevel["amount"] = new Money(10m);
            productPriceLevel["pricelevelid"] = priceLevel.ToEntityReference();
            productPriceLevel["productid"] = product.ToEntityReference();
            productPriceLevel["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel);

            //another productpricelevel to ensure that the request takes the correct one
            Entity productPriceLevel2 = new Entity("productpricelevel");
            productPriceLevel2.Id = Guid.NewGuid();
            productPriceLevel2["amount"] = new Money(15m);
            productPriceLevel2["pricelevelid"] = new EntityReference("pricelevel", Guid.NewGuid());
            productPriceLevel2["productid"] = product.ToEntityReference();
            productPriceLevel2["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel2);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            invoiceDetail["productid"] = product.ToEntityReference();
            invoiceDetail["uomid"] = uom.ToEntityReference();
            invoiceDetail["quantity"] = 3m;
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostCreate["priceperunit"]);
            Assert.Equal(3m, testPostCreate["quantity"]);
            Assert.Equal(new Money(3m * 10m), testPostCreate["amount"]);
            Assert.Equal(new Money(3m * 10m), testPostCreate["extendedamount"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(3m * 10m), testPostInvoiceCreate["totalamount"]);
        }

        [Fact]
        public void Invoice_Product_Pricelist_UoM_And_Price_Overriden_Is_False_Price_Is_In_ProductPriceLevel_Quantity_3_ManualDiscount_Tax()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity uom = new Entity("unitofmeasure");
            uom.Id = Guid.NewGuid();
            initialEntities.Add(uom);

            Entity priceLevel = new Entity("pricelevel");
            priceLevel.Id = Guid.NewGuid();
            priceLevel["amount"] = new Money(10m);
            initialEntities.Add(priceLevel);

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            invoice["pricelevelid"] = priceLevel.ToEntityReference();
            initialEntities.Add(invoice);

            Entity product = new Entity("product");
            product.Id = Guid.NewGuid();
            initialEntities.Add(product);

            Entity productPriceLevel = new Entity("productpricelevel");
            productPriceLevel.Id = Guid.NewGuid();
            productPriceLevel["amount"] = new Money(10m);
            productPriceLevel["pricelevelid"] = priceLevel.ToEntityReference();
            productPriceLevel["productid"] = product.ToEntityReference();
            productPriceLevel["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel);

            //another productpricelevel to ensure that the request takes the correct one
            Entity productPriceLevel2 = new Entity("productpricelevel");
            productPriceLevel2.Id = Guid.NewGuid();
            productPriceLevel2["amount"] = new Money(15m);
            productPriceLevel2["pricelevelid"] = new EntityReference("pricelevel", Guid.NewGuid());
            productPriceLevel2["productid"] = product.ToEntityReference();
            productPriceLevel2["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel2);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            invoiceDetail["productid"] = product.ToEntityReference();
            invoiceDetail["uomid"] = uom.ToEntityReference();
            invoiceDetail["quantity"] = 3m;
            invoiceDetail["manualdiscountamount"] = new Money(12m);
            invoiceDetail["tax"] = new Money(3m);
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostCreate["priceperunit"]);
            Assert.Equal(3m, testPostCreate["quantity"]);
            Assert.Equal(new Money(3m * 10m), testPostCreate["amount"]);
            Assert.Equal(new Money(3m * 10m - 12m + 3m), testPostCreate["extendedamount"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(3m * 10m - 12m + 3m), testPostInvoiceCreate["totalamount"]);
        }

        [Fact]
        public void Invoice_Product_Pricelist_UoM_And_Price_Overriden_Is_False_Price_Is_In_ProductPriceLevel_Quantity_3_AddsAmountToInvoice()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity uom = new Entity("unitofmeasure");
            uom.Id = Guid.NewGuid();
            initialEntities.Add(uom);

            Entity priceLevel = new Entity("pricelevel");
            priceLevel.Id = Guid.NewGuid();
            priceLevel["amount"] = new Money(10m);
            initialEntities.Add(priceLevel);

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            invoice["pricelevelid"] = priceLevel.ToEntityReference();
            invoice["totalamount"] = new Money(40m);
            initialEntities.Add(invoice);

            Entity product = new Entity("product");
            product.Id = Guid.NewGuid();
            initialEntities.Add(product);

            Entity productPriceLevel = new Entity("productpricelevel");
            productPriceLevel.Id = Guid.NewGuid();
            productPriceLevel["amount"] = new Money(10m);
            productPriceLevel["pricelevelid"] = priceLevel.ToEntityReference();
            productPriceLevel["productid"] = product.ToEntityReference();
            productPriceLevel["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel);

            //another productpricelevel to ensure that the request takes the correct one
            Entity productPriceLevel2 = new Entity("productpricelevel");
            productPriceLevel2.Id = Guid.NewGuid();
            productPriceLevel2["amount"] = new Money(15m);
            productPriceLevel2["pricelevelid"] = new EntityReference("pricelevel", Guid.NewGuid());
            productPriceLevel2["productid"] = product.ToEntityReference();
            productPriceLevel2["uomid"] = uom.ToEntityReference();
            initialEntities.Add(productPriceLevel2);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = false;
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            invoiceDetail["productid"] = product.ToEntityReference();
            invoiceDetail["uomid"] = uom.ToEntityReference();
            invoiceDetail["quantity"] = 3m;
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostCreate["priceperunit"]);
            Assert.Equal(3m, testPostCreate["quantity"]);
            Assert.Equal(new Money(3m * 10m), testPostCreate["amount"]);
            Assert.Equal(new Money(3m * 10m), testPostCreate["extendedamount"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(40m + 3m * 10m), testPostInvoiceCreate["totalamount"]);
        }

        [Fact]
        public void Invoice_And_Price_Overriden_Is_True()
        {
            var context = new XrmFakedContext() { InitializationLevel = EntityInitializationLevel.PerEntity };
            IOrganizationService service = context.GetOrganizationService();

            List<Entity> initialEntities = new List<Entity>();

            Entity invoice = new Entity("invoice");
            invoice.Id = Guid.NewGuid();
            initialEntities.Add(invoice);

            Entity invoiceDetail = new Entity("invoicedetail");
            invoiceDetail.Id = Guid.NewGuid();
            invoiceDetail["ispriceoverridden"] = true;
            invoiceDetail["priceperunit"] = new Money(10m);
            invoiceDetail["invoiceid"] = invoice.ToEntityReference();
            initialEntities.Add(invoiceDetail);

            context.Initialize(initialEntities);
            Entity testPostCreate = service.Retrieve(invoiceDetail.LogicalName, invoiceDetail.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostCreate["priceperunit"]);
            Assert.Equal(1m, testPostCreate["quantity"]);
            Assert.Equal(new Money(10m), testPostCreate["amount"]);
            Assert.Equal(new Money(10m), testPostCreate["extendedamount"]);

            Entity testPostInvoiceCreate = service.Retrieve(invoice.LogicalName, invoice.Id, new ColumnSet(true));
            Assert.Equal(new Money(10m), testPostInvoiceCreate["totalamount"]);
        }
    }
}
