using System;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace Crew.CrewMemberActor.Interfaces
{
    public class CrewConnectionFactory
    {
        private static readonly Uri CrewServiceUrl = new Uri("fabric:/Crew/CrewMemberActorService");

        public static ICrewMemberActor GetCrewMemberActor(ActorId crewId)
        {
            return ActorProxy.Create<ICrewMemberActor>(crewId, CrewServiceUrl);
        }
    }
}
