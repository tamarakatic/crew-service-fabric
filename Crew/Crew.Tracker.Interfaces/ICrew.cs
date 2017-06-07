using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Crew.Tracker.Interfaces
{
    public interface ICrew : IService
    {
        Task CreateCrew(CrewMemberActor.Interfaces.Models.Crew crew);
        Task<CrewMemberActor.Interfaces.Models.Crew> GetCrewByName(string name);
    }
}
