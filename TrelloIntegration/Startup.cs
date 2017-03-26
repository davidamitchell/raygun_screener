using Microsoft.Owin;
using Owin;
using System;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(TrelloIntegration.Startup))]
namespace TrelloIntegration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            if (!TrelloIntegration.Library.TrelloApiConfig.ConfigValid())
            {
                // not sure about this exception, perhaps should use my own
                throw new ConfigurationException("Both TRELLO_APP_NAME and TRELLO_DEV_KEY are required to be set in as environment variables");
            }
        }
        
    }

}
