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

        IActionRegistrationModifiers<TRequest> IActionRegistrationModifiers<TRequest>.ExecuteIf(Action<IActionExecutionContextFilter<TRequest>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
