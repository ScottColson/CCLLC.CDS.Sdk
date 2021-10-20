namespace CCLLC.CDS.Sdk
{
    using CCLLC.Telemetry;

    public interface IInstrumentedCDSWorkflowActivity : ICDSWorkflowActivity
    {
        ITelemetrySink TelemetrySink { get; }

        bool ConfigureTelemetrySink(ICDSWorkflowExecutionContext processContext);

        bool TrackExecutionPerformance { get; set; }

        bool FlushTelemetryAfterExecution { get; set; }

        string DefaultInstrumentationKey { get; set; }
    }
}
