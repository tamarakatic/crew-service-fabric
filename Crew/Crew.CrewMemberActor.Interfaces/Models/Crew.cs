using Crew.CrewMemberActor.Interfaces.Interfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Crew.CrewMemberActor.Interfaces.Models
{
    [DataContract]
    public class Crew : ICrew
    {
        public Crew()
        {
            this.Members = new List<Member>();
            this.PlannedAssignments = new List<Planned>();
            this.UnplannedAssignments = new List<Unplanned>();
            this.Vehicles = new List<Vehicle>();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public CrewLocation Location { get; set; }

        [DataMember]
        public List<Member> Members { get; set; }

        [DataMember]
        public List<Planned> PlannedAssignments { get; set; }

        [DataMember]
        public List<Unplanned> UnplannedAssignments { get; set; }

        [DataMember]
        public List<Vehicle> Vehicles { get; set; }

        [DataMember]
        public Company Companies { get; set; }
    }
}
