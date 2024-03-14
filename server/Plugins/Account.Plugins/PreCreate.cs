using Microsoft.Xrm.Sdk;
using System;


namespace Entity.Plugins
{
    /// <summary>
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public class PostCreate : PluginBase
    {
        public PostCreate(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(PostCreate))
        {
            // TODO: Implement your custom configuration handling
            // https://docs.microsoft.com/powerapps/developer/common-data-service/register-plug-in#set-configuration-data
        }

        // Entry point for custom business logic execution
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null) throw new ArgumentNullException(nameof(localPluginContext));

            var service = localPluginContext.PluginUserService;
            var target = localPluginContext.Target;
            var entityImage = localPluginContext.GetPluginImage("preImage", "");
            //service.Update()
            //localPluginContext.GetImage()
            
        }
    }
}
