using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
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
      <xs:enumeration value="last-seven-days" />
      <xs:enumeration value="next-x-weeks" />
      <xs:enumeration value="next-seven-days" />
      <xs:enumeration value="last-week" />
      <xs:enumeration value="this-week" />
      <xs:enumeration value="next-week" />
      <xs:enumeration value="last-year" />
      <xs:enumeration value="this-year" />
      <xs:enumeration value="next-year" />
      <xs:enumeration value="last-month" />
      <xs:enumeration value="this-month" />
      <xs:enumeration value="next-month" />
      <xs:enumeration value="last-x-hours" />
      <xs:enumeration value="next-x-hours" />
      <xs:enumeration value="last-x-days" />
      <xs:enumeration value="next-x-days" />
      <xs:enumeration value="last-x-weeks" />    
      <xs:enumeration value="last-x-months" />
      <xs:enumeration value="next-x-months" />     
      <xs:enumeration value="last-x-years" />
      <xs:enumeration value="next-x-years" />
      <xs:enumeration value="olderthan-x-years" />
      <xs:enumeration value="olderthan-x-weeks" />
      <xs:enumeration value="olderthan-x-days" />
      <xs:enumeration value="olderthan-x-hours" />
      <xs:enumeration value="olderthan-x-minutes" />  

    TODO:

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

#if FAKE_XRM_EASY_9
        [Fact]
        public void FetchXml_Operator_In_MultiSelectOptionSet()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                        <filter type='and'>
                                            <condition attribute='new_multiselectattribute' operator='in'>
                                                <value>1</value>
                                                <value>2</value>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("new_multiselectattribute", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.In, query.Criteria.Conditions[0].Operator);
            Assert.Equal(1, query.Criteria.Conditions[0].Values[0]);
            Assert.Equal(2, query.Criteria.Conditions[0].Values[1]);
        }

        [Fact]
        public void FetchXml_Operator_NotIn_MultiSelectOptionSet()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                        <filter type='and'>
                                            <condition attribute='new_multiselectattribute' operator='not-in'>
                                                <value>1</value>
                                                <value>2</value>
                                            </condition>
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("new_multiselectattribute", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NotIn, query.Criteria.Conditions[0].Operator);
            Assert.Equal(1, query.Criteria.Conditions[0].Values[0]);
            Assert.Equal(2, query.Criteria.Conditions[0].Values[1]);
        }
#endif

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
            var service = ctx.GetOrganizationService();

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

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXMonths, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Months_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='olderthan-x-months' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXMonths, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMonths(-1) }; //Shouldnt
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMonths(1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMonths(-3) }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct3.Id);
        }

#if !FAKE_XRM_EASY && !FAKE_XRM_EASY_2013
        [Fact]
        public void FetchXml_Operator_Older_Than_X_Minutes_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='olderthan-x-minutes' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXMinutes, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Minutes_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='olderthan-x-minutes' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXMinutes, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMinutes(-1) }; //Shouldnt
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMinutes(1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMinutes(-3) }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct3.Id);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Hours_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='olderthan-x-hours' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXHours, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Hours_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='olderthan-x-hours' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXHours, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddHours(-1) }; //Shouldnt
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddHours(1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddHours(-3) }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct3.Id);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Days_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='olderthan-x-days' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXDays, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Days_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='olderthan-x-days' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXDays, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-1) }; //Shouldnt
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-3) }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct3.Id);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Weeks_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='olderthan-x-weeks' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXWeeks, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Weeks_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='olderthan-x-weeks' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXWeeks, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-7) }; //Shouldnt
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(7) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-21) }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct3.Id);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Years_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='olderthan-x-years' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXYears, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Older_Than_X_Years_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='olderthan-x-years' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.OlderThanXYears, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddYears(-1) }; //Shouldnt
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddYears(1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddYears(-3) }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct3.Id);
        }
