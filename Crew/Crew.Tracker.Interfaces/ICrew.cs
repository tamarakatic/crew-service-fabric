using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Crew.Tracker.Interfaces
{
    public interface ICrew : IService
    {
        Task CreateCrew(CrewMemberActor.Interfaces.Crew crew);
        Task<CrewMemberActor.Interfaces.Crew> GetCrewByName(string crewName);
    }
}
