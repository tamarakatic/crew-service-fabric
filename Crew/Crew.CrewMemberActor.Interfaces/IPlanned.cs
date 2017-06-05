using System;

namespace Crew.CrewMemberActor.Interfaces
{
    public interface IPlanned
    {
        DateTime From { get; set; }
        DateTime To { get; set; }
    }
}
