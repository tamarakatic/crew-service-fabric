using System.Runtime.Serialization;
using Crew.CrewMemberActor.Interfaces.Interfaces;

namespace Crew.CrewMemberActor.Interfaces.Models
{
    [DataContract]
    public class Member : IMember
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Telephone { get; set; }

        [DataMember]
        public bool IsLead { get; set; }

        [DataMember]
        public bool IsDriver { get; set; }
    }
}
