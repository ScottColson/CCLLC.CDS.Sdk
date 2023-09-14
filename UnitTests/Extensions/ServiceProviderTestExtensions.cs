namespace CCLLC.CDS.Sdk.Tests
{
    using System;
    using Microsoft.Xrm.Sdk;

    public static class ServiceProviderTestExtensions
    {
        public static IPluginExecutionContext GetExecutionContext(this IServiceProvider serviceProvider)
        {
            _ = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            return (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
        }

        public static T GetOutputParameter<T>(this IServiceProvider serviceProvider, string paramName)
        {
            _ = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            var executionContext = serviceProvider.GetExecutionContext();

            return (T)executionContext.OutputParameters[paramName];
        }

        public static TEntity GetTarget<TEntity>(this IServiceProvider serviceProvider)
            where TEntity : Entity
        {
            _ = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            var executionContext = serviceProvider.GetExecutionContext();

            return (executionContext.InputParameters["Target"] as Entity).ToEntity<TEntity>();
        }
    }
}