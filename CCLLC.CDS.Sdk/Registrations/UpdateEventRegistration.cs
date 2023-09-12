namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.Xrm.Sdk;

    public class UpdateEventRegistration<E> : EventRegistration, IUpdateRegistrationModifiers<E> where E : Entity, new()
    {        

        public Action<ICDSPluginExecutionContext, E> PluginAction { get; set; }

        public UpdateEventRegistration() 
            : base(new E().LogicalName, MessageNames.Update)
        {
        }

        protected override void InvokeRegistration(ICDSPluginExecutionContext executionContext)
        {
            E target = executionContext.TargetEntity.ToEntity<E>();
            PluginAction.Invoke(executionContext, target);
        }

        IUpdateRegistrationModifiers<E> IUpdateRegistrationModifiers<E>.ExecuteIf(Action<IUpdateExecutionFilter<E>> expression)
        {
            throw new NotImplementedException("The ExecuteIf registration modifier is not implemented.");
        }

        IUpdateRegistrationModifiers<E> IRegistrationPreImageModifiers<IUpdateRegistrationModifiers<E>, E>.RequirePreImage()
        {
            AddPreImageRequirement();
            return this;
        }

        IUpdateRegistrationModifiers<E> IRegistrationPreImageModifiers<IUpdateRegistrationModifiers<E>, E>.RequirePreImage(params string[] fields)
        {
            AddPreImageRequirement(fields);
            return this;
        }

        IUpdateRegistrationModifiers<E> IRegistrationPreImageModifiers<IUpdateRegistrationModifiers<E>, E>.RequirePreImage(Expression<Func<E, object>> anonymousTypeInitializer)
        {
            AddPreImageRequirement(anonymousTypeInitializer);
            return this;
        }

        IUpdateRegistrationModifiers<E> IRegistrationPostImageModifiers<IUpdateRegistrationModifiers<E>, E>.RequirePostImage()
        {
            AddPostImageRequirement();
            return this;
        }

        IUpdateRegistrationModifiers<E> IRegistrationPostImageModifiers<IUpdateRegistrationModifiers<E>, E>.RequirePostImage(params string[] fields)
        {
            AddPostImageRequirement(fields);
            return this;
        }

        IUpdateRegistrationModifiers<E> IRegistrationPostImageModifiers<IUpdateRegistrationModifiers<E>, E>.RequirePostImage(Expression<Func<E, object>> anonymousTypeInitializer)
        {
            AddPostImageRequirement(anonymousTypeInitializer);
            return this;
        }
    }
}
