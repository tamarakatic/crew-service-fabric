using System;
using System.Collections.Generic;

namespace Crew.CrewMemberActor.Interfaces
{
    public interface ICrew
    {
        String Name { get; set; }
        String Status { get; set; }
        CrewLocation Location { get; set; }
        List<Member> Members { get; set; }
        List<Planned> PlannedAssignments { get; set; }
        List<Unplanned> UnplannedAssignments { get; set; }
        List<Vehicle> Vehicles { get; set; }
        List<Company> Companies { get; set; }
    }
}
