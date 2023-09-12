using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{  
    public interface IQueryExpressionBuilder<E> : IFluentQuery<IQueryExpressionBuilder<E>,E> where E : Entity 
    {
        IQueryExpressionBuilder<E> WithSearchValue(string searchValue);
        QueryExpression Build();
    }    
}
