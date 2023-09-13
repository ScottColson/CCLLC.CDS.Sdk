namespace CCLLC.CDS.Sdk.Tests.Builders
{
    using DLaB.Xrm.Test;
    using Shared.Proxies;

    public class TeamBuilder : EntityBuilder<Team>
    {
        public TeamBuilder(Id id) : base(id)
        {
        }

        public TeamBuilder WithName(string value)
        {
            Record.Name = value;
            return this;
        }

    }
}
