using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Crew.CrewMemberActor.Interfaces;
using Crew.CrewMemberActor.Interfaces.Models;
using Crew.Tracker.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using ICrewLocation = Crew.Tracker.Interfaces.ICrewLocation;

namespace Crew.StatefulTracker
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatefulTracker : StatefulService, 
                                            ICrewLocation,
                                            Tracker.Interfaces.ICrew,
                                            IMember,
                                            IAssignment
    {
        public StatefulTracker(StatefulServiceContext context)
            : base(context)
        { }

        public StatefulTracker(StatefulServiceContext serviceContext, 
                               IReliableStateManagerReplica reliableStateManagerReplica) 
            : base(serviceContext, reliableStateManagerReplica)
        {
        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(this.CreateServiceRemotingListener)
            };
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var tx = this.StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");

                    ServiceEventSource.Current.ServiceMessage(this.Context, "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

                    await tx.CommitAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

        public async Task CreateCrew(CrewMemberActor.Interfaces.Models.Crew crew)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var actorDictionary = await StateManager.GetOrAddAsync<IReliableDictionary<String, ActorId>>("actorDict");
                var crewActorId = await actorDictionary.GetOrAddAsync(tx, crew.Name, ActorId.CreateRandom());

                await CrewConnectionFactory
                    .GetCrewMemberActor(crewActorId)
                    .CreateCrewAsync(crew);

                await tx.CommitAsync();
            }
        }
        
        public async Task UpdateCrewLocation(String crewName, CrewLocation location)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var actorDictionary = await StateManager
                    .GetOrAddAsync<IReliableDictionary<String, ActorId>>("actorDict");
                var crewActorId = await actorDictionary.TryGetValueAsync(tx, crewName);

                await CrewConnectionFactory
                    .GetCrewMemberActor(crewActorId.Value)
                    .UpdateLocation(location); 

                await tx.CommitAsync();
            }
        }
        
        public async Task<CrewMemberActor.Interfaces.Models.Crew> GetCrewByName(string name)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var actorDicitonary = await StateManager
                    .GetOrAddAsync<IReliableDictionary<String, ActorId>>("actorDict");
                var crewActorName = await actorDicitonary.TryGetValueAsync(tx, name);

                if (!crewActorName.HasValue)
                    return null;

                var crew = CrewConnectionFactory.GetCrewMemberActor(crewActorName.Value);
                return await crew.GetCrewByName();

            }
        }

        public async Task<CrewLocation> GetLastCrewLocation(String crewName)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var actorDict = await StateManager.GetOrAddAsync<IReliableDictionary<String, ActorId>>("actorDict");

                var crewActorId = await actorDict.TryGetValueAsync(tx, crewName);
                if (!crewActorId.HasValue)
                    return null;

                var crew = CrewConnectionFactory.GetCrewMemberActor(crewActorId.Value);
                return await crew.GetLatestLocation();
            }
        }

        public async Task CreateMember(String crewName, Member member)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var actorDictionary =
                    await StateManager.GetOrAddAsync<IReliableDictionary<String, ActorId>>("actorDict");
                var crewActorId = await actorDictionary.TryGetValueAsync(tx, crewName);

                await CrewConnectionFactory
                    .GetCrewMemberActor(crewActorId.Value)
                    .CreateMember(member);

                await tx.CommitAsync();
            }
        }

        public async Task AddPlannedAssignment(String crewName, Planned assignment)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var actorDictionary =
                    await StateManager.GetOrAddAsync<IReliableDictionary<String, ActorId>>("actorDict");
                var crewActorId = await actorDictionary.TryGetValueAsync(tx, crewName);

                await CrewConnectionFactory
                    .GetCrewMemberActor(crewActorId.Value)
                    .AddPlannedAssignment(assignment);

                await tx.CommitAsync();
            }
        }

        public async Task AddUnplannedAssignment(String crewName, Unplanned assignment)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var actorDictionary =
                    await StateManager.GetOrAddAsync<IReliableDictionary<String, ActorId>>("actorDict");
                var crewActorId = await actorDictionary.TryGetValueAsync(tx, crewName);

                await CrewConnectionFactory
                    .GetCrewMemberActor(crewActorId.Value)
                    .AddUnplannedAssignment(assignment);

                await tx.CommitAsync();
            }
        }
    }
}
