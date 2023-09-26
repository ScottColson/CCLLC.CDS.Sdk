namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using Microsoft.Xrm.Sdk;
    using System.Linq.Expressions;

    public abstract class EntityExecutionFilter<TParent, TEntity> : ExecutionFilter<TParent>, IEntityExecutionFilter<TParent, TEntity> where TEntity : Entity where TParent : IExecutionFilter
    {
        public EntityExecutionFilter()
            : base()
        {
        }

        public TParent ContainsAll(params string[] fields)
        {
            var condition = new ContainsAllFilterCondition(fields);
            AddCondition(condition);
            return (TParent)(object)this;      
        }

        public TParent ContainsAll(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            var fields = anonymousTypeInitializer.GetAttributeNamesArray();
            return ContainsAll(fields);
        }

        public TParent ContainsAny(params string[] fields)
        {
            var condition = new ContainsAnyFilterCondition(fields);
            AddCondition(condition);
            return (TParent)(object)this;
        }

        public TParent ContainsAny(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            var fields = anonymousTypeInitializer.GetAttributeNamesArray();
            return ContainsAny(fields);
        }

        public TParent HasStatus<TStatus>(params TStatus[] status) where TStatus : Enum
        {
            IExecutionFilterValueCondition<TParent> condition = IncomingValue("statuscode");
            return condition.IsEqualTo<TStatus>(status);
        }

        public TParent HasStatus(params int[] status)
        {
            IExecutionFilterValueCondition<TParent> condition = IncomingValue("statuscode");
            return condition.IsEqualTo<int>(status);
        }

        public TParent IsActive()
        {
            var condition = IncomingValue("statecode");
            return condition.IsEqualTo(0);
        }

        public IExecutionFilterValueCondition<TParent> IncomingValue(string fieldName) 
        {
            var condition = new TargetValueCondition<TParent>(this, fieldName);
            AddCondition(condition);
            return condition;
        }
    }
}
