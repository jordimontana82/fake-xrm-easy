using Crm;
using System;
using System.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue491
    {
        
        [Fact]
        public void AccountWithLocalDateTime_RetrieveUtcWithCorrectTimeStamp()
        {
            var dateTimeNow = new DateTime(2020, 03, 30, 0, 0, 0, DateTimeKind.Local);
            
            var context = new XrmFakedContext();

            var accountId = context.GetOrganizationService().Create(new Account
            {
                Id = Guid.NewGuid(),
                LastUsedInCampaign = dateTimeNow
            });

            var retrievedAccount = context.CreateQuery<Account>().SingleOrDefault(p => p.Id == accountId);
            
            Assert.NotNull(retrievedAccount);
            Assert.True(retrievedAccount.LastUsedInCampaign.HasValue);
            Assert.Equal(dateTimeNow, retrievedAccount.LastUsedInCampaign.Value.ToLocalTime());
        }

        [Fact]
        public void AccountWithUtcDateTime_RetrieveUtcWithCorrectTimeStamp()
        {
            var dateTimeNow = new DateTime(2020, 03, 30, 0, 0, 0, DateTimeKind.Utc);

            var context = new XrmFakedContext();

            var accountId = context.GetOrganizationService().Create(new Account
            {
                Id = Guid.NewGuid(),
                LastUsedInCampaign = dateTimeNow
            });

            var retrievedAccount = context.CreateQuery<Account>().SingleOrDefault(p => p.Id == accountId);

            Assert.NotNull(retrievedAccount);
            Assert.True(retrievedAccount.LastUsedInCampaign.HasValue);
            Assert.Equal(dateTimeNow, retrievedAccount.LastUsedInCampaign.Value.ToUniversalTime());
        }

        [Fact]
        public void AccountWithUnspecifiedDateTime_RetrieveUtcWithCorrectTimeStamp()
        {
            var dateTimeNow = new DateTime(2020, 03, 30, 0, 0, 0, DateTimeKind.Unspecified);

            var context = new XrmFakedContext();

            var accountId = context.GetOrganizationService().Create(new Account
            {
                Id = Guid.NewGuid(),
                LastUsedInCampaign = dateTimeNow
            });

            var retrievedAccount = context.CreateQuery<Account>().SingleOrDefault(p => p.Id == accountId);

            Assert.NotNull(retrievedAccount);
            Assert.True(retrievedAccount.LastUsedInCampaign.HasValue);
            Assert.Equal(dateTimeNow, retrievedAccount.LastUsedInCampaign.Value.ToLocalTime());
        }
    }
}