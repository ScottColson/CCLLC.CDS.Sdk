namespace CCLLC.CDS.Sdk
{
    using System.Activities;
    using CCLLC.Core;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Workflow;

    public class CDSWorkflowExecutionContext : CDSExecutionContext, ICDSWorkflowExecutionContext 
    {
        public CodeActivityContext CodeActivityContext { get; }
        private IWorkflowContext WorkflowContext { get; }

        public string StageName => WorkflowContext.StageName;

        public int WorkflowCategory => WorkflowContext.WorkflowCategory;

        public int WorkflowMode => WorkflowContext.WorkflowMode;

        public IWorkflowContext ParentContext => throw new System.NotImplementedException();

        public CDSWorkflowExecutionContext(IWorkflowContext executionContext, CodeActivityContext codeActivityContext, IIocContainer container)
          : base(executionContext, container)
        {
            this.CodeActivityContext = codeActivityContext;
            this.WorkflowContext = executionContext;
        }

        protected override IOrganizationServiceFactory CreateOrganizationServiceFactory()
        {
            return  this.CodeActivityContext.GetExtension<IOrganizationServiceFactory>();
        }

        protected override ITracingService CreateTracingService()
        {
            return this.CodeActivityContext.GetExtension<ITracingService>();            
        }
    }
}
