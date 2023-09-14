namespace CCLLC.CDS.Sdk.Registrations
{
    using System;
    using System.Linq;
    using Microsoft.Xrm.Sdk;

    public abstract class ChangedValuesCondition : ExecutionFilterCondition, IExecutionFilterCondition
    {
        protected string[] Fields { get; }

        public  ChangedValuesCondition(string[] fields)
        {
            Fields = fields;
        }

        protected bool HasFieldChanged(ICDSPluginExecutionContext executionContext, string field)
        {
            var existingValue = executionContext.PreImage?.GetAttributeValue<object>(field);
            var newValue = executionContext.TargetEntity?.GetAttributeValue<object>(field);

            if (existingValue is null && newValue is null)
            {
                return false;
            }

            if (existingValue is null ||newValue is null)
            {
                return true;
            }

            if (existingValue.GetType() != newValue.GetType())
            {
                return true;
            }

            if (existingValue.GetType() == typeof(string))
            {
                string newAsString = (string)newValue;
                string existingAsString = (string)existingValue;

                return string.Compare(newAsString, existingAsString) != 0;
            }
            else if (existingValue.GetType() == typeof(OptionSetValue))
            {
                int newOption = ((OptionSetValue)newValue).Value;
                int existingOption = ((OptionSetValue)existingValue).Value;

                return newOption != existingOption;
            }
            else if (existingValue.GetType() == typeof(EntityReference))
            {
                Guid newGuid = ((EntityReference)newValue).Id;
                Guid existingGuid = ((EntityReference)existingValue).Id;

                return newGuid != existingGuid;
            }
            else if (existingValue.GetType() == typeof(Money))
            {
                decimal newMoney = ((Money)newValue).Value;
                decimal existingMoney = ((Money)existingValue).Value;

                return newMoney != existingMoney;
            }
            else if (existingValue.GetType() == typeof(OptionSetValueCollection))
            {
                var newOptions = ((OptionSetValueCollection)newValue).Select(r => r.Value).OrderBy(r => r).ToArray();
                var existingOptions = ((OptionSetValueCollection)existingValue).Select(r => r.Value).OrderBy(r => r).ToArray();

                if(newOptions.Length != existingOptions.Length)
                {
                    return true;
                }

                for(int i=0; i >= newOptions.Length-1; i++)
                {
                    if(newOptions[i] != existingOptions[i])
                    {
                        return true;
                    }
                }
            }

            return false == newValue.Equals(existingValue);
        }
    }

    public class ChangedAnyValuesCondition : ChangedValuesCondition
    {
        public ChangedAnyValuesCondition(string[] fields) 
            : base(fields)
        {
        }

        public override bool TestCondition(ICDSPluginExecutionContext executionContext)
        {
            foreach(var field in Fields)
            {
                if (true == HasFieldChanged(executionContext, field))
                {
                    return true;
                }
            }

            return false;            
        }
    }

    public class ChangedAllValuesCondition : ChangedValuesCondition
    {
        public ChangedAllValuesCondition(string[] fields) : base(fields)
        {
        }

        public override bool TestCondition(ICDSPluginExecutionContext executionContext)
        {
            foreach (var field in Fields)
            {
                if (false == HasFieldChanged(executionContext, field))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
