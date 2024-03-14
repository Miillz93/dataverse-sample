using System.Text;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace shared.configuration
{
    public class DataverseServiceClient : IDataverseServiceClient
    {
        //private static ServiceClient
        public ServiceClient Connect(DataverseConfiguration configuration)
        {
            var connectionString = $"Url=https://{configuration.ServerName};AuthType=ClientSecret;ClientId={configuration.ClientId};ClientSecret={configuration.ClientSecret};RequireNewInstance=true";
            var serviceClient = new ServiceClient(connectionString);
            serviceClient.UseWebApi = true;

            if(!serviceClient.IsReady) throw new ArgumentNullException(nameof(serviceClient));

            return serviceClient;
            //var service = new ServiceClien
        }
    }
}
