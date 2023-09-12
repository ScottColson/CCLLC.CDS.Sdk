namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.Xrm.Sdk;

    public class DeleteEventRegistration<TEntity> : EventRegistration, IDeleteRegistrationModifiers<TEntity>, IPluginEventRegistration where TEntity : Entity, new()
    {        
        public Action<ICDSPluginExecutionContext, EntityReference> PluginAction { get; set; }

        public DeleteEventRegistration() :
            base(new TEntity().LogicalName, MessageNames.Delete)
        { }

        protected override void InvokeRegistration(ICDSPluginExecutionContext executionContext)
        {
            var target = executionContext.TargetReference;
            PluginAction.Invoke(executionContext, target);
        }

        IDeleteRegistrationModifiers<TEntity> IDeleteRegistrationModifiers<TEntity>.ExecuteIf(Action<IDeleteExecutionFilter> expression)
        {
            throw new NotImplementedException("The ExecuteIf registration modifier is not implemented.");
        }

        IDeleteRegistrationModifiers<TEntity> IRegistrationPreImageModifiers<IDeleteRegistrationModifiers<TEntity>, TEntity>.RequirePreImage()
        {
            AddPreImageRequirement();
            return this;
        }

        IDeleteRegistrationModifiers<TEntity> IRegistrationPreImageModifiers<IDeleteRegistrationModifiers<TEntity>, TEntity>.RequirePreImage(params string[] fields)
        {
            AddPreImageRequirement(fields);
            return this;
        }

        IDeleteRegistrationModifiers<TEntity> IRegistrationPreImageModifiers<IDeleteRegistrationModifiers<TEntity>, TEntity>.RequirePreImage(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            AddPreImageRequirement(anonymousTypeInitializer);
            return this;
        }
    }
}
