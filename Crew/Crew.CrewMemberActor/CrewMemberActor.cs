using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Crew.CrewMemberActor.Interfaces;
using Crew.CrewMemberActor.Interfaces.Models;

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

            var state = await StateManager.TryGetStateAsync<Interfaces.Models.Crew>("State");
            if (!state.HasValue)
                await StateManager.AddStateAsync("State", new Interfaces.Models.Crew());
        }

        public async Task<CrewLocation> GetLatestLocation()
        {
            var state = await StateManager.GetStateAsync<Interfaces.Models.Crew>("State");
            return state.Location;
        }

        public async Task CreateMember(Member member)
        {
            var state = await StateManager.GetStateAsync<Interfaces.Models.Crew>("State");
            state.Members.Add(member);

            await StateManager.AddOrUpdateStateAsync("State", state, (s, crewState) => state);
        }

        public async Task AddPlannedAssignment(Planned assignment)
        {
            var state = await StateManager.GetStateAsync<Interfaces.Models.Crew>("State");
            state.PlannedAssignments.Add(assignment);

            await StateManager.AddOrUpdateStateAsync("State", state, (s, crewState) => state);
        }

        public async Task AddUnplannedAssignment(Unplanned assignment)
        {
            var state = await StateManager.GetStateAsync<Interfaces.Models.Crew>("State");
            state.UnplannedAssignments.Add(assignment);

            await StateManager.AddOrUpdateStateAsync("State", state, (s, crewState) => state);
        }

        public async Task CreateCrewAsync(Interfaces.Models.Crew crew)
        {
            var state = await StateManager.GetStateAsync<Interfaces.Models.Crew>("State");
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

        public async Task<Interfaces.Models.Crew> GetCrewByName()
        {
            var crew = await StateManager.GetStateAsync<Interfaces.Models.Crew>("State");
            return crew;
        }

        public async Task UpdateLocation(CrewLocation location)
        {
            var state = await StateManager.GetStateAsync<Interfaces.Models.Crew>("State");
            state.Location.Timestamp = location.Timestamp;
            state.Location.Latitude = location.Latitude;
            state.Location.Longitude = location.Longitude;

            await StateManager.AddOrUpdateStateAsync("State", state, (s, crewState) => state);
        }
    }
}
