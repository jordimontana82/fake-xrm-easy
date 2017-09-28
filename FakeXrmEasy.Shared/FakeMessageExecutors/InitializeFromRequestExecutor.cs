using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using System.ServiceModel;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class InitializeFromRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is InitializeFromRequest;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(InitializeFromRequest);
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as InitializeFromRequest;
            if (req == null)
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault(), "Cannot execute InitializeFromRequest without the request");

            //TODO: Implement logic to filter mapping attributes based on the req.TargetFieldType
            if (req.TargetFieldType != TargetFieldType.All)
                throw PullRequestException.PartiallyNotImplementedOrganizationRequest(req.GetType(), "logic for filtering attributes based on TargetFieldType other than All is missing");

            var service = ctx.GetOrganizationService();
            var fetchXml = string.Format(FetchMappingsByEntity, req.EntityMoniker.LogicalName, req.TargetEntityName);
            var mapping = service.RetrieveMultiple(new FetchExpression(fetchXml));
            var sourceAttributes = mapping.Entities.Select(a => a.GetAttributeValue<AliasedValue>("attributemap.sourceattributename").Value.ToString()).ToArray();
            var columnSet = sourceAttributes.Length == 0 ? new ColumnSet(true) : new ColumnSet(sourceAttributes);
            var source = service.Retrieve(req.EntityMoniker.LogicalName, req.EntityMoniker.Id, columnSet);

            var entity = new Entity
            {
                LogicalName = req.TargetEntityName,
                Id = Guid.Empty
            };

            if (mapping.Entities.Count > 0)
            {
                foreach (var attr in source.Attributes)
                {
                    var mappingEntity = mapping.Entities.FirstOrDefault(e => e.GetAttributeValue<AliasedValue>("attributemap.sourceattributename").Value.ToString() == attr.Key);
                    if (mappingEntity == null) continue;
                    var targetAttribute = mappingEntity.GetAttributeValue<AliasedValue>("attributemap.targetattributename").Value.ToString();
                    entity[targetAttribute] = attr.Value;

                    var isEntityReference = string.Equals(attr.Key, source.LogicalName + "id", StringComparison.CurrentCultureIgnoreCase);
                    if (isEntityReference)
                    {
                        entity[targetAttribute] = new EntityReference(source.LogicalName, (Guid)attr.Value);
                    }
                    else
                    {
                        entity[targetAttribute] = attr.Value;
                    }
                }
            }

            var response = new InitializeFromResponse
            {
                Results =
                {
                    ["Entity"] = entity
                }
            };

            return response;
        }

        private const string FetchMappingsByEntity = @"<fetch version='1.0' mapping='logical' distinct='false'>
                                                           <entity name='entitymap'>
                                                              <attribute name='sourceentityname'/>
                                                              <attribute name='targetentityname'/>
                                                              <link-entity name='attributemap' alias='attributemap' to='entitymapid' from='entitymapid' link-type='inner'>
                                                                 <attribute name='sourceattributename'/>
                                                                 <attribute name='targetattributename'/>
                                                              </link-entity>
                                                              <filter type='and'>
                                                                 <condition attribute='sourceentityname' operator='eq' value='{0}' />
                                                                 <condition attribute='targetentityname' operator='eq' value='{1}' />
                                                              </filter>
                                                           </entity>
                                                        </fetch>";
    }
}