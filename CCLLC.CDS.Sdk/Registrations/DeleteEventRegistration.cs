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

        public IDeleteRegistrationModifiers<TEntity> ExecuteIf(Action<IDeleteExecutionFilter> expression)
        {
            _ = expression ?? throw new ArgumentNullException(nameof(expression));

            var executionFilter = new DeleteExecutionFilter<TEntity>();
            expression(executionFilter);
            AddExecutionFilter(executionFilter);
            return this;
        }

        public IDeleteRegistrationModifiers<TEntity> RequirePreImage()
        {
            AddPreImageRequirement();
            return this;
        }

        public IDeleteRegistrationModifiers<TEntity> RequirePreImage(params string[] fields)
        {
            AddPreImageRequirement(fields);
            return this;
        }

        public IDeleteRegistrationModifiers<TEntity> RequirePreImage(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            AddPreImageRequirement(anonymousTypeInitializer);
            return this;
        }
    }
}
