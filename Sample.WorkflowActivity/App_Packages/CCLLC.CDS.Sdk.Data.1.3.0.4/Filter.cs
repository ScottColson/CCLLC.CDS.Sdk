using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    public partial class Filter<P> : Filterable<P>, IFilter<P> where P : IFilterable
    {
        private bool isQuickFind;

        public LogicalOperator Operator { get; }      
     
        public Filter(IFilterable<P> parent, LogicalOperator logicalOperator) : base()
        {            
            this.Parent = parent;
            this.Operator = logicalOperator;           
        }

        public IFilter<P> IsActive(bool value = true)
        {
            var condition = new Condition<P>(this, "statecode");
            condition.IsEqualTo<int>((value == true) ? 0 : 1);            
            return (IFilter<P>)this;
        }

        public IFilter<P> QuickFind(bool value = true)
        {
            isQuickFind = value;
            return this;
        }

        public IFilter<P> HasStatus(params int[] status)
        {
            var condition = new Condition<P>(this, "statuscode");
            condition.IsEqualTo<int>(status);            
            return this;
        }

        public IFilter<P> HasStatus<T>(params T[] status) where T : Enum
        {
            if(status.Length > 0)
            {
                var statusAsInt =  Array.ConvertAll(status, value => (int)(object)value);
                return HasStatus(statusAsInt);
            }
            return this;            
        }

        public FilterExpression ToFilterExpression(string searchValue)
        {
            var filterExpression = new FilterExpression
            {
                FilterOperator = Operator,
                IsQuickFindFilter = isQuickFind
            };
            
            foreach(var c in Conditions)
            {
                filterExpression.AddCondition(c.ToConditionExpression());
            }            
            foreach(var f in Filters)
            {
                filterExpression.AddFilter(f.ToFilterExpression(searchValue));
            }

            var searchFilter = GenerateSearchFilter(searchValue);
            if(searchFilter != null)
            {
                filterExpression.AddFilter(searchFilter);
            }

            return filterExpression;            
        }

        public ICondition<P> Attribute(string name)
        {
            return new Condition<P>(this, name);
        }

        public ICondition<P> Attribute(bool onlyWhenTrue, string name)
        {
            return new Condition<P>(this, name, onlyWhenTrue);
        }

        public IFilter<P> WithSearchFields(params string[] fields)
        {
            foreach(var f in fields)
            {
                if (!SearchFields.Contains(f))
                {
                    SearchFields.Add(f);
                }
            }
            return this;
        }

        public IFilter<P> WithSearchFields<E>(System.Linq.Expressions.Expression<Func<E, object>> anonymousTypeInitializer) where E : Entity
        {
            var fields = anonymousTypeInitializer.GetAttributeNamesArray<E>();
            return this.WithSearchFields(fields);
        }

        public IFilter<P> WithDateSearchFields(params string[] fields)
        {
            foreach (var f in fields)
            {
                if (!DateSearchFields.Contains(f))
                {
                    DateSearchFields.Add(f);
                }
            }
            return this;
        }

        public IFilter<P> WithDateSearchFields<E>(System.Linq.Expressions.Expression<Func<E, object>> anonymousTypeInitializer) where E : Entity
        {
            var fields = anonymousTypeInitializer.GetAttributeNamesArray<E>();
            return this.WithDateSearchFields(fields);
        }
    }

}
