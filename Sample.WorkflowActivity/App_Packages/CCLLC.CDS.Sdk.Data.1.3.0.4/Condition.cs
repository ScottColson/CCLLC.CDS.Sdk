using Microsoft.Xrm.Sdk.Query;
using System;

namespace CCLLC.CDS.Sdk
{
    public class Condition<P> : ICondition<P> where P : IFilterable
    {
        public IFilter<P> Parent { get; }
        protected string AttributeName { get; }
        protected ConditionOperator Operator { get; private set; }
        protected object Value { get; private set; }
        private bool applyIfTrue = true;

        public Condition(IFilter<P> parent, string attributeName)
        {
            this.AttributeName = attributeName;
            this.Parent = parent;
            this.applyIfTrue = true;
        }

        public Condition(IFilter<P> parent, string attributeName, bool whenTrue)
        {
            this.AttributeName = attributeName;
            this.Parent = parent;
            this.applyIfTrue = whenTrue;
        }

        public IFilter<P> Is<T>(ConditionOperator conditionOperator, params T[] values)
        {
            AddToFilter<T>(conditionOperator, values);                     
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsEqualTo<T>(params T[] values)
        {
            AddToFilter(ConditionOperator.Equal, values);
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsNotEqualTo<T>(params T[] values)
        {
            AddToFilter<T>(ConditionOperator.NotEqual, values);
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsGreaterThan<T>(T value)
        {
            AddToFilter<T>(ConditionOperator.GreaterThan, value);           
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsGreaterThanOrEqualTo<T>(T value)
        {
            AddToFilter<T>(ConditionOperator.GreaterEqual, value);            
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsLessThan<T>(T value)
        {
            AddToFilter<T>(ConditionOperator.LessThan, value);            
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsLessThanOrEqualTo<T>(T value)
        {
            AddToFilter<T>(ConditionOperator.LessEqual, value);            
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsLike(params string[] values)
        {           
            //handle wild card conversion
            for(int i=0; i<values.Length; i++)
            {
                if (values[i] != null && values[i].Contains("*"))
                {
                    values[i] = values[i].Replace('*', '%');
                }
            }

            AddToFilter<string>(ConditionOperator.Like, values);            
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsNotNull()
        {
            AddToFilter(ConditionOperator.NotNull);            
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsNull()
        {
            AddToFilter(ConditionOperator.Null);
            return (IFilter<P>)Parent;
        }

        public ConditionExpression ToConditionExpression()
        {
            if(Value is null)
            {
                return new ConditionExpression(AttributeName, Operator);
            }

            return new ConditionExpression(AttributeName, Operator, Value);
        }

        private void AddToFilter(ConditionOperator conditionOperator)
        {
            if (!applyIfTrue) return;
            
            this.Operator = conditionOperator;
            this.Value = null;
            Parent.Conditions.Add(this);
        }
       
        private void AddToFilter<T>(ConditionOperator conditionOperator, T value)
        {
            if (!applyIfTrue) return;

            this.Operator = conditionOperator;
            this.Value = value;
            Parent.Conditions.Add(this);
        }

        private void AddToFilter<T>(ConditionOperator conditionOperator, T[] values)
        {
            if (!applyIfTrue) return;

            if (conditionOperator == ConditionOperator.In)
            {
                this.Operator = conditionOperator;
                this.Value = values;
                Parent.Conditions.Add(this);
                return;
            }

            if (values.Length <= 1)
            {
                this.Operator = conditionOperator;
                if(values.Length == 1)
                {
                    this.Value = values[0];
                }
                
                Parent.Conditions.Add(this);
                return;
            }

            //All other situation where more than one value are specified imply child Or filter
            var filterExpression = new Filter<P>(Parent, LogicalOperator.Or);
            foreach(var v in values)
            {
                var condition = new Condition<P>(filterExpression, AttributeName) ;
                condition.Is<object>(conditionOperator,v);                
            }

            Parent.Filters.Add(filterExpression);            
        }
    }
}
