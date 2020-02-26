using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.Permissions
{
    public interface IAccessRightsRepository
    {
        /// <summary>
        /// Grants the specified rights to the security principal (user or team) for the specified record
        /// </summary>
        /// <param name="er"></param>
        /// <param name="pa"></param>
        void GrantAccessTo(EntityReference er, PrincipalAccess pa);

        /// <summary>
        /// Revokes the specified rights to the security principal (user or team) for the specified record
        /// </summary>
        /// <param name="er"></param>
        /// <param name="pa"></param>
        void RevokeAccessTo(EntityReference er, EntityReference principal);

        /// <summary>
        /// Retrieves the RetrievePrincipalAccessResponse for the specified security principal (user or team) and record
        /// </summary>
        /// <param name="er"></param>
        /// <param name="principal"></param>
        /// <returns></returns>
        RetrievePrincipalAccessResponse RetrievePrincipalAccess(EntityReference er, EntityReference principal);

        /// <summary>
        /// Retrieves the list of permitted security principals (user or team) that have access to the given record
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        RetrieveSharedPrincipalsAndAccessResponse RetrieveSharedPrincipalsAndAccess(EntityReference er);

        /// <summary>
        /// Retrieves all principals (security principals) who have any access to the specified record
        /// </summary>
        /// <param name="er"></param>
        void GetAllPrincipalAccessFor(EntityReference er);

        /// <summary>
        /// Modify the access rights for a specific entity
        /// </summary>
        /// <param name="er">The entity to of which we are modify access to </param>
        /// <param name="pa">The modified access</param>
        void ModifyAccessOn(EntityReference er, PrincipalAccess pa);
    }
}