namespace CCLLC.CDS.Sdk.Registrations
{
    using Microsoft.Xrm.Sdk;

    public class CreateExecutionFilter<TEntity> : EntityExecutionFilter<ICreateExecutionFilter<TEntity>, TEntity>, ICreateExecutionFilter<TEntity> where TEntity : Entity, new()
    {
        public CreateExecutionFilter()
            : base()
        {
        }
    }
}
