using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Crew.CrewMemberActor.Interfaces;
using Crew.Tracker.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace Crew.StatefulTracker
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatefulTracker : StatefulService, ILocationReporter, ILocationViewer
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

        public async Task ReportLocation(Location location)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var timestamp = DateTime.UtcNow;

                var actorDict = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, ActorId>>("actorDict");
                var crewActorId = await actorDict.GetOrAddAsync(tx, location.CrewId, ActorId.CreateRandom());

                await CrewConnectionFactory.GetCrewMemberActor(crewActorId)
                    .SetLocationAsync(timestamp, 
                                      location.Latitude,
                                      location.Longitude);

                await tx.CommitAsync();
            }
        }

        public async Task<KeyValuePair<float, float>?> GetLastCrewLocation(Guid crewIdGuid)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var actorDict = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, ActorId>>("actorDict");

                var crewActorId = await actorDict.TryGetValueAsync(tx, crewIdGuid);
                if (!crewActorId.HasValue)
                    return null;

                var crew = CrewConnectionFactory.GetCrewMemberActor(crewActorId.Value);
                return await crew.GetLatestLocationAsync();
            }
        }

        public async Task<DateTime?> GetLastReportTime(Guid crewId)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var actorDict = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, ActorId>>("actorDict");

                var crewActorId = await actorDict.TryGetValueAsync(tx, crewId);
                if (!crewActorId.HasValue)
                    return null;

                var crew = CrewConnectionFactory.GetCrewMemberActor(crewActorId.Value);
                var timestamp = crew.GetLastReportTime();

                return await timestamp;
            }
        }
    }
}