#endif

        [Fact]
        public void FetchXml_Operator_Last_Seven_Days_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='createdon' operator='last-seven-days' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("createdon", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.Last7Days, query.Criteria.Conditions[0].Operator);
            Assert.Equal(0, query.Criteria.Conditions[0].Values.Count);
        }

        [Fact]
        public void FetchXml_Operator_Last_Seven_Days_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='last-seven-days' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var date = DateTime.UtcNow;

            var ct1 = new Contact() { Id = Guid.NewGuid(), BirthDate = date.AddDays(-1) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), BirthDate = date.AddDays(-8) }; //Shouldn't be returned
            var ct3 = new Contact() { Id = Guid.NewGuid(), BirthDate = date.AddDays(1) }; //Shouldn't be returned
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            Assert.Equal(ct1.Id, collection.Entities[0].Id);
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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

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
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);

            var retrievedDate = collection.Entities[0]["anniversary"] as DateTime?;
            Assert.Equal(retrievedDate, date);
        }

        [Fact]
        public void FetchXml_Operator_ThisYear_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='this-year' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var today = DateTime.Today;
            var thisYear = today.Year;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = today }; //Today - Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, 1, 1) };        // First day of this year - should be returned
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, 12, 31) };      // Last day of this year - should be returned
            var ct4 = new Contact() { Id = Guid.NewGuid(), Anniversary = today.AddYears(-1) };                  // One year ago - should not be returned
            var ct5 = new Contact() { Id = Guid.NewGuid(), Anniversary = today.AddYears(1) };                   // One year in the future - should not be returned
            var ct6 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear + 1, 1, 1) };      // First day of next year - should not be returned
            var ct7 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear - 1, 12, 31) };    // Last day of last year - should not be returned
            ctx.Initialize(new[] { ct1, ct2, ct3, ct4, ct5, ct6, ct7 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);

            Assert.Equal(((DateTime)collection.Entities[0]["anniversary"]).Year, thisYear);
            Assert.Equal(((DateTime)collection.Entities[1]["anniversary"]).Year, thisYear);
            Assert.Equal(((DateTime)collection.Entities[2]["anniversary"]).Year, thisYear);
        }

        [Fact]
        public void FetchXml_Operator_InFiscalYear_Execution()
        {
            var today = DateTime.Today;
            var thisYear = today.Year;

            var ctx = new XrmFakedContext();
            ctx.FiscalYearSettings = new FiscalYearSettings() { StartDate = new DateTime(thisYear, 1, 2), FiscalPeriodTemplate = FiscalYearSettings.Template.Annually };
            var fetchXml = $@"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='in-fiscal-year' value='{thisYear}' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, 1, 2) };        // Second day of this year - should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, 12, 31) };      // Last day of this year - should be returned
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear + 1, 1, 2) };      // Second day of next year - should not be returned
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(2, collection.Entities.Count);

            Assert.Equal(((DateTime)collection.Entities[0]["anniversary"]).Year, thisYear);
            Assert.Equal(((DateTime)collection.Entities[1]["anniversary"]).Year, thisYear);
        }

        [Fact]
        public void FetchXml_Operator_ThisMonth_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='this-month' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var today = DateTime.Today;
            var thisYear = today.Year;
            var thisMonth = today.Month;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = today }; //Today - Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1) };                            // First day of this month - should be returned
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddMonths(1).AddDays(-1) };   // Last day of this month - should be returned
            var ct4 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddMonths(1) };               // First day of next month - should not be returned
            var ct5 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddDays(-1) };                // Last day of previous month - should not be returned
            var ct6 = new Contact() { Id = Guid.NewGuid(), Anniversary = today.AddYears(1) };                                               // One year in the future - should not be returned
            var ct7 = new Contact() { Id = Guid.NewGuid(), Anniversary = today.AddYears(1) };                                               // One year in the past - should not be returned
            ctx.Initialize(new[] { ct1, ct2, ct3, ct4, ct5, ct6, ct7 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);

            Assert.Equal(((DateTime)collection.Entities[0]["anniversary"]).Month, thisMonth);
            Assert.Equal(((DateTime)collection.Entities[1]["anniversary"]).Month, thisMonth);
            Assert.Equal(((DateTime)collection.Entities[2]["anniversary"]).Month, thisMonth);
        }

        [Fact]
        public void FetchXml_Operator_LastMonth_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='last-month' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var today = DateTime.Today;
            var thisYear = today.Year;
            var thisMonth = today.Month;
            var lastMonth = new DateTime(thisYear, thisMonth, 1).AddMonths(-1).Month;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = today };                                                           // Today - Should not be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1) };                            // First day of this month - should not be returned
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddMonths(1).AddDays(-1) };   // Last day of this month - should not be returned
            var ct4 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddMonths(1) };               // First day of next month - should not be returned
            var ct5 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddMonths(-1) };              // First day of last month - should be returned
            var ct6 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddDays(-1) };                // Last day of last month - should be returned
            ctx.Initialize(new[] { ct1, ct2, ct3, ct4, ct5, ct6 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(2, collection.Entities.Count);

            Assert.Equal(((DateTime)collection.Entities[0]["anniversary"]).Month, lastMonth);
            Assert.Equal(((DateTime)collection.Entities[1]["anniversary"]).Month, lastMonth);
        }

        [Fact]
        public void FetchXml_Operator_NextMonth_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='next-month' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var today = DateTime.Today;
            var thisYear = today.Year;
            var thisMonth = today.Month;
            var nextMonth = new DateTime(thisYear, thisMonth, 1).AddMonths(1).Month;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = today };                                                           // Today - Should not be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1) };                            // First day of this month - should not be returned
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddMonths(1).AddDays(-1) };   // Last day of this month - should not be returned
            var ct4 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddMonths(1) };               // First day of next month - should be returned 
            var ct5 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddMonths(2).AddDays(-1) };   // Last day of next month - should be returned 
            var ct6 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddDays(-1) };                // Last day of last month - should not be returned
            var ct7 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, thisMonth, 1).AddMonths(-1) };              // First day of last month - should not be returned

            ctx.Initialize(new[] { ct1, ct2, ct3, ct4, ct5, ct6, ct7 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(2, collection.Entities.Count);

            Assert.Equal(((DateTime)collection.Entities[0]["anniversary"]).Month, nextMonth);
            Assert.Equal(((DateTime)collection.Entities[1]["anniversary"]).Month, nextMonth);
        }

        [Fact]
        public void FetchXml_Operator_LastYear_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='last-year' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var today = DateTime.Today;
            var thisYear = today.Year;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = today };                               //Today - Should not be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, 1, 1) };        // First day of this year - should not be returned
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, 12, 31) };      // Last day of this year - should not be returned
            var ct4 = new Contact() { Id = Guid.NewGuid(), Anniversary = today.AddYears(-1) };                  // One year ago - should be returned
            var ct5 = new Contact() { Id = Guid.NewGuid(), Anniversary = today.AddYears(1) };                   // One year in the future - should not be returned
            var ct6 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear - 1, 1, 1) };    // First day of last year - should be returned
            var ct7 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear - 1, 12, 31) };  // Last day of last year - should be returned
            ctx.Initialize(new[] { ct1, ct2, ct3, ct4, ct5, ct6, ct7 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);

            Assert.Equal(((DateTime)collection.Entities[0]["anniversary"]).Year, thisYear - 1);
            Assert.Equal(((DateTime)collection.Entities[1]["anniversary"]).Year, thisYear - 1);
            Assert.Equal(((DateTime)collection.Entities[2]["anniversary"]).Year, thisYear - 1);
        }

        [Fact]
        public void FetchXml_Operator_NextYear_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='next-year' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var today = DateTime.Today;
            var thisYear = today.Year;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = today };                               //Today - Should not be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, 1, 1) };        // First day of this year - should not be returned
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear, 12, 31) };      // Last day of this year - should not be returned
            var ct4 = new Contact() { Id = Guid.NewGuid(), Anniversary = today.AddYears(1) };                   // One year from now - should be returned
            var ct5 = new Contact() { Id = Guid.NewGuid(), Anniversary = today.AddYears(-1) };                  // One year in the past - should not be returned
            var ct6 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear + 1, 1, 1) };    // First day of next year - should be returned
            var ct7 = new Contact() { Id = Guid.NewGuid(), Anniversary = new DateTime(thisYear + 1, 12, 31) };  // Last day of next year - should be returned
            ctx.Initialize(new[] { ct1, ct2, ct3, ct4, ct5, ct6, ct7 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(3, collection.Entities.Count);

            Assert.Equal(((DateTime)collection.Entities[0]["anniversary"]).Year, thisYear + 1);
            Assert.Equal(((DateTime)collection.Entities[1]["anniversary"]).Year, thisYear + 1);
            Assert.Equal(((DateTime)collection.Entities[2]["anniversary"]).Year, thisYear + 1);
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
        [Fact]
        public void FetchXml_Operator_Next_X_Weeks_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='anniversary' />
                                        <filter type='and'>
                                            <condition attribute='anniversary' operator='next-x-weeks' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var date = DateTime.Now;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(7 * 2) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(7 * 4) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            var retrievedDateFirst = collection.Entities[0]["anniversary"] as DateTime?;
            //var retrievedDateSecond = collection.Entities[1]["anniversary"] as DateTime?;
            //Assert.Equal(23, retrievedDateFirst.Value.Day);
            //Assert.Equal(22, retrievedDateSecond.Value.Day);
        }
        [Fact]
        public void FetchXml_Operator_Next_Seven_Days_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='next-seven-days' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.Next7Days, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow.AddMinutes(1);
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(3) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(7) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(8) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }
        [Fact]
        public void FetchXml_Operator_Last_Week_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='last-week' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastWeek, query.Criteria.Conditions[0].Operator);

            var date = DateTime.Now;
            var weekOfYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
            var lastWeek = weekOfYear - 1;

            Func<int, DateTime> getRandomDateOfWeek = (week) =>
            {
                Random rnd = new Random();
                DateTime d = new DateTime();
                do
                {
                    d = date.AddDays(rnd.Next(-10, 10));
                }
                while (CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d
                    , CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule
                    , CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                    != week);
                return d;
            };

            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = getRandomDateOfWeek(lastWeek) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = getRandomDateOfWeek(weekOfYear) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }
        [Fact]
        public void FetchXml_Operator_This_Week_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='this-week' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.ThisWeek, query.Criteria.Conditions[0].Operator);

            var date = DateTime.Now;
            var weekOfYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
            var lastWeek = weekOfYear - 1;

            Func<int, DateTime> getRandomDateOfWeek = (week) =>
            {
                Random rnd = new Random();
                DateTime d = new DateTime();
                do
                {
                    d = date.AddDays(rnd.Next(-10, 10));
                }
                while (CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d
                    , CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule
                    , CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                    != week);
                return d;
            };

            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = getRandomDateOfWeek(weekOfYear) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = getRandomDateOfWeek(lastWeek) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }
        [Fact]
        public void FetchXml_Operator_Next_Week_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='next-week' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NextWeek, query.Criteria.Conditions[0].Operator);

            var date = DateTime.Now;
            var weekOfYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
            var nextWeek = weekOfYear + 1;

            Func<int, DateTime> getRandomDateOfWeek = (week) =>
            {
                Random rnd = new Random();
                DateTime d = new DateTime();
                do
                {
                    d = date.AddDays(rnd.Next(-10, 10));
                }
                while (CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d
                    , CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule
                    , CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                    != week);
                return d;
            };

            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = getRandomDateOfWeek(nextWeek) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = getRandomDateOfWeek(weekOfYear) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }


