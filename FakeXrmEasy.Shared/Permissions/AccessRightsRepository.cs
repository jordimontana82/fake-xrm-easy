using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;

namespace FakeXrmEasy.Permissions
{
    public class AccessRightsRepository: IAccessRightsRepository
    {
        protected Dictionary<EntityReference, List<PrincipalAccess>> _accessRights; 

        public AccessRightsRepository()
        {
            //One record might be accessed from many security principals
            _accessRights = new Dictionary<EntityReference, List<PrincipalAccess>>();
        }

        public void GrantAccessTo(EntityReference er, PrincipalAccess pa)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Revokes the specified rights to the security principal (user or team) for the specified record
        /// </summary>
        /// <param name="er"></param>
        /// <param name="pa"></param>
        public void RevokeAccessTo(EntityReference er, PrincipalAccess pa)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Revokes any access to any record for the specified security principal (kind of 'Clear All')
        /// </summary>
        /// <param name="er"></param>
        /// <param name="pa"></param>
        public void RevokeAccessToAllRecordsTo(PrincipalAccess pa)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves all principals (security principals) who have any access to the specified record
        /// </summary>
        /// <param name="er"></param>
        public void GetAllPrincipalAccessFor(EntityReference er)
        {
            throw new NotImplementedException();
        }
    }
}
