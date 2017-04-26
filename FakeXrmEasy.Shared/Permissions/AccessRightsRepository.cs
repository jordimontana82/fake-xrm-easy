using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Grants the specified rights to the security principal (user or team) for the specified record
        /// </summary>
        /// <param name="er"></param>
        /// <param name="pa"></param>
        public void GrantAccessTo(EntityReference er, PrincipalAccess pa)
        {
            List<PrincipalAccess> accessList = GetAccessListForRecord(er);
            if (!accessList.Contains(pa))
                accessList.Add(pa);


        }

        /// <summary>
        /// Retrieves the RetrievePrincipalAccessResponse for the specified security principal (user or team) and record
        /// </summary>
        /// <param name="er"></param>
        /// <param name="principal"></param>
        public RetrievePrincipalAccessResponse RetrievePrincipalAccess(EntityReference er, EntityReference principal)
        {
            List<PrincipalAccess> accessList = GetAccessListForRecord(er);
            PrincipalAccess pAcc = accessList.Where(pa => pa.Principal.Id == principal.Id).SingleOrDefault();
            RetrievePrincipalAccessResponse resp = new RetrievePrincipalAccessResponse();

            if (pAcc != null)
                resp.Results["AccessRights"] = pAcc.AccessMask;

            return resp;
        }

        /// <summary>
        /// Revokes the specified rights to the security principal (user or team) for the specified record
        /// </summary>
        /// <param name="er"></param>
        /// <param name="pa"></param>
        public void RevokeAccessTo(EntityReference er, PrincipalAccess pa)
        {
            List<PrincipalAccess> accessList = GetAccessListForRecord(er);
            if (accessList.Contains(pa))
                accessList.Remove(pa);
        }

        /// <summary>
        /// Revokes any access to any record for the specified security principal (kind of 'Clear All')
        /// </summary>
        /// <param name="er"></param>
        /// <param name="pa"></param>
        public void RevokeAccessToAllRecordsTo(PrincipalAccess pa)
        {
            foreach (EntityReference er in _accessRights.Keys)
            {
                List<PrincipalAccess> accessList = GetAccessListForRecord(er);
                if (accessList.Contains(pa))
                    accessList.Remove(pa);
            }
        }

        /// <summary>
        /// Retrieves all principals (security principals) who have any access to the specified record
        /// </summary>
        /// <param name="er"></param>
        public void GetAllPrincipalAccessFor(EntityReference er)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fetches the List&lt;PrincipalAccess&gt; for the given EntityReference
        /// </summary>
        /// <param name="er"></param>
        private List<PrincipalAccess> GetAccessListForRecord(EntityReference er)
        {
            List<PrincipalAccess> accessList = null;
            if (!_accessRights.TryGetValue(er, out accessList))
            {
                accessList = new List<PrincipalAccess>();
                _accessRights.Add(er, accessList);
            }

            return accessList;
        }
    }
}
