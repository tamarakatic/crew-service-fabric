using System;

namespace Crew.CrewMemberActor.Interfaces.Interfaces
{
    public interface IPlanned
    {
        DateTime From { get; set; }
        DateTime To { get; set; }
    }
}
