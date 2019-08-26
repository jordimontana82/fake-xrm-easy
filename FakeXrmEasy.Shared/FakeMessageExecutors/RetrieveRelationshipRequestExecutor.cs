using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;

namespace FakeXrmEasy.FakeMessageExecutors
{
    public class RetrieveRelationshipRequestExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is RetrieveRelationshipRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var retrieveRequest = request as RetrieveRelationshipRequest;
            if (retrieveRequest == null)
            {
                throw new Exception("Only RetrieveRelationshipRequest can be processed!");
            }

            var service = ctx.GetOrganizationService();
            var fakeRelationShip = ctx.GetRelationship(retrieveRequest.Name);
            if (fakeRelationShip == null)
            {
                throw new Exception(string.Format("Relationship {0} does not exist in the metadata cache", retrieveRequest.Name));
            }

            
            var response = new RetrieveRelationshipResponse();
            response.Results = new ParameterCollection();
            response.Results.Add("RelationshipMetadata", GetRelationshipMetadata(fakeRelationShip));
            response.ResponseName = "RetrieveRelationship";

            return response;
        }

        private static object GetRelationshipMetadata(XrmFakedRelationship fakeRelationShip)
        {
            if (fakeRelationShip.RelationshipType == XrmFakedRelationship.enmFakeRelationshipType.ManyToMany)
            {
                var mtm = new Microsoft.Xrm.Sdk.Metadata.ManyToManyRelationshipMetadata();
                mtm.Entity1LogicalName = fakeRelationShip.Entity1LogicalName;
                mtm.Entity1IntersectAttribute = fakeRelationShip.Entity1Attribute;
                mtm.Entity2LogicalName = fakeRelationShip.Entity2LogicalName;
                mtm.Entity2IntersectAttribute = fakeRelationShip.Entity2Attribute;
                mtm.SchemaName = fakeRelationShip.IntersectEntity;
                mtm.IntersectEntityName = fakeRelationShip.IntersectEntity.ToLower();
                return mtm;
            } else {

                var otm = new Microsoft.Xrm.Sdk.Metadata.OneToManyRelationshipMetadata();
#if FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
                otm.ReferencedEntityNavigationPropertyName = fakeRelationShip.IntersectEntity;
#endif
                otm.ReferencingAttribute = fakeRelationShip.Entity1Attribute;
                otm.ReferencingEntity = fakeRelationShip.Entity1LogicalName;
                otm.ReferencedAttribute = fakeRelationShip.Entity2Attribute;
                otm.ReferencedEntity = fakeRelationShip.Entity2LogicalName;
                otm.SchemaName = fakeRelationShip.IntersectEntity;
                return otm;
            }
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveRelationshipRequest);
        }
    }
}