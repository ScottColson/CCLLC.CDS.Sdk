namespace CCLLC.CDS.Sdk
{
    using System;
    using System.Activities;
    using System.Collections.Generic;
    using System.Globalization;
    using CCLLC.Core.Net;
    using CCLLC.Telemetry;
    using CCLLC.Telemetry.Client;
    using CCLLC.Telemetry.Context;
    using CCLLC.Telemetry.EventLogger;
    using CCLLC.Telemetry.Serializer;
    using CCLLC.Telemetry.Sink;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Workflow;

    public abstract class InstrumentedCDSWorkflowActivity : CDSWorkflowActivity, IInstrumentedCDSWorkflowActivity
    {       

        /// <summary>
        /// Provides a <see cref="ITelemetrySink"/> to receive and process various 
        /// <see cref="ITelemetry"/> items generated during the execution of the 
        /// WorkflowActivity.
        /// </summary>
        public virtual ITelemetrySink TelemetrySink { get; private set; }
       
        public InstrumentedCDSWorkflowActivity()
            : base()
        {
            this.TrackExecutionPerformance = true;
            this.FlushTelemetryAfterExecution = false;

            // Dependencies for instrumented execution context.
            Container.Implement<IInstrumentedCDSWorkflowExecutionContextFactory<IInstrumentedCDSWorkflowExecutionContext>>().Using<InstrumentedCDSWorkflowExecutionContextFactory>().AsSingleInstance();
            Container.Implement<IInstrumentedWebRequestFactory>().Using<InstrumenetedWebRequestFactory>().AsSingleInstance();

            // Telemetry issue event logger. Use inert logger for plugins because we don't have
            // the required security level to interact directly with the event log.
            Container.Implement<IEventLogger>().Using<InertEventLogger>().AsSingleInstance();

            // Setup the objects needed to create/capture telemetry items.
            Container.Implement<ITelemetryFactory>().Using<TelemetryFactory>().AsSingleInstance();  //ITelemetryFactory is used to create new telemetry items.
            Container.Implement<ITelemetryClientFactory>().Using<TelemetryClientFactory>().AsSingleInstance(); //ITelemetryClientFactory is used to create and configure a telemetry client.
            Container.Implement<ICDSWorkflowTelemetryPropertyManager>().Using<CDSWorkflowTelemetryPropertyManager>().AsSingleInstance(); //Plugin property manager.
            Container.Implement<ITelemetryContext>().Using<TelemetryContext>(); //ITelemetryContext is a dependency for telemetry creation.
            Container.Implement<ITelemetryInitializerChain>().Using<TelemetryInitializerChain>(); //ITelemetryInitializerChain is a dependency for building a telemetry client.

            // Setup the objects needed to buffer and send telemetry to Application Insights. TelemetrySink
            // is setup as a single instance so that all plugins share the same sink. This may not always be
            // desirable. If multiple TelemetrySinks are needed then overwrite this implementation in the 
            // inheriting class.
            Container.Implement<ITelemetrySink>().Using<TelemetrySink>().AsSingleInstance(); //ITelemetrySink receives telemetry from one or more telemetry clients, processes it, buffers it, and transmits it.
            Container.Implement<ITelemetryProcessChain>().Using<TelemetryProcessChain>(); //ITelemetryProcessChain holds 0 or more processors that can modify the telemetry prior to transmission.
            Container.Implement<ITelemetryChannel>().Using<SyncMemoryChannel>(); //ITelemetryChannel provides the buffering and transmission. There is a sync and an asynch channel.
            Container.Implement<ITelemetryBuffer>().Using<TelemetryBuffer>(); //ITelemetryBuffer is used the channel
            Container.Implement<ITelemetryTransmitter>().Using<AITelemetryTransmitter>(); //ITelemetryTransmitter transmits a block of telemetry to Application Insights.

            // Setup the objects needed to serialize telemetry as part of transmission.
            Container.Implement<IContextTagKeys>().Using<AIContextTagKeys>(); //Defines context tags expected by Application Insights.
            Container.Implement<ITelemetrySerializer>().Using<AITelemetrySerializer>(); //Serialize telemetry items into a compressed Gzip data.
            Container.Implement<IJsonWriterFactory>().Using<JsonWriterFactory>(); //Factory to create JSON converters as needed.
        }

       
        /// <summary>
        /// Flag to capture execution performance metrics using request telemetry.
        /// </summary>
        public bool TrackExecutionPerformance { get; set; }

        /// <summary>
        /// Flag to force flushing the telemetry sink buffer after handler execution completes.
        /// </summary>
        public bool FlushTelemetryAfterExecution { get; set; }
        public string DefaultInstrumentationKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        protected override void Execute(CodeActivityContext codeActivityContext)
        {
            _ = codeActivityContext ?? throw new ArgumentNullException(nameof(codeActivityContext));

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var success = true;
            var responseCode = "200";

            var tracingService = codeActivityContext.GetExtension<ITracingService>();
            tracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Entered {0}.Execute()", this.GetType().ToString()));
                       
            var executionContext = codeActivityContext.GetExtension<IWorkflowContext>();

            // Setup sink if it has not already been done. This is done here to cover the possibility that an implementing class might override
            // the default, non-static, property implementation.
            var lockObj = new object();
            lock (lockObj)
            {
                if (TelemetrySink is null)
                {
                    TelemetrySink = Container.Resolve<ITelemetrySink>();
                }
            }

            var telemetryFactory = Container.Resolve<ITelemetryFactory>();
            var telemetryClientFactory = Container.Resolve<ITelemetryClientFactory>();

            //Create a dictionary of custom telemetry properties based on values in the execution context.
            var propertyManager = Container.Resolve<ICDSWorkflowTelemetryPropertyManager>();
            var properties = propertyManager.CreatePropertiesDictionary(this.GetType().ToString(), executionContext);

            //generate a telemetry client capturing all execution context information. Values
            //that do not map cleanly to telemetry context are kept in the custom properties
            //list.
            using (var telemetryClient = telemetryClientFactory.BuildClient(
                this.GetType().ToString(),
                this.TelemetrySink,
                properties))
            {

                #region Setup Telementry Context

                telemetryClient.Context.Operation.Name = executionContext.MessageName;
                telemetryClient.Context.Operation.CorrelationVector = executionContext.CorrelationId.ToString();
                telemetryClient.Context.Operation.Id = executionContext.OperationId.ToString();
                telemetryClient.Context.Session.Id = executionContext.CorrelationId.ToString();
                         
                //use data context if supported.
                var asDataContext = telemetryClient.Context as ISupportDataKeyContext;
                if (asDataContext != null)
                {
                    asDataContext.Data.RecordSource = executionContext.OrganizationName;
                    asDataContext.Data.RecordType = executionContext.PrimaryEntityName;
                    asDataContext.Data.RecordId = executionContext.PrimaryEntityId.ToString();
                }
                
                #endregion

                try
                {
                    var factory = Container.Resolve<IInstrumentedCDSWorkflowExecutionContextFactory<IInstrumentedCDSWorkflowExecutionContext>>();

                    using (var cdsExecutionContext = factory.CreateCDSExecutionContext(executionContext, codeActivityContext, Container, telemetryClient))
                    {
                        if (!TelemetrySink.IsConfigured)
                        {
                            TelemetrySink.OnConfigure = () => { return this.ConfigureTelemetrySink(cdsExecutionContext); };
                        }

                        try
                        {
                            ExecuteInternal(cdsExecutionContext);
                        }
                        catch (InvalidWorkflowException ex)
                        {
                            success = false;
                            responseCode = "400"; //business rule exception
                            if (telemetryClient != null && telemetryFactory != null)
                            {
                                telemetryClient.Track(telemetryFactory.BuildMessageTelemetry(ex.Message, eSeverityLevel.Error));
                            }
                            throw;
                        }
                        catch (InvalidPluginExecutionException ex)
                        {
                            success = false;
                            responseCode = "400"; //business rule exception
                            if (telemetryClient != null && telemetryFactory != null)
                            {
                                telemetryClient.Track(telemetryFactory.BuildMessageTelemetry(ex.Message, eSeverityLevel.Error));
                            }
                            throw;
                        }
                        catch (Exception ex)
                        {
                            success = false;
                            responseCode = "500"; //Unexpected exception.
                            if (telemetryClient != null && telemetryFactory != null)
                            {
                                telemetryClient.Track(telemetryFactory.BuildExceptionTelemetry(ex));
                            }
                            throw;
                        }
                        finally
                        {
                            if (this.TrackExecutionPerformance && telemetryFactory != null && telemetryClient != null)
                            {
                                var r = telemetryFactory.BuildRequestTelemetry("WorkflowExecution", null, new Dictionary<string, string> { { "handlerName", "ExecuteInternal" } });
                                r.Duration = sw.Elapsed;
                                r.ResponseCode = responseCode;
                                r.Success = success;

                                telemetryClient.Track(r);
                            }

                            if (this.FlushTelemetryAfterExecution && telemetryClient != null)
                            {
                                telemetryClient.Flush();
                            }

                            sw.Stop();
                        }

                    } //using localContext
                }
                catch (InvalidPluginExecutionException ex)
                {
                    if (tracingService != null)
                    {
                        tracingService.Trace(string.Format("Exception: {0}", ex.Message));
                    }
                    throw;
                }
                catch (InvalidWorkflowException ex)
                {   if (tracingService != null)
                    {
                        tracingService.Trace(string.Format("Exception: {0}", ex.Message));
                    }
                    throw new InvalidPluginExecutionException(ex.Message, ex); //wrap the invalid workflow exception in an invalid plugin execution exception.
                }
                catch (Exception ex)
                {                    
                    if (tracingService != null)
                    {
                        tracingService.Trace(string.Format("Exception: {0}", ex.Message));
                    }
                    throw new InvalidPluginExecutionException(string.Format("Unhandled Workflow Exception {0}", ex.Message), ex);
                }                

            } //using telemetryClient

            sw.Stop();
            sw = null;
        }

        public virtual bool ConfigureTelemetrySink(ICDSWorkflowExecutionContext cdsExecutionContext)
        {
            if (cdsExecutionContext != null)
            {
                var key = cdsExecutionContext.Settings.GetValue<string>("Telemetry.InstrumentationKey");

                if (!string.IsNullOrEmpty(key))
                {
                    cdsExecutionContext.TracingService.Trace("Retrieved Telemetry Instrumentation Key.");
                    TelemetrySink.ProcessChain.TelemetryProcessors.Add(new SequencePropertyProcessor());
                    TelemetrySink.ProcessChain.TelemetryProcessors.Add(new InstrumentationKeyPropertyProcessor(key));
                    TelemetrySink.Channel.Buffer.Capacity = 5000;

                    return true; //telemetry sink is configured.
                }
            }

            return false; //telemetry sink is not configured.
        }

    }
}
