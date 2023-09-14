namespace CCLLC.CDS.Sdk.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    public partial class CDSSecurity
    {
        const string USERROLE_CACHE_KEY_BASE = "CCLLC.CDS.Security.UserRoles";

        public bool IsUserAssignedRole(Guid userId, params Guid[] roles)
        {
            return IsUserAssignedRole(userId, DefaultCacheTimeout, roles);
        }

        public bool IsUserAssignedRole(Guid userId, TimeSpan? cacheTimeout, params Guid[] roles)
        {
            if (roles is null)
            {
                return false;
            }

            var assingedRoles = GetAssignedSecurityRolesForUser(userId, cacheTimeout);

            foreach (var role in roles)
            {
                if (assingedRoles.ContainsKey(role))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsUserAssignedRole(Guid userId, params string[] roles)
        {
            return IsUserAssignedRole(userId, DefaultCacheTimeout, roles);
        }

        public bool IsUserAssignedRole(Guid userId, TimeSpan? cacheTimeout, params string[] roles)
        {
            if (roles is null)
            {
                return false;
            }

            // check for wild card
            if (roles.Contains("*"))
            {
                return true;
            }

            var assingedRoles = GetAssignedSecurityRolesForUser(userId, cacheTimeout);

            foreach (var role in roles)
            {
                if (assingedRoles.Where(r => string.Compare(r.Value, role, true) == 0).Any())
                {
                    return true;
                }
            }

            return false;
        }

        private IDictionary<Guid, string> GetAssignedSecurityRolesForUser(Guid userId, TimeSpan? cacheTimeout)
        {
            var cacheKey = $"{USERROLE_CACHE_KEY_BASE}.{userId}";

            if (Cache != null && cacheTimeout != null && Cache.Exists(cacheKey))
            {
                return Cache.Get<IDictionary<Guid, string>>(cacheKey);
            }

            IDictionary<Guid, string> assignedRoles = new Dictionary<Guid, string>();

            #region Fetch Direct User Roles

            var qryDirectUserRoles = new QueryExpression
            {
                EntityName = "role",
                ColumnSet = new ColumnSet(new string[] { "name", "parentrootroleid" }),

                LinkEntities = {
                    new LinkEntity
                    {
                        JoinOperator = JoinOperator.Inner,
                        LinkFromEntityName = "role",
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = "systemuserroles",
                        LinkToAttributeName = "roleid",

                        LinkCriteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression("systemuserid", ConditionOperator.Equal, userId)
                            }
                        }
                    }
                }
            };

            var userRoles = this.OrganizationService.RetrieveMultiple(qryDirectUserRoles);
            foreach (var r in userRoles.Entities)
            {
                var roleName = r.GetAttributeValue<string>("name");
                var rootRoleId = r.GetAttributeValue<EntityReference>("parentrootroleid");

                assignedRoles.Add(rootRoleId.Id, roleName);
            }

            #endregion

            #region Fetch User Team Roles

            var qryRolesByTeam = new QueryExpression
            {
                EntityName = "role",
                ColumnSet = new ColumnSet(new string[] { "name", "parentrootroleid" }),

                LinkEntities =
                {
                    new LinkEntity //INNER JOIN TeamRoles
                    {
                        JoinOperator = JoinOperator.Inner,
                        LinkFromEntityName = "role",
                        LinkFromAttributeName = "roleid",
                        LinkToEntityName = "teamroles",
                        LinkToAttributeName = "roleid",

                        LinkEntities =
                        {
                            new LinkEntity //INNER JOIN TeamMembership where user is a team member
                            {
                                JoinOperator = JoinOperator.Inner,
                                LinkFromEntityName = "teamroles",
                                LinkFromAttributeName = "teamid",
                                LinkToEntityName = "teammembership",
                                LinkToAttributeName = "teamid",
                                LinkCriteria = new FilterExpression
                                {
                                    FilterOperator = LogicalOperator.Or,
                                    Conditions =
                                    {
                                        new ConditionExpression("systemuserid", ConditionOperator.Equal, userId)
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var userTeamRoles = this.OrganizationService.RetrieveMultiple(qryRolesByTeam);
            foreach (var r in userTeamRoles.Entities)
            {
                var roleName = r.GetAttributeValue<string>("name");
                var rootRoleId = r.GetAttributeValue<EntityReference>("parentrootroleid");

                if (assignedRoles.ContainsKey(rootRoleId.Id) == false)
                {
                    assignedRoles.Add(rootRoleId.Id, roleName);
                }
            }

            #endregion

            if (Cache != null && cacheTimeout != null && assignedRoles.Count > 0)
            {
                Cache.Add<IDictionary<Guid, string>>(cacheKey, assignedRoles, cacheTimeout.Value);
            }

            return assignedRoles;
        }
    }
}
