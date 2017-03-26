using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(TrelloIntegration.Startup))]
namespace TrelloIntegration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //[Environment]::SetEnvironmentVariable("TRELLO_APP_NAME", "SOME APPLICATION NAME", "Machine")
            //[Environment]::SetEnvironmentVariable("TRELLO_DEV_KEY", "273686af81c20054647f4a9915be4447", "Machine")
            // https://trello.com/1/authorize?expiration=never&scope=read,write&response_type=token&name=TrelloIntegrationMvc&key=273686af81c20054647f4a9915be4447

            var AppName = Environment.GetEnvironmentVariable("TRELLO_APP_NAME", EnvironmentVariableTarget.Machine);
            var DevKey = Environment.GetEnvironmentVariable("TRELLO_DEV_KEY", EnvironmentVariableTarget.Machine);

            if (!TrelloIntegration.Library.TrelloApiConfig.ConfigValid())
            {
                // not sure about this exception, perhaps should use my own
                throw new System.Configuration.ConfigurationException("Both TRELLO_APP_NAME and TRELLO_DEV_KEY are required");
            }


        }
        
    }

  
}
