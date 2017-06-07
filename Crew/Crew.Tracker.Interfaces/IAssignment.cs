using System;
using System.Threading.Tasks;
using Crew.CrewMemberActor.Interfaces.Models;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Crew.Tracker.Interfaces
{
    public interface IAssignment : IService
    {
        Task AddPlannedAssignment(String crewName, Planned assignment);
        Task AddUnplannedAssignment(String crewName, Unplanned assignment);
    }
}
