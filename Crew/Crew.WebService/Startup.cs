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
            ConfigureFormatters(config.Formatters);

            appBuilder.UseWebApi(config);
        }

        private void ConfigureFormatters(MediaTypeFormatterCollection formatterCollection)
        {
            formatterCollection.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
