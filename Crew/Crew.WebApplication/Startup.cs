using System.Net.Http.Headers;
using System.Web.Http;
using Owin;

namespace Crew.WebApplication
{
    public class Startup : IOwinAppBuilder
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Formatters
                  .JsonFormatter
                  .SupportedMediaTypes
                  .Add(new MediaTypeHeaderValue("text/html"));

            appBuilder.UseWebApi(config);
        }
    }
}
