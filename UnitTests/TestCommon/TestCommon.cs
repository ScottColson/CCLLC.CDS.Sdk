namespace CCLLC.CDS.Sdk.Tests
{ 
    using CCLLC.CDS.Sdk.Tests.Builders;

    public static partial class TestCommon
    {
        public partial struct Ids
        {
        }

        public static CrmEnvironmentBuilder WithCommonData(this CrmEnvironmentBuilder builder)
        {

            builder
                .WithUsers()
                .WithTeams();

            return builder;
        }
    }
}
