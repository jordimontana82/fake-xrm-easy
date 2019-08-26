using Crm;
using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Xunit;

namespace FakeXrmEasy.Tests.FakeContextTests.ExecuteFetchRequestTests
{
    public class ExecuteFetchRequestTests
    {
        [Fact]
        public void Test_Conversion_DateTime_ToXml()
        {
            var executor = new ExecuteFetchRequestExecutor();
            var date = new DateTime(2011, 7, 12, 13, 12, 43, DateTimeKind.Local);
            var element = executor.AttributeValueToFetchResult(new KeyValuePair<string, object>("new_startdate", date), null, null);
            var utcOffset = date.ToString("zz");
            var localizedAMPMDesignator = date.ToString("tt"); //Spanish and other cultures don't have the AM/PM designator
            Assert.NotNull(element);
            Assert.Equal($"<new_startdate date=\"2011-07-12\" time=\"01:12 {localizedAMPMDesignator}\">2011-07-12T13:12:43{utcOffset}:00</new_startdate>", element.ToString());
        }

        [Fact]
        public void Test_Conversion_AlliasedDateTime_ToXml()
        {
            var executor = new ExecuteFetchRequestExecutor();
            var date = new DateTime(2011, 7, 12, 13, 12, 43, DateTimeKind.Local);
            var element = executor.AttributeValueToFetchResult(new KeyValuePair<string, object>("alias.new_startdate", new AliasedValue(null, null, date)), null, null);
            var utcOffset = date.ToString("zz");
            var localizedAMPMDesignator = date.ToString("tt"); //Spanish and other cultures don't have the AM/PM designator
            Assert.NotNull(element);
            Assert.Equal($"<alias.new_startdate date=\"2011-07-12\" time=\"01:12 {localizedAMPMDesignator}\">2011-07-12T13:12:43{utcOffset}:00</alias.new_startdate>", element.ToString());
        }

        [Fact]
        public void Test_Conversion_EntityReference_ToXml()
        {
            var fake = new XrmFakedContext();
            fake.ProxyTypesAssembly = typeof(Crm.Contact).Assembly;
            var executor = new ExecuteFetchRequestExecutor();
            var contactGuid = Guid.NewGuid();
            var element = executor.AttributeValueToFetchResult(new KeyValuePair<string, object>("new_contact", new EntityReference("contact", contactGuid) { Name = "John Doe" }), null, fake);
            Assert.NotNull(element);
            Assert.Equal(@"<new_contact dsc=""0"" yomi=""John Doe"" name=""John Doe"" type=""2"">" + contactGuid.ToString().ToUpper() + "</new_contact>", element.ToString());
        }

        [Fact]
        public void Test_Conversion_OptionSetValue_ToXml()
        {
            var fake = new XrmFakedContext();
            fake.ProxyTypesAssembly = typeof(Crm.Contact).Assembly;
            var executor = new ExecuteFetchRequestExecutor();
            var formattedValues = new FormattedValueCollection();
            formattedValues.Add("new_contact", "Test");
            var element = executor.AttributeValueToFetchResult(new KeyValuePair<string, object>("new_contact", new OptionSetValue(1)), formattedValues, fake);
            Assert.NotNull(element);
            Assert.Equal(@"<new_contact name=""Test"" formattedvalue=""1"">1</new_contact>", element.ToString());
        }

#if FAKE_XRM_EASY_9
        [Fact]
        public void Test_Conversion_OptionSetValueCollection_ToXml()
        {
            var fake = new XrmFakedContext();
            fake.ProxyTypesAssembly = typeof(Crm.Contact).Assembly;
            var executor = new ExecuteFetchRequestExecutor();
            var formattedValues = new FormattedValueCollection();
            var element = executor.AttributeValueToFetchResult(
                new KeyValuePair<string, object>("new_multiselectattribute", new OptionSetValueCollection() { new OptionSetValue(1), new OptionSetValue(2) }),
                formattedValues,
                fake);
            Assert.NotNull(element);
            Assert.Equal(@"<new_multiselectattribute name=""[-1,1,2,-1]"">[-1,1,2,-1]</new_multiselectattribute>", element.ToString());
        }
#endif

