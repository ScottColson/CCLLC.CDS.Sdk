namespace CCLLC.CDS.Sdk.Registrations
{
    using System;

    public abstract class ExecutionFilterCondition : IExecutionFilterCondition
    {
        protected bool IsInverted { get; private set; } = false;
        public void Invert()
        {
            IsInverted = !IsInverted;
        }

        public bool Test(ICDSPluginExecutionContext executionContext)
        {
            return TestCondition(executionContext) ^ IsInverted;            
        }

        public abstract bool TestCondition(ICDSPluginExecutionContext executionContext);
 
    }
}
