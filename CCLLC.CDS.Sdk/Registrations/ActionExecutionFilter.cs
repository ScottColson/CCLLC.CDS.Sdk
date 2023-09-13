namespace CCLLC.CDS.Sdk.Registrations
{
    using Microsoft.Xrm.Sdk;

    public class ActionExecutionFilter<TRequest> : ExecutionFilter<IActionExecutionContextFilter<TRequest>>, IActionExecutionContextFilter<TRequest> where TRequest : OrganizationRequest, new()
    {
        public ActionExecutionFilter() 
            : base()
        {}
    }
}
