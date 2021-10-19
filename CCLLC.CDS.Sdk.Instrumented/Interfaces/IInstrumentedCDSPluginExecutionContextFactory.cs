using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Telemetry;

    public interface IInstrumentedCDSPluginExecutionContextFactory<T> where T :  IInstrumentedCDSPluginExecutionContext
    {
        T CreateCDSExecutionContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container, IComponentTelemetryClient telemetryClient );
    }
}
