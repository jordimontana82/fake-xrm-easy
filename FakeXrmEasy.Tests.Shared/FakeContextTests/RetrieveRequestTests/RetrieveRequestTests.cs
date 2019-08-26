using Crm;
using FakeItEasy;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests
{
    public class RetrieveRequestTests
    {
        [Fact]
        public static void Should_Populate_EntityReference_Name_When_Metadata_Is_Provided()
        {
            var userMetadata = new EntityMetadata() { LogicalName = "systemuser" };
            userMetadata.SetSealedPropertyValue("PrimaryNameAttribute", "fullname");

            var user = new Entity() { LogicalName = "systemuser", Id=Guid.NewGuid() };
            user["fullname"] = "Fake XrmEasy";

            var context = new XrmFakedContext();
            context.InitializeMetadata(userMetadata);
            context.Initialize(user);
            context.CallerId = user.ToEntityReference();

            var account = new Entity() { LogicalName = "account" };

            var service = context.GetOrganizationService();

            var accountId = service.Create(account);

            account = service.Retrieve("account", accountId, new ColumnSet(true));

            Assert.Equal("Fake XrmEasy", account.GetAttributeValue<EntityReference>("ownerid").Name);
        }

        [Fact]
        public static void Should_Retrieve_A_Correct_Entity()
        {
            var account1 = new Account
            {
                AccountId = Guid.NewGuid(),
                Name = "Account 1"
            };

            var account2 = new Account
            {
                AccountId = Guid.NewGuid(),
                Name = "Account 2"
            };

            var account3 = new Account
            {
                AccountId = Guid.NewGuid(),
                Name = "Account 3"
            };

            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            fakedContext.Initialize(new[] { account1, account2, account3 });

            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet("name"),
                Target = account2.ToEntityReference(),
            };

            var result = (RetrieveResponse)fakedService.Execute(request);

            Assert.NotNull(result.Entity);

            var resultAccount = result.Entity.ToEntity<Account>();
            Assert.Equal(account2.Id, resultAccount.Id);
            Assert.Equal(account2.Name, resultAccount.Name);
        }

        [Fact]
        public static void Should_Retrieve_A_Correct_Entity_With_1_To_N_Related_Records()
        {
            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 2"
            };

            var account3 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 3"
            };

            var contact1 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 1",
                ParentCustomerId = account2.ToEntityReference()
            };

            var contact2 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 2",
                ParentCustomerId = account1.ToEntityReference()
            };

            var contact3 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 3",
                ParentCustomerId = account2.ToEntityReference()
            };

            var contact4 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 4",
                ParentCustomerId = account3.ToEntityReference()
            };

            var contact5 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 5"
            };

            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            fakedContext.Initialize(new Entity[] { account1, account2, account3, contact1, contact2, contact3, contact4, contact5 });
            fakedContext.AddRelationship("contact_customer_accounts", new XrmFakedRelationship
            (
                entity1LogicalName: Contact.EntityLogicalName,
                entity1Attribute: "parentcustomerid",
                entity2LogicalName: Account.EntityLogicalName,
                entity2Attribute: "accountid"
            ));

            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet("name"),
                Target = account2.ToEntityReference(),
                RelatedEntitiesQuery = new RelationshipQueryCollection
                {
                    {
                        new Relationship("contact_customer_accounts"),
                        new QueryExpression
                        {
                            ColumnSet = new ColumnSet("firstname"),
                            EntityName = Contact.EntityLogicalName
                        }
                    }
                }
            };

            var result = (RetrieveResponse)fakedService.Execute(request);

            Assert.NotNull(result.Entity);

            //check account
            var resultAccount = result.Entity.ToEntity<Account>();
            Assert.Equal(account2.Id, resultAccount.Id);
            Assert.Equal(account2.Name, resultAccount.Name);

            //check relationship
            Assert.NotNull(resultAccount.contact_customer_accounts);
            Assert.Equal(2, resultAccount.contact_customer_accounts.Count());

            //check that contacts are retrieved
            var resultRelatedRecordsList = resultAccount.contact_customer_accounts;

            Assert.True(resultRelatedRecordsList.Any(x => x.Id == contact1.Id));
            Assert.True(resultRelatedRecordsList.Any(x => x.Id == contact3.Id));

            //check contacts (optional check)
            Assert.Equal(contact1.FirstName, resultRelatedRecordsList.First(x => x.Id == contact1.Id).FirstName);
            Assert.Equal(contact3.FirstName, resultRelatedRecordsList.First(x => x.Id == contact3.Id).FirstName);
        }

        [Fact]
        public static void Should_Retrieve_A_Correct_Entity_With_N_To_N_Related_Records()
        {
            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 2"
            };

            var account3 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 3"
            };

            var lead1 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 1"
            };

            var lead2 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 2"
            };

            var lead3 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 3"
            };

            var lead4 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 4"
            };

            var lead5 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 5"
            };

            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            var relationshipName = "accountleads_association";

            fakedContext.AddRelationship(relationshipName, new XrmFakedRelationship
            {
                IntersectEntity = "accountleads",
                Entity1LogicalName = Account.EntityLogicalName,
                Entity1Attribute = "accountid",
                Entity2LogicalName = Lead.EntityLogicalName,
                Entity2Attribute = "leadid"
            });

            fakedContext.Initialize(new Entity[] { account1, account2, account3, lead1, lead2, lead3, lead4, lead5 });
            fakedService.Associate(Account.EntityLogicalName, account2.Id, new Relationship(relationshipName),
                new EntityReferenceCollection()
                {
                    lead2.ToEntityReference(),
                    lead4.ToEntityReference(),
                    lead5.ToEntityReference()
                });

            fakedService.Associate(Account.EntityLogicalName, account1.Id, new Relationship(relationshipName),
                new EntityReferenceCollection()
                {
                    lead2.ToEntityReference(),
                    lead3.ToEntityReference()
                });

            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet("name"),
                Target = account2.ToEntityReference(),
                RelatedEntitiesQuery = new RelationshipQueryCollection
                {
                    {
                        new Relationship(relationshipName),
                        new QueryExpression
                        {
                            ColumnSet = new ColumnSet("subject"),
                            EntityName = Lead.EntityLogicalName
                        }
                    }
                }
            };

            var result = (RetrieveResponse)fakedService.Execute(request);

            Assert.NotNull(result.Entity);

            //check account
            var resultAccount = result.Entity.ToEntity<Account>();
            Assert.Equal(account2.Id, resultAccount.Id);
            Assert.Equal(account2.Name, resultAccount.Name);

            //check relationship
            Assert.NotNull(resultAccount.accountleads_association);
            Assert.Equal(3, resultAccount.accountleads_association.Count());

            //check that leads are retrieved
            var resultRelatedRecordsList = resultAccount.accountleads_association;

            Assert.True(resultRelatedRecordsList.Any(x => x.Id == lead2.Id));
            Assert.True(resultRelatedRecordsList.Any(x => x.Id == lead4.Id));
            Assert.True(resultRelatedRecordsList.Any(x => x.Id == lead5.Id));

            //check leads (optional check)
            Assert.Equal(lead2.Subject, resultRelatedRecordsList.First(x => x.Id == lead2.Id).Subject);
            Assert.Equal(lead4.Subject, resultRelatedRecordsList.First(x => x.Id == lead4.Id).Subject);
            Assert.Equal(lead5.Subject, resultRelatedRecordsList.First(x => x.Id == lead5.Id).Subject);
        }

        [Fact]
        public static void Should_Retrieve_A_Correct_Entity_With_N_To_N_Related_Records_Vice_Versa()
        {
            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 2"
            };

            var account3 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 3"
            };

            var lead1 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 1"
            };

            var lead2 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 2"
            };

            var lead3 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 3"
            };

            var lead4 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 4"
            };

            var lead5 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 5"
            };

            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            var relationshipName = "accountleads_association";

            fakedContext.AddRelationship(relationshipName, new XrmFakedRelationship
            {
                IntersectEntity = "accountleads",
                Entity1LogicalName = Account.EntityLogicalName,
                Entity1Attribute = "accountid",
                Entity2LogicalName = Lead.EntityLogicalName,
                Entity2Attribute = "leadid"
            });

            fakedContext.Initialize(new Entity[] { account1, account2, account3, lead1, lead2, lead3, lead4, lead5 });
            fakedService.Associate(Account.EntityLogicalName, account2.Id, new Relationship(relationshipName),
                new EntityReferenceCollection()
                {
                    lead2.ToEntityReference(),
                    lead4.ToEntityReference(),
                    lead5.ToEntityReference()
                });

            fakedService.Associate(Account.EntityLogicalName, account1.Id, new Relationship(relationshipName),
                new EntityReferenceCollection()
                {
                    lead2.ToEntityReference(),
                    lead3.ToEntityReference()
                });

            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet("subject"),
                Target = lead2.ToEntityReference(),
                RelatedEntitiesQuery = new RelationshipQueryCollection
                {
                    {
                        new Relationship(relationshipName),
                        new QueryExpression
                        {
                            ColumnSet = new ColumnSet("name"),
                            EntityName = Account.EntityLogicalName
                        }
                    }
                }
            };

            var result = (RetrieveResponse)fakedService.Execute(request);

            Assert.NotNull(result.Entity);

            //check account
            var resultLead = result.Entity.ToEntity<Lead>();
            Assert.Equal(lead2.Id, lead2.Id);
            Assert.Equal(lead2.Subject, lead2.Subject);

            //check relationship
            Assert.NotNull(resultLead.accountleads_association);
            Assert.Equal(2, resultLead.accountleads_association.Count());

            //check that accounts are retrieved
            var resultRelatedRecordsList = resultLead.accountleads_association;

            Assert.True(resultRelatedRecordsList.Any(x => x.Id == account2.Id));
            Assert.True(resultRelatedRecordsList.Any(x => x.Id == account1.Id));

            //check accounts (optional check)
            Assert.Equal(account2.Name, resultRelatedRecordsList.First(x => x.Id == account2.Id).Name);
            Assert.Equal(account1.Name, resultRelatedRecordsList.First(x => x.Id == account1.Id).Name);
        }

        [Fact]
        public static void Should_Retrieve_A_Correct_Entity_With_Multiple_Related_Record_Queries()
        {
            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 2"
            };

            var lead1 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 1"
            };

            var lead2 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 2"
            };

            var lead3 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 3"
            };

            var contact1 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 1",
                ParentCustomerId = account2.ToEntityReference()
            };

            var contact2 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 2",
                ParentCustomerId = account1.ToEntityReference()
            };

            var contact3 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 3",
                ParentCustomerId = account2.ToEntityReference()
            };

            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            var accountLeadsRelationshipName = "accountleads_association";

            //account N:N leads
            fakedContext.AddRelationship(accountLeadsRelationshipName, new XrmFakedRelationship
            {
                IntersectEntity = "accountleads",
                Entity1LogicalName = Account.EntityLogicalName,
                Entity1Attribute = "accountid",
                Entity2LogicalName = Lead.EntityLogicalName,
                Entity2Attribute = "leadid"
            });

            //account 1:N contacts
            fakedContext.AddRelationship("contact_customer_accounts", new XrmFakedRelationship
            (
                entity1LogicalName: Contact.EntityLogicalName,
                entity1Attribute: "parentcustomerid",
                entity2LogicalName: Account.EntityLogicalName,
                entity2Attribute: "accountid"
            ));

            fakedContext.Initialize(new Entity[] { account1, account2, lead1, lead2, lead3, contact1, contact2, contact3 });

            //associate account N:N leads
            fakedService.Associate(Account.EntityLogicalName, account2.Id, new Relationship(accountLeadsRelationshipName),
                new EntityReferenceCollection()
                {
                    lead1.ToEntityReference(),
                    lead3.ToEntityReference(),
                });

            fakedService.Associate(Account.EntityLogicalName, account1.Id, new Relationship(accountLeadsRelationshipName),
                new EntityReferenceCollection()
                {
                    lead2.ToEntityReference()
                });

            //build a request
            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet("name"),
                Target = account2.ToEntityReference(),
                RelatedEntitiesQuery = new RelationshipQueryCollection
                {
                    {
                        new Relationship(accountLeadsRelationshipName),
                        new QueryExpression
                        {
                            ColumnSet = new ColumnSet("subject"),
                            EntityName = Lead.EntityLogicalName
                        }
                    },
                    {
                        new Relationship("contact_customer_accounts"),
                        new QueryExpression
                        {
                            ColumnSet = new ColumnSet("firstname"),
                            EntityName = Contact.EntityLogicalName
                        }
                    }
                }
            };

            //execute request
            var result = (RetrieveResponse)fakedService.Execute(request);

            Assert.NotNull(result.Entity);

            //check account
            var resultAccount = result.Entity.ToEntity<Account>();
            Assert.Equal(account2.Id, resultAccount.Id);

            //check first relationship
            Assert.NotNull(resultAccount.accountleads_association);
            Assert.Equal(2, resultAccount.accountleads_association.Count());

            //and check another relationship
            Assert.NotNull(resultAccount.contact_customer_accounts);
            Assert.Equal(2, resultAccount.contact_customer_accounts.Count());
        }

        [Fact]
        public static void Should_Retrieve_A_Correct_Entity_With_Relationship_Early_Bound()
        {
            var account1 = new Entity("account")
            {
                Id = Guid.NewGuid(),
                Attributes =
                {
                    { "name", "Account 1" }
                }
            };

            var account2 = new Entity("account")
            {
                Id = Guid.NewGuid(),
                Attributes =
                {
                    { "name", "Account 2" }
                }
            };

            var contact1 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                Attributes =
                {
                    { "firstname", "Contact 1"},
                    { "parentcustomerid", account2.ToEntityReference() }
                }
            };

            var contact2 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                Attributes =
                {
                    { "firstname", "Contact 2"},
                    { "parentcustomerid", account1.ToEntityReference() }
                }
            };

            var contact3 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                Attributes =
                {
                    { "firstname", "Contact 3"},
                    { "parentcustomerid", account2.ToEntityReference() }
                }
            };

            var contact4 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                Attributes =
                {
                    { "firstname", "Contact 4"}
                }
            };

            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            fakedContext.Initialize(new[] { account1, account2, contact1, contact2, contact3, contact4 });
            fakedContext.AddRelationship("contact_customer_accounts", new XrmFakedRelationship
            (
                entity1LogicalName: "contact",
                entity1Attribute: "parentcustomerid",
                entity2LogicalName: "account",
                entity2Attribute: "accountid"
            ));

            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet("name"),
                Target = account2.ToEntityReference(),
                RelatedEntitiesQuery = new RelationshipQueryCollection
                {
                    {
                        new Relationship("contact_customer_accounts"),
                        new QueryExpression
                        {
                            ColumnSet = new ColumnSet("firstname"),
                            EntityName = "contact"
                        }
                    }
                }
            };

            var result = (RetrieveResponse)fakedService.Execute(request);

            Assert.NotNull(result.Entity);

            //check account
            var resultAccount = result.Entity;
            Assert.Equal(account2.Id, resultAccount.Id);
            Assert.Equal(account2["name"], resultAccount["name"]);

            //check relationship
            Assert.NotNull(resultAccount.RelatedEntities.FirstOrDefault(x => x.Key.SchemaName == "contact_customer_accounts"));

            var relatedEntityCollection = resultAccount.RelatedEntities.FirstOrDefault(x => x.Key.SchemaName == "contact_customer_accounts");
            Assert.NotNull(relatedEntityCollection.Value);
            Assert.NotNull(relatedEntityCollection.Value.Entities);

            var relatedEntities = relatedEntityCollection.Value.Entities;
            Assert.Equal(2, relatedEntities.Count);

            Assert.True(relatedEntities.Any(x => x.Id == contact1.Id));
            Assert.True(relatedEntities.Any(x => x.Id == contact3.Id));

            //check contacts (optional check)
            Assert.Equal(contact1["firstname"], relatedEntities.First(x => x.Id == contact1.Id)["firstname"]);
            Assert.Equal(contact3["firstname"], relatedEntities.First(x => x.Id == contact3.Id)["firstname"]);
        }

        [Fact]
        public static void Should_Retrieve_A_Correct_Entity_With_1_To_N_Related_Records_And_Related_Record_Query_Criteria()
        {
            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 2"
            };

            var contact1 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 1",
                ParentCustomerId = account2.ToEntityReference()
            };

            var contact2 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 2",
                ParentCustomerId = account1.ToEntityReference()
            };

            var contact3 = new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = "Contact 3",
                ParentCustomerId = account2.ToEntityReference()
            };

            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            fakedContext.Initialize(new Entity[] { account1, account2, contact1, contact2, contact3 });
            fakedContext.AddRelationship("contact_customer_accounts", new XrmFakedRelationship
            (
                entity1LogicalName: Contact.EntityLogicalName,
                entity1Attribute: "parentcustomerid",
                entity2LogicalName: Account.EntityLogicalName,
                entity2Attribute: "accountid"
            ));

            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet("name"),
                Target = account2.ToEntityReference(),
                RelatedEntitiesQuery = new RelationshipQueryCollection
                {
                    {
                        new Relationship("contact_customer_accounts"),
                        new QueryExpression
                        {
                            ColumnSet = new ColumnSet("firstname"),
                            EntityName = Contact.EntityLogicalName,
                            Criteria = new FilterExpression
                            {
                                Conditions =
                                {
                                    new ConditionExpression("firstname", ConditionOperator.Equal, contact3.FirstName),
                                    new ConditionExpression("statecode", ConditionOperator.Equal, 0)
                                }
                            }
                        }
                    }
                }
            };

            var result = (RetrieveResponse)fakedService.Execute(request);

            Assert.NotNull(result.Entity);

            var resultAccount = result.Entity.ToEntity<Account>();
            Assert.Equal(account2.Id, resultAccount.Id);

            Assert.NotNull(resultAccount.contact_customer_accounts);
            Assert.Equal(1, resultAccount.contact_customer_accounts.Count());
            Assert.Equal(contact3.Id, resultAccount.contact_customer_accounts.First().Id);
        }

        [Fact]
        public static void Should_Retrieve_A_Correct_Entity_With_N_To_N_Related_Records_And_Related_Record_Query_Criteria()
        {
            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 2"
            };

            var account3 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 3"
            };

            var lead1 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 1"
            };

            var lead2 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 2"
            };

            var lead3 = new Lead
            {
                Id = Guid.NewGuid(),
                Subject = "Lead 3"
            };

            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            var relationshipName = "accountleads_association";

            fakedContext.AddRelationship(relationshipName, new XrmFakedRelationship
            {
                IntersectEntity = "accountleads",
                Entity1LogicalName = Account.EntityLogicalName,
                Entity1Attribute = "accountid",
                Entity2LogicalName = Lead.EntityLogicalName,
                Entity2Attribute = "leadid"
            });

            fakedContext.Initialize(new Entity[] { account1, account2, account3, lead1, lead2, lead3 });
            fakedService.Associate(Account.EntityLogicalName, account2.Id, new Relationship(relationshipName),
                new EntityReferenceCollection()
                {
                    lead2.ToEntityReference(),
                    lead3.ToEntityReference(),
                });

            fakedService.Associate(Account.EntityLogicalName, account1.Id, new Relationship(relationshipName),
                new EntityReferenceCollection()
                {
                    lead1.ToEntityReference(),
                });

            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet("name"),
                Target = account2.ToEntityReference(),
                RelatedEntitiesQuery = new RelationshipQueryCollection
                {
                    {
                        new Relationship(relationshipName),
                        new QueryExpression
                        {
                            ColumnSet = new ColumnSet("subject"),
                            EntityName = Lead.EntityLogicalName,
                            Criteria = new FilterExpression
                            {
                                Conditions =
                                {
                                    new ConditionExpression("subject", ConditionOperator.Equal, "Lead 3"),
                                    new ConditionExpression("statecode", ConditionOperator.Equal, 0)
                                }
                            }
                        }
                    }
                }
            };

            var result = (RetrieveResponse)fakedService.Execute(request);

            Assert.NotNull(result.Entity);

            //check account
            var resultAccount = result.Entity.ToEntity<Account>();
            Assert.Equal(account2.Id, resultAccount.Id);

            Assert.NotNull(resultAccount.accountleads_association);
            Assert.Equal(1, resultAccount.accountleads_association.Count());
            Assert.Equal(lead3.Id, resultAccount.accountleads_association.First().Id);
        }

        [Fact]
        public static void Should_Retrieve_All_Related_Records()
        {
            //related entities should all be returned
            //even when PageInfo is set
            //AM: There is no reason for the result to retun all related records regardless of the PageSize - at least that is not the CRM behavior
            //The result set shoudl not exceed the number specifit in he Page info

            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 1"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account 2"
            };

            var leads = new List<Lead>();
            for (int i = 0; i < 50; i++)
            {
                leads.Add(new Lead { Id = Guid.NewGuid() });
            }

            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            var relationshipName = "accountleads_association";

            fakedContext.AddRelationship(relationshipName, new XrmFakedRelationship
            {
                IntersectEntity = "accountleads",
                Entity1LogicalName = Account.EntityLogicalName,
                Entity1Attribute = "accountid",
                Entity2LogicalName = Lead.EntityLogicalName,
                Entity2Attribute = "leadid"
            });

            var initData = new List<Entity>();
            initData.AddRange(new Entity[] { account1, account2 });
            initData.AddRange(leads);

            fakedContext.Initialize(initData);
            fakedService.Associate(Account.EntityLogicalName, account2.Id, new Relationship(relationshipName),
                new EntityReferenceCollection(leads.Select(x => x.ToEntityReference()).ToList()));

            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet("name"),
                Target = account2.ToEntityReference(),
                RelatedEntitiesQuery = new RelationshipQueryCollection
                {
                    {
                        new Relationship(relationshipName),
                        new QueryExpression
                        {
                            PageInfo = new PagingInfo { Count = 20, PageNumber = 1 },
                            ColumnSet = new ColumnSet(false),
                            EntityName = Lead.EntityLogicalName
                        }
                    }
                }
            };

            var result = (RetrieveResponse)fakedService.Execute(request);

            Assert.NotNull(result.Entity);

            //check account
            var resultAccount = result.Entity.ToEntity<Account>();

            //check relationship
            Assert.NotNull(resultAccount.accountleads_association);
            //The
            Assert.Equal(20, resultAccount.accountleads_association.Count());
        }

        [Fact]
        public static void Should_Throw_When_Target_Not_Set()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            var request = new RetrieveRequest
            {
                ColumnSet = new ColumnSet("name")
            };

            var exception = Assert.Throws<ArgumentNullException>(() => fakedService.Execute(request));
            Assert.Equal("Target", exception.ParamName);
        }

        [Fact]
        public static void Should_Throw_When_Related_Record_Query_Not_Set_For_Relationship()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();
            var account = new Entity(Account.EntityLogicalName);
            account.Id = Guid.NewGuid();
            fakedContext.Initialize(account);

            var request = new RetrieveRequest
            {
                Target = account.ToEntityReference(),
                ColumnSet = new ColumnSet(true),
                RelatedEntitiesQuery = new RelationshipQueryCollection
                {
                    { new Relationship("any"), null }
                }
            };

            var exception = Assert.Throws<ArgumentNullException>(() => fakedService.Execute(request));
            Assert.Equal("relateEntitiesQuery.Value", exception.ParamName);
        }

        [Fact]
        public static void Should_Throw_When_Relationship_Not_Set_In_Metadata()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            var account = new Entity(Account.EntityLogicalName);
            account.Id = Guid.NewGuid();
            fakedContext.Initialize(account);

            var request = new RetrieveRequest
            {
                Target = account.ToEntityReference(),
                ColumnSet = new ColumnSet(true),
                RelatedEntitiesQuery = new RelationshipQueryCollection
                {
                    { new Relationship("any"), new QueryExpression() }
                }
            };

            var exception = Assert.Throws<Exception>(() => fakedService.Execute(request));
            Assert.Equal("Relationship \"any\" does not exist in the metadata cache.", exception.Message);
        }
