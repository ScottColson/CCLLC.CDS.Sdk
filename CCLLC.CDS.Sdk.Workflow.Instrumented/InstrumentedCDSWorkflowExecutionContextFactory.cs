namespace CCLLC.CDS.Sdk
{
    using System.Activities;
    using CCLLC.Core;
    using CCLLC.Telemetry;
    using Microsoft.Xrm.Sdk.Workflow;

    public class InstrumentedCDSWorkflowExecutionContextFactory : IInstrumentedCDSWorkflowExecutionContextFactory<IInstrumentedCDSWorkflowExecutionContext>
    {
        public IInstrumentedCDSWorkflowExecutionContext CreateCDSExecutionContext(IWorkflowContext executionContext, CodeActivityContext codeActivityContext, IIocContainer container, IComponentTelemetryClient telemetryClient)
        {
            return new InstrumentedCDSWorkflowExecutionContext(executionContext, codeActivityContext, container, telemetryClient);
        }
    }
}
