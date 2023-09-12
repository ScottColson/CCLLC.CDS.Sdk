namespace CCLLC.CDS.Sdk.Registrations
{
    using System;

    public abstract class ContainsFilterCondition : ExecutionFilterCondition, IExecutionFilterCondition
    {
        protected string[] Fields { get; }

        public ContainsFilterCondition(params string[] fields)
        {
            Fields = fields;
        }

        override public abstract bool TestCondition(ICDSPluginExecutionContext executionContext);
    }

    public class ContainsAnyFilterCondition : ContainsFilterCondition
    {
        public ContainsAnyFilterCondition(params string[] fields)
            : base(fields)
        {
        }

        public override bool TestCondition(ICDSPluginExecutionContext executionContext)
        {
            return executionContext.TargetEntity.ContainsAny(Fields);
        }
    }

    public class ContainsAllFilterCondition : ContainsFilterCondition
    {
        public ContainsAllFilterCondition(params string[] fields)
            : base(fields)
        {
        }

        public override bool TestCondition(ICDSPluginExecutionContext executionContext)
        {
            var target = executionContext.TargetEntity;

            foreach(var field in Fields)
            {
                if(false == executionContext.TargetEntity.Contains(field))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
