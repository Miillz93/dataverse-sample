using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace shared.configuration
{

    /// <summary>
    /// This interface provides an abstraction on top of IServiceProvider for commonly used PowerPlatform Dataverse Plugin development constructs
    /// </summary>
    public interface ILocalPluginContext
    {
        /// <summary>
        /// The PowerPlatform Dataverse organization service for the Current Executing user.
        /// </summary>
        IOrganizationService InitiatingUserService { get; }

        /// <summary>
        /// The PowerPlatform Dataverse organization service for the Account that was registered to run this plugin, This could be the same user as InitiatingUserService.
        /// </summary>
        IOrganizationService PluginUserService { get; }

        /// <summary>
        /// IPluginExecutionContext contains information that describes the run-time environment in which the plug-in executes, information related to the execution pipeline, and entity business information.
        /// </summary>
        IPluginExecutionContext PluginExecutionContext { get; }

        /// <summary>
        /// Synchronous registered plug-ins can post the execution context to the Microsoft Azure Service Bus. <br/>
        /// It is through this notification service that synchronous plug-ins can send brokered messages to the Microsoft Azure Service Bus.
        /// </summary>
        IServiceEndpointNotificationService NotificationService { get; }

        /// <summary>
        /// Provides logging run-time trace information for plug-ins.
        /// </summary>
        ITracingService TracingService { get; }

        /// <summary>
        /// General Service Provide for things not accounted for in the base class.
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// OrganizationService Factory for creating connection for other then current user and system.
        /// </summary>
        IOrganizationServiceFactory OrgSvcFactory { get; }

        /// <summary>
        /// ILogger for this plugin.
        /// </summary>
        ILogger Logger { get;  }

        /// <summary>
        /// Plugin Entity Target
        /// </summary>
        Microsoft.Xrm.Sdk.Entity Target 
        { 
            get;
        }

        /// <summary>
        /// Get Plugin Image
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Microsoft.Xrm.Sdk.Entity GetPluginImage(string alias, string name);

        /// <summary>
        /// Writes a trace message to the trace log.
        /// </summary>
        /// <param name="message">Message name to trace.</param>
        void Trace(string message, [CallerMemberName] string method = null);

    }
}
