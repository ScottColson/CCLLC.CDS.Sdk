namespace CCLLC.CDS.Sdk.Interfaces
{
    public interface IParameterValueCondition<TParent> where TParent : IExecutionFilter
    {
        TParent IsNull();
        TParent IsNotNull();
        TParent IsEqualTo<T>(params T[] values);
        TParent IsNotEqualTo<T>(params T[] values);
        TParent IsGreaterThanOrEqualTo<T>(T value);
        TParent IsGreaterThan<T>(T value);
        TParent IsLessThanOrEqualTo<T>(T value);
        TParent IsLessThan<T>(T value);
        TParent IsLike(params string[] values);
    }
}
