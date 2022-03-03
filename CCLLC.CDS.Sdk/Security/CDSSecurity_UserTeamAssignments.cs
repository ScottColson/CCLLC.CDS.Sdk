namespace CCLLC.CDS.Sdk.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xrm.Sdk.Query;

    public partial class CDSSecurity
    {
        const string USERTEAM_CACHE_KEY_BASE = "CCLLC.CDS.Security.UserTeams";

        public bool IsUserTeamMember(Guid userId, params Guid[] teams)
        {
            return IsUserTeamMember(userId, DefaultCacheTimeout, teams);
        }

        public bool IsUserTeamMember(Guid userId, TimeSpan? cacheTimeout, params Guid[] teams)
        {
            if (teams is null)
            {
                return false;
            }

            var assignedTeams = GetAssignedTeamsForUser(userId, cacheTimeout);

            foreach (var team in teams)
            {
                if (assignedTeams.ContainsKey(team))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsUserTeamMember(Guid userId, params string[] teams)
        {
            return IsUserTeamMember(userId, DefaultCacheTimeout, teams);
        }       

        public bool IsUserTeamMember(Guid userId, TimeSpan? cacheTimeout, params string[] teams)
        {
            if (teams is null)
            {
                return false;
            }

            // check for wild card
            if (teams.Contains("*"))
            {
                return true;
            }

            var assingedTeams = GetAssignedTeamsForUser(userId, cacheTimeout);

            foreach (var team in teams)
            {
                if (assingedTeams.Where(r => string.Compare(r.Value, team, true) == 0).Any())
                {
                    return true;
                }
            }

            return false;
        }

        private IDictionary<Guid, string> GetAssignedTeamsForUser(Guid userId, TimeSpan? cacheTimeout)
        {
            var cacheKey = $"{USERTEAM_CACHE_KEY_BASE}.{userId}";

            if (Cache != null && cacheTimeout != null && Cache.Exists(cacheKey))
            {
                return Cache.Get<IDictionary<Guid, string>>(cacheKey);
            }

            IDictionary<Guid, string> assignedTeams = new Dictionary<Guid, string>();

            var qryAssignedTeams = new QueryExpression
            {
                EntityName = "team",
                ColumnSet = new ColumnSet(new string[] { "name" }),

                LinkEntities = {
                    new LinkEntity {
                        JoinOperator = Microsoft.Xrm.Sdk.Query.JoinOperator.Inner,
                        LinkFromEntityName = "team",
                        LinkFromAttributeName = "teamid",
                        LinkToEntityName = "teammembership",
                        LinkToAttributeName = "teamid",
                        LinkCriteria = {
                            FilterOperator  = LogicalOperator.And,
                            Conditions = {
                                new ConditionExpression("systemuserid", ConditionOperator.Equal, userId)
                            }
                        }
                    }
                }
            };

            var userTeams = this.OrganizationService.RetrieveMultiple(qryAssignedTeams);
            foreach (var r in userTeams.Entities)
            {
                assignedTeams.Add(r.Id, r.GetAttributeValue<string>("name"));
            }

            if (Cache != null && cacheTimeout != null && assignedTeams.Count > 0)
            {
                Cache.Add<IDictionary<Guid, string>>(cacheKey, assignedTeams, cacheTimeout.Value);
            }

            return assignedTeams;
        }
    }
}
