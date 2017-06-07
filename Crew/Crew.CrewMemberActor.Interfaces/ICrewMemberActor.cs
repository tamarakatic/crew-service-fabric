using System.Threading.Tasks;
using Crew.CrewMemberActor.Interfaces.Models;
using Microsoft.ServiceFabric.Actors;

namespace Crew.CrewMemberActor.Interfaces
{
    public interface ICrewMemberActor : IActor
    {
        Task CreateCrewAsync(Models.Crew crew);
        Task UpdateLocation(CrewLocation location);
        Task<Models.Crew> GetCrewByName();
        Task<CrewLocation> GetLatestLocation();
        Task CreateMember(Member member);
        Task AddPlannedAssignment(Planned assignment);
        Task AddUnplannedAssignment(Unplanned assignment);
    }
}
