namespace Sample.WorkflowActivity
{
    using System.Activities;
    using CCLLC.CDS.Sdk;
    using Microsoft.Xrm.Sdk.Workflow;    

    public class SimpleWorkflowActivity : CDSWorkflowActivity
    {
        [Input("data type input")]
        public InArgument Input1 { get; set; }

        [Output("data type output")]
        public OutArgument Output1 { get; set; }

        public override void ExecuteInternal(ICDSWorkflowExecutionContext executionContext)
        {
            // Get input
            var input1 = Input1.Get<string>(executionContext);

            // Do stuff 

            // Set output
            Output1.Set(executionContext, "value");
           
        }
    }
}