#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013 && !FAKE_XRM_EASY_2015
        [Fact]
        public static void Should_Retrieve_A_Correct_Entity_By_Alternate_Key()
        {
            var fakedContext = new XrmFakedContext();
            var accountMetadata = new Microsoft.Xrm.Sdk.Metadata.EntityMetadata();
            accountMetadata.LogicalName = Account.EntityLogicalName;
            var alternateKeyMetadata = new Microsoft.Xrm.Sdk.Metadata.EntityKeyMetadata();
            alternateKeyMetadata.KeyAttributes = new string[] { "alternateKey" };
            accountMetadata.SetFieldValue("_keys", new Microsoft.Xrm.Sdk.Metadata.EntityKeyMetadata[]
                 {
                 alternateKeyMetadata
                 });
            fakedContext.InitializeMetadata(accountMetadata);
            var account = new Entity(Account.EntityLogicalName);
            account.Id = Guid.NewGuid();
            account.Attributes.Add("alternateKey", "key");
            fakedContext.Initialize(account);
            var fakedService = fakedContext.GetOrganizationService();

            var request = new RetrieveRequest
            {
                Target = new EntityReference(Account.EntityLogicalName, "alternateKey", "key"),
                ColumnSet = new ColumnSet(allColumns: true)
            };

            var retrievedAccount = (RetrieveResponse)fakedService.Execute(request);
            Assert.Equal(account.Id, retrievedAccount.Entity.Id);
        }


        [Fact]
        public static void Should_Throw_When_Alternate_Key_Not_In_Metadata()
        {
            var fakedContext = new XrmFakedContext();
            var account = new Entity(Account.EntityLogicalName);
            account.Id = Guid.NewGuid();
            account.Attributes.Add("alternateKey", "key");
            fakedContext.Initialize(account);
            var fakedService = fakedContext.GetOrganizationService();

            var request = new RetrieveRequest
            {
                Target = new EntityReference(Account.EntityLogicalName, "alternateKey", "key"),
                ColumnSet = new ColumnSet(allColumns: true)
            };

            var exception = Assert.Throws<InvalidOperationException>(() => fakedService.Execute(request));
            Assert.Equal($"The requested key attributes do not exist for the entity {Account.EntityLogicalName}", exception.Message);
        }
#endif 
    }
}