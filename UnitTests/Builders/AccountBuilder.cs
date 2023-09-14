namespace CCLLC.CDS.Sdk.Tests.Builders
{
    using System;
    using DLaB.Xrm.Test;
    using Microsoft.Xrm.Sdk;
    using Shared.Proxies;

    public class AccountBuilder : EntityBuilder<Account>
    {
        public AccountBuilder(Id id) : base(id) { }
  
        public AccountBuilder WithZip(string value)
        {
            Record.Address1PostalCode = value;
            return this;
        }
    }
}
