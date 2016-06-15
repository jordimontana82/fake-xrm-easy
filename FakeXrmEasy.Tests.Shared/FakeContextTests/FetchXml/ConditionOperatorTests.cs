using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
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

        TODO:

      <xs:enumeration value="gt" />
      <xs:enumeration value="ge" />
      <xs:enumeration value="le" />
      <xs:enumeration value="lt" />
      <xs:enumeration value="null" />
      <xs:enumeration value="not-null" />

      <xs:enumeration value="between" />
      <xs:enumeration value="not-between" />

      <xs:enumeration value="yesterday" />
      <xs:enumeration value="today" />
      <xs:enumeration value="tomorrow" />
      <xs:enumeration value="last-seven-days" />
      <xs:enumeration value="next-seven-days" />
      <xs:enumeration value="last-week" />
      <xs:enumeration value="this-week" />
      <xs:enumeration value="next-week" />
      <xs:enumeration value="last-month" />
      <xs:enumeration value="this-month" />
      <xs:enumeration value="next-month" />
      <xs:enumeration value="on" />
      <xs:enumeration value="on-or-before" />
      <xs:enumeration value="on-or-after" />
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
      <xs:enumeration value="olderthan-x-months" />
      <xs:enumeration value="olderthan-x-years" />
      <xs:enumeration value="olderthan-x-weeks" />
      <xs:enumeration value="olderthan-x-days" />
      <xs:enumeration value="olderthan-x-hours" />
      <xs:enumeration value="olderthan-x-minutes" />
      <xs:enumeration value="last-x-years" />
      <xs:enumeration value="next-x-years" />
      <xs:enumeration value="eq-userid" />
      <xs:enumeration value="ne-userid" />
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
            Assert.Equal(ConditionOperator.Like, query.Criteria.Conditions[0].Operator);
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
            Assert.Equal(ConditionOperator.NotLike, query.Criteria.Conditions[0].Operator);
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
    }
}
