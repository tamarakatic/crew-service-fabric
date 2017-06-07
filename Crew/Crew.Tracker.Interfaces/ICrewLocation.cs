using System;
using System.Threading.Tasks;
using Crew.CrewMemberActor.Interfaces.Models;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Crew.Tracker.Interfaces
{
    public interface ICrewLocation : IService
    {
        Task UpdateCrewLocation(String crewName, CrewLocation location);
        Task<CrewLocation> GetLastCrewLocation(String crewName);
    }
}