        [Fact]
        public void When_executing_fetchxml_right_result_is_returned()
        {
            //This will test a query expression is generated and executed

            var ctx = new XrmFakedContext();
            ctx.Initialize(new List<Entity>()
            {
                new Contact() {
                    Id = Guid.NewGuid(),
                    FirstName = "Leo Messi",
                    Telephone1 = "123",
                    AnnualIncome = new Money(200.25m),
                    BirthDate = new DateTime(1981,10,23),
                    ["createdby"] = new EntityReference("contact", Guid.NewGuid()) {Name = "John" },
                    DoNotFax = true,
                    GenderCode = new OptionSetValue(2)
                }, //should be returned
                new Contact() {Id = Guid.NewGuid(), FirstName = "Leo Messi", Telephone1 = "234" }, //should be returned
                new Contact() {Id = Guid.NewGuid(), FirstName = "Leo", Telephone1 = "789" }, //shouldnt
                new Contact() {Id = Guid.NewGuid(), FirstName = "Andrés", Telephone1 = "123" }, //shouldnt
            });

            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                              <entity name='contact'>
                                    <attribute name='firstname' />
                                    <attribute name='telephone1' />
                                    <attribute name='annualincome' />
                                    <attribute name='birthdate' />
                                    <attribute name='createdby' />
                                    <attribute name='donotfax' />
                                    <attribute name='gendercode' />
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

            var service = ctx.GetOrganizationService();

            var response = service.Execute(new ExecuteFetchRequest { FetchXml = fetchXml }) as ExecuteFetchResponse;

            Assert.NotEmpty(response.FetchXmlResult);
            XDocument resXml = XDocument.Parse(response.FetchXmlResult);
            Assert.NotNull(resXml);
            Assert.NotNull(resXml.Element("resultset"));
            var rows = resXml.Element("resultset").Elements("result");
            //Executing the same via ExecuteMultiple returns also the same
            Assert.Equal(2, rows.Count());
            var row234 = rows.Where(e => e.Element("telephone1").Value == "234").FirstOrDefault();
            Assert.NotNull(row234);
            Assert.Equal(4, row234.Elements().Count());

            var row123 = rows.Where(e => e.Element("telephone1").Value == "123").FirstOrDefault();
            Assert.NotNull(row123);
            Assert.Equal(8, row123.Elements().Count());
        }

        [Fact]
        public void When_executing_fetchxml_with_count_attribute_only_that_number_of_results_is_returned()
        {
            //This will test a query expression is generated and executed

            var ctx = new XrmFakedContext();

            //Arrange
            var contactList = new List<Entity>();
            for (var i = 0; i < 20; i++)
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

            var service = ctx.GetOrganizationService();

            var response = service.Execute(new ExecuteFetchRequest { FetchXml = fetchXml }) as ExecuteFetchResponse;

            Assert.NotEmpty(response.FetchXmlResult);
            XDocument resXml = XDocument.Parse(response.FetchXmlResult);
            Assert.NotNull(resXml);
            Assert.NotNull(resXml.Element("resultset"));
            var rows = resXml.Element("resultset").Elements("result");

            Assert.Equal(7, rows.Count());
        }

        [Fact]
        public void When_executing_fetchxml_with_paging_cookies_correct_page_is_returned()
        {
            //This will test a query expression is generated and executed

            var ctx = new XrmFakedContext();

            //Arrange
            var contactList = new List<Entity>();
            for (var i = 0; i < 20; i++)
            {
                contactList.Add(new Contact() { Id = Guid.NewGuid() });
            }
            ctx.Initialize(contactList);

            //Act
            var fetchXml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' count='7' {0}>
                              <entity name='contact'>
                                    <attribute name='fullname' />
                                    <attribute name='telephone1' />
                                    <attribute name='contactid' />
                                  </entity>
                            </fetch>";

            var service = ctx.GetOrganizationService();

            var response = service.Execute(new ExecuteFetchRequest { FetchXml = string.Format(fetchXml, String.Empty) }) as ExecuteFetchResponse;

            Assert.NotEmpty(response.FetchXmlResult);
            XDocument resXml = XDocument.Parse(response.FetchXmlResult);
            Assert.NotNull(resXml);
            Assert.NotNull(resXml.Element("resultset"));
            var rows = resXml.Element("resultset").Elements("result");

            Assert.Equal(7, rows.Count());
            Assert.Equal("1", resXml.Element("resultset").Attribute("morerecords").Value);
            var pageCookie = resXml.Element("resultset").Attribute("paging-cookie");
            var pagingXml = "page=\"2\" " + pageCookie.ToString();

            var response2 = service.Execute(new ExecuteFetchRequest { FetchXml = string.Format(fetchXml, pagingXml) }) as ExecuteFetchResponse;
            Assert.NotEmpty(response2.FetchXmlResult);
            resXml = XDocument.Parse(response2.FetchXmlResult);
            Assert.NotNull(resXml);
            Assert.NotNull(resXml.Element("resultset"));
            rows = resXml.Element("resultset").Elements("result");

            Assert.Equal(7, rows.Count());
            Assert.Equal("1", resXml.Element("resultset").Attribute("morerecords").Value);
            pageCookie = resXml.Element("resultset").Attribute("paging-cookie");
            pagingXml = "page=\"3\" " + pageCookie;

            var response3 = service.Execute(new ExecuteFetchRequest { FetchXml = string.Format(fetchXml, pagingXml) }) as ExecuteFetchResponse;
            Assert.NotEmpty(response3.FetchXmlResult);
            resXml = XDocument.Parse(response3.FetchXmlResult);
            Assert.NotNull(resXml);
            Assert.NotNull(resXml.Element("resultset"));
            rows = resXml.Element("resultset").Elements("result");

            Assert.Equal(6, rows.Count());
            Assert.Equal("0", resXml.Element("resultset").Attribute("morerecords").Value);
        }

