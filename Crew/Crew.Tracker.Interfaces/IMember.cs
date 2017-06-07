using System;
using System.Threading.Tasks;
using Crew.CrewMemberActor.Interfaces.Models;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Crew.Tracker.Interfaces
{
    public interface IMember : IService
    {
        Task CreateMember(String crewName, Member member);
    }
}
