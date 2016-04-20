using System;
using System.Collections.Generic;
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

            Assert.DoesNotThrow(() => XrmFakedContext.TranslateFetchXmlToQueryExpression(ctx, "<fetch></fetch>"));
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
    }
}