#if FAKE_XRM_EASY_9
        [Fact]
        public void FetchXml_Operator_ContainValues_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version=""1.0"" output-format=""xml-platform"" mapping=""logical"" distinct=""false"">
                               <entity name=""contact"">
                                 <attribute name=""firstname"" />
                                 <filter type=""and"">
                                   <condition attribute=""new_multiselectattribute"" operator=""contain-values"">
                                     <value>1</value>
                                     <value>2</value>
                                   </condition>
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("new_multiselectattribute", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.ContainValues, query.Criteria.Conditions[0].Operator);
            Assert.Equal(2, query.Criteria.Conditions[0].Values.Count);
            Assert.Equal(1, query.Criteria.Conditions[0].Values[0]);
            Assert.Equal(2, query.Criteria.Conditions[0].Values[1]);
        }

        [Fact]
        public void FetchXml_Operator_ContainValues_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version=""1.0"" output-format=""xml-platform"" mapping=""logical"" distinct=""false"">
                               <entity name=""contact"">
                                 <attribute name=""firstname"" />
                                 <filter type=""and"">
                                   <condition attribute=""new_multiselectattribute"" operator=""contain-values"">
                                     <value>1</value>
                                     <value>2</value>
                                   </condition>
                                 </filter>
                               </entity>
                             </fetch>";

            var ct1 = new Contact() { Id = Guid.NewGuid(), new_MultiSelectAttribute = new OptionSetValueCollection() { new OptionSetValue(1) } }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), new_MultiSelectAttribute = new OptionSetValueCollection() { new OptionSetValue(3) } }; //Shouldn't be returned
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            Assert.Equal(ct1.Id, collection.Entities[0].Id);
        }

        [Fact]
        public void FetchXml_Operator_DoesNotContainValues_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version=""1.0"" output-format=""xml-platform"" mapping=""logical"" distinct=""false"">
                               <entity name=""contact"">
                                 <attribute name=""firstname"" />
                                 <filter type=""and"">
                                   <condition attribute=""new_multiselectattribute"" operator=""not-contain-values"">
                                     <value>1</value>
                                     <value>2</value>
                                   </condition>
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("new_multiselectattribute", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.DoesNotContainValues, query.Criteria.Conditions[0].Operator);
            Assert.Equal(2, query.Criteria.Conditions[0].Values.Count);
            Assert.Equal(1, query.Criteria.Conditions[0].Values[0]);
            Assert.Equal(2, query.Criteria.Conditions[0].Values[1]);
        }

        [Fact]
        public void FetchXml_Operator_DoesNotContainValues_Execution()
        {
            var ctx = new XrmFakedContext();
            var fetchXml = @"<fetch version=""1.0"" output-format=""xml-platform"" mapping=""logical"" distinct=""false"">
                               <entity name=""contact"">
                                 <attribute name=""firstname"" />
                                 <filter type=""and"">
                                   <condition attribute=""new_multiselectattribute"" operator=""not-contain-values"">
                                     <value>1</value>
                                     <value>2</value>
                                   </condition>
                                 </filter>
                               </entity>
                             </fetch>";

            var ct1 = new Contact() { Id = Guid.NewGuid(), new_MultiSelectAttribute = new OptionSetValueCollection() { new OptionSetValue(1) } }; //Shouldn't be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), new_MultiSelectAttribute = new OptionSetValueCollection() { new OptionSetValue(3) } }; //Should be returned
            ctx.Initialize(new[] { ct1, ct2 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(1, collection.Entities.Count);
            Assert.Equal(ct2.Id, collection.Entities[0].Id);
        }
#endif

#if FAKE_XRM_EASY_2013 || FAKE_XRM_EASY_2015 || FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9

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

            var collection = ctx.GetOrganizationService().RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Equal(0, collection.Entities.Count);
        }

        [Fact]
        public void FetchXml_EntityName_Attribute_Alias_Execution()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();

            Entity e = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["retrieve"] = "Yes"
            };

            Entity e2 = new Entity("account")
            {
                Id = Guid.NewGuid(),
                ["contactid"] = e.ToEntityReference()
            };

            context.Initialize(new Entity[] { e, e2 });

            var fetchXml = @"<fetch top='50' >
                              <entity name='account' >
                                <filter>
                                  <condition entityname='mycontact' attribute='retrieve' operator='eq' value='Yes' />
                                </filter>
                                <link-entity name='contact' from='contactid' to='contactid' link-type='inner' alias='mycontact' >
                                  <attribute name='retrieve' />
                                </link-entity>
                              </entity>
                            </fetch>";

            var result = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.True(result.Entities.Any());
        }

        [Fact]
        public void FetchXml_EntityName_Attribute_No_Alias_Execution()
        {
            XrmFakedContext context = new XrmFakedContext();
            IOrganizationService service = context.GetOrganizationService();

            Entity e = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["retrieve"] = "Yes"
            };

            Entity e2 = new Entity("account")
            {
                Id = Guid.NewGuid(),
                ["contactid"] = e.ToEntityReference()
            };

            context.Initialize(new Entity[] { e, e2 });

            var fetchXml = @"<fetch top='50' >
                              <entity name='account' >
                                <filter>
                                  <condition entityname='contact' attribute='retrieve' operator='eq' value='Yes' />
                                </filter>
                                <link-entity name='contact' from='contactid' to='contactid' link-type='inner'>
                                  <attribute name='retrieve' />
                                </link-entity>
                              </entity>
                            </fetch>";

            var result = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.True(result.Entities.Any());
        }
