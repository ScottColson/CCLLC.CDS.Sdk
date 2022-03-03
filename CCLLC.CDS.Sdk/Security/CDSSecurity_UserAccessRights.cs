namespace CCLLC.CDS.Sdk.Security
{
    using System;
    using System.Linq;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Crm.Sdk.Messages;

    public partial class CDSSecurity
    {
        public void GrantUserReadAccess(Guid userId, EntityReference recordId)
        {
            GrantUserAccess(userId, recordId, AccessRights.ReadAccess);
        }     
        
        public void GrantUserWriteAccess(Guid userId, EntityReference recordId)
        {
            GrantUserAccess(userId, recordId, AccessRights.WriteAccess);
        }

        public void GrantUserAccess(Guid userId, EntityReference recordId, AccessRights accessRights)
        {
            var principalAccess = new PrincipalAccess();
            principalAccess.Principal = new EntityReference("systemuser", userId);
            principalAccess.AccessMask = accessRights;

            var grantAccessRequest = new GrantAccessRequest();
            grantAccessRequest.Target = recordId;
            grantAccessRequest.PrincipalAccess = principalAccess;

            OrganizationService.Execute(grantAccessRequest);
        }

        public bool UserHasDeleteAccess(Guid userId, EntityReference recordId)
        {
            AccessRights rights = this.GetAccessRights(userId, recordId);

            return (rights & AccessRights.DeleteAccess) != AccessRights.None;
        }

        public bool UserHasWriteAccess(Guid userId, EntityReference recordId)
        {
            AccessRights rights = this.GetAccessRights(userId, recordId);

            return (rights & AccessRights.WriteAccess) != AccessRights.None;
        }
        
        private AccessRights GetAccessRights(Guid userId, EntityReference recordId)
        {
            var req = new RetrievePrincipalAccessRequest
            {
                Principal = new EntityReference("systemuser", userId),
                Target = recordId
            };

            return ((RetrievePrincipalAccessResponse)OrganizationService.Execute(req)).AccessRights;
        }


    }
}
