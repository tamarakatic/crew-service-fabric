using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Crew.CrewMemberActor.Interfaces
{
    [DataContract]
    public class CrewLocation : ICrewLocation
    {
        public CrewLocation(float longitude, float latitude, DateTime timestamp)
        {
            Latitude = latitude;
            Longitude = longitude;
            Timestamp = timestamp;
        }

        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public float Latitude { get; set; }

        [DataMember]
        public float Longitude { get; set; }
    }
}
