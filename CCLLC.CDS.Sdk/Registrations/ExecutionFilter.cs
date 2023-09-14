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
            return (TParent)(object)this;
        }

        public IExecutionFilterUserCondition<TParent> ExecutingUser() 
        {
            var condition = new ExecutingUserCondition<TParent>((TParent)(object)this);
            AddCondition(condition);
            return condition;
        }
        
        public IExecutionFilterUserCondition<TParent> InitiatingUser()
        {
            var condition = new InitiatingUserCondition<TParent>((TParent)(object)this);
            AddCondition(condition);
            return condition;
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
