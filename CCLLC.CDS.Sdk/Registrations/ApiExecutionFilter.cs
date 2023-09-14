namespace CCLLC.CDS.Sdk.Registrations
{
    using Microsoft.Xrm.Sdk;

    public class ApiExecutionFilter<TRequest> : ExecutionFilter<IApiExecutionContextFilter<TRequest>>, IApiExecutionContextFilter<TRequest> where TRequest : OrganizationRequest, new()
    {
        public ApiExecutionFilter() 
            : base() { }
    }
}
