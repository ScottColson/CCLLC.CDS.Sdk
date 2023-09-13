namespace CCLLC.CDS.Sdk.Tests.Builders
{
    using DLaB.Xrm.Test;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Shared.Proxies;

    public class SystemUserBuilder : EntityBuilder<SystemUser>
    {
        public SystemUserBuilder(Id id) : base(id)
        {
        }

        public SystemUserBuilder WithName(string value)
        {
            Record.FirstName = value;
            return this;
        }

    }
}
