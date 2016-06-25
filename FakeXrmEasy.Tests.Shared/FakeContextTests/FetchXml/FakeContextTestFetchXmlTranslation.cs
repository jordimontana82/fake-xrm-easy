using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.FetchXml
{
    /// <summary>
    /// This will test that a fetchxml is correctly translated into a QueryExpression
    /// which was already tested
    /// 
    /// We'll leave FetchXml aggregations for a later version
    /// </summary>
    public class FakeContextTestFetchXmlTranslation
    {
        [Fact]
        public void When_translating_a_fetch_xml_expression_fetchxml_must_be_an_xml()
        {
            var ctx = new XrmFakedContext();

            Assert.Throws<Exception>(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "this is not an xml"));
        }

        [Fact]
        public void When_translating_a_fetch_xml_expression_first_node_must_be_a_fetch_element_otherwise_exception_is_thrown()
        {
            var ctx = new XrmFakedContext();

            Assert.DoesNotThrow(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<fetch><entity name='contact'></entity></fetch>"));
            Assert.Throws<Exception>(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<attribute></attribute>"));
            Assert.Throws<Exception>(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<entity></entity>"));
        }

        [Fact]
        public void When_translating_a_fetch_xml_entity_node_must_have_a_name_attribute()
        {
            var ctx = new XrmFakedContext();

            Assert.Throws<Exception>(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<fetch><entity></entity></fetch>"));
            Assert.DoesNotThrow(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<fetch><entity name='contact'></entity></fetch>"));
        }

        [Fact]
        public void When_translating_a_fetch_xml_attribute_node_must_have_a_name_attribute()
        {
            var ctx = new XrmFakedContext();

            Assert.Throws<Exception>(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<fetch><entity name='contact'><attribute></attribute></entity></fetch>"));
            Assert.DoesNotThrow(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<fetch><entity name='contact'><attribute name='firstname'></attribute></entity></fetch>"));
        }

        [Fact]
        public void When_translating_a_fetch_xml_order_node_must_have_2_attributes()
        {
            var ctx = new XrmFakedContext();

            Assert.Throws<Exception>(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<fetch><entity name='contact'><order></order></entity></fetch>"));
            Assert.Throws<Exception>(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<fetch><entity name='contact'><order attribute=''></order></entity></fetch>"));
            Assert.Throws<Exception>(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<fetch><entity name='contact'><order descending=''></order></entity></fetch>"));
            Assert.DoesNotThrow(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<fetch><entity name='contact'><order attribute='firstname' descending='true'></order></entity></fetch>"));
        }

        [Fact]
        public void When_translating_a_fetch_xml_unknown_elements_throw_an_exception()
        {
            var ctx = new XrmFakedContext();
            Assert.Throws<Exception>(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<thisdoesntexist></thisdoesntexist>"));
        }
        
        [Fact]
        public void When_translating_a_fetch_xml_expression_queryexpression_name_matches_entity_node()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                              </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.EntityName.Equals("contact"));
        }

        [Fact]
        public void When_translating_a_fetch_xml_expression_attributes_are_translated_to_a_list_of_columns()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                              </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.ColumnSet != null);
            Assert.Equal(false, query.ColumnSet.AllColumns);
            Assert.Equal(3, query.ColumnSet.Columns.Count);
            Assert.True(query.ColumnSet.Columns.Contains("fullname"));
            Assert.True(query.ColumnSet.Columns.Contains("telephone1"));
            Assert.True(query.ColumnSet.Columns.Contains("contactid"));
        }

        [Fact]
        public void When_translating_a_fetch_xml_expression_all_attributes_is_translated_to_a_columnset_with_all_columns()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <all-attributes />
                              </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.ColumnSet != null);
            Assert.Equal(true, query.ColumnSet.AllColumns);
        }

        [Fact]
        public void When_translating_a_fetch_xml_expression_orderby_ascending_is_correct()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                    <order attribute='fullname' descending='false' />
                              </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Orders != null);
            Assert.Equal(1, query.Orders.Count);
            Assert.Equal("fullname", query.Orders[0].AttributeName);
            Assert.Equal(Microsoft.Xrm.Sdk.Query.OrderType.Ascending, query.Orders[0].OrderType);
        }

        [Fact]
        public void When_translating_a_fetch_xml_expression_orderby_descending_is_correct()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                    <order attribute='fullname' descending='true' />
                              </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Orders != null);
            Assert.Equal(1, query.Orders.Count);
            Assert.Equal("fullname", query.Orders[0].AttributeName);
            Assert.Equal(Microsoft.Xrm.Sdk.Query.OrderType.Descending, query.Orders[0].OrderType);
        }

        [Fact]
        public void When_translating_a_fetch_xml_expression_2_orderby_elements_are_translated_correctly()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                    <order attribute='fullname' descending='true' />
                                    <order attribute = 'telephone1' descending = 'false' />
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Orders != null);
            Assert.Equal(2, query.Orders.Count);
            Assert.Equal("fullname", query.Orders[0].AttributeName);
            Assert.Equal(OrderType.Descending, query.Orders[0].OrderType);
            Assert.Equal("telephone1", query.Orders[1].AttributeName);
            Assert.Equal(OrderType.Ascending, query.Orders[1].OrderType);
        }

        

        [Fact]
        public void When_translating_a_fetch_xml_expression_nested_filters_are_correct()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='not-like' value='%Messi' />
                                                <filter type='or'>
                                                    <condition attribute='telephone1' operator='eq' value='123' />
                                                    <condition attribute='telephone1' operator='eq' value='234' />
                                                </filter>
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal(1, query.Criteria.Filters.Count);
            Assert.Equal(LogicalOperator.Or, query.Criteria.Filters[0].FilterOperator);
            Assert.Equal(2, query.Criteria.Filters[0].Conditions.Count);
        }

        [Fact]
        public void When_translating_a_linked_entity_right_result_is_returned()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"
                    <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                      <entity name='account'>
                        <attribute name='name' />
                        <attribute name='primarycontactid' />
                        <attribute name='telephone1' />
                        <attribute name='accountid' />
                        <order attribute='name' descending='false' />
                        <link-entity name='account' from='parentaccountid' to='accountid' alias='ab'>
                          <filter type='and'>
                            <condition attribute='name' operator='eq' value='MS' />
                          </filter>
                        </link-entity>
                      </entity>
                    </fetch>
                    ";


            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.LinkEntities != null);
            Assert.Equal(1, query.LinkEntities.Count);
            Assert.Equal("account", query.LinkEntities[0].LinkFromEntityName);
            Assert.Equal("parentaccountid", query.LinkEntities[0].LinkFromAttributeName);
            Assert.Equal("account", query.LinkEntities[0].LinkToEntityName);
            Assert.Equal("accountid", query.LinkEntities[0].LinkToAttributeName);
            Assert.Equal("ab", query.LinkEntities[0].EntityAlias);
            Assert.True(query.LinkEntities[0].LinkCriteria != null);
        }

        [Fact]
        public void When_translating_a_linked_entity_with_columnset_right_result_is_returned()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"
                    <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                      <entity name='account'>
                        <attribute name='name' />
                        <attribute name='primarycontactid' />
                        <attribute name='telephone1' />
                        <attribute name='accountid' />
                        <order attribute='name' descending='false' />
                        <link-entity name='account' from='parentaccountid' to='accountid' alias='ab'>
                          <attribute name='telephone2' />
                        </link-entity>
                      </entity>
                    </fetch>
                    ";


            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.LinkEntities != null);
            Assert.Equal(1, query.LinkEntities.Count);
            Assert.False(query.LinkEntities[0].Columns.AllColumns);
            Assert.Equal(1, query.LinkEntities[0].Columns.Columns.Count);
            Assert.Equal("telephone2", query.LinkEntities[0].Columns.Columns[0]);
        }

        [Fact]
        public void When_translating_a_linked_entity_with_columnset_with_all_attributes_right_result_is_returned()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"
                    <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                      <entity name='account'>
                        <attribute name='name' />
                        <attribute name='primarycontactid' />
                        <attribute name='telephone1' />
                        <attribute name='accountid' />
                        <order attribute='name' descending='false' />
                        <link-entity name='account' from='parentaccountid' to='accountid' alias='ab'>
                          <all-attributes />
                        </link-entity>
                      </entity>
                    </fetch>
                    ";


            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.LinkEntities != null);
            Assert.Equal(1, query.LinkEntities.Count);
            Assert.True(query.LinkEntities[0].Columns.AllColumns);
        }

        [Fact]
        public void When_translating_a_linked_entity_with_filters_right_result_is_returned()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"
                    <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                      <entity name='account'>
                        <attribute name='name' />
                        <attribute name='primarycontactid' />
                        <attribute name='telephone1' />
                        <attribute name='accountid' />
                        <order attribute='name' descending='false' />
                        <link-entity name='account' from='parentaccountid' to='accountid' alias='ab'>
                          <all-attributes />
                          <filter type='and'>
                                <condition attribute='fullname' operator='not-like' value='%Messi' />
                                    <filter type='or'>
                                        <condition attribute='telephone1' operator='eq' value='123' />
                                        <condition attribute='telephone1' operator='eq' value='234' />
                                    </filter>
                            </filter>
                        </link-entity>
                      </entity>
                    </fetch>
                    ";


            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.LinkEntities != null);
            Assert.Equal(1, query.LinkEntities.Count);
            Assert.True(query.LinkEntities[0].LinkCriteria != null);
            Assert.Equal(1, query.LinkEntities[0].LinkCriteria.Filters.Count);
            Assert.Equal(1, query.LinkEntities[0].LinkCriteria.Conditions.Count);
            Assert.Equal(2, query.LinkEntities[0].LinkCriteria.Filters[0].Conditions.Count);
        }


        [Fact]
        public void When_executing_fetchxml_right_result_is_returned()
        {
            //This will test a query expression is generated and executed

            var ctx = new XrmFakedContext();
            ctx.Initialize(new List<Entity>()
            {
                new Contact() {Id = Guid.NewGuid(), FirstName = "Leo Messi", Telephone1 = "123" }, //should be returned
                new Contact() {Id = Guid.NewGuid(), FirstName = "Leo Messi", Telephone1 = "234" }, //should be returned
                new Contact() {Id = Guid.NewGuid(), FirstName = "Leo", Telephone1 = "789" }, //shouldnt
                new Contact() {Id = Guid.NewGuid(), FirstName = "Andrés", Telephone1 = "123" }, //shouldnt
            });

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='firstname' operator='like' value='%Leo%' />
                                                <filter type='or'>
                                                    <condition attribute='telephone1' operator='eq' value='123' />
                                                    <condition attribute='telephone1' operator='eq' value='234' />
                                                </filter>
                                        </filter>
                                  </entity>
                            </fetch>";


            var retrieveMultiple = new RetrieveMultipleRequest()
            {
                Query = new FetchExpression(fetchXml)
            };

            var service = ctx.GetFakedOrganizationService();
            var response = service.Execute(retrieveMultiple) as RetrieveMultipleResponse;

            Assert.Equal(2, response.EntityCollection.Entities.Count);

            //Executing the same via ExecuteMultiple returns also the same
            var response2 = service.RetrieveMultiple(retrieveMultiple.Query);
            Assert.Equal(2, response2.Entities.Count);

        }

        [Fact]
        public void When_executing_fetchxml_with_count_attribute_only_that_number_of_results_is_returned()
        {
            //This will test a query expression is generated and executed

            var ctx = new XrmFakedContext();

            //Arrange
            var contactList = new List<Entity>();
            for(var i=0; i < 20; i++)
            {
                contactList.Add(new Contact() { Id = Guid.NewGuid() });
            }
            ctx.Initialize(contactList);

            //Act
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' count='7'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' /> 
                                  </entity>
                            </fetch>";


            var retrieveMultiple = new RetrieveMultipleRequest()
            {
                Query = new FetchExpression(fetchXml)
            };

            var service = ctx.GetFakedOrganizationService();
            var response = service.Execute(retrieveMultiple) as RetrieveMultipleResponse;

            Assert.Equal(7, response.EntityCollection.Entities.Count);
        }

        [Fact]
        public void When_filtering_by_a_guid_attribute_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var accountId = Guid.NewGuid();
            var license1 = new Entity("pl_license");
            license1.Id = Guid.NewGuid();
            license1.Attributes["pl_no"] = 1;
            license1.Attributes["pl_accountid"] = new EntityReference("account", accountId);

            var license2 = new Entity("pl_license");
            license2.Id = Guid.NewGuid();
            license2.Attributes["pl_no"] = 2;
            license2.Attributes["pl_accountid"] = new EntityReference("account", accountId);

            context.Initialize(new List<Entity> { license1, license2 });

            var fetchXml =
                 "<fetch>" +
                 "  <entity name='pl_license'>" +
                 "     <attribute name='pl_no'/>" +
                 "     <filter type='and'>" +
                 "         <condition attribute='pl_accountid' operator='eq' value='{0}' />" +
                 "     </filter>" +
                 "  </entity>" +
                 "</fetch>";
            fetchXml = string.Format(fetchXml, accountId);
            var rows = context.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));
            Assert.Equal(rows.Entities.Count, 2);
        }

        [Fact]
        public void When_querying_the_same_entity_records_with_joins_no_collection_modified_exception_is_thrown()
        {
            var fakedContext = new XrmFakedContext { };
            var service = fakedContext.GetFakedOrganizationService();

            var entityAccount = new Account { Id = Guid.NewGuid(), Name = "My First Faked Account yeah!", LogicalName = "account" };
            var entityContact = new Contact { Id = Guid.NewGuid(), ParentCustomerId = entityAccount.ToEntityReference() };

            var entityBusinessUnit = new BusinessUnit { Name = "TestBU", BusinessUnitId = Guid.NewGuid() };

            var initiatingUser = new SystemUser
            {
                Id = Guid.NewGuid(),
                FirstName = "TestUser",
                DomainName = "TestDomain",
                BusinessUnitId = entityBusinessUnit.ToEntityReference()
            };

            fakedContext.Initialize(new List<Entity>() {
               entityBusinessUnit,entityAccount,entityContact,initiatingUser
            });


            var fetchXml = @"
                    <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                      <entity name='systemuser'>
                        <attribute name='fullname' />
                         <attribute name='systemuserid' />
                         <attribute name='businessunitid' />
                         <filter type='and'>
                          <condition attribute='systemuserid' operator='eq' uitype='systemuser' value='#userId#' />
                         </filter>
                            <link-entity name='businessunit' from='businessunitid' to='businessunitid' alias='bu' intersect='true' >
                                <attribute name='name' />
                            </link-entity>
                      </entity>
                    </fetch>
                ";

            var UserRequest = new RetrieveMultipleRequest { Query = new FetchExpression(fetchXml.Replace("#userId#", initiatingUser.Id.ToString())) };
            var response = ((RetrieveMultipleResponse)service.Execute(UserRequest));

            var entities = response.EntityCollection.Entities;
            Assert.True(entities.Count == 1);
            Assert.True(entities[0].Attributes.ContainsKey("bu.name"));
            Assert.IsType<AliasedValue>(entities[0]["bu.name"]);
            Assert.Equal("TestBU", (entities[0]["bu.name"] as AliasedValue).Value.ToString());
        }


    }
}
