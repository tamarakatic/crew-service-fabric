using System.Net.Http.Headers;
using System.Web.Http;
using Owin;
using Swashbuckle.Application;

namespace Crew.WebService
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

            config
                .EnableSwagger(c => c.SingleApiVersion("v1", "Crew API"))
                .EnableSwaggerUi();

            appBuilder.UseWebApi(config);
        }
    }
}
