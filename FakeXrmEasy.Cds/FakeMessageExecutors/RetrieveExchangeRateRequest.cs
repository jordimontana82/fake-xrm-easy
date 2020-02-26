﻿using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Query;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveExchangeRateRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveExchangeRateRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var retrieveExchangeRateRequest = (RetrieveExchangeRateRequest)request;

            var currencyId = retrieveExchangeRateRequest.TransactionCurrencyId;

            if (currencyId == null)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), new FaultReason("Can not retrieve Exchange Rate without Transaction Currency Guid"));
            }

            var service = ctx.GetOrganizationService();

            var result = service.RetrieveMultiple(new QueryExpression("transactioncurrency")
            {
                ColumnSet = new ColumnSet("exchangerate"),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("transactioncurrencyid", ConditionOperator.Equal, currencyId)
                    }
                }
            }).Entities;

            if (!result.Any())
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), new FaultReason("Transaction Currency not found"));
            }

            var exchangeRate = result.First().GetAttributeValue<decimal>("exchangerate");

            return new RetrieveExchangeRateResponse
            {
                Results = new ParameterCollection
                {
                    {"ExchangeRate", exchangeRate}
                }
            };
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveExchangeRateRequest);
        }
    }
}