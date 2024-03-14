using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace shared.configuration
{
    /// <summary>
    /// Base class for all plug-in classes.
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public abstract class PluginBase : IPlugin
    {
        protected string PluginClassName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginBase"/> class.
        /// </summary>
        /// <param name="pluginClassName">The <see cref="Type"/> of the plugin class.</param>
        internal PluginBase(Type pluginClassName)
        {
            PluginClassName = pluginClassName.ToString();
        }

        /// <summary>
        /// Main entry point for he business logic that the plug-in is to execute.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <remarks>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Execute")]
        public void Execute(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new InvalidPluginExecutionException(nameof(serviceProvider));
            }

            // Construct the local plug-in context.
            var localPluginContext = new LocalPluginContext(serviceProvider);

            localPluginContext.Trace($"Entered {PluginClassName}.Execute() " +
                $"Correlation Id: {localPluginContext.PluginExecutionContext.CorrelationId}, " +
                $"Initiating User: {localPluginContext.PluginExecutionContext.InitiatingUserId}");

            try
            {
                // Invoke the custom implementation
                ExecuteDataversePlugin(localPluginContext);

                // Now exit - if the derived plugin has incorrectly registered overlapping event registrations, guard against multiple executions.
                return;
            }
            catch (FaultException<OrganizationServiceFault> orgServiceFault)
            {
                localPluginContext.Trace($"Exception: {orgServiceFault.ToString()}");

                throw new InvalidPluginExecutionException($"OrganizationServiceFault: {orgServiceFault.Message}", orgServiceFault);
            }
            finally
            {
                localPluginContext.Trace($"Exiting {PluginClassName}.Execute()");
            }
        }

        /// <summary>
        /// Placeholder for a custom plug-in implementation.
        /// </summary>
        /// <param name="localPluginContext">Context for the current plug-in.</param>
        protected virtual void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            // Do nothing.
        }
    }
}
