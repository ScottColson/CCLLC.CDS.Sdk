namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using Microsoft.Xrm.Sdk;    

    public class ActionEventRegistration<TRequest,TResponse> : EventRegistration, IActionRegistrationModifiers<TRequest> 
        where TRequest : OrganizationRequest, new()
        where TResponse : OrganizationResponse, new()
    {
        public Action<ICDSPluginExecutionContext, TRequest, TResponse> PluginAction { get; set; }

        public ActionEventRegistration() :
            base(null, new TRequest().RequestName)
        {            
        }

        protected override void InvokeRegistration(ICDSPluginExecutionContext executionContext)
        {
            var request = new TRequest()
            {
                Parameters = executionContext.InputParameters
            }; 
        
            if (Stage == ePluginStage.PostOperation) 
            {
                var response = new TResponse()
                {
                    Results = executionContext.OutputParameters
                };

                PluginAction.Invoke(executionContext, request, response);                
            }
            else
            {
                PluginAction.Invoke(executionContext, request, null);
            }          
        }

        public IActionRegistrationModifiers<TRequest> ExecuteIf(Action<IActionExecutionContextFilter<TRequest>> expression)
        {
            _ = expression ?? throw new ArgumentNullException(nameof(expression));

            var executionFilter = new ActionExecutionFilter<TRequest>();
            expression(executionFilter);
            AddExecutionFilter(executionFilter);
            return this;
        }
    }
}
