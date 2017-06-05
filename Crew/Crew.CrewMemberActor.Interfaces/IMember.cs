using System;

namespace Crew.CrewMemberActor.Interfaces
{
    public interface IMember
    {
        String Name { get; set; }
        Int32 Telephone { get; set; }
        Boolean IsLead { get; set; }
        Boolean IsDriver { get; set; }
    }
}
