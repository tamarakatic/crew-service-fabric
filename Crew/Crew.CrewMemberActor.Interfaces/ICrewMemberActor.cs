using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace Crew.CrewMemberActor.Interfaces
{
    public interface ICrewMemberActor : IActor
    {
        Task<CrewLocation> GetLatestLocationAsync();
        Task SetLocationAsync(CrewLocation location);
        Task<DateTime?> GetLastReportTime();
        Task CreateCrewAsync(Crew crew);
        Task<Crew> GetCrewByName();
    }
}
