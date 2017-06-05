using System.Runtime.Serialization;

namespace Crew.CrewMemberActor.Interfaces
{
    [DataContract]
    public class Company : ICompany
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public int Telephone { get; set; }

        [DataMember]
        public CompanyType CompanyType { get; set; }
    }
}
