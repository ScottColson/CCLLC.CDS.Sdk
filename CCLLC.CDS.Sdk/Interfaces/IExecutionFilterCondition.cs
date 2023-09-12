namespace CCLLC.CDS.Sdk
{
    public interface IExecutionFilterCondition
    {
        void Invert();
        bool Test(ICDSPluginExecutionContext executionContext);
    }
}
