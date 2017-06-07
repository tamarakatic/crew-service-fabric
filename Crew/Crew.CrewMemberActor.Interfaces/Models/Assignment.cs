using System.Collections.Generic;
using System.Runtime.Serialization;
using Crew.CrewMemberActor.Interfaces.Interfaces;

namespace Crew.CrewMemberActor.Interfaces.Models
{
    [DataContract]
    public abstract class Assignment : IAssignment
    {
        public Assignment()
        {
            this.AffectedCustomers = new List<string>();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public bool IsPriority { get; set; }

        [DataMember]
        public List<string> AffectedCustomers { get; set; }

        [DataMember]
        public AssignmentType AssignmentType { get; set; }
    }
}
