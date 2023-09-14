namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.Xrm.Sdk;

    public class CreateEventRegistration<TEntity> : EventRegistration, ICreateRegistrationModifiers<TEntity> where TEntity : Entity, new()
    {
        public Action<ICDSPluginExecutionContext, TEntity, EntityReference> PluginAction { get; set; }

        public CreateEventRegistration() :
            base(new TEntity().LogicalName, MessageNames.Create)
        { 
        }

        protected override void InvokeRegistration(ICDSPluginExecutionContext executionContext)
        {
            TEntity target = executionContext.TargetEntity.ToEntity<TEntity>();         

            if (Stage == ePluginStage.PostOperation) 
            {
                var response = new EntityReference(this.EntityName,(Guid)executionContext.OutputParameters["id"]);
                PluginAction.Invoke(executionContext, target, response);
                executionContext.OutputParameters["id"] = response?.Id;
            }
            else
            {
                PluginAction.Invoke(executionContext, target, null);
            }          
        }

        public ICreateRegistrationModifiers<TEntity> ExecuteIf(Action<ICreateExecutionFilter<TEntity>> expression)
        {
            _ = expression ?? throw new ArgumentNullException(nameof(expression));

            var executionFilter = new CreateExecutionFilter<TEntity>();
            expression(executionFilter);
            AddExecutionFilter(executionFilter);
            return this;
        }

        public ICreateRegistrationModifiers<TEntity> RequirePostImage()
        {
            AddPostImageRequirement();
            return this;
        }

        public ICreateRegistrationModifiers<TEntity> RequirePostImage(params string[] fields)
        {
            AddPostImageRequirement(fields);
            return this;
        }

        public ICreateRegistrationModifiers<TEntity> RequirePostImage(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            AddPostImageRequirement(anonymousTypeInitializer);
            return this;
        }
    }
}
