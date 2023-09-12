namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using System.Collections.Generic;

    public abstract class ExecutionFilter<TParent> : IExecutionFilter, IExecutionFilter<TParent> where TParent : IExecutionFilter
    {
        public ExecutionFilter()
        {
        }

        private bool invertNextCondition = false;

        private IList<IExecutionFilterCondition> Conditions { get; } = new List<IExecutionFilterCondition>();

        protected IExecutionFilter ThisFilter => this;

        protected void AddCondition(IExecutionFilterCondition condition)
        {
            if (invertNextCondition)
            {
                condition.Invert();
                invertNextCondition = false;
            }

            Conditions.Add(condition);
        }

        public TParent Not()
        {
            invertNextCondition = !invertNextCondition;
            return (TParent)ThisFilter;
        }

       
        
        public abstract IExecutionFilterUserCondition<TParent> ExecutingUser();
        
        public abstract IExecutionFilterUserCondition<TParent> InitiatingUser();

        bool IExecutionFilter.Test(ICDSPluginExecutionContext executionContext)
        {
            throw new NotImplementedException();
        }
    }
}
