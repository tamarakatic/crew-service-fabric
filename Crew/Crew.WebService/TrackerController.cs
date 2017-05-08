using System.Web.Http;

namespace Crew.WebService
{
    public class TrackerController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Welcome to Crew application!";
        }
    }
}
