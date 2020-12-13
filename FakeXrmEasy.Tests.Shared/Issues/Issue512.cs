using Crm;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace FakeXrmEasy.Tests.Issues
{
    public class Issue512: FakeXrmEasyTestsBase
    {
        private readonly Entity _privilegeObjectTypeCode;
        private readonly Entity _privilege;
        
        public Issue512()
        {
            _privilege = new Entity("privilege")
            {
                Id = Guid.NewGuid()
            };

            _privilegeObjectTypeCode = new Entity("privilegeobjecttypecodes")
            {
                Id = Guid.NewGuid(),
                ["objecttypecode"] = "contact",
                ["privilegeid"] = _privilege.ToEntityReference()
            };
        }

        [Fact]
        public void Should_retrieve_privilege_type_code_entity()
        {
            _context.Initialize(new List<Entity>()
            {
                _privilegeObjectTypeCode, _privilege
            });

            using(var svcCtx = new XrmServiceContext(_service))
            {
                var privilegeObjectTypeCodes = (from privilegeObjectTypeCode in svcCtx.CreateQuery("privilegeobjecttypecodes")
                                                where (string)privilegeObjectTypeCode["objecttypecode"] == "contact"
                                                select privilegeObjectTypeCode).ToList();

                Assert.Single(privilegeObjectTypeCodes);
                Assert.Equal(_privilege.Id, ((EntityReference)privilegeObjectTypeCodes[0]["privilegeid"]).Id);
            }
        }
    }
}
