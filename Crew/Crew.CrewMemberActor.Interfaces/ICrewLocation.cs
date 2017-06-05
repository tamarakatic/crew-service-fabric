using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crew.CrewMemberActor.Interfaces
{
    public interface ICrewLocation
    {
        DateTime Timestamp { get; set; }
        float Latitude { get; set; }
        float Longitude { get; set; }
    }
}
