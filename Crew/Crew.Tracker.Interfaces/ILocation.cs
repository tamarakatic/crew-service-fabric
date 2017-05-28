using System;

namespace Crew.Tracker.Interfaces
{
    public interface ILocation
    {
        Guid CrewId { get; set; }
        float Latitude { get; set; }
        float Longitude { get; set; }
    }
}
