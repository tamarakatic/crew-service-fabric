using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace Crew.CrewMemberActor.Interfaces
{
    public interface ICrewMemberActor : IActor
    {
        Task<KeyValuePair<float, float>> GetLatestLocationAsync();
        Task SetLocationAsync(DateTime timestamp, float latitude, float longitude);
        Task<DateTime?> GetLastReportTime();
    }
}
