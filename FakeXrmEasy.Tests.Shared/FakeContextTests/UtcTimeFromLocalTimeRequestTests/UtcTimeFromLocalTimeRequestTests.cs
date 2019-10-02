using System;
using Microsoft.Crm.Sdk.Messages;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.UtcTimeFromLocalTimeRequestTests
{
    public class UtcTimeFromLocalTimeRequestTests
    {
        [Fact]
        public static void UtcTimeResponse_GivesCorrectResponse_ForSAPacificTime()
        {
            const string localTime = "2019-10-02 12:00:00 -05:00";
            const string utcTime = "2019-10-02 17:00:00";
            const int crmTimeZoneCode = 45; // SA Pacific Time
            
            var returnedUtcTime = GetUtcTimeResponse(localTime, crmTimeZoneCode);
            var expectedUtcDateTime = DateTime.SpecifyKind(DateTime.Parse(utcTime), DateTimeKind.Utc);
            Assert.Equal(expectedUtcDateTime, returnedUtcTime);
        }
        
        [Fact]
        public static void UtcTimeResponse_GivesCorrectResponse_ForBST()
        {
            const string localTime = "2019-10-02 12:00:00";
            const string utcTime = "2019-10-02 11:00:00";
            const int crmTimeZoneCode = 90; // Greenwich Time (date in summer time)
            
            var returnedUtcTime = GetUtcTimeResponse(localTime, crmTimeZoneCode);
            var expectedUtcDateTime = DateTime.SpecifyKind(DateTime.Parse(utcTime), DateTimeKind.Utc);
            Assert.Equal(expectedUtcDateTime, returnedUtcTime);
        }
        
        [Fact]
        public static void UtcTimeResponse_GivesCorrectResponse_ForUTC()
        {
            const string localTime = "2019-11-02 12:00:00";
            const string utcTime = "2019-11-02 12:00:00";
            const int crmTimeZoneCode = 90; // Greenwich Time (date not in summer time)
            
            var returnedUtcTime = GetUtcTimeResponse(localTime, crmTimeZoneCode);
            var expectedUtcDateTime = DateTime.SpecifyKind(DateTime.Parse(utcTime), DateTimeKind.Utc);
            Assert.Equal(expectedUtcDateTime, returnedUtcTime);
        }
        
        [Fact]
        public static void UtcTimeResponse_GivesCorrectResponse_ForCET()
        {
            const string localTime = "2019-11-02 12:00:00 +01:00";
            const string utcTime = "2019-11-02 11:00:00";
            const int crmTimeZoneCode = 95; // Central European Time
            
            var returnedUtcTime = GetUtcTimeResponse(localTime, crmTimeZoneCode);
            var expectedUtcDateTime = DateTime.SpecifyKind(DateTime.Parse(utcTime), DateTimeKind.Utc);
            Assert.Equal(expectedUtcDateTime, returnedUtcTime);
        }
        
        [Fact]
        public static void UtcTimeResponse_GivesCorrectResponse_ForCentralAustralianTime()
        {
            const string localTime = "2019-11-02 12:00:00 +09:30";
            const string utcTime = "2019-11-02 02:30:00";
            const int crmTimeZoneCode = 245; // Central Australian Time
            
            var returnedUtcTime = GetUtcTimeResponse(localTime, crmTimeZoneCode);
            var expectedUtcDateTime = DateTime.SpecifyKind(DateTime.Parse(utcTime), DateTimeKind.Utc);
            Assert.Equal(expectedUtcDateTime, returnedUtcTime);
        }

        private static DateTime GetUtcTimeResponse(string time, int crmTimeZoneCode)
        {
            var context = new XrmFakedContext();
            var organizationService = context.GetOrganizationService();
            
            var localTime = DateTime.SpecifyKind(DateTime.Parse(time), DateTimeKind.Local);
            var request = new UtcTimeFromLocalTimeRequest
            {
                LocalTime = localTime,
                TimeZoneCode = crmTimeZoneCode
            };

            var response = organizationService.Execute(request) as UtcTimeFromLocalTimeResponse;
            return response.UtcTime;
        }
    }
}