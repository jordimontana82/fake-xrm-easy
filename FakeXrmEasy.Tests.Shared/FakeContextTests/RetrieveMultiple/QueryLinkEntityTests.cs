using System;
using System.Collections.Generic;
using System.Linq;
using Crm;
using Microsoft.Xrm.Sdk;
using Xunit;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System.Reflection;

namespace FakeXrmEasy.Tests.FakeContextTests
{
    public class QueryLinkEntityTests
    {
        [Fact]
        public static void Should_Find_Faked_N_To_N_Records()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetFakedOrganizationService();

            var userId = new Guid("11111111-7982-4276-A8FE-7CE05FABEAB4");
            var businessId = Guid.NewGuid();

            var testUser = new SystemUser
            {
                Id = userId
            };

            var testRole = new Role
            {
                Id = new Guid("22222222-7982-4276-A8FE-7CE05FABEAB4"),
                Name = "Test Role",
                BusinessUnitId = new EntityReference(BusinessUnit.EntityLogicalName, businessId)
            };

            fakedContext.Initialize(new Entity[] {testUser, testRole});

            fakedContext.AddRelationship("systemuserroles_association", new XrmFakedRelationship
            {
                IntersectEntity = "systemuserroles",
                Entity1LogicalName = SystemUser.EntityLogicalName,
                Entity1Attribute = "systemuserid",
                Entity2LogicalName = Role.EntityLogicalName,
                Entity2Attribute = "roleid"
            });

            var request = new AssociateRequest()
            {
                Target = testUser.ToEntityReference(),
                RelatedEntities = new EntityReferenceCollection()
                {
                    new EntityReference(Role.EntityLogicalName, testRole.Id),
                },
                Relationship = new Relationship("systemuserroles_association")
            };

            fakedService.Execute(request);

