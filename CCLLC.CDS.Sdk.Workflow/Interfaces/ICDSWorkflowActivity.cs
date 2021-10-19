namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    public interface ICDSWorkflowActivity
    {
        IIocContainer Container { get; }  
        
        void ExecuteInternal(ICDSWorkflowExecutionContext context);       
    }
}
