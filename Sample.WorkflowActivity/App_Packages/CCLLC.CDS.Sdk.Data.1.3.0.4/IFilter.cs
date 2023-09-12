using System;
using System.Linq.Expressions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{   
    public interface IFilter : IFilterable
    {
        FilterExpression ToFilterExpression(string searchValue);
    }

    public interface IFilter<P> : IFilterable<P>, IFilter where P : IFilterable
    {
        LogicalOperator Operator { get; }

        IFilter<P> QuickFind(bool value = true);

        IFilter<P> IsActive(bool value = true);

        IFilter<P> HasStatus(params int[] status);

        IFilter<P> HasStatus<T>(params T[] status) where T : Enum;

        ICondition<P> Attribute(string name);

        ICondition<P> Attribute(bool onlyWhenTrue, string name);

        IFilter<P> WithSearchFields(params string[] fields);

        IFilter<P> WithSearchFields<E>(Expression<Func<E, object>> anonymousTypeInitializer) where E : Entity;

        IFilter<P> WithDateSearchFields(params string[] fields);

        IFilter<P> WithDateSearchFields<E>(Expression<Func<E, object>> anonymousTypeInitializer) where E : Entity;
    
    }   
}
