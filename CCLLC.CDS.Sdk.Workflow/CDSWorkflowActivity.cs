using System;
using System.Activities;
namespace CCLLC.CDS.Sdk
{
    using System.Globalization;
    using CCLLC.Core;
    using CCLLC.Core.Net;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Workflow;    

    public abstract partial class CDSWorkflowActivity : CodeActivity, ICDSWorkflowActivity
    {
        private IIocContainer _container = null;

        /// <summary>
        /// Provides an <see cref="IIocContainer"/> instance to register all objects used by the
        /// base plugin. 
        /// </summary>
        public virtual IIocContainer Container
        {
            get
            {
                if (_container == null) { _container = new IocContainer(); }
                return _container;
            }
        }

        public CDSWorkflowActivity()
        {            
            Container.Implement<ICache>().Using<DefaultCache>().AsSingleInstance();
            Container.Implement<ISettingsProviderFactory>().Using<SettingsProviderFactory>().AsSingleInstance();
            Container.Implement<ISettingsProviderDataConnector>().Using<EnvironmentVariablesDataConnector>().AsSingleInstance();
            Container.Implement<IXmlConfigurationResourceFactory>().Using<XmlConfigurationResourceFactory>().AsSingleInstance();
            Container.Implement<IWebRequestFactory>().Using<WebRequestFactory>().AsSingleInstance();
            Container.Implement<ICDSWorkflowExecutionContextFactory<ICDSWorkflowExecutionContext>>().Using<CDSWorkflowExecutionContextFactory>().AsSingleInstance();
        }

       

        /// <summary>
        /// The handler called by CRM when a WFA is executed. Spins up a <see cref="ICDSWorkflowExecutionContext"/>
        /// instance and passes it to the <see cref="ExecuteInternal(ICDSWorkflowExecutionContext)"/> method
        /// which is implemented in the inheriting class.
        /// </summary>
        /// <param name="codeActivityContext">Context information passed in by CRM.</param>
        protected override void Execute(CodeActivityContext codeActivityContext)
        {
            if (codeActivityContext == null) { throw new ArgumentNullException("codeActivityContext"); }

            var tracingService = codeActivityContext.GetExtension<ITracingService>();

            tracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Entered {0}.Execute()", this.GetType().ToString()));
            
            var executionContext = codeActivityContext.GetExtension<IWorkflowContext>();

            try
            {
                var factory = Container.Resolve<ICDSWorkflowExecutionContextFactory<ICDSWorkflowExecutionContext>>();

                using (var cdsExecutionContext = factory.CreateCDSExecutionContext(executionContext, codeActivityContext, Container))
                {
                    ExecuteInternal(cdsExecutionContext);
                } 
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
            {
                if (tracingService != null)
                {
                    tracingService.Trace(string.Format("Exception: {0}", ex.Message));
                }
                throw new InvalidPluginExecutionException(ex.Message,ex);
            }
            catch (Exception ex)
            {
                if (tracingService != null)
                {
                    tracingService.Trace(string.Format("Unhandled Exception: {0}", ex.Message));
                }
                throw new InvalidPluginExecutionException(string.Format("Unhandled Workflow Exception {0}", ex.Message), ex);
            }

        }
        
        /// <summary>
        /// Handler that must be implemented in inheriting class to do the actual work of the WFA.
        /// </summary>
        /// <param name="localContext">Instance of <see cref="ILocalWorkflowActivityContext"/> passed 
        /// in from <see cref="Execute(CodeActivityContext)"/>.</param>
        public abstract void ExecuteInternal(ICDSWorkflowExecutionContext executionContext);

    }
}
