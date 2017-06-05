using System.Runtime.Serialization;

namespace Crew.CrewMemberActor.Interfaces
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
