namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.Xrm.Sdk;

    public class UpdateEventRegistration<TEntity> : EventRegistration, IUpdateRegistrationModifiers<TEntity> where TEntity : Entity, new()
    {        

        public Action<ICDSPluginExecutionContext, TEntity> PluginAction { get; set; }

        public UpdateEventRegistration() 
            : base(new TEntity().LogicalName, MessageNames.Update)
        {
        }

        protected override void InvokeRegistration(ICDSPluginExecutionContext executionContext)
        {
            TEntity target = executionContext.TargetEntity.ToEntity<TEntity>();
            PluginAction.Invoke(executionContext, target);
        }

        public IUpdateRegistrationModifiers<TEntity> ExecuteIf(Action<IUpdateExecutionFilter<TEntity>> expression)
        {
            _ = expression ?? throw new ArgumentNullException(nameof(expression));

            var executionFilter = new UpdateExecutionFilter<TEntity>(this);
            expression(executionFilter);
            AddExecutionFilter(executionFilter);
            return this;
        }

        public IUpdateRegistrationModifiers<TEntity> RequirePreImage()
        {
            AddPreImageRequirement();
            return this;
        }

        public IUpdateRegistrationModifiers<TEntity> RequirePreImage(params string[] fields)
        {
            AddPreImageRequirement(fields);
            return this;
        }

        public IUpdateRegistrationModifiers<TEntity> RequirePreImage(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            AddPreImageRequirement(anonymousTypeInitializer);
            return this;
        }

        public IUpdateRegistrationModifiers<TEntity> RequirePostImage()
        {
            AddPostImageRequirement();
            return this;
        }

        public IUpdateRegistrationModifiers<TEntity> RequirePostImage(params string[] fields)
        {
            AddPostImageRequirement(fields);
            return this;
        }

        public IUpdateRegistrationModifiers<TEntity> RequirePostImage(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            AddPostImageRequirement(anonymousTypeInitializer);
            return this;
        }
    }
}
