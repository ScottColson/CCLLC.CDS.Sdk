namespace CCLLC.CDS.Sdk
{
    using System.Activities;
    using CCLLC.Core;
    using CCLLC.Telemetry;
    using Microsoft.Xrm.Sdk.Workflow;

    public interface IInstrumentedCDSWorkflowExecutionContextFactory<T> where T : IInstrumentedCDSExecutionContext
    {
        T CreateCDSExecutionContext(IWorkflowContext executionContext, CodeActivityContext codeActivityContext, IIocContainer container, IComponentTelemetryClient telemetryClient);
    }
}
