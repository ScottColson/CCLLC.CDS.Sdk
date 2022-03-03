namespace CCLLC.CDS.Sdk.Security
{
    using System;
    using CCLLC.Core;
    using Microsoft.Xrm.Sdk;

    public partial class CDSSecurity : ICDSSecurity
    {
        private TimeSpan DefaultCacheTimeout { get; } = new TimeSpan(0, 5, 0);
        private IOrganizationService OrganizationService { get; }
        private ICache Cache { get; }

        internal CDSSecurity(IOrganizationService organizationService, ICache cache)
        {
            OrganizationService = organizationService;
            Cache = cache;
        }
    }
}
