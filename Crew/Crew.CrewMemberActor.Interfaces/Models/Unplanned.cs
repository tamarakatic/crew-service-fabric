using System.Runtime.Serialization;
using Crew.CrewMemberActor.Interfaces.Interfaces;

namespace Crew.CrewMemberActor.Interfaces.Models
{
    [DataContract]
    public class Unplanned : Assignment, IUnplanned
    {
        [DataMember]
        public string Problem { get; set; }

        [DataMember]
        public string Comment { get; set; }
    }
}
