namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    public class RetrieveEventRegistration<E> : EventRegistration, IRetrieveRegistrationModifiers<E> where E : Entity, new()
    {
        public Action<ICDSPluginExecutionContext, EntityReference, ColumnSet, E> PluginAction { get; set; }

        public RetrieveEventRegistration() :
            base(new E().LogicalName, MessageNames.Retrieve)
        {
        }

        protected override void InvokeRegistration(ICDSPluginExecutionContext executionContext)
        {
            var target = (EntityReference)executionContext.InputParameters["Target"];
            var columnSet = (ColumnSet)executionContext.InputParameters["ColumnSet"];

            if (Stage == ePluginStage.PostOperation) 
            {
                var response = ((Entity)(executionContext.OutputParameters["BusinessEntity"])).ToEntity<E>();
                PluginAction.Invoke(executionContext, target, columnSet, response);
                executionContext.OutputParameters["BusinessEntity"] = response.ToEntity<Entity>() ;
            }
            else
            {
                PluginAction.Invoke(executionContext, target, columnSet, null);
            }          
        }

        public IRetrieveRegistrationModifiers<E> ExecuteIf(Action<IRetrieveExecutionFilter> expression)
        {
            _ = expression ?? throw new ArgumentNullException(nameof(expression));

            var executionFilter = new RetrieveExecutionFilter();
            expression(executionFilter);
            AddExecutionFilter(executionFilter);
            return this;
        }
    }
}
