using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Collections.Generic;
using System.Text;


namespace shared.configuration
{
    public interface IDataverseServiceClient
    {
        ServiceClient Connect(DataverseConfiguration configuration);
    }
}
