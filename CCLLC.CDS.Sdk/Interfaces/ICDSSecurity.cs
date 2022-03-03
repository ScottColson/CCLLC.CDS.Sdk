namespace CCLLC.CDS.Sdk
{
    using System;
    using Microsoft.Xrm.Sdk;

    public interface ICDSSecurity
    {
        void GrantUserReadAccess(Guid userId, EntityReference recordId);
        void GrantUserWriteAccess(Guid userId, EntityReference recordId);
        bool IsUserAssignedRole(Guid userId, params Guid[] roles);
        bool IsUserAssignedRole(Guid userId, TimeSpan? cacheTimeout, params Guid[] roles);
        bool IsUserAssignedRole(Guid userId, params string[] roles);
        bool IsUserAssignedRole(Guid userId, TimeSpan? cacheTimeout, params string[] roles);
        bool IsUserTeamMember(Guid userId, params Guid[] teams);
        bool IsUserTeamMember(Guid userId, TimeSpan? cacheTimeout, params Guid[] teams);
        bool IsUserTeamMember(Guid userId, params string[] teams);
        bool IsUserTeamMember(Guid userId, TimeSpan? cacheTimeout, params string[] teams);
        bool UserHasDeleteAccess(Guid userId, EntityReference recordId);
        bool UserHasWriteAccess(Guid userId, EntityReference recordId);
    }
}
