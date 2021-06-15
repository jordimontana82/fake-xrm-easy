using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Crm;
using System.Linq;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue255
    {
        [Fact]
        public void Create_RelatedRecordIsNew()
        {
            var fake = new XrmFakedContext();
            fake.AddRelationship("account_primary_contact", new XrmFakedRelationship("contactid", "primarycontactid", Contact.EntityLogicalName, Account.EntityLogicalName));
            var fakedService = fake.GetOrganizationService();
            using (var ctx = new Crm.XrmServiceContext(fakedService))
            {
                var cntc = new Contact();
                ctx.AddObject(cntc);
                
                var accnt = new Account();
                ctx.AddRelatedObject(cntc, new Microsoft.Xrm.Sdk.Relationship("account_primary_contact"), accnt);

                ctx.SaveChanges();

                accnt = ctx.AccountSet.Where(a => a.Id == accnt.Id).FirstOrDefault();
                Assert.Equal(cntc.Id, accnt.PrimaryContactId.Id);
                
            }

        }

        [Fact]
        public void Create_RelatedRecordExists()
        {
            var fake = new XrmFakedContext();
            var accnt = new Account { Id = Guid.NewGuid()};
            fake.Initialize(accnt);
            fake.AddRelationship("account_primary_contact", new XrmFakedRelationship("contactid", "primarycontactid", Contact.EntityLogicalName, Account.EntityLogicalName));
            var fakedService = fake.GetOrganizationService();
            using (var ctx = new Crm.XrmServiceContext(fakedService))
            {
                var cntc = new Contact();
                ctx.AddObject(cntc);

                ctx.Attach(accnt);
                ctx.AddLink(cntc, new Microsoft.Xrm.Sdk.Relationship("account_primary_contact"), accnt);

                ctx.SaveChanges();

                accnt = ctx.AccountSet.Where(a => a.Id == accnt.Id).FirstOrDefault();
                Assert.Equal(cntc.Id, accnt.PrimaryContactId.Id);

            }

        }
    }
}
