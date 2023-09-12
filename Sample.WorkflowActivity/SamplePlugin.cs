namespace Sample.Plugnin
{
    using CCLLC.CDS.Sdk;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using Shared.Proxies;

    public class SamplePlugin : CDSPlugin
    {
        public SamplePlugin(string unsecureConfig, string secureConfig) 
            : base(unsecureConfig, secureConfig)
        {           
            RegisterCreateHandler<Account>(ePluginStage.PreOperation, onCreateHandler)
                .ExecuteIf(filter => filter
                    .ExecutingUser().SystemUser()
                    .Not().InitiatingUser().MemberOfTeam("Compliance")
                    .Not().ContainsAny(target => new {target.Name})
                    .IncomingValue(Account.Fields.Name).IsEqualTo("Sam"))
                .RequirePostImage();

            RegisterUpdateHandler<Account>(ePluginStage.PreOperation, onUpdateHandler)
                .RequirePreImage(image => new { image.AccountCategoryCode, image.AccountNumber, image.Name, image.StateCode })
                .ExecuteIf(filter => filter
                    .ChangedAll(target => new { target.Name, target.AccountNumber }))          
                .ExecuteIf(filter => filter
                    .ContainsAll("statecode")
                    .ChangedAny(target => new { target.AccountCategoryCode, target.AccountClassificationCode }))
                .ExecuteIf(filter => filter
                    .IsActive()
                    .OriginalValue(Account.Fields.Name).IsNotNull()
                    .IncomingValue(Account.Fields.Name).IsNull()
                    .CoalescedValue(Account.Fields.AccountCategoryCode)
                        .IsEqualTo(Account.eAccountCategoryCode.PreferredCustomer))
                .RequirePostImage();

            RegisterRetrieveHandler<Account>(ePluginStage.PreOperation, onRetrieveHandler)
                .ExecuteIf(filter => filter
                    .InitiatingUser().SystemUser());

            RegisterDeleteHandler<Account>(ePluginStage.PreOperation, onDeleteHandler)
                .RequirePreImage(image => new { image.Name, image.OwnerId })
                .ExecuteIf(filter => filter
                    .Not().OriginalValue(Account.Fields.AccountCategoryCode).IsEqualTo(Account.eAccountCategoryCode.PreferredCustomer));

            RegisterQueryHandler<Account>(ePluginStage.PreOperation, onQueryExecute)
                .ExecuteIf(filter => filter
                    .InitiatingUser().HasRole("Compliance Admin"));

            RegisterActionHandler<OrganizationRequest, OrganizationResponse>(ePluginStage.PostOperation, onActionExecute)
               .ExecuteIf(filter => filter
                   .InitiatingUser().SystemUser());

            RegisterApiHandler<OrganizationRequest, OrganizationResponse>(onApiExecute)
               .ExecuteIf(filter => filter
               
                   .InitiatingUser().SystemUser());
        }

        private static void onActionExecute(ICDSExecutionContext executionContext, OrganizationRequest request, OrganizationResponse response)
        {

        }

        private static void onApiExecute(ICDSExecutionContext executionContext, OrganizationRequest request, OrganizationResponse response)
        {

        }

        private static void onCreateHandler(ICDSPluginExecutionContext executionContext, Account target, EntityReference createdId)
        {

        }

        private static void onRetrieveHandler(ICDSExecutionContext executionContext, EntityReference targetId, ColumnSet columns, Account returnValue)
        {

        }

        private static void onUpdateHandler(ICDSPluginExecutionContext executionContext, Account target)
        {

        }

        private static void onDeleteHandler(ICDSPluginExecutionContext executionContext, EntityReference deletedId)
        {

        }

        private static void onQueryExecute(ICDSPluginExecutionContext executionContext, QueryExpression query, EntityCollection returnValue)
        {

        }
    }
}