        [Fact]
        public void When_querying_fetchxml_with_linked_entities_with_left_outer_join_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FirstName = "Lionel"
            };

            var account = new Account() { Id = Guid.NewGuid(), PrimaryContactId = contact.ToEntityReference() };
            var account2 = new Account() { Id = Guid.NewGuid(), PrimaryContactId = null };

            context.Initialize(new List<Entity>
            {
                contact, account, account2
            });

            var fetchXml = @"
                    <fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                      <entity name='account'>
                        <attribute name='name' />
                        <attribute name='primarycontactid' />
                        <attribute name='telephone1' />
                        <attribute name='accountid' />
                        <order attribute='name' descending='false' />
                        <link-entity name='contact' from='contactid' to='primarycontactid' alias='aa' link-type='outer'>
                          <attribute name='firstname' />
                        </link-entity>
                        <filter type='and'>
                            <condition attribute='statecode' operator='eq' value='0' />
                        </filter>
                      </entity>
                    </fetch>
                ";

            var response = service.Execute(new ExecuteFetchRequest { FetchXml = fetchXml }) as ExecuteFetchResponse;

            Assert.NotEmpty(response.FetchXmlResult);
            XDocument resXml = XDocument.Parse(response.FetchXmlResult);
            Assert.NotNull(resXml);
            Assert.NotNull(resXml.Element("resultset"));
            var rows = resXml.Element("resultset").Elements("result");
            //Executing the same via ExecuteMultiple returns also the same
            Assert.Equal(2, rows.Count());

            var rowsWithLinkedData = rows.Where(r => r.Element("aa.firstname") != null);
            Assert.Equal(1, rowsWithLinkedData.Count());
        }

        [Fact]
        public void When_querying_fetchxml_with_multiple_linked_entities_with_the_same_entity_right_result_is_returned()
        {
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            var user1 = new SystemUser
            {
                Id = Guid.NewGuid(),
                ["fullname"] = "User1"
            };

            var user2 = new SystemUser
            {
                Id = Guid.NewGuid(),
                ["fullname"] = "User2",
                ["modifiedby"] = user1.ToEntityReference()
            };

            var user3 = new SystemUser
            {
                Id = Guid.NewGuid(),
                ["fullname"] = "User3",
                ["modifiedby"] = user2.ToEntityReference()
            };

            var user4 = new SystemUser
            {
                Id = Guid.NewGuid(),
                ["fullname"] = "User4",
                ["modifiedby"] = user3.ToEntityReference()
            };

            context.Initialize(new List<Entity>
            {
                user1,
                user2,
                user3,
                user4
            });

            const string fetchXml = @"
                    <fetch top=""1"" >
                      <entity name=""systemuser"" >
                        <attribute name=""fullname"" />
                        <link-entity name=""systemuser"" from=""modifiedby"" to=""systemuserid"" link-type=""inner"" >
                          <attribute name=""fullname"" />
                          <link-entity name=""systemuser"" from=""modifiedby"" to=""systemuserid"" >
                            <attribute name=""fullname"" />
                            <link-entity name=""systemuser"" from=""modifiedby"" to=""systemuserid"" link-type=""inner"" >
                              <attribute name=""fullname"" />
                            </link-entity>
                          </link-entity>
                        </link-entity>
                      </entity>
                    </fetch>";

            var response = service.Execute(new ExecuteFetchRequest { FetchXml = fetchXml }) as ExecuteFetchResponse;

            Assert.NotNull(response);
            Assert.NotEmpty(response.FetchXmlResult);

            var responseXml = XDocument.Parse(response.FetchXmlResult);
            Assert.NotNull(responseXml);
            Assert.NotNull(responseXml.Element("resultset"));

            var rows = responseXml.Element("resultset").Elements("result");
            Assert.Equal(1, rows.Count());

            var linkedAttributes = rows.First().Elements("systemuser.fullname");
            Assert.Equal(3, linkedAttributes.Count());

            Assert.Equal(1, linkedAttributes.Count(e => e.Value == "User2"));
            Assert.Equal(1, linkedAttributes.Count(e => e.Value == "User3"));
            Assert.Equal(1, linkedAttributes.Count(e => e.Value == "User4"));
        }
    }
}