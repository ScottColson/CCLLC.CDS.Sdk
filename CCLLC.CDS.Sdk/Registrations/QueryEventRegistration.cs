namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using Microsoft.Crm.Sdk.Messages;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    public class QueryEventRegistration<TEntity> : EventRegistration, IQueryRegistrationModifiers<TEntity> where TEntity : Entity, new()
    {
        public Action<ICDSPluginExecutionContext, QueryExpression, EntityCollection> PluginAction { get; set; }

        public QueryEventRegistration() :
            base(new TEntity().LogicalName, MessageNames.RetrieveMultiple)
        {
        }

        protected override void InvokeRegistration(ICDSPluginExecutionContext executionContext)
        {
            _ = executionContext.InputParameters["Query"] ?? 
                throw new ArgumentNullException("RetrieveMultiple is missing required Query input parameter.");
            
            if(executionContext.InputParameters["Query"] is FetchExpression)
            {
                // Convert FetchXML to query expression.
                var fetchExpression = executionContext.InputParameters["Query"] as FetchExpression;

                var conversionRequest = new FetchXmlToQueryExpressionRequest
                {
                    FetchXml = fetchExpression.Query
                };

                var conversionResponse = (FetchXmlToQueryExpressionResponse)executionContext.OrganizationService.Execute(conversionRequest);

                executionContext.InputParameters["Query"] = conversionResponse.Query;
            }

            var qryExpression = executionContext.InputParameters["Query"] as QueryExpression;

           
            if (Stage == ePluginStage.PostOperation) 
            {
                var response = (executionContext.OutputParameters["EntityCollection"] as EntityCollection) ??
                    throw new ArgumentNullException("PostOp RetrieveMultiple Message is missing required EntityCollection output parameter.");
                PluginAction.Invoke(executionContext, qryExpression, response);
                executionContext.OutputParameters["EntityColection"] = response;
            }
            else
            {
                PluginAction.Invoke(executionContext, qryExpression, null);
            }          
        }

        IQueryRegistrationModifiers<TEntity> IQueryRegistrationModifiers<TEntity>.ExecuteIf(Action<IQueryExecutionFilter> expression)
        {
            _ = expression ?? throw new ArgumentNullException(nameof(expression));

            var executionFilter = new QueryExecutionFilter();
            expression(executionFilter);
            AddExecutionFilter(executionFilter);
            return this;
        }
    }
}
