﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;
using Microsoft.Xrm.Sdk.Metadata;

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

            var fakeRelationShip = ctx.GetRelationship(retrieveRequest.Name);
            if (fakeRelationShip == null)
            {
                throw new Exception(string.Format("Relationship {0} does not exist in the metadata cache", retrieveRequest.Name));
            }

            
            var response = new RetrieveRelationshipResponse();
            response.Results = new ParameterCollection();
            response.Results.Add("RelationshipMetadata", GetRelationshipMetadata(retrieveRequest.Name, fakeRelationShip));
            response.ResponseName = "RetrieveRelationship";

            return response;
        }

        private static RelationshipMetadataBase GetRelationshipMetadata(string schemaName, XrmFakedRelationship fakeRelationShip)
        {
            if (fakeRelationShip.RelationshipType == XrmFakedRelationship.enmFakeRelationshipType.ManyToMany)
            {
                var mtm = new ManyToManyRelationshipMetadata();
                mtm.Entity1LogicalName = fakeRelationShip.Entity1LogicalName;
                mtm.Entity1IntersectAttribute = fakeRelationShip.Entity1Attribute;
                mtm.Entity2LogicalName = fakeRelationShip.Entity2LogicalName;
                mtm.Entity2IntersectAttribute = fakeRelationShip.Entity2Attribute;
                mtm.IntersectEntityName = fakeRelationShip.IntersectEntity.ToLower();
                mtm.SchemaName = schemaName;
                return mtm;
            } else {

                var otm = new OneToManyRelationshipMetadata();
#if FAKE_XRM_EASY_2016 || FAKE_XRM_EASY_365 || FAKE_XRM_EASY_9
                otm.ReferencedEntityNavigationPropertyName = fakeRelationShip.IntersectEntity;
#endif
                otm.ReferencingAttribute = fakeRelationShip.Entity1Attribute;
                otm.ReferencingEntity = fakeRelationShip.Entity1LogicalName;
                otm.ReferencedAttribute = fakeRelationShip.Entity2Attribute;
                otm.ReferencedEntity = fakeRelationShip.Entity2LogicalName;
                otm.SchemaName = fakeRelationShip.IntersectEntity;
                otm.SchemaName = schemaName;
                return otm;
            }
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(RetrieveRelationshipRequest);
        }
    }
}