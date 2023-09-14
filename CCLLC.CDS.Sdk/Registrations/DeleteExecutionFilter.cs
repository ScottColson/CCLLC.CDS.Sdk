namespace CCLLC.CDS.Sdk.Registrations
{
    using Microsoft.Xrm.Sdk;

    public class DeleteExecutionFilter<TEntity> : EntityExecutionFilter<IDeleteExecutionFilter, TEntity>, IDeleteExecutionFilter where TEntity : Entity, new()
    {
        public IExecutionFilterValueCondition<IDeleteExecutionFilter> OriginalValue(string fieldName)
        {
            var condition = new PreImageValueCondition<IDeleteExecutionFilter>(this, fieldName);
            AddCondition(condition);
            return condition;
        }
    }
}
