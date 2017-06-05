using System.Runtime.Serialization;

namespace Crew.CrewMemberActor.Interfaces
{
    [DataContract]
    public class Vehicle : IVehicle
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public VehicleType VehicleType { get; set; }
    }
}
