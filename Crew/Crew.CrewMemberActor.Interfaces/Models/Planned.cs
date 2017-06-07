using System;
using System.Runtime.Serialization;
using Crew.CrewMemberActor.Interfaces.Interfaces;

namespace Crew.CrewMemberActor.Interfaces.Models
{
    [DataContract]
    public class Planned : Assignment, IPlanned 
    {
        [DataMember]
        public DateTime From { get; set; }

        [DataMember]
        public DateTime To { get; set; }
    }
}
