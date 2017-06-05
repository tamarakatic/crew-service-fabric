using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crew.CrewMemberActor.Interfaces;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Crew.Tracker.Interfaces
{
    public interface ILocationViewer : IService
    {
        Task<CrewLocation> GetLastCrewLocation(Guid crewId);
        Task<DateTime?> GetLastReportTime(Guid crewId);
    }
}
