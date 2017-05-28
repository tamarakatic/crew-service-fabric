using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Crew.Tracker.Interfaces
{
    public interface ILocationReporter : IService
    {
        Task ReportLocation(Location location);
    }
}
