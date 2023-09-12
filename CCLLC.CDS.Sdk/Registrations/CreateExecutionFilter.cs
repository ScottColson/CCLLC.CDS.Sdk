namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using Microsoft.Xrm.Sdk;
    using System.Linq.Expressions;

    public class CreateExecutionFilter<TEntity> : EntityExecutionFilter<ICreateExecutionFilter<TEntity>, TEntity>, ICreateExecutionFilter<TEntity> where TEntity : Entity
    {
        public CreateExecutionFilter()
            : base()
        {
        }

        public new ICreateExecutionFilter<TEntity> Not()
        {
            return base.Not();
        }

        public new ICreateExecutionFilter<TEntity> ContainsAll(params string[] fields)
        {
            return base.ContainsAll(fields);
        }

        public new ICreateExecutionFilter<TEntity> ContainsAll(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            return base.ContainsAll(anonymousTypeInitializer);
        }

        public new ICreateExecutionFilter<TEntity> ContainsAny(params string[] fields)
        {
            return base.ContainsAny(fields);
        }

        public new ICreateExecutionFilter<TEntity> ContainsAny(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            return base.ContainsAny(anonymousTypeInitializer);
        }

        public override IExecutionFilterUserCondition<ICreateExecutionFilter<TEntity>> ExecutingUser()
        {
            var condition = new ExecutingUserCondition<ICreateExecutionFilter<TEntity>>(this);
            AddCondition(condition);
            return condition;
        }

        public new ICreateExecutionFilter<TEntity> HasStatus(params int[] status)
        {
            return base.HasStatus(status);
        }

        public override IExecutionFilterUserCondition<ICreateExecutionFilter<TEntity>> InitiatingUser()
        {
            var condition = new InitiatingUserCondition<ICreateExecutionFilter<TEntity>>(this);
            AddCondition(condition);
            return condition;
        }

        public new ICreateExecutionFilter<TEntity> IsActive()
        {
            return base.IsActive();
        }



        IExecutionFilterUserCondition<ICreateExecutionFilter<TEntity>> IExecutionFilter<ICreateExecutionFilter<TEntity>>.ExecutingUser()
        {
            throw new NotImplementedException();
        }

        ICreateExecutionFilter<TEntity> IEntityExecutionFilter<ICreateExecutionFilter<TEntity>, TEntity>.HasStatus<TStatus>(params TStatus[] status)
        {
            throw new NotImplementedException();
        }

        IExecutionFilterUserCondition<ICreateExecutionFilter<TEntity>> IExecutionFilter<ICreateExecutionFilter<TEntity>>.InitiatingUser()
        {
            throw new NotImplementedException();
        }

        IExecutionFilterValueCondition<ICreateExecutionFilter<TEntity>> IEntityExecutionFilter<ICreateExecutionFilter<TEntity>, TEntity>.IncomingValue(string fieldName)
        {
            throw new NotImplementedException();
        }
    }
}
