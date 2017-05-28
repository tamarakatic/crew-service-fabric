using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using Crew.CrewMemberActor.Interfaces;

namespace Crew.CrewMemberActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class CrewMemberActor : Actor, ICrewMemberActor
    {
        [DataContract]
        internal sealed class LocationAtTime
        {
            [DataMember]
            public DateTime Timestamp { get; set; }
            [DataMember]
            public float Latitude { get; set; }
            [DataMember]
            public float Longitude { get; set; }
        }

        [DataContract]
        internal sealed class CrewState
        {
            [DataMember]
            public List<LocationAtTime> LocationHistory { get; set; }
        }
        /// <summary>
        /// Initializes a new instance of CrewMemberActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public CrewMemberActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        protected override async Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            var state = await StateManager.TryGetStateAsync<CrewState>("State");
            if (!state.HasValue)
                await StateManager.AddStateAsync("State", new CrewState {LocationHistory = new List<LocationAtTime>()});
        }

        public async Task<KeyValuePair<float, float>> GetLatestLocationAsync()
        {
            var state = await StateManager.GetStateAsync<CrewState>("State");
            var location = state.LocationHistory.OrderByDescending(x => x.Timestamp)
                .Select(x => new KeyValuePair<float, float>(x.Latitude, x.Longitude))
                .FirstOrDefault();
            return location;
        }

        public async Task SetLocationAsync(DateTime timestamp, float latitude, float longitude)
        {
            var state = await StateManager.GetStateAsync<CrewState>("State");
            state.LocationHistory.Add(new LocationAtTime()
            {
                Timestamp = timestamp,
                Latitude = latitude,
                Longitude = longitude
            });

            await StateManager.AddOrUpdateStateAsync("State", state, (s, crewState) => state);
        }
    }
}
