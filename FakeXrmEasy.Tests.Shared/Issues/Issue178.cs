﻿using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue178
    {
        [Fact]
        public void Reproduce_issue_178()
        {

            // ARRANGE
            IOrganizationService fakedService = Arrange();
            using (var ctx = new XrmServiceContext(fakedService))
            {
                // ACT
                var contact = ctx.ContactSet.First();
                ctx.LoadProperty(contact, "gbp_gbp_customaddress_contact");   // THis will trigger the RetrieveRelationshipRequest

                // ASSERT
                Assert.NotNull(contact.gbp_gbp_customaddress_contact);
            }
        }

        private static IOrganizationService Arrange()
        {
            Account account = new Account();
            account.Id = Guid.NewGuid();
            account.Name = "Goggle ltd";

            Contact contact = new Contact();
            contact.Id = new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc");
            contact.FirstName = "Test";
            contact.LastName = "Contact 1";
            contact.ParentCustomerId = account.ToEntityReference();

            gbp_globecountry country = new gbp_globecountry()
            {
                Id = Guid.NewGuid(),
                gbp_name = "United Kingdom",
                gbp_code = "GB"
            };

            gbp_customaddress customAddress = new gbp_customaddress()
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                gbp_addresstype = new OptionSetValue(3),
                gbp_country = country.ToEntityReference(),
            };
            var ugh = new gbp_gbp_customaddress_contact() { Id = new Guid("12345678-0000-0000-0000-000000000000") };
            ugh.Attributes["contactid"] = contact.Id;
            ugh.Attributes["gbp_customaddressid"] = customAddress.Id;

            var context = new XrmFakedContext();
            context.AddRelationship("gbp_gbp_customaddress_contact",
                new XrmFakedRelationship()
                {
                    IntersectEntity = "gbp_gbp_customaddress_contact",
                    Entity1LogicalName = gbp_customaddress.EntityLogicalName,
                    Entity1Attribute = "gbp_customaddressid",
                    Entity2LogicalName = Contact.EntityLogicalName,
                    Entity2Attribute = "contactid"
                });

            context.Initialize(new List<Entity>()
            {
                contact, customAddress, ugh
            });

            var fakedService = context.GetOrganizationService();
            return fakedService;
        }
    }
}

namespace Crm
{
    partial class Contact
    {
        /// <summary>
		/// N:N gbp_gbp_customaddress_contact
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("gbp_gbp_customaddress_contact")]
        public System.Collections.Generic.IEnumerable<gbp_customaddress> gbp_gbp_customaddress_contact
        {
            get
            {
                return this.GetRelatedEntities<gbp_customaddress>("gbp_gbp_customaddress_contact", null);
            }
            set
            {
                this.OnPropertyChanging("gbp_gbp_customaddress_contact");
                this.SetRelatedEntities<gbp_customaddress>("gbp_gbp_customaddress_contact", null, value);
                this.OnPropertyChanged("gbp_gbp_customaddress_contact");
            }
        }
    }


}