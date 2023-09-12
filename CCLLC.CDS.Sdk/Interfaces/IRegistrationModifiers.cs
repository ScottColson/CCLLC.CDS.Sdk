namespace CCLLC.CDS.Sdk
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.Xrm.Sdk;
    
    public interface IRegistrationModifiers { }

    public interface IRegistrationPreImageModifiers<TParent, TEntity> : IRegistrationModifiers where TEntity : Entity where TParent : IRegistrationModifiers
    {
        TParent RequirePreImage();
        TParent RequirePreImage(params string[] fields);
        TParent RequirePreImage(Expression<Func<TEntity, object>> anonymousTypeInitializer);
    }

    public interface IRegistrationPostImageModifiers<TParent, TEntity> : IRegistrationModifiers where TEntity : Entity where TParent : IRegistrationModifiers
    {
        TParent RequirePostImage();
        TParent RequirePostImage(params string[] fields);
        TParent RequirePostImage(Expression<Func<TEntity, object>> anonymousTypeInitializer);
    }

    public interface IActionRegistrationModifiers<TRequest> : IRegistrationModifiers where TRequest : OrganizationRequest
    {
        IActionRegistrationModifiers<TRequest> ExecuteIf(Action<IActionExecutionContextFilter<TRequest>> expression);
    }

    public interface IApiRegistrationModifiers<TRequest> : IRegistrationModifiers where TRequest : OrganizationRequest
    {
        IApiRegistrationModifiers<TRequest> ExecuteIf(Action<IApiExecutionContextFilter<TRequest>> expression);
    }

    public interface ICreateRegistrationModifiers<TEntity> : IRegistrationPostImageModifiers<ICreateRegistrationModifiers<TEntity>, TEntity> where TEntity : Entity
    {
        ICreateRegistrationModifiers<TEntity> ExecuteIf(Action<ICreateExecutionFilter<TEntity>> expression);
    }

    public interface IRetrieveRegistrationModifiers<TEntity> : IRegistrationModifiers where TEntity : Entity
    {
        IRetrieveRegistrationModifiers<TEntity> ExecuteIf(Action<IRetrieveExecutionFilter> expression);
    }

    public interface IUpdateRegistrationModifiers<TEntity> : IRegistrationPreImageModifiers<IUpdateRegistrationModifiers<TEntity>, TEntity>, IRegistrationPostImageModifiers<IUpdateRegistrationModifiers<TEntity>, TEntity> where TEntity : Entity
    {
        IUpdateRegistrationModifiers<TEntity> ExecuteIf(Action<IUpdateExecutionFilter<TEntity>> expression);
    }

    public interface IDeleteRegistrationModifiers<TEntity> : IRegistrationPreImageModifiers<IDeleteRegistrationModifiers<TEntity>, TEntity> where TEntity : Entity
    {
        IDeleteRegistrationModifiers<TEntity> ExecuteIf(Action<IDeleteExecutionFilter> expression);
    }

    public interface IQueryRegistrationModifiers<TEntity> : IRegistrationModifiers where TEntity : Entity
    {
        IQueryRegistrationModifiers<TEntity> ExecuteIf(Action<IQueryExecutionFilter> expression);
    }
}
