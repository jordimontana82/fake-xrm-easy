using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeXrmEasy.Services
{
    public class InvoiceDetailInitializerService : IEntityInitializerService
    {
        public const string EntityLogicalName = "invoicedetail";

        public Entity Initialize(Entity e, Guid gCallerId, XrmFakedContext ctx, bool isManyToManyRelationshipEntity = false)
        {
            Entity invoice = null;

            var invoiceReference = e.GetAttributeValue<EntityReference>("invoiceid");
            if (invoiceReference != null)
            {
                invoice = ctx.Service.Retrieve(invoiceReference.LogicalName, invoiceReference.Id, new ColumnSet(true));
            }

            var isPriceOverriden = e.GetAttributeValue<bool>("ispriceoverridden");
            //if Pricing set to "Use Default"
            if (!isPriceOverriden)
            {
                //retrieve price per unit from product price list item if it's not set in the invoice
                Entity product = null;
                var productReference = e.GetAttributeValue<EntityReference>("productid");

                if (productReference != null)
                    product = ctx.Service.Retrieve(productReference.LogicalName, productReference.Id, new ColumnSet(true));

                Entity productOrInvoice = invoice;
                if (productOrInvoice == null)
                    productOrInvoice = product;

                EntityReference priceLevelReference = null;

                //get price level
                if (productOrInvoice != null)
                    priceLevelReference = productOrInvoice.GetAttributeValue<EntityReference>("pricelevelid");

                //get UoM
                var uomReference = e.GetAttributeValue<EntityReference>("uomid");
                if (uomReference == null && product != null)
                {
                    uomReference = product.GetAttributeValue<EntityReference>("defaultuomscheduleid");
                    if (uomReference == null)
                        uomReference = product.GetAttributeValue<EntityReference>("defaultuomid");

                    //set uom to invoice detail
                    if (uomReference != null)
                        e["uomid"] = uomReference;
                }

                if (priceLevelReference != null && productReference != null && uomReference != null)
                {
                    var queryByAttribute = new QueryByAttribute
                    {
                        ColumnSet = new ColumnSet(true),
                        EntityName = "productpricelevel"
                    };

                    queryByAttribute.AddAttributeValue("pricelevelid", priceLevelReference.Id);
                    queryByAttribute.AddAttributeValue("productid", productReference.Id);
                    queryByAttribute.AddAttributeValue("uomid", uomReference.Id);

                    var result = ctx.Service.RetrieveMultiple(queryByAttribute);
                    if (result.Entities.Count > 0)
                    {
                        e["priceperunit"] = result.Entities[0].GetAttributeValue<Money>("amount");
                    }
                }
            }

            if (!e.Contains("priceperunit") || (e.Contains("priceperunit") && e["priceperunit"] == null))
            {
                e["priceperunit"] = new Money(0m);
            }

            //calculate other amounts
            var quantity = e.GetAttributeValue<decimal>("quantity");
            if (quantity <= 0m)
            {
                quantity = 1m;
                e["quantity"] = quantity;
            }

            var pricePerUnit = e.GetAttributeValue<Money>("priceperunit");

            decimal extendedAmount = Math.Round(pricePerUnit.Value * quantity, 2);

            e["amount"] = new Money(extendedAmount);

            var manualDiscount = e.GetAttributeValue<Money>("manualdiscountamount");
            if (manualDiscount != null)
                extendedAmount -= manualDiscount.Value;

            var tax = e.GetAttributeValue<Money>("tax");
            if (tax != null)
                extendedAmount += tax.Value;

            e["extendedamount"] = new Money(extendedAmount);

            if (invoice != null)
            {
                var totalAmount = invoice.GetAttributeValue<Money>("totalamount") ?? new Money(0m);
                totalAmount.Value += ((Money)e["extendedamount"]).Value;

                invoice["totalamount"] = totalAmount;
                ctx.Service.Update(invoice);
            }

            return e;
        }

        public Entity Initialize(Entity e, XrmFakedContext ctx, bool isManyToManyRelationshipEntity = false)
        {
            return this.Initialize(e, Guid.NewGuid(), ctx, isManyToManyRelationshipEntity);
        }
    }
}
