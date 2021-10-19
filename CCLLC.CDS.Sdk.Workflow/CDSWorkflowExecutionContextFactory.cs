namespace CCLLC.CDS.Sdk
{
    using System.Activities;
    using CCLLC.Core;
    using Microsoft.Xrm.Sdk.Workflow;

    public class CDSWorkflowExecutionContextFactory : ICDSWorkflowExecutionContextFactory<ICDSWorkflowExecutionContext>
    {
        public ICDSWorkflowExecutionContext CreateCDSExecutionContext(IWorkflowContext executionContext, CodeActivityContext codeActivityContext, IIocContainer container)
        {
            return new CDSWorkflowExecutionContext(executionContext, codeActivityContext, container);
        }
    }
}
