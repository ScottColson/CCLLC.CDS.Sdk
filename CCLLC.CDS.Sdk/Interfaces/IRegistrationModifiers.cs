namespace CCLLC.CDS.Sdk
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.Xrm.Sdk;
    
    public interface IRegistrationModifiers { }

    public interface IRegistrationPreImageModifiers<TParent, TEntity> : IRegistrationModifiers where TEntity : Entity where TParent : IRegistrationModifiers
    {
        /// <summary>
        /// Forces a check for a pre-image during execution. If fields are identified then each field must exist in 
        /// the pre-image. Failure of the check will result in an exception.
        /// </summary>
        /// <returns></returns>
        TParent RequirePreImage();
        /// <summary>
        /// Forces a check for a pre-image during execution. If fields are identified then each field must exist in 
        /// the pre-image. Failure of the check will result in an exception.
        /// </summary>
        /// <returns></returns>
        TParent RequirePreImage(params string[] fields);
        /// <summary>
        /// Forces a check for a pre-image during execution. If fields are identified then each field must exist in 
        /// the pre-image. Failure of the check will result in an exception.
        /// </summary>
        /// <returns></returns>
        TParent RequirePreImage(Expression<Func<TEntity, object>> anonymousTypeInitializer);
    }

    public interface IRegistrationPostImageModifiers<TParent, TEntity> : IRegistrationModifiers where TEntity : Entity where TParent : IRegistrationModifiers
    {
        /// <summary>
        /// Forces a check for a post-image during execution. If fields are identified then each field must exist in 
        /// the post-image. Failure of the check will result in an exception.
        /// </summary>
        /// <returns></returns>
        TParent RequirePostImage();
        /// <summary>
        /// Forces a check for a post-image during execution. If fields are identified then each field must exist in 
        /// the post-image. Failure of the check will result in an exception.
        /// </summary>
        /// <returns></returns>
        TParent RequirePostImage(params string[] fields);
        /// <summary>
        /// Forces a check for a post-image during execution. If fields are identified then each field must exist in 
        /// the post-image. Failure of the check will result in an exception.
        /// </summary>
        /// <returns></returns>
        TParent RequirePostImage(Expression<Func<TEntity, object>> anonymousTypeInitializer);
    }

    public interface IActionRegistrationModifiers<TRequest> : IRegistrationModifiers where TRequest : OrganizationRequest
    {
        /// <summary>
        /// Prevent execution of the handler unless all conditions in the filter expression are met.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IActionRegistrationModifiers<TRequest> ExecuteIf(Action<IActionExecutionContextFilter<TRequest>> expression);
    }

    public interface IApiRegistrationModifiers<TRequest> : IRegistrationModifiers where TRequest : OrganizationRequest
    {
        /// <summary>
        /// Prevent execution of the handler unless all conditions in the filter expression are met.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IApiRegistrationModifiers<TRequest> ExecuteIf(Action<IApiExecutionContextFilter<TRequest>> expression);
    }

    public interface ICreateRegistrationModifiers<TEntity> : IRegistrationPostImageModifiers<ICreateRegistrationModifiers<TEntity>, TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Prevent execution of the handler unless all conditions in the filter expression are met.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ICreateRegistrationModifiers<TEntity> ExecuteIf(Action<ICreateExecutionFilter<TEntity>> expression);
    }

    public interface IRetrieveRegistrationModifiers<TEntity> : IRegistrationModifiers where TEntity : Entity
    {
        /// <summary>
        /// Prevent execution of the handler unless all conditions in the filter expression are met.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IRetrieveRegistrationModifiers<TEntity> ExecuteIf(Action<IRetrieveExecutionFilter> expression);
    }

    public interface IUpdateRegistrationModifiers<TEntity> : IRegistrationPreImageModifiers<IUpdateRegistrationModifiers<TEntity>, TEntity>, IRegistrationPostImageModifiers<IUpdateRegistrationModifiers<TEntity>, TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Prevent execution of the handler unless all conditions in the filter expression are met.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IUpdateRegistrationModifiers<TEntity> ExecuteIf(Action<IUpdateExecutionFilter<TEntity>> expression);
    }

    public interface IDeleteRegistrationModifiers<TEntity> : IRegistrationPreImageModifiers<IDeleteRegistrationModifiers<TEntity>, TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Prevent execution of the handler unless all conditions in the filter expression are met.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IDeleteRegistrationModifiers<TEntity> ExecuteIf(Action<IDeleteExecutionFilter> expression);
    }

    public interface IQueryRegistrationModifiers<TEntity> : IRegistrationModifiers where TEntity : Entity
    {
        /// <summary>
        /// Prevent execution of the handler unless all conditions in the filter expression are met.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryRegistrationModifiers<TEntity> ExecuteIf(Action<IQueryExecutionFilter> expression);
    }
}
