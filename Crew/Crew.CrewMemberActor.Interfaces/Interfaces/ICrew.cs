using System;
using System.Collections.Generic;
using Crew.CrewMemberActor.Interfaces.Models;

namespace Crew.CrewMemberActor.Interfaces.Interfaces
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
        Company Companies { get; set; }
    }
}
