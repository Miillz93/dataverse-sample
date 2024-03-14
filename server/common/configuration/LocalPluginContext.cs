using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System;
using System.Runtime.CompilerServices;

namespace shared.configuration
{
    
    /// <summary>
    /// Plug-in context object.
    /// </summary>
    public class LocalPluginContext : ILocalPluginContext
    {
        /// <summary>
        /// The PowerPlatform Dataverse organization service for the Current Executing user.
        /// </summary>
        public IOrganizationService InitiatingUserService { get; }

        /// <summary>
        /// The PowerPlatform Dataverse organization service for the Account that was registered to run this plugin, This could be the same user as InitiatingUserService.
        /// </summary>
        public IOrganizationService PluginUserService { get; }

        /// <summary>
        /// IPluginExecutionContext contains information that describes the run-time environment in which the plug-in executes, information related to the execution pipeline, and entity business information.
        /// </summary>
        public IPluginExecutionContext PluginExecutionContext { get; }

        /// <summary>
        /// Synchronous registered plug-ins can post the execution context to the Microsoft Azure Service Bus. <br/>
        /// It is through this notification service that synchronous plug-ins can send brokered messages to the Microsoft Azure Service Bus.
        /// </summary>
        public IServiceEndpointNotificationService NotificationService { get; }

        /// <summary>
        /// Provides logging run-time trace information for plug-ins.
        /// </summary>
        public ITracingService TracingService { get; }

        /// <summary>
        /// General Service Provider for things not accounted for in the base class.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }


        /// <summary>
        /// OrganizationService Factory for creating connection for other then current user and system.
        /// </summary>
        public IOrganizationServiceFactory OrgSvcFactory { get; }

        /// <summary>
        /// ILogger for this plugin.
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// Plugin Entity Target
        /// </summary>
        public Microsoft.Xrm.Sdk.Entity Target { 
            get 
            {
                return (Microsoft.Xrm.Sdk.Entity) this.PluginExecutionContext.InputParameters["Target"];
            }
        }

        /// <summary>
        /// Helper object that stores the services available in this plug-in.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public LocalPluginContext(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new InvalidPluginExecutionException(nameof(serviceProvider));
            }

            ServiceProvider = serviceProvider;

            Logger = serviceProvider.Get<ILogger>();

            PluginExecutionContext = serviceProvider.Get<IPluginExecutionContext>();

            TracingService = new LocalTracingService(serviceProvider);

            NotificationService = serviceProvider.Get<IServiceEndpointNotificationService>();

            OrgSvcFactory = serviceProvider.Get<IOrganizationServiceFactory>();

            PluginUserService = serviceProvider.GetOrganizationService(PluginExecutionContext.UserId); // User that the plugin is registered to run as, Could be same as current user.

            InitiatingUserService = serviceProvider.GetOrganizationService(PluginExecutionContext.InitiatingUserId); //User who's action called the plugin.

        }


        /// <summary>
        /// Get Plugin Image
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Microsoft.Xrm.Sdk.Entity GetPluginImage(string name, string alias = null)
        {
            if (this.PluginExecutionContext.PreEntityImages.ContainsKey(name))
            {
                return (Microsoft.Xrm.Sdk.Entity)this.PluginExecutionContext.PreEntityImages[alias][name];
            }
            else if (this.PluginExecutionContext.PostEntityImages.ContainsKey(name))
            {
                return (Microsoft.Xrm.Sdk.Entity)this.PluginExecutionContext.PostEntityImages[alias][name];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Writes a trace message to the trace log.
        /// </summary>
        /// <param name="message">Message name to trace.</param>
        public void Trace(string message, [CallerMemberName] string method = null)
        {
            if (string.IsNullOrWhiteSpace(message) || TracingService == null)
            {
                return;
            }

            if (method != null)
                TracingService.Trace($"[{method}] - {message}");
            else
                TracingService.Trace($"{message}");
        }
    }

}
