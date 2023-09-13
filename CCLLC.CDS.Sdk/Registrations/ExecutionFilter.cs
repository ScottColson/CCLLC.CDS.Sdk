namespace CCLLC.CDS.Sdk.Registrations
{
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

        public IExecutionFilterUserCondition<TParent> ExecutingUser() 
        {
            var condition = new ExecutingUserCondition<IExecutionFilter>(this);
            AddCondition(condition);
            return (IExecutionFilterUserCondition<TParent>)condition;
        }
        
        public IExecutionFilterUserCondition<TParent> InitiatingUser()
        {
            var condition = new InitiatingUserCondition<IExecutionFilter>(this);
            AddCondition(condition);
            return (IExecutionFilterUserCondition<TParent>)condition;
        }

        public bool Test(ICDSPluginExecutionContext executionContext)
        {
            foreach(var condition in Conditions)
            {
                if (false == condition.Test(executionContext))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
