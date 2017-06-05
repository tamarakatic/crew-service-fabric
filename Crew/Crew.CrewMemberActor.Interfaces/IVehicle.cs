using System;

namespace Crew.CrewMemberActor.Interfaces
{
    public enum VehicleType
    {
        Car,
        Van,
        CarWithCaravan
    }

    public interface IVehicle
    {
        String Name { get; set; }
        String Status { get; set; }
        VehicleType VehicleType { get; set; }
    }
}