            var query = new QueryExpression()
            {
                EntityName = "role",
                ColumnSet = new ColumnSet("name"),
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = "roleid",
                        LinkCriteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "systemuserid",
                                    Operator = ConditionOperator.Equal,
                                    Values = {userId}
                                }
                            }
                        }
                    }
                }
            };

            var result = fakedService.RetrieveMultiple(query);
            Assert.NotEmpty(result.Entities);
            Assert.Equal(1, result.Entities.Count);
        }

        [Fact]
        public static void Should_Only_Find_Correct_Faked_N_To_N_Records()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetFakedOrganizationService();

            var userId = new Guid("11111111-7982-4276-A8FE-7CE05FABEAB4");
            var businessId = Guid.NewGuid();

            var testUser = new SystemUser
            {
                Id = userId
            };

            var testRole = new Role
            {
                Id = new Guid("22222222-7982-4276-A8FE-7CE05FABEAB4"),
                Name = "Test Role",
                BusinessUnitId = new EntityReference(BusinessUnit.EntityLogicalName, businessId)
            };

            var testUser2 = new SystemUser
            {
                Id = Guid.NewGuid()
            };

            var testRole2 = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Test Role",
                BusinessUnitId = new EntityReference(BusinessUnit.EntityLogicalName, businessId)
            };

            fakedContext.Initialize(new Entity[] {testUser, testRole, testUser2, testRole2});

            fakedContext.AddRelationship("systemuserroles_association", new XrmFakedRelationship
            {
                IntersectEntity = "systemuserroles",
                Entity1LogicalName = SystemUser.EntityLogicalName,
                Entity1Attribute = "systemuserid",
                Entity2LogicalName = Role.EntityLogicalName,
                Entity2Attribute = "roleid"
            });

            var request = new AssociateRequest()
            {
                Target = testUser.ToEntityReference(),
                RelatedEntities = new EntityReferenceCollection()
                {
                    new EntityReference(Role.EntityLogicalName, testRole.Id),
                },
                Relationship = new Relationship("systemuserroles_association")
            };

            fakedService.Execute(request);

            var request2 = new AssociateRequest()
            {
                Target = testUser2.ToEntityReference(),
                RelatedEntities = new EntityReferenceCollection()
                {
                    new EntityReference(Role.EntityLogicalName, testRole2.Id),
                },
                Relationship = new Relationship("systemuserroles_association")
            };

            fakedService.Execute(request2);

            var query = new QueryExpression()
            {
                EntityName = "role",
                ColumnSet = new ColumnSet("name"),
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = "roleid",
                        LinkCriteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "systemuserid",
                                    Operator = ConditionOperator.Equal,
                                    Values = {userId}
                                }
                            }
                        }
                    }
                }
            };

            var result = fakedService.RetrieveMultiple(query);
            Assert.NotEmpty(result.Entities);
            Assert.Equal(1, result.Entities.Count);
        }

        [Fact]
        public static void Should_Not_Find_Faked_N_To_N_Records_If_Disassociated_Again()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetFakedOrganizationService();

            var userId = new Guid("11111111-7982-4276-A8FE-7CE05FABEAB4");
            var businessId = Guid.NewGuid();

            var testUser = new SystemUser
            {
                Id = userId
            };

            var testRole = new Role
            {
                Id = new Guid("22222222-7982-4276-A8FE-7CE05FABEAB4"),
                Name = "Test Role",
                BusinessUnitId = new EntityReference(BusinessUnit.EntityLogicalName, businessId)
            };

            fakedContext.Initialize(new Entity[] {testUser, testRole});

            fakedContext.AddRelationship("systemuserroles_association", new XrmFakedRelationship
            {
                IntersectEntity = "systemuserroles",
                Entity1LogicalName = SystemUser.EntityLogicalName,
                Entity1Attribute = "systemuserid",
                Entity2LogicalName = Role.EntityLogicalName,
                Entity2Attribute = "roleid"
            });

            var request = new AssociateRequest()
            {
                Target = testUser.ToEntityReference(),
                RelatedEntities = new EntityReferenceCollection()
                {
                    new EntityReference(Role.EntityLogicalName, testRole.Id),
                },
                Relationship = new Relationship("systemuserroles_association")
            };

            fakedService.Execute(request);

            var disassociate = new DisassociateRequest
            {
                Target = testUser.ToEntityReference(),
                RelatedEntities = new EntityReferenceCollection()
                {
                    new EntityReference(Role.EntityLogicalName, testRole.Id),
                },
                Relationship = new Relationship("systemuserroles_association")
            };

            fakedService.Execute(disassociate);

            var query = new QueryExpression()
            {
                EntityName = "role",
                ColumnSet = new ColumnSet("name"),
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = "roleid",
                        LinkCriteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "systemuserid",
                                    Operator = ConditionOperator.Equal,
                                    Values = {userId}
                                }
                            }
                        }
                    }
                }
            };

            var result = fakedService.RetrieveMultiple(query);
            Assert.Empty(result.Entities);
        }

        [Fact]
        public static void Should_Find_Faked_N_To_N_Records_Using_Associate_Method()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetFakedOrganizationService();

            var userId = new Guid("11111111-7982-4276-A8FE-7CE05FABEAB4");
            var businessId = Guid.NewGuid();

            var testUser = new SystemUser
            {
                Id = userId
            };

            var testRole = new Role
            {
                Id = new Guid("22222222-7982-4276-A8FE-7CE05FABEAB4"),
                Name = "Test Role",
                BusinessUnitId = new EntityReference(BusinessUnit.EntityLogicalName, businessId)
            };

            fakedContext.Initialize(new Entity[] {testUser, testRole});

            fakedContext.AddRelationship("systemuserroles", new XrmFakedRelationship
            {
                IntersectEntity = "systemuserroles",
                Entity1LogicalName = SystemUser.EntityLogicalName,
                Entity1Attribute = "systemuserid",
                Entity2LogicalName = Role.EntityLogicalName,
                Entity2Attribute = "roleid"
            });

            fakedService.Associate("systemuserroles",
                testUser.Id,
                new Relationship("systemuserroles"),
                new EntityReferenceCollection()
                {
                    new EntityReference(Role.EntityLogicalName, testRole.Id),
                });

            var query = new QueryExpression()
            {
                EntityName = "role",
                ColumnSet = new ColumnSet("name"),
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = "roleid",
                        LinkCriteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "systemuserid",
                                    Operator = ConditionOperator.Equal,
                                    Values = {userId}
                                }
                            }
                        }
                    }
                }
            };

            var result = fakedService.RetrieveMultiple(query);
            Assert.NotEmpty(result.Entities);
            Assert.Equal(1, result.Entities.Count);
        }

        [Fact]
        public static void Should_Not_Find_Faked_N_To_N_Records_If_Disassociated_Again_Using_Disassociate_Method()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetFakedOrganizationService();

            var userId = new Guid("11111111-7982-4276-A8FE-7CE05FABEAB4");
            var businessId = Guid.NewGuid();

            var testUser = new SystemUser
            {
                Id = userId
            };

            var testRole = new Role
            {
                Id = new Guid("22222222-7982-4276-A8FE-7CE05FABEAB4"),
                Name = "Test Role",
                BusinessUnitId = new EntityReference(BusinessUnit.EntityLogicalName, businessId)
            };

            fakedContext.Initialize(new Entity[] {testUser, testRole});

            fakedContext.AddRelationship("systemuserroles", new XrmFakedRelationship
            {
                IntersectEntity = "systemuserroles",
                Entity1LogicalName = SystemUser.EntityLogicalName,
                Entity1Attribute = "systemuserid",
                Entity2LogicalName = Role.EntityLogicalName,
                Entity2Attribute = "roleid"
            });

            fakedService.Associate("systemuserroles",
                testUser.Id,
                new Relationship("systemuserroles"),
                new EntityReferenceCollection()
                {
                    new EntityReference(Role.EntityLogicalName, testRole.Id),
                });

            fakedService.Disassociate("systemuserroles",
                testUser.Id,
                new Relationship("systemuserroles"),
                new EntityReferenceCollection()
                {
                    new EntityReference(Role.EntityLogicalName, testRole.Id),
                });

            var query = new QueryExpression()
            {
                EntityName = "role",
                ColumnSet = new ColumnSet("name"),
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = "roleid",
                        LinkCriteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "systemuserid",
                                    Operator = ConditionOperator.Equal,
                                    Values = {userId}
                                }
                            }
                        }
                    }
                }
            };

            var result = fakedService.RetrieveMultiple(query);
            Assert.Empty(result.Entities);
        }

        [Fact]
        public static void Should_Not_Fail_On_Conditions_In_Link_Entities()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetFakedOrganizationService();

            var testEntity1 = new Entity("entity1")
            {
                Attributes = new AttributeCollection
                {
                    {"entity1attr", "test1"}
                }
            };
            var testEntity2 = new Entity("entity2")
            {
                Attributes = new AttributeCollection
                {
                    {"entity2attr", "test2"}
                }
            };

            testEntity1.Id = fakedService.Create(testEntity1);
            testEntity2.Id = fakedService.Create(testEntity2);

            var testRelation = new XrmFakedRelationship
            {
                IntersectEntity = "TestIntersectEntity",
                Entity1LogicalName = "entity1",
                Entity1Attribute = "entity1attr",
                Entity2LogicalName = "entity2",
                Entity2Attribute = "entity2attr"
            };
            fakedContext.AddRelationship(testRelation.Entity2LogicalName, testRelation);
            fakedService.Associate(testEntity1.LogicalName, testEntity1.Id,
                new Relationship(testRelation.Entity2LogicalName),
                new EntityReferenceCollection {testEntity2.ToEntityReference()});

            var query = new QueryExpression
            {
                EntityName = "entity1",
                Criteria = new FilterExpression {FilterOperator = LogicalOperator.And},
                ColumnSet = new ColumnSet(true)
            };

            var link = new LinkEntity
            {
                JoinOperator = JoinOperator.Natural,
                LinkFromEntityName = "entity1",
                LinkFromAttributeName = "entity1attr",
                LinkToEntityName = "entity2",
                LinkToAttributeName = "entity2attr",
                LinkCriteria = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                    {
                        new ConditionExpression
                        {
                            AttributeName = "entity2attr",
                            Operator = ConditionOperator.Equal,
                            Values = {"test2"}
                        }
                    }
                }
            };
            query.LinkEntities.Add(link);

            var result = fakedService.RetrieveMultiple(query);
            Assert.NotEmpty(result.Entities);
            Assert.Equal(1, result.Entities.Count);
        }

        [Fact]
        public void Should_evaluate_all_LinkEntity_conditions()
        {
            var fakedContext = new XrmFakedContext();
            var fakedService = fakedContext.GetOrganizationService();

            var entity1 = new Entity("entity1")
            {
                Id = Guid.NewGuid()
            };

            var entity2 = new Entity("entity2")
            {
                Id = Guid.NewGuid(),
                ["associated1"] = entity1.ToEntityReference()
            };

            var entity3 = new Entity("entity3")
            {
                Id = Guid.NewGuid(),
                ["associated2"] = entity2.ToEntityReference(),
                ["testFilter"] = "testValue"
            };

            var query = new QueryExpression
            {
                EntityName = "entity1",
                ColumnSet = new ColumnSet(true),
                LinkEntities =
                {
                    new LinkEntity("entity1", "entity2", "entity1id", "associated1", JoinOperator.Inner)
                    {
                        LinkEntities =
                        {
                            new LinkEntity("entity2", "entity3", "entity2id", "associated2", JoinOperator.Inner)
                            {
                                LinkCriteria =
                                {
                                    Conditions =
                                    {
                                        new ConditionExpression("testFilter", ConditionOperator.Equal, "doesNotMatch")
                                    }
                                }
                            }
                        }
                    }
                }
            };

            fakedContext.Initialize(new[] {entity1, entity2, entity3});

            var result = fakedService.RetrieveMultiple(query);

            Assert.Equal(0, result.Entities.Count);
        }

        [Fact]
        public void
            When_querying_by_an_attribute_which_wasnt_initialised_null_value_is_returned_for_early_bound_and_not_an_exception
            ()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof (Contact));

            var service = ctx.GetFakedOrganizationService();
            ctx.Initialize(new List<Entity>()
            {
                new Contact()
                {
                    Id = Guid.NewGuid(),
                    LastName = "Mcdonald"
                }
            });

            var name = "Mcdonald";

            using (var context = new XrmServiceContext(service))
            {
                var contacts = (from c in context.ContactSet
                    where c.FirstName == name || c.LastName == name
                    select new Contact {Id = c.Id, FirstName = c.FirstName, LastName = c.LastName}).ToList();

                Assert.Equal(1, contacts.Count);
                Assert.Null(contacts[0].FirstName);
            }
        }

        [Fact]
        public void When_sorting_by_an_attribute_which_wasnt_initialised_an_exception_is_not_thrown()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof (Contact));

            var service = ctx.GetFakedOrganizationService();
            ctx.Initialize(new List<Entity>()
            {
                new Contact() {Id = Guid.NewGuid(), FirstName = "Ronald", LastName = "Mcdonald"},
                new Contact() {Id = Guid.NewGuid(), LastName = "Jordan"}
            });

            using (var context = new XrmServiceContext(service))
            {
                var contacts = (from c in context.ContactSet
                    orderby c.FirstName
                    select new Contact {Id = c.Id, FirstName = c.FirstName, LastName = c.LastName}).ToList();

                Assert.Equal(2, contacts.Count);
                Assert.Null(contacts[0].FirstName);
            }
        }

