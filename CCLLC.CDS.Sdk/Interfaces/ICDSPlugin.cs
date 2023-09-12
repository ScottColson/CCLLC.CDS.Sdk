namespace CCLLC.CDS.Sdk
{
    using System;
    using CCLLC.Core;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;

    /// <summary>
    /// Enhanced plugin definition for creating CDS plugins that provides support for dependency injection.
    /// </summary>
    public interface ICDSPlugin
    {
        /// <summary>
        /// Inversion of Control/Dependency Injection container used to inject various dependencies required during the execution 
        /// of the plugin.
        /// </summary>
        IIocContainer Container { get; }

        /// <summary>
        /// Execution flag that indicates whether the code runs under the security profile of the user or the system. When 
        /// set to System then the OrganizationService always runs with elevated permissions.
        /// </summary>
        eRunAs RunAs { get; set; }

        /// <summary>
        /// Mechanism to register a generic plugin event handler for a particular event signature.
        /// </summary>
        /// <param name="entityName">Logical name of the entity that triggered the execution</param>
        /// <param name="messageName">Message name for the call</param>
        /// <param name="stage">Plugin stage for the call</param>
        /// <param name="handler">The handler that will be executed</param>
        /// <param name="handlerId">An ad that can be passed through to telemetry when used.</param>
        void RegisterEventHandler(string entityName, string messageName, ePluginStage stage, Action<ICDSPluginExecutionContext> handler, string handlerId = "");

        ICreateRegistrationModifiers<TEntity> RegisterCreateHandler<TEntity>(ePluginStage stage, Action<ICDSPluginExecutionContext, TEntity, EntityReference> handler, string handlerId = "") where TEntity : Entity, new();

        IUpdateRegistrationModifiers<TEntity> RegisterUpdateHandler<TEntity>(ePluginStage stage, Action<ICDSPluginExecutionContext, TEntity> handler, string handlerId = "") where TEntity : Entity, new();

        IDeleteRegistrationModifiers<TEntity> RegisterDeleteHandler<TEntity>(ePluginStage stage, Action<ICDSPluginExecutionContext, EntityReference> handler, string handlerId = "") where TEntity : Entity, new();

        IRetrieveRegistrationModifiers<TEntity> RegisterRetrieveHandler<TEntity>(ePluginStage stage, Action<ICDSPluginExecutionContext, EntityReference, ColumnSet, TEntity> handler, string handlerId = "") where TEntity : Entity, new();

        IQueryRegistrationModifiers<TEntity> RegisterQueryHandler<TEntity>(ePluginStage stage, Action<ICDSPluginExecutionContext, QueryExpression, EntityCollection> handler, string handlerId = "") where TEntity : Entity, new();

        IActionRegistrationModifiers<TRequest> RegisterActionHandler<TRequest, TResponse>(ePluginStage stage, Action<ICDSPluginExecutionContext, TRequest, TResponse> handler, string handlerId = "") 
            where TRequest : OrganizationRequest, new() 
            where TResponse : OrganizationResponse, new();

        IApiRegistrationModifiers<TRequest> RegisterApiHandler<TRequest, TResponse>(Action<ICDSPluginExecutionContext, TRequest, TResponse> handler, string handlerId = "")
            where TRequest : OrganizationRequest, new()
            where TResponse : OrganizationResponse, new();
    }
}
