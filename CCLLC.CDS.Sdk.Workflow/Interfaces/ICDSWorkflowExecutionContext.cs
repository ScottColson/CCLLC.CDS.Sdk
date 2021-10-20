namespace CCLLC.CDS.Sdk
{
    using System.Activities;
    using Microsoft.Xrm.Sdk.Workflow;

    public interface ICDSWorkflowExecutionContext : ICDSExecutionContext, IWorkflowContext
    {
        CodeActivityContext CodeActivityContext { get; }
    }
}