#if FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_365
        [Fact]
        public void Should_Find_Expired_Case_And_Create_Email_But_Not_Send_By_Default()
        {
            var context = new XrmFakedContext();
            var service = context.GetFakedOrganizationService();

            // Date for filtering, we only want "expired" records, i.e. those that weren't set as regarding in any emails for this period and logically even exist this long
            var days = 5;

            var incident = new Incident
            {
                Id = Guid.NewGuid(),
                Title = "Test case",
                StatusCode = new OptionSetValue((int)IncidentState.Active)
            };
            incident["createdon"] = DateTime.UtcNow.AddDays(-6);

            context.Initialize(new[] {incident});

            // Remove either incident createdon conditionexpression, or LinkEntities and the e-mail conditionexpression and it will pass
            // What this query expresses: Get all incidents, that are older than given number of days and that also didn't receive emails for this number of days
            var query = new QueryExpression
            {
                ColumnSet = new ColumnSet(true),
                EntityName = Incident.EntityLogicalName,
                Criteria =
                {
                    FilterOperator = LogicalOperator.And,
                    Filters =
                    {
                        new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression("statuscode", ConditionOperator.Equal, new OptionSetValue((int) IncidentState.Active)),
                                new ConditionExpression("createdon", ConditionOperator.LessEqual, DateTime.UtcNow.AddDays(-1 * days)),
                                new ConditionExpression("email", "regardingobjectid", ConditionOperator.Null)
                            }
                        }
                    }
                },
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = "incident",
                        LinkToEntityName = "email",
                        LinkFromAttributeName = "incidentid",
                        LinkToAttributeName = "regardingobjectid",
                        JoinOperator = JoinOperator.LeftOuter,
                        LinkCriteria = new FilterExpression
                        {
                            Filters =
                            {
                                new FilterExpression
                                {
                                    FilterOperator = LogicalOperator.And,
                                    Conditions =
                                    {
                                        new ConditionExpression("createdon", ConditionOperator.GreaterEqual, DateTime.UtcNow.AddDays(-1*days))
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var incidents = service.RetrieveMultiple(query).Entities;
            Assert.Equal(1, incidents.Count);
        }
#endif
    }
}