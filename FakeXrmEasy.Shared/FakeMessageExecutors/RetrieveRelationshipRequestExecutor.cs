using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;

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

            var service = ctx.GetFakedOrganizationService();
            var fakeRelationShip = ctx.GetRelationship(retrieveRequest.Name);
            if (fakeRelationShip == null)
            {
                throw new Exception(string.Format("Relationship {0} does not exist in the metadata cache", retrieveRequest.Name));
            }


            var mtm = new Microsoft.Xrm.Sdk.Metadata.ManyToManyRelationshipMetadata();
            mtm.Entity1LogicalName = fakeRelationShip.Entity1LogicalName;
            mtm.Entity1IntersectAttribute = fakeRelationShip.Entity1Attribute;
            mtm.Entity2LogicalName = fakeRelationShip.Entity2LogicalName;
            mtm.Entity2IntersectAttribute = fakeRelationShip.Entity2Attribute;
            mtm.SchemaName = fakeRelationShip.IntersectEntity;
            mtm.IntersectEntityName = fakeRelationShip.IntersectEntity.ToLower();

            var response = new RetrieveRelationshipResponse();
            response.Results = new ParameterCollection();
            response.Results.Add("RelationshipMetadata", mtm);
            response.ResponseName = "RetrieveRelationship";
            //response.RelationshipMetadata = mtm;

            return response;
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveRelationshipRequest);
        }
    }
}