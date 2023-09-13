namespace CCLLC.CDS.Sdk.Registrations
{    
    using System;
    using Microsoft.Xrm.Sdk;

    public class ApiEventRegistration<TRequest,TResponse> : EventRegistration, IApiRegistrationModifiers<TRequest>
        where TRequest : OrganizationRequest, new()
        where TResponse : OrganizationResponse, new()
    {
        public Action<ICDSPluginExecutionContext, TRequest, TResponse> PluginAction { get; set; }

        public ApiEventRegistration() : 
            base(null, new TRequest().RequestName)
        {            
            Stage = ePluginStage.Main;
        }

        protected override void InvokeRegistration(ICDSPluginExecutionContext executionContext)
        {
            var request = new TRequest()
            {
                Parameters = executionContext.InputParameters
            };

            var response = new TResponse();
            PluginAction.Invoke(executionContext, request, response);
            executionContext.OutputParameters.AddRange(response.Results);

        }

        public IApiRegistrationModifiers<TRequest> ExecuteIf(Action<IApiExecutionContextFilter<TRequest>> expression)
        {
            _ = expression ?? throw new ArgumentNullException(nameof(expression));

            var executionFilter = new ApiExecutionFilter<TRequest>();
            expression(executionFilter);
            AddExecutionFilter(executionFilter);
            return this;
        }
    }
}
