using System;
using System.Collections.Generic;
using System.Linq;
using Crm;
using Microsoft.Xrm.Sdk;
using Xunit;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

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

            fakedContext.Initialize(new Entity[] { testUser, testRole });

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
                LinkEntities = {
                    new LinkEntity {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = "roleid",
                        LinkCriteria = new FilterExpression {
                            FilterOperator = LogicalOperator.And,
                            Conditions = {
                                new ConditionExpression {
                                    AttributeName = "systemuserid",
                                    Operator = ConditionOperator.Equal,
                                    Values = { userId }
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

            fakedContext.Initialize(new Entity[] { testUser, testRole, testUser2, testRole2 });

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
                LinkEntities = {
                    new LinkEntity {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = "roleid",
                        LinkCriteria = new FilterExpression {
                            FilterOperator = LogicalOperator.And,
                            Conditions = {
                                new ConditionExpression {
                                    AttributeName = "systemuserid",
                                    Operator = ConditionOperator.Equal,
                                    Values = { userId }
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

            fakedContext.Initialize(new Entity[] { testUser, testRole });

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
                LinkEntities = {
                    new LinkEntity {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = "roleid",
                        LinkCriteria = new FilterExpression {
                            FilterOperator = LogicalOperator.And,
                            Conditions = {
                                new ConditionExpression {
                                    AttributeName = "systemuserid",
                                    Operator = ConditionOperator.Equal,
                                    Values = { userId }
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

            fakedContext.Initialize(new Entity[] { testUser, testRole });

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
                LinkEntities = {
                    new LinkEntity {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = "roleid",
                        LinkCriteria = new FilterExpression {
                            FilterOperator = LogicalOperator.And,
                            Conditions = {
                                new ConditionExpression {
                                    AttributeName = "systemuserid",
                                    Operator = ConditionOperator.Equal,
                                    Values = { userId }
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

            fakedContext.Initialize(new Entity[] { testUser, testRole });

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
                LinkEntities = {
                    new LinkEntity {
                        LinkFromEntityName = Role.EntityLogicalName,
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = SystemUserRoles.EntityLogicalName,
                        LinkToAttributeName = "roleid",
                        LinkCriteria = new FilterExpression {
                            FilterOperator = LogicalOperator.And,
                            Conditions = {
                                new ConditionExpression {
                                    AttributeName = "systemuserid",
                                    Operator = ConditionOperator.Equal,
                                    Values = { userId }
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
                    {"entity1attr", "test1" }
                }
            };
            var testEntity2 = new Entity("entity2")
            {
                Attributes = new AttributeCollection
                {
                    {"entity2attr", "test2" }
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
            fakedService.Associate(testEntity1.LogicalName, testEntity1.Id, new Relationship(testRelation.Entity2LogicalName), new EntityReferenceCollection { testEntity2.ToEntityReference() });

            var query = new QueryExpression
            {
                EntityName = "entity1",
                Criteria = new FilterExpression { FilterOperator = LogicalOperator.And },
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
                    Conditions = { new ConditionExpression { AttributeName = "entity2attr", Operator = ConditionOperator.Equal, Values = { "test2" } } }
                }
            };
            query.LinkEntities.Add(link);

            var result = fakedService.RetrieveMultiple(query);
            Assert.NotEmpty(result.Entities);
            Assert.Equal(1, result.Entities.Count);
        }
    }
}