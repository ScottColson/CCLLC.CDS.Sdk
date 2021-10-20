namespace CCLLC.CDS.Sdk
{
    using System.Activities;

    public static class ArgumentExtensions
    {
        public static object Get(this Argument argument, ICDSWorkflowExecutionContext executionContext)
        {
            return argument.Get(executionContext.CodeActivityContext);
        }

        public static T Get<T>(this Argument argument, ICDSWorkflowExecutionContext executionContext) 
        {
            return (T)argument.Get(executionContext.CodeActivityContext);
        }

        public static void Set(this Argument argument, ICDSWorkflowExecutionContext executionContext, object value)
        {
            argument.Set(executionContext.CodeActivityContext, value);
        }

        public static void Set<T>(this Argument argument, ICDSWorkflowExecutionContext executionContext, T value)
        {
            argument.Set(executionContext.CodeActivityContext, value);
        }
    }
}
