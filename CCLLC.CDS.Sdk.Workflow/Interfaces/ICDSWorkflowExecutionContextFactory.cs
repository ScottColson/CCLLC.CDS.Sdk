namespace CCLLC.CDS.Sdk
{
    using System.Activities;
    using CCLLC.Core;
    using Microsoft.Xrm.Sdk.Workflow;

    /// <summary>
    /// Factory for creating an enhanced CDS execution context based on <see cref="ICDSExecutionContext"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface ICDSWorkflowExecutionContextFactory<T> where T : ICDSExecutionContext
    {        
        T CreateCDSExecutionContext(IWorkflowContext executionContext, CodeActivityContext codeActivityContext, IIocContainer container);
    }
}
