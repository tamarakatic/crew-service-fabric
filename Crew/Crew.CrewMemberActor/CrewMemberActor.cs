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
using Crew = Crew.CrewMemberActor.Interfaces.Crew;

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
        //[DataContract]
        //internal sealed class CrewState
        //{
        //    [DataMember]
        //    public string Name { get; set; }

        //    [DataMember]
        //    public string Status { get; set; }

        //    public CrewLocation Location { get; set; }

        //    public List<Member> Members { get; set; }

        //    public List<Assignment> Assignments { get; set; }

        //    public List<Vehicle> Vehicles { get; set; }

        //    public List<Company> Companies { get; set; }
        //}

        //[DataContract]
        //internal sealed class CrewState
        //{
        //    [DataMember]
        //    public List<LocationAtTime> LocationHistory { get; set; }
        //}
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

            var state = await StateManager.TryGetStateAsync<Interfaces.Crew>("State");
            if (!state.HasValue)
                await StateManager.AddStateAsync("State", new Interfaces.Crew());
        }

        public async Task<CrewLocation> GetLatestLocationAsync()
        {
            var state = await StateManager.GetStateAsync<Interfaces.Crew>("State");
            return state.Location;
        }

        public async Task<DateTime?> GetLastReportTime()
        {
            var state = await StateManager.GetStateAsync<Interfaces.Crew>("State");

            return state.Location.Timestamp;
        }

        public async Task CreateCrewAsync(Interfaces.Crew crew)
        {
            var state = await StateManager.GetStateAsync<Interfaces.Crew>("State");
            state.Name = crew.Name;
            state.Status = crew.Status;
            state.Location = crew.Location;
            state.Members = crew.Members;
            state.PlannedAssignments = crew.PlannedAssignments;
            state.UnplannedAssignments = crew.UnplannedAssignments;
            state.Vehicles = crew.Vehicles;
            state.Companies = crew.Companies;

            await StateManager.AddOrUpdateStateAsync("State", state, (s, crewState) => state);
        }

        public async Task<Interfaces.Crew> GetCrewByName()
        {
            var crew = await StateManager.GetStateAsync<Interfaces.Crew>("State");
            return crew;
        }

        public async Task SetLocationAsync(CrewLocation location)
        {
            var state = await StateManager.GetStateAsync<Interfaces.Crew>("State");
            state.Location = location;

            await StateManager.AddOrUpdateStateAsync("State", state, (s, crewState) => state);
        }
    }
}
