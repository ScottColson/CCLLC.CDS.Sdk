using Microsoft.Xrm.Sdk;

[assembly: Microsoft.Xrm.Sdk.Client.ProxyTypesAssemblyAttribute()]

namespace Shared.Proxies
{


    public partial class TestServiceContext : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
    {
        public TestServiceContext(IOrganizationService service) : base(service)
        {
        }
    }
}
