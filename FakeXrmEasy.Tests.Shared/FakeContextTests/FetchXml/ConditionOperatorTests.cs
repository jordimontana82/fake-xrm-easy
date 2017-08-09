using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.FetchXml
{
    /// <summary>
    /// To test all different condition operators found in FetchXml
    /// https://msdn.microsoft.com/en-us/library/gg309405.aspx

    /*
     <xs:simpleType name="operator">
    <xs:restriction base="xs:NMTOKEN">

        DONE:

      <xs:enumeration value="eq" />
      <xs:enumeration value="neq" />
      <xs:enumeration value="ne" />
      <xs:enumeration value="like" />
      <xs:enumeration value="not-like" />
      <xs:enumeration value="begins-with" />
      <xs:enumeration value="not-begin-with" />
      <xs:enumeration value="ends-with" />
      <xs:enumeration value="not-end-with" />
      <xs:enumeration value="in" />
      <xs:enumeration value="not-in" />
      <xs:enumeration value="null" />
      <xs:enumeration value="not-null" />
      <xs:enumeration value="gt" />
      <xs:enumeration value="ge" />
      <xs:enumeration value="le" />
      <xs:enumeration value="lt" />
      <xs:enumeration value="on" />
      <xs:enumeration value="on-or-before" />
      <xs:enumeration value="on-or-after" />
      <xs:enumeration value="yesterday" />
      <xs:enumeration value="today" />
      <xs:enumeration value="tomorrow" />
      <xs:enumeration value="between" />
      <xs:enumeration value="not-between" />
      <xs:enumeration value="eq-userid" />
      <xs:enumeration value="ne-userid" />
      <xs:enumeration value="olderthan-x-months" />

    TODO:

      DATEs:
      <xs:enumeration value="last-seven-days" />
      <xs:enumeration value="next-seven-days" />
      <xs:enumeration value="last-week" />
      <xs:enumeration value="this-week" />
      <xs:enumeration value="next-week" />
      <xs:enumeration value="last-month" />
      <xs:enumeration value="this-month" />
      <xs:enumeration value="next-month" />
      <xs:enumeration value="last-year" />
      <xs:enumeration value="this-year" />
      <xs:enumeration value="next-year" />
      <xs:enumeration value="last-x-hours" />
      <xs:enumeration value="next-x-hours" />
      <xs:enumeration value="last-x-days" />
      <xs:enumeration value="next-x-days" />
      <xs:enumeration value="last-x-weeks" />
      <xs:enumeration value="next-x-weeks" />
      <xs:enumeration value="last-x-months" />
      <xs:enumeration value="next-x-months" />     
      <xs:enumeration value="olderthan-x-years" />
      <xs:enumeration value="olderthan-x-weeks" />
      <xs:enumeration value="olderthan-x-days" />
      <xs:enumeration value="olderthan-x-hours" />
      <xs:enumeration value="olderthan-x-minutes" />
      <xs:enumeration value="last-x-years" />
      <xs:enumeration value="next-x-years" />


      <xs:enumeration value="eq-userteams" />
      <xs:enumeration value="eq-useroruserteams" />
      <xs:enumeration value="eq-useroruserhierarchy" />
      <xs:enumeration value="eq-useroruserhierarchyandteams" />
      <xs:enumeration value="eq-businessid" />
      <xs:enumeration value="ne-businessid" />
      <xs:enumeration value="eq-userlanguage" />

      
      <xs:enumeration value="this-fiscal-year" />
      <xs:enumeration value="this-fiscal-period" />
      <xs:enumeration value="next-fiscal-year" />
      <xs:enumeration value="next-fiscal-period" />
      <xs:enumeration value="last-fiscal-year" />
      <xs:enumeration value="last-fiscal-period" />
      <xs:enumeration value="last-x-fiscal-years" />
      <xs:enumeration value="last-x-fiscal-periods" />
      <xs:enumeration value="next-x-fiscal-years" />
      <xs:enumeration value="next-x-fiscal-periods" />
      <xs:enumeration value="in-fiscal-year" />
      <xs:enumeration value="in-fiscal-period" />
      <xs:enumeration value="in-fiscal-period-and-year" />
      <xs:enumeration value="in-or-before-fiscal-period-and-year" />
      <xs:enumeration value="in-or-after-fiscal-period-and-year" />

    
      HIERACHY OPERATORS:
      <xs:enumeration value="under"/>
      <xs:enumeration value="eq-or-under" />
      <xs:enumeration value="not-under"/>
      <xs:enumeration value="above" />
      <xs:enumeration value="eq-or-above" />

    </xs:restriction>
  </xs:simpleType>
      
     
     */
    /// </summary>
    public class ConditionOperatorTests
    {
        [Fact]
        public void FetchXml_Operator_Eq()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='eq' value='Messi' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.Equal, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_Ne()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='ne' value='Messi' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NotEqual, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_Neq()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='neq' value='Messi' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NotEqual, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_Like()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='like' value='%Messi%' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.Contains, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_Like_As_BeginsWith()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='like' value='Messi%' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.BeginsWith, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_BeginsWith()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='begins-with' value='Messi' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.BeginsWith, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_NotBeginWith()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='not-begin-with' value='Messi' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.DoesNotBeginWith, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_Like_As_EndsWith()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='like' value='%Messi' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.EndsWith, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_EndsWith()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='ends-with' value='Messi' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.EndsWith, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_NotEndWith()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='not-end-with' value='Messi' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.DoesNotEndWith, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_NotLike()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='not-like' value='%Messi%' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.DoesNotContain, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_NotLike_As_Not_BeginWith()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='not-like' value='Messi%' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.DoesNotBeginWith, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_NotLike_As_Not_EndWith()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='not-like' value='%Messi' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.DoesNotEndWith, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
        }

        [Fact]
        public void FetchXml_Operator_In()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='in'>
                                                <value>Messi</value>
                                                <value>Iniesta</value>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.In, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
            Assert.Equal("Iniesta", query.Criteria.Conditions[0].Values[1].ToString());
        }

        [Fact]
        public void FetchXml_Operator_NotIn()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='not-in'>
                                                <value>Messi</value>
                                                <value>Iniesta</value>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NotIn, query.Criteria.Conditions[0].Operator);
            Assert.Equal("Messi", query.Criteria.Conditions[0].Values[0].ToString());
            Assert.Equal("Iniesta", query.Criteria.Conditions[0].Values[1].ToString());
        }

        [Fact]
        public void FetchXml_Operator_Null()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='null' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.Null, query.Criteria.Conditions[0].Operator);
            Assert.Equal(0, query.Criteria.Conditions[0].Values.Count);
        }

        [Fact]
        public void FetchXml_Operator_NotNull()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='fullname' operator='not-null' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("fullname", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NotNull, query.Criteria.Conditions[0].Operator);
            Assert.Equal(0, query.Criteria.Conditions[0].Values.Count);
        }

        [Fact]
        public void FetchXml_Operator_Gt_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='address1_longitude' operator='gt' value='1.2345' />
                                        </filter>
                                  </entity>
                            </fetch>";



            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("address1_longitude", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.GreaterThan, query.Criteria.Conditions[0].Operator);
            Assert.Equal(1.2345, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Gt_Execution()
        {
            var ctx = new XrmFakedContext();

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='address1_longitude' />
                                        <filter type='and'>
                                            <condition attribute='address1_longitude' operator='gt' value='1.2345' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var ct1 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.23 };
            var ct2 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.33 };
            var ct3 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.2345 };
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            Assert.Equal(1.33, collection.Entities[0]["address1_longitude"]);
        }

        [Fact]
        public void FetchXml_Operator_Ge_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='address1_longitude' operator='ge' value='1.2345' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var ct = new Contact();

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("address1_longitude", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.GreaterEqual, query.Criteria.Conditions[0].Operator);
            Assert.Equal(1.2345, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Months_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='olderthan-x-months' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var ct = new Contact();

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXMonths, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Ge_Execution()
        {
            var ctx = new XrmFakedContext();

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='address1_longitude' />
                                        <filter type='and'>
                                            <condition attribute='address1_longitude' operator='ge' value='1.2345' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var ct1 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.23 };
            var ct2 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.33 };
            var ct3 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.2345 };
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(2, collection.Entities.Count);
            Assert.Equal(1.33, collection.Entities[0]["address1_longitude"]);
            Assert.Equal(1.2345, collection.Entities[1]["address1_longitude"]);
        }

        [Fact]
        public void FetchXml_Operator_Lt_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='address1_longitude' operator='lt' value='1.2345' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var ct = new Contact();

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("address1_longitude", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LessThan, query.Criteria.Conditions[0].Operator);
            Assert.Equal(1.2345, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Lt_Execution()
        {
            var ctx = new XrmFakedContext();

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='address1_longitude' />
                                        <filter type='and'>
                                            <condition attribute='address1_longitude' operator='lt' value='1.2345' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var ct1 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.23 };
            var ct2 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.33 };
            var ct3 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.2345 };
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            Assert.Equal(1.23, collection.Entities[0]["address1_longitude"]);
        }

        [Fact]
        public void FetchXml_Operator_Le_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='address1_longitude' operator='le' value='1.2345' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("address1_longitude", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LessEqual, query.Criteria.Conditions[0].Operator);
            Assert.Equal(1.2345, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Le_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='address1_longitude' />
                                        <filter type='and'>
                                            <condition attribute='address1_longitude' operator='le' value='1.2345' />
                                        </filter>
                                  </entity>
                            </fetch>";
            var ct1 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.23 };
            var ct2 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.33 };
            var ct3 = new Contact() { Id = Guid.NewGuid(), Address1_Longitude = 1.2345 };
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(2, collection.Entities.Count);
            Assert.Equal(1.23, collection.Entities[0]["address1_longitude"]);
            Assert.Equal(1.2345, collection.Entities[1]["address1_longitude"]);
        }

        [Fact]
        public void FetchXml_Operator_On_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='on' value='2014-11-23' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);

            var date = query.Criteria.Conditions[0].Values[0] as DateTime?;

            Assert.Equal(ConditionOperator.On, query.Criteria.Conditions[0].Operator);
            Assert.Equal(2014, date.Value.Year);
            Assert.Equal(11, date.Value.Month);
            Assert.Equal(23, date.Value.Day);
        }

        [Fact]
        public void FetchXml_Operator_On_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='on' value='2014-11-23' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var date = new DateTime(2014, 11, 23);
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date };
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(1) };
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            var retrievedDate = collection.Entities[0]["anniversary"] as DateTime?;
            Assert.Equal(2014, retrievedDate.Value.Year);
            Assert.Equal(11, retrievedDate.Value.Month);
            Assert.Equal(23, retrievedDate.Value.Day);
        }


        [Fact]
        public void FetchXml_Operator_On_Or_Before_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='on-or-before' value='2014-11-23' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var date = new DateTime(2014, 11, 23);
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-1) }; //Should be returned
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(1) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(2, collection.Entities.Count);
            var retrievedDateFirst = collection.Entities[0]["anniversary"] as DateTime?;
            var retrievedDateSecond = collection.Entities[1]["anniversary"] as DateTime?;
            Assert.Equal(23, retrievedDateFirst.Value.Day);
            Assert.Equal(22, retrievedDateSecond.Value.Day);
        }

        [Fact]
        public void FetchXml_Operator_On_Or_After_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='on-or-after' value='2014-11-23' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var date = new DateTime(2014, 11, 23);
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(1) }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(2, collection.Entities.Count);
            var retrievedDateFirst = collection.Entities[0]["anniversary"] as DateTime?;
            var retrievedDateSecond = collection.Entities[1]["anniversary"] as DateTime?;
            Assert.Equal(23, retrievedDateFirst.Value.Day);
            Assert.Equal(24, retrievedDateSecond.Value.Day);
        }

        [Fact]
        public void FetchXml_Operator_Today_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='today' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var date = DateTime.Today;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-1) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);

            var retrievedDate = collection.Entities[0]["anniversary"] as DateTime?;
            Assert.Equal(retrievedDate, DateTime.Today);
        }

        [Fact]
        public void FetchXml_Operator_Yesterday_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='yesterday' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var date = DateTime.Today;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date }; //Shouldnt 
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-1) }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);

            var retrievedDate = collection.Entities[0]["anniversary"] as DateTime?;
            Assert.Equal(retrievedDate, DateTime.Today.AddDays(-1));
        }

        [Fact]
        public void FetchXml_Operator_Tomorrow_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='tomorrow' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var date = DateTime.Today;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date }; //Shouldnt 
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(1) }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);

            var retrievedDate = collection.Entities[0]["anniversary"] as DateTime?;
            Assert.Equal(retrievedDate, DateTime.Today.AddDays(1));
        }

        [Fact]
        public void FetchXml_Operator_Between_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='between'>
                                                <value>2013-05-17 00:00:00</value>
				                                <value>2013-05-20 14:40:00</value>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.Between, query.Criteria.Conditions[0].Operator);
            Assert.Equal(new DateTime(2013, 5, 17), query.Criteria.Conditions[0].Values[0]);
            Assert.Equal(new DateTime(2013, 5, 20, 14, 40, 0), query.Criteria.Conditions[0].Values[1]);
        }

        [Fact]
        public void FetchXml_Operator_Between_Execution_Without_Exact_Values_Raises_Exception()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetFakedOrganizationService();

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='between'>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            Assert.Throws<Exception>(() => service.RetrieveMultiple(new FetchExpression(fetchXml)));

            fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='between'>
                                                <value>2013-05-17 00:00:00</value>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            Assert.Throws<Exception>(() => service.RetrieveMultiple(new FetchExpression(fetchXml)));
        }

        [Fact]
        public void FetchXml_Operator_Between_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='between'>
                                                <value>2013-05-17 00:00:00</value>
				                                <value>2013-05-20 14:40:00</value>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            var date = DateTime.Today;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date }; //Shouldnt 
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(2013, 05, 19) }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);

            var retrievedDate = collection.Entities[0]["anniversary"] as DateTime?;
            Assert.Equal(retrievedDate, new DateTime(2013, 05, 19));
        }

        [Fact]
        public void FetchXml_Operator_NotBetween_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='not-between'>
                                                <value>2013-05-17 00:00:00</value>
				                                <value>2013-05-20 14:40:00</value>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NotBetween, query.Criteria.Conditions[0].Operator);
            Assert.Equal(new DateTime(2013, 5, 17), query.Criteria.Conditions[0].Values[0]);
            Assert.Equal(new DateTime(2013, 5, 20, 14, 40, 0), query.Criteria.Conditions[0].Values[1]);
        }

        [Fact]
        public void FetchXml_Operator_NotBetween_Execution_Without_Exact_Values_Raises_Exception()
        {
            var ctx = new XrmFakedContext();
            var service = ctx.GetFakedOrganizationService();

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='not-between'>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            Assert.Throws<Exception>(() => service.RetrieveMultiple(new FetchExpression(fetchXml)));

            fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='not-between'>
                                                <value>2013-05-17 00:00:00</value>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            Assert.Throws<Exception>(() => service.RetrieveMultiple(new FetchExpression(fetchXml)));
        }

        [Fact]
        public void FetchXml_Operator_NotBetween_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='not-between'>
                                                <value>2013-05-17 00:00:00</value>
				                                <value>2013-05-20 14:40:00</value>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            var date = DateTime.Today;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date }; //Should
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(2013, 05, 19) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetFakedOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);

            var retrievedDate = collection.Entities[0]["anniversary"] as DateTime?;
            Assert.Equal(retrievedDate, date);
        }

        [Fact]
        public void FetchXml_Operator_EqUserId_Translation()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='systemuser'>
                                        <filter type='and'>
                                            <condition attribute='systemuserid' operator='eq-userid' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("systemuserid", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.EqualUserId, query.Criteria.Conditions[0].Operator);
            Assert.Equal(0, query.Criteria.Conditions[0].Values.Count);
        }

        [Fact]
        public void FetchXml_Operator_EqUserId_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='systemuser'>
                                        <filter type='and'>
                                            <condition attribute='systemuserid' operator='eq-userid' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var su1 = new SystemUser() { Id = Guid.NewGuid() }; //Should
            var su2 = new SystemUser() { Id = Guid.NewGuid() }; //Shouldnt
            ctx.Initialize(new[] { su1, su2 });

            var service = ctx.GetOrganizationService();
            ctx.CallerId = su1.ToEntityReference();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, su1.Id);
        }

        [Fact]
        public void FetchXml_Operator_NeUserId_Translation()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='systemuser'>
                                        <filter type='and'>
                                            <condition attribute='systemuserid' operator='ne-userid' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("systemuserid", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NotEqualUserId, query.Criteria.Conditions[0].Operator);
            Assert.Equal(0, query.Criteria.Conditions[0].Values.Count);
        }

        [Fact]
        public void FetchXml_Operator_NeUserId_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='systemuser'>
                                        <filter type='and'>
                                            <condition attribute='systemuserid' operator='ne-userid' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var su1 = new SystemUser() { Id = Guid.NewGuid() }; //Shouldnt
            var su2 = new SystemUser() { Id = Guid.NewGuid() }; //Should
            ctx.Initialize(new[] { su1, su2 });

            var service = ctx.GetOrganizationService();
            ctx.CallerId = su1.ToEntityReference();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, su2.Id);
        }

