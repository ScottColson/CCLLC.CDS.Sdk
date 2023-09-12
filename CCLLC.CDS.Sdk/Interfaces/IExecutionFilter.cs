namespace CCLLC.CDS.Sdk
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.Xrm.Sdk;

    public interface IExecutionFilter
    {
        bool Test(ICDSPluginExecutionContext executionContext);
    }

    public interface IExecutionFilter<TParent> : IExecutionFilter where TParent : IExecutionFilter
    {        
        IExecutionFilterUserCondition<TParent> InitiatingUser();
        IExecutionFilterUserCondition<TParent> ExecutingUser();
        TParent Not();
    }

    public interface IRequestExecutionContextFilter<TParent, TRequest> : IExecutionFilter<TParent> where TRequest : OrganizationRequest where TParent : IExecutionFilter 
    { 
    }

    public interface IActionExecutionContextFilter<TRequest> : IRequestExecutionContextFilter<IActionExecutionContextFilter<TRequest>, TRequest> where TRequest : OrganizationRequest
    {
    }

    public interface IApiExecutionContextFilter<TRequest> : IRequestExecutionContextFilter<IApiExecutionContextFilter<TRequest>, TRequest> where TRequest : OrganizationRequest
    {
    }

    public interface IEntityExecutionFilter<TParent, TEntity> : IExecutionFilter<TParent> where TEntity : Entity where TParent : IExecutionFilter
    {
        TParent ContainsAll(params string[] fields);
        TParent ContainsAll(Expression<Func<TEntity, object>> anonymousTypeInitializer);
        TParent ContainsAny(params string[] fields);
        TParent ContainsAny(Expression<Func<TEntity, object>> anonymousTypeInitializer);
        TParent HasStatus(params int[] status);
        TParent HasStatus<TStatus>(params TStatus[] status) where TStatus : Enum;
        TParent IsActive();
        IExecutionFilterValueCondition<TParent> IncomingValue(string fieldName);        
    }

    public interface ICreateExecutionFilter<TEntity> : IEntityExecutionFilter<ICreateExecutionFilter<TEntity>, TEntity> where TEntity : Entity
    {
    }

    public interface IRetrieveExecutionFilter : IExecutionFilter<IRetrieveExecutionFilter>
    {
    }

    public interface IUpdateExecutionFilter<TEntity> : IEntityExecutionFilter<IUpdateExecutionFilter<TEntity>, TEntity> where TEntity : Entity
    {
        IUpdateExecutionFilter<TEntity> ChangedAll(params string[] fields);
        IUpdateExecutionFilter<TEntity> ChangedAll(Expression<Func<TEntity, object>> anonymousTypeInitializer);
        IUpdateExecutionFilter<TEntity> ChangedAny(params string[] fields);
        IUpdateExecutionFilter<TEntity> ChangedAny(Expression<Func<TEntity, object>> anonymousTypeInitializer);
        IExecutionFilterValueCondition<IUpdateExecutionFilter<TEntity>> OriginalValue(string fieldName);
        IExecutionFilterValueCondition<IUpdateExecutionFilter<TEntity>> CoalescedValue(string fieldName);
    }

    public interface IDeleteExecutionFilter : IExecutionFilter<IDeleteExecutionFilter>
    {
        IExecutionFilterValueCondition<IDeleteExecutionFilter> OriginalValue(string fieldName);
    }

    public interface IQueryExecutionFilter : IExecutionFilter<IQueryExecutionFilter>
    {
    }
}
