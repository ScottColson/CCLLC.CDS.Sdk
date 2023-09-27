namespace CCLLC.CDS.Sdk.Registrations
{
    using Microsoft.Xrm.Sdk;
    using System;
    using System.Linq.Expressions;

    public class UpdateExecutionFilter<TEntity> : EntityExecutionFilter<IUpdateExecutionFilter<TEntity>, TEntity>, IUpdateExecutionFilter<TEntity> where TEntity : Entity, new()
    {
        private IUpdateRegistrationModifiers<TEntity> RegistrationModifier { get; }
        public UpdateExecutionFilter(IUpdateRegistrationModifiers<TEntity> registrationModifier)
            : base() 
        {
            RegistrationModifier = registrationModifier;
        }

        public IUpdateExecutionFilter<TEntity> ChangedAll(params string[] fields)
        {
            var condition = new ChangedAllValuesCondition(fields);
            AddCondition(condition);
            return this;
        }

        public IUpdateExecutionFilter<TEntity> ChangedAll(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {           
            var fields = anonymousTypeInitializer.GetAttributeNamesArray();
            return ChangedAll(fields);
        }

        public IUpdateExecutionFilter<TEntity> ChangedAny(params string[] fields)
        {
            var condition = new ChangedAnyValuesCondition(fields);
            AddCondition(condition);
            return this;
        }

        public IUpdateExecutionFilter<TEntity> ChangedAny(Expression<Func<TEntity, object>> anonymousTypeInitializer)
        {
            var fields = anonymousTypeInitializer.GetAttributeNamesArray();
            return ChangedAny(fields);
        }

        public IExecutionFilterValueCondition<IUpdateExecutionFilter<TEntity>> CoalescedValue(string fieldName)
        {
            var condition = new CoalescedValueCondition<IUpdateExecutionFilter<TEntity>>(this, fieldName);
            AddCondition(condition);
            return condition;
        }

        public IExecutionFilterValueCondition<IUpdateExecutionFilter<TEntity>> OriginalValue(string fieldName)
        {
            var condition = new PreImageValueCondition<IUpdateExecutionFilter<TEntity>>(this, fieldName);
            AddCondition(condition);
            return condition;
        }
    }
}
