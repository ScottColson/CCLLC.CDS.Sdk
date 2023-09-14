namespace CCLLC.CDS.Sdk.Tests
{
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using Shared.Proxies;

    public abstract class TestPluginBase : CDSPlugin
    {
        protected TestPluginBase(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig)
        {            
        }

        protected static void onActionExecute(ICDSExecutionContext executionContext, OrganizationRequest request, OrganizationResponse response)
        {

        }

        protected static void onApiExecute(ICDSExecutionContext executionContext, OrganizationRequest request, OrganizationResponse response)
        {

        }

        protected static void onCreateHandler(ICDSPluginExecutionContext executionContext, Account target, EntityReference createdId)
        {
            target.Name = "HandlerExecuted";
        }

        protected static void onRetrieveHandler(ICDSExecutionContext executionContext, EntityReference targetId, ColumnSet columns, Account returnValue)
        {

        }

        protected static void onUpdateHandler(ICDSPluginExecutionContext executionContext, Account target)
        {
            target.Name = "HandlerExecuted";
        }

        protected static void onDeleteHandler(ICDSPluginExecutionContext executionContext, EntityReference deletedId)
        {

        }

        protected static void onQueryExecute(ICDSPluginExecutionContext executionContext, QueryExpression query, EntityCollection returnValue)
        {

        }
    }
}
