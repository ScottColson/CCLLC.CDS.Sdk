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
        /// <summary>
        /// Test attributes of the user that is initiating the execution.
        /// </summary>
        /// <returns></returns>
        IExecutionFilterUserCondition<TParent> InitiatingUser();

        /// <summary>
        /// Test attributes of the user that is executing the process..
        /// </summary>
        /// <returns></returns>
        IExecutionFilterUserCondition<TParent> ExecutingUser();

        /// <summary>
        /// Invert the result of the following condition.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Test whether incoming (target) entity contains all specified fields.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        TParent ContainsAll(params string[] fields);

        /// <summary>
        /// Test whether incoming (target) entity contains all specified fields.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        TParent ContainsAll(Expression<Func<TEntity, object>> anonymousTypeInitializer);

        /// <summary>
        /// Test whether incoming (target) entity contains at least one of the specified fields.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        TParent ContainsAny(params string[] fields);

        /// <summary>
        /// Test whether incoming (target) entity contains at least one of the specified fields.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        TParent ContainsAny(Expression<Func<TEntity, object>> anonymousTypeInitializer);

        /// <summary>
        /// Test whether incoming (target) entity has a particular Status Code. If multiple values are provided a match on any single value will satisfy the test.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        TParent HasStatus(params int[] status);

        /// <summary>
        /// Test whether incoming (target) entity has a particular Status Code. If multiple values are provided a match on any single value will satisfy the test.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        TParent HasStatus<TStatus>(params TStatus[] status) where TStatus : Enum;

        /// <summary>
        /// Test whether incoming (target) entity has an active State Code.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        TParent IsActive();

        /// <summary>
        /// Test value of a particular incoming (target) entity field.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Tests values of incoming (target) entity against values of the pre-image. Test passes if all specified fields have changed. Requires 
        /// a pre-image but any missing fields in the pre-image will be evaluated as null.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        IUpdateExecutionFilter<TEntity> ChangedAll(params string[] fields);

        /// <summary>
        /// Tests values of incoming (target) entity against values of the pre-image. Test passes if all specified fields have changed. Requires 
        /// a pre-image but any missing fields in the pre-image will be evaluated as null.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        IUpdateExecutionFilter<TEntity> ChangedAll(Expression<Func<TEntity, object>> anonymousTypeInitializer);

        /// <summary>
        /// Tests values of incoming (target) entity against values of the pre-image. Test passes if any specified fields have changed. Requires 
        /// a pre-image but any missing fields in the pre-image will be evaluated as null.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        IUpdateExecutionFilter<TEntity> ChangedAny(params string[] fields);

        /// <summary>
        /// Tests values of incoming (target) entity against values of the pre-image. Test passes if any specified fields have changed. Requires 
        /// a pre-image but any missing fields in the pre-image will be evaluated as null.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        IUpdateExecutionFilter<TEntity> ChangedAny(Expression<Func<TEntity, object>> anonymousTypeInitializer);

        /// <summary>
        /// Test value of a particular existing field based on the pre-image data. Requires a pre-image. Any 
        /// fields missing in the pre-image will be evaluated as null.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        IExecutionFilterValueCondition<IUpdateExecutionFilter<TEntity>> OriginalValue(string fieldName);

        /// <summary>
        /// Test value of a particular field based what the value would be after the update occurs. Requires a pre-image. Any
        /// fields missing in the pre-image will be evaluated as null.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        IExecutionFilterValueCondition<IUpdateExecutionFilter<TEntity>> CoalescedValue(string fieldName);
    }

    public interface IDeleteExecutionFilter : IExecutionFilter<IDeleteExecutionFilter>
    {
        /// <summary>
        /// Test value of a particular existing field based on the pre-image data. Requires a pre-image. Any 
        /// fields missing in the pre-image will be evaluated as null.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        IExecutionFilterValueCondition<IDeleteExecutionFilter> OriginalValue(string fieldName);
    }

    public interface IQueryExecutionFilter : IExecutionFilter<IQueryExecutionFilter>
    {
    }
}
