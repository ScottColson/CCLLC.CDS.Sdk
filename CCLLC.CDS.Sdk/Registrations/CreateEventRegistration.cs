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

        ICreateRegistrationModifiers<TEntity> ICreateRegistrationModifiers<TEntity>.ExecuteIf(Action<ICreateExecutionFilter<TEntity>> expression)
        {
            throw new NotImplementedException("The ExecuteIf registration modifier is not implemented.");
        }        

        ICreateRegistrationModifiers<TEntity> IRegistrationPostImageModifiers<ICreateRegistrationModifiers<TEntity>, TEntity>.RequirePostImage()
        {
            AddPostImageRequirement();
            return this;
        }

        ICreateRegistrationModifiers<TEntity> IRegistrationPostImageModifiers<ICreateRegistrationModifiers<TEntity>, TEntity>.RequirePostImage(params string[] fields)
        {
            AddPostImageRequirement(fields);
            return this;
        }

        ICreateRegistrationModifiers<TEntity> IRegistrationPostImageModifiers<ICreateRegistrationModifiers<TEntity>, TEntity>.RequirePostImage(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            AddPostImageRequirement(anonymousTypeInitializer);
            return this;
        }
    }
}
