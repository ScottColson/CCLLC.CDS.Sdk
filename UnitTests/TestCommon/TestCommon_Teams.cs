namespace CCLLC.CDS.Sdk.Tests
{
    using DLaB.Xrm.Test;
    using CCLLC.CDS.Sdk.Tests.Builders;
    using Shared.Proxies;

    public static partial class TestCommon
    {
        public partial struct Ids
        {
            public partial struct Teams
            {
                public static readonly Id Team1 = new Id<Team>("{A47AA727-EAD6-4D51-B548-AADFF28EF08D}");
                public static readonly Id Team2 = new Id<Team>("{C6E1C62D-AF90-4B46-BAB6-D8DB2D4CD01E}");
            }
        }
        

        private static CrmEnvironmentBuilder WithTeams(this CrmEnvironmentBuilder builder)
        {
            builder
                .WithBuilder<TeamBuilder>(Ids.Teams.Team1, b => b
                    .WithName("Team 1"))
                .WithBuilder<TeamBuilder>(Ids.Teams.Team2, b => b
                    .WithName("Team 2"));        

            return builder;
        }
    }
}
