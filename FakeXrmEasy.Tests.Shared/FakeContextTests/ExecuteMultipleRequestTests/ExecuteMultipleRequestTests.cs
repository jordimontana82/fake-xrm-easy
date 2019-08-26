using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using System.ServiceModel;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.ExecuteMultipleRequestTests
{
    public class ExecuteMultipleRequestTests
    {
        [Fact]
        public static void Should_Execute_Subsequent_Requests()
        {
            var context = new XrmFakedContext();

            var service = context.GetOrganizationService();

            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc2"
            };

            var executeMultipleRequest = new ExecuteMultipleRequest
            {
                Settings = new ExecuteMultipleSettings
                {
                    ReturnResponses = true
                },
                Requests = new OrganizationRequestCollection
                {
                    new CreateRequest
                    {
                        Target = account1
                    },

                    new CreateRequest
                    {
                        Target = account2
                    }
                }
            };

            var response = service.Execute(executeMultipleRequest) as ExecuteMultipleResponse;

            Assert.False(response.IsFaulted);
            Assert.NotEmpty(response.Responses);

            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account1.Id, new ColumnSet(true)));
            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account2.Id, new ColumnSet(true)));
        }

        [Fact]
        public static void Should_Execute_Subsequent_Requests_In_Order()
        {
            var context = new XrmFakedContext();

            var service = context.GetOrganizationService();

            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc2"
            };

            var executeMultipleRequest = new ExecuteMultipleRequest
            {
                Settings = new ExecuteMultipleSettings
                {
                    ReturnResponses = true
                },
                Requests = new OrganizationRequestCollection
                {
                    new CreateRequest
                    {
                        Target = account1
                    },

                    new CreateRequest
                    {
                        Target = account2
                    },

                    new UpdateRequest
                    {
                        Target = new Account
                        {
                            Id = account1.Id,
                            Name = "Acc1 - Updated"
                        }
                    },

                    new UpdateRequest
                    {
                        Target = new Account
                        {
                            Id = account2.Id,
                            Name = "Acc2 - Updated"
                        }
                    }
                }
            };

            var response = service.Execute(executeMultipleRequest) as ExecuteMultipleResponse;

            Assert.False(response.IsFaulted);
            Assert.NotEmpty(response.Responses);

            var acc1 = service.Retrieve(Account.EntityLogicalName, account1.Id, new ColumnSet(true)).ToEntity<Account>();
            Assert.NotNull(acc1);
            var acc2 = (service.Retrieve(Account.EntityLogicalName, account2.Id, new ColumnSet(true))).ToEntity<Account>();
            Assert.NotNull(acc2);

            Assert.Equal("Acc1 - Updated", acc1.Name);
            Assert.Equal("Acc2 - Updated", acc2.Name);
        }

        [Fact]
        public static void Should_Not_Return_Responses_If_Not_Told_To_Do_So_And_No_Faults_Occur()
        {
            var context = new XrmFakedContext();

            var service = context.GetOrganizationService();

            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc2"
            };

            var executeMultipleRequest = new ExecuteMultipleRequest
            {
                Settings = new ExecuteMultipleSettings
                {
                    ReturnResponses = false
                },
                Requests = new OrganizationRequestCollection
                {
                    new CreateRequest
                    {
                        Target = account1
                    },

                    new CreateRequest
                    {
                        Target = account2
                    }
                }
            };

            var response = service.Execute(executeMultipleRequest) as ExecuteMultipleResponse;

            Assert.False(response.IsFaulted);
            Assert.Empty(response.Responses);

            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account1.Id, new ColumnSet(true)));
            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account2.Id, new ColumnSet(true)));
        }

        [Fact]
        public static void Should_Return_Error_Responses_Only_If_Faults_Occur_And_Return_Is_False()
        {
            var context = new XrmFakedContext();

            var service = context.GetOrganizationService();

            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc1"
            };

            var account2 = new Account
            {
                Id = account1.Id,
                Name = "Acc2"
            };

            var executeMultipleRequest = new ExecuteMultipleRequest
            {
                Settings = new ExecuteMultipleSettings
                {
                    ReturnResponses = false
                },
                Requests = new OrganizationRequestCollection
                {
                    new CreateRequest
                    {
                        Target = account1
                    },

                    new CreateRequest
                    {
                        Target = account2
                    }
                }
            };

            var response = service.Execute(executeMultipleRequest) as ExecuteMultipleResponse;

            Assert.True(response.IsFaulted);
            Assert.NotEmpty(response.Responses);
            Assert.Equal(1, response.Responses.Count);

            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account1.Id, new ColumnSet(true)));
        }

        [Fact]
        public static void Should_Return_All_Responses_If_Told_To_Do_So()
        {
            var context = new XrmFakedContext();

            var service = context.GetOrganizationService();

            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc2"
            };

            var executeMultipleRequest = new ExecuteMultipleRequest
            {
                Settings = new ExecuteMultipleSettings
                {
                    ReturnResponses = true
                },
                Requests = new OrganizationRequestCollection
                {
                    new CreateRequest
                    {
                        Target = account1
                    },

                    new CreateRequest
                    {
                        Target = account2
                    }
                }
            };

            var response = service.Execute(executeMultipleRequest) as ExecuteMultipleResponse;

            Assert.False(response.IsFaulted);
            Assert.NotEmpty(response.Responses);
            Assert.Equal(2, response.Responses.Count);
            Assert.True(response.Responses[0].Response is CreateResponse);
            Assert.True(response.Responses[1].Response is CreateResponse);

            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account1.Id, new ColumnSet(true)));
            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account2.Id, new ColumnSet(true)));
        }

        [Fact]
        public static void Should_Continue_On_Error_If_Told_To_Do_So()
        {
            var context = new XrmFakedContext();

            var service = context.GetOrganizationService();

            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc1"
            };

            var account2 = new Account
            {
                Id = account1.Id,
                Name = "Acc2 - Same ID as Acc 1"
            };

            var account3 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc3"
            };

            var executeMultipleRequest = new ExecuteMultipleRequest
            {
                Settings = new ExecuteMultipleSettings
                {
                    ReturnResponses = true,
                    ContinueOnError = true
                },
                Requests = new OrganizationRequestCollection
                {
                    new CreateRequest
                    {
                        Target = account1
                    },

                    new CreateRequest
                    {
                        Target = account2
                    },

                    new CreateRequest
                    {
                        Target = account3
                    }
                }
            };

            var response = service.Execute(executeMultipleRequest) as ExecuteMultipleResponse;

            Assert.True(response.IsFaulted);
            Assert.NotEmpty(response.Responses);

            Assert.True(response.Responses.Any(resp => resp.Fault != null));

            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account1.Id, new ColumnSet(true)));
            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account3.Id, new ColumnSet(true)));
        }

        [Fact]
        public static void Should_Not_Continue_On_Error_If_Not_Told_To_Do_So()
        {
            var context = new XrmFakedContext();

            var service = context.GetOrganizationService();

            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc1"
            };

            var account2 = new Account
            {
                Id = account1.Id,
                Name = "Acc2 - Same ID as Acc 1"
            };

            var account3 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Acc3"
            };

            var executeMultipleRequest = new ExecuteMultipleRequest
            {
                Settings = new ExecuteMultipleSettings
                {
                    ReturnResponses = true,
                    ContinueOnError = false
                },
                Requests = new OrganizationRequestCollection
                {
                    new CreateRequest
                    {
                        Target = account1
                    },

                    new CreateRequest
                    {
                        Target = account2
                    },

                    new CreateRequest
                    {
                        Target = account3
                    }
                }
            };

            var response = service.Execute(executeMultipleRequest) as ExecuteMultipleResponse;

            Assert.True(response.IsFaulted);
            Assert.NotEmpty(response.Responses);

            Assert.True(response.Responses.Any(resp => resp.Fault != null));

            Assert.NotNull(service.Retrieve(Account.EntityLogicalName, account1.Id, new ColumnSet(true)));
            Assert.Throws<FaultException<OrganizationServiceFault>>(() => service.Retrieve(Account.EntityLogicalName, account3.Id, new ColumnSet(true)));
        }
    }
}