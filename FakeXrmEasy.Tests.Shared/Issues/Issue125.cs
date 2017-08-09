using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using Xunit;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue125
    {
        [Fact]
        public void Reproduce_issue_125()
        {
            Account account = new Account();
            account.Id = Guid.NewGuid();
            account.Name = "Goggle ltd";

            Contact contact = new Contact();
            contact.Id = Guid.NewGuid();
            contact.FirstName = "Test";
            contact.LastName = "Contact 1";
            contact.ParentCustomerId = account.ToEntityReference();

            PhoneCall phonecall = new PhoneCall();
            phonecall.Id = Guid.NewGuid();
            phonecall["statecode"] = PhoneCallState.Open;
            //phonecall.To = new List<ActivityParty>() { contact.ToEntityReference() };
            phonecall.StatusCode = new OptionSetValue(1);
            phonecall.Subject = "Test phone call";

            gbp_globecountry Country = new gbp_globecountry()
            {
                Id = Guid.NewGuid(),
                gbp_name = "United Kingdom",
                gbp_code = "GB"
            };

            gbp_customaddress customerA = new gbp_customaddress()
            {
                Id = Guid.NewGuid(),
                gbp_addresstype = new OptionSetValue(3),
                gbp_country = new EntityReference(gbp_globecountry.EntityLogicalName, Country.Id),
            };

            var context = new XrmFakedContext();
            context.AddRelationship("gbp_gbp_customaddress_contact_assosciation",
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
                contact, customerA
            });

            var fakedService = context.GetOrganizationService();

            var request = new AssociateRequest()
            {
                Target = customerA.ToEntityReference(),
                RelatedEntities = new EntityReferenceCollection()
                {
                    new EntityReference(Contact.EntityLogicalName, contact.Id),
                },
                Relationship = new Relationship("gbp_gbp_customaddress_contact_assosciation")
            };
            fakedService.Execute(request);

            string fetchQuery = string.Format(@"<fetch distinct='false' mapping='logical' output-format='xml-platform' version='1.0' >
                          <entity name='gbp_customaddress' >
                            <attribute name='gbp_country' />
                            <link-entity name='gbp_gbp_customaddress_contact' from='gbp_customaddressid' to='gbp_customaddressid' alias='NtoN' intersect='true' >
                              <link-entity name='contact' from='contactid' to='contactid' alias='Contact' >
                                <filter>
                                  <condition attribute='contactid' operator='eq' value='{0}' />
                                </filter>
                              </link-entity>
                            </link-entity>
                          </entity>
                        </fetch> ", contact.Id);

            EntityCollection result = fakedService.RetrieveMultiple(new FetchExpression(fetchQuery));
            Assert.Equal(1, result.Entities.Count);
        }
    }
}