using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crew.CrewMemberActor.Interfaces
{
    public interface IUnplanned
    {
        String Problem { get; set; }
        String Comment { get; set; }
    }
}