#endif

        [Fact]
        public void FetchXml_Operator_Last_X_Hours_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='last-x-hours' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastXHours, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Last_X_Hours_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='last-x-hours' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastXHours, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddHours(-1) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddHours(1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddHours(-3) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }

        [Fact]
        public void FetchXml_Operator_Next_X_Hours_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='next-x-hours' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NextXHours, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Next_X_Hours_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='next-x-hours' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NextXHours, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddHours(1) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddHours(-1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddHours(3) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }

        [Fact]
        public void FetchXml_Operator_Last_X_Days_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='last-x-days' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastXDays, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Last_X_Days_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='last-x-days' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastXDays, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-1) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-3) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }

        [Fact]
        public void FetchXml_Operator_Next_X_Days_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='next-x-days' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NextXDays, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Next_X_Days_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='next-x-days' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NextXDays, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(1) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(3) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }

        [Fact]
        public void FetchXml_Operator_Last_X_Weeks_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='last-x-weeks' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastXWeeks, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Last_X_Weeks_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='last-x-weeks' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastXWeeks, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-7) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(7) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddDays(-21) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }

        [Fact]
        public void FetchXml_Operator_Next_X_Weeks_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='next-x-weeks' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NextXWeeks, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Last_X_Months_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='last-x-months' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastXMonths, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Last_X_Months_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='last-x-months' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastXMonths, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMonths(-1) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMonths(1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMonths(-3) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }

        [Fact]
        public void FetchXml_Operator_Next_X_Months_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='next-x-months' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NextXMonths, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Next_X_Months_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='next-x-months' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NextXMonths, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMonths(1) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMonths(-1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddMonths(3) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }

        [Fact]
        public void FetchXml_Operator_Last_X_Years_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='last-x-years' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastXYears, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Last_X_Years_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='last-x-years' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.LastXYears, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddYears(-1) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddYears(1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddYears(-3) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }

        [Fact]
        public void FetchXml_Operator_Next_X_Years_Translation()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                        <filter type='and'>
                                            <condition attribute='birthdate' operator='next-x-years' value='3' />
                                        </filter>
                                  </entity>
                            </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Equal(1, query.Criteria.Conditions.Count);
            Assert.Equal("birthdate", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NextXYears, query.Criteria.Conditions[0].Operator);
            Assert.Equal(3, query.Criteria.Conditions[0].Values[0]);
        }

        [Fact]
        public void FetchXml_Operator_Next_X_Years_Execution()
        {
            var ctx = new XrmFakedContext();
            ctx.ProxyTypesAssembly = Assembly.GetAssembly(typeof(Contact));

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                               <entity name='contact'>
                                 <attribute name='fullname' />
                                 <attribute name='telephone1' />
                                 <attribute name='contactid' />
                                 <order attribute='fullname' descending='false' />
                                 <filter type='and'>
                                   <condition attribute='anniversary' operator='next-x-years' value='2' />
                                 </filter>
                               </entity>
                             </fetch>";

            var query = XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, fetchXml);

            Assert.True(query.Criteria != null);
            Assert.Single(query.Criteria.Conditions);
            Assert.Equal("anniversary", query.Criteria.Conditions[0].AttributeName);
            Assert.Equal(ConditionOperator.NextXYears, query.Criteria.Conditions[0].Operator);

            var date = DateTime.UtcNow;
            var ct1 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddYears(1) }; //Should be returned
            var ct2 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddYears(-1) }; //Shouldnt
            var ct3 = new Contact() { Id = Guid.NewGuid(), Anniversary = date.AddYears(3) }; //Shouldnt
            ctx.Initialize(new[] { ct1, ct2, ct3 });
            var service = ctx.GetOrganizationService();

            var collection = service.RetrieveMultiple(new FetchExpression(fetchXml));

            Assert.Single(collection.Entities);
            var retrievedUser = collection.Entities[0].Id;
            Assert.Equal(retrievedUser, ct1.Id);
        }
    }
}
