using System;
using System.ServiceModel;
using Crm;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.RetrieveExchangeRateRequestTests
{
    public class RetrieveExchangeRateRequestTests
    {
        [Fact]
        public void Throws_Fault_Exception_When_CurrencyId_Not_On_Request()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var exchangeRateRequest = new RetrieveExchangeRateRequest();

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(exchangeRateRequest));
        }
        
        [Fact]
        public void Throws_Fault_Exception_When_CurrencyId_Does_Not_Exist()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var exchangeRateRequest = new RetrieveExchangeRateRequest
            {
                TransactionCurrencyId = Guid.NewGuid()
            };

            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Execute(exchangeRateRequest));
        }
        
        [Fact]
        public void Returns_Response_With_Stored_Exchange_Rate_From_Specified_Currency()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var currencyOne = new TransactionCurrency
            {
                ExchangeRate = (decimal) 0.9
            };
            var selectedCurrency = new TransactionCurrency
            {
                ExchangeRate = (decimal) 4.2
            };
            var currencyThree = new TransactionCurrency
            {
                ExchangeRate = 2
            };
            service.Create(currencyOne);
            var currencyId = service.Create(selectedCurrency);
            service.Create(currencyThree);

            var exchangeRateRequest = new RetrieveExchangeRateRequest
            {
                TransactionCurrencyId = currencyId
            };

            var response = service.Execute(exchangeRateRequest) as RetrieveExchangeRateResponse;
            
            Assert.NotNull(response);
            Assert.Equal(selectedCurrency.ExchangeRate, response.ExchangeRate);
        }
    }
}