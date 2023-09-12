using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    public interface IFilterable 
    {
        IList<IFilter> Filters { get; }
        IList<ICondition> Conditions { get; }        
    }
    
    public interface IFilterable<P> : IFilterable where P : IFilterable
    {
        IFilterable<P> Parent { get; }
        P WhereAll(Action<IFilter<P>> expression);

        /// <summary>
        /// Evaluate contained conditions using a logical OR.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        P WhereAny(Action<IFilter<P>> expression);
    }   
}
