using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Crew.Tracker.Interfaces
{
    public interface ILocationViewer : IService
    {
        Task<KeyValuePair<float, float>?> GetLastCrewLocation(Guid crewId);
        Task<DateTime?> GetLastReportTime(Guid crewId);
    }
}
