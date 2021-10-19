using CCLLC.Telemetry;

namespace CCLLC.CDS.Sdk

{
    public interface ISupportWorkflowActivityInstrumentation
    {
        ITelemetrySink TelemetrySink { get; }
        bool ConfigureTelemetrySink(ILocalWorkflowActivityContext localContext);

        bool TrackExecutionPerformance { get; set; }

        bool FlushTelemetryAfterExecution { get; set; }
    }
}


