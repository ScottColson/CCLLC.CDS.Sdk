namespace CCLLC.CDS.Sdk.Tests.Builders
{
    using System;
    using DLaB.Xrm.Test;
    using Microsoft.Xrm.Sdk;
    using Shared.Proxies;

    public class TeamMemberBuilder : EntityBuilder<TeamMembership>
    {
        public TeamMemberBuilder(Id id) : base(id) { }
  
        public TeamMemberBuilder WithUser(Id value)
        {
            Record[TeamMembership.Fields.SystemUserId] = value.EntityId;
            return this;
        }

        public TeamMemberBuilder WithTeam(Id value)
        {
            Record[TeamMembership.Fields.TeamId] = value.EntityId;
            return this;
        }
    }
}
