using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Owin;

namespace Crew.WebService
{
    public class Startup : IOwinAppBuilder
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.
                SupportedMediaTypes.
                Add(new MediaTypeHeaderValue("text/html"));

            appBuilder.UseWebApi(config);
        }
    }
}
