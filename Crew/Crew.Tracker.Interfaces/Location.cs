using System;

namespace Crew.Tracker.Interfaces
{
    public class Location : ILocation
    {
        public Guid CrewId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
