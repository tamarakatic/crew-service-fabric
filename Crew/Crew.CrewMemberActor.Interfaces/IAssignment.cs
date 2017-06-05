using System;
using System.Collections.Generic;

namespace Crew.CrewMemberActor.Interfaces
{
    public interface IAssignment
    {
        String Name { get; set; }
        String Status { get; set; }
        Boolean IsPriority { get; set; }
        List<String> AffectedCustomers { get; set; }
        AssignmentType AssignmentType { get; set; }
    }
    public enum AssignmentType
    {
        Construction,
        ElectricalInstallation,
        MaintenanceOfElectricalEquipment
    }
}
