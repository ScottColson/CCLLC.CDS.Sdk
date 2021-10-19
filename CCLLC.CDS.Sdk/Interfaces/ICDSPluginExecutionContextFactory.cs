namespace CCLLC.CDS.Sdk
{
    using System;
    using CCLLC.Core;
    using Microsoft.Xrm.Sdk;
    
    /// <summary>
    /// Factory for creating an enhanced CDS execution context based on <see cref="ICDSPluginExecutionContext"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICDSPluginExecutionContextFactory<T> where T :  ICDSPluginExecutionContext
    {
        /// <summary>
        /// Factory create method.
        /// </summary>
        /// <param name="executionContext"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="container"></param>
        /// <param name="runAs"></param>
        /// <returns></returns>
        T CreateCDSExecutionContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container, eRunAs runAs = eRunAs.User);
    }
}