#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365

        [Fact]
        public void FetchXml_EntityName_Attribute_Translation()
        {
            var ctx = new XrmFakedContext();
            var quote = new Quote { Id = Guid.NewGuid() };

            string fetchXml =
                $@"<fetch>
                    <entity name='quotedetail' >
                    <link-entity name='product' from='productid' to='productid' link-type='inner' alias='product' >
                        <attribute name='currentcost' />
                    </link-entity>
                    <filter type='and'>
                        <condition entityname='product' attribute='currentcost' operator='null' />
                    </filter>
                    </entity>
                </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("currentcost", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal("product", query.Criteria.Conditions[0].EntityName);
            Assert.Equal(ConditionOperator.Null, query.Criteria.Conditions[0].Operator);
            Assert.Equal(0, query.Criteria.Conditions[0].Values.Count);

        }

        [Fact]
        public void FetchXml_EntityName_Attribute_Execution()
        {
            var ctx = new XrmFakedContext();
            var quote = new Quote { Id = Guid.NewGuid() };

            string fetchXml =
                $@"<fetch>
                    <entity name='quotedetail' >
                    <link-entity name='product' from='productid' to='productid' link-type='inner' alias='product' >
                        <attribute name='currentcost' />
                    </link-entity>
                    <filter type='and'>
                        <condition attribute='quoteid' operator='eq' value='{quote.Id}' />
                        <condition entityname='product' attribute='currentcost' operator='null' />
                    </filter>
                    </entity>
                </fetch>";

            var product = new Product
            {
                Id = Guid.NewGuid(),
                CurrentCost = new Money(100)
            };

            var quoteProduct = new QuoteDetail
            {
                Id = Guid.NewGuid(),
                ProductId = new EntityReference("product", product.Id),
                Quantity = 4M,
                QuoteId = new EntityReference("quote", quote.Id)
            };

            ctx.Initialize(new List<Entity>() {
                product, quote, quoteProduct
            });

            var collection = ctx.GetFakedOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(0, collection.Entities.Count);
        }
#endif

    }
}
