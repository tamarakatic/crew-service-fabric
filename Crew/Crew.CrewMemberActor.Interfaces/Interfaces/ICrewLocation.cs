using System;

namespace Crew.CrewMemberActor.Interfaces.Interfaces
{
    public interface ICrewLocation
    {
        DateTime Timestamp { get; set; }
        float Latitude { get; set; }
        float Longitude { get; set; }
    }
}
