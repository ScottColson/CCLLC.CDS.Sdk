namespace CCLLC.CDS.Sdk
{
    using System.Activities;
    using CCLLC.Core;
    using CCLLC.Telemetry;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Workflow;

    public class InstrumentedCDSWorkflowExecutionContext : InstrumentedCDSExecutionContext, IInstrumentedCDSWorkflowExecutionContext
    {
        public CodeActivityContext CodeActivityContext { get; }

        private IWorkflowContext WorkflowContext { get; }

        public string StageName => WorkflowContext.StageName;

        public int WorkflowCategory => WorkflowContext.WorkflowCategory;

        public int WorkflowMode => WorkflowContext.WorkflowMode;

        public IWorkflowContext ParentContext => WorkflowContext.ParentContext;

        public InstrumentedCDSWorkflowExecutionContext(IWorkflowContext executionContext, CodeActivityContext codeActivityContext, IIocContainer container, IComponentTelemetryClient telemetryClient)
          : base(executionContext, container, telemetryClient)
        {
            this.CodeActivityContext = codeActivityContext;
            this.WorkflowContext = executionContext;
        }

        protected override IOrganizationServiceFactory CreateOrganizationServiceFactory()
        {
            return this.CodeActivityContext.GetExtension<IOrganizationServiceFactory>();
        }

        protected override ITracingService CreateTracingService()
        {
            return this.CodeActivityContext.GetExtension<ITracingService>();
        }
    }
}

