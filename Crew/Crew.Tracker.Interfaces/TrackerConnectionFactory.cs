using System;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Crew.Tracker.Interfaces
{
    public class TrackerConncetionFactory
    {
        private static readonly Uri LocationReporterServiceUrl = new Uri("fabric:/Crew/StatefulTracker");

        public static ICrewLocation UpdateCrewLocation()
        {
            return ServiceProxy.Create<ICrewLocation>(LocationReporterServiceUrl, new ServicePartitionKey(0));
        }

        public static ICrewLocation GetLastCrewLocation()
        {
            return ServiceProxy.Create<ICrewLocation>(LocationReporterServiceUrl, new ServicePartitionKey(0));
        }

        public static ICrew CreateCrew()
        {
            return ServiceProxy.Create<ICrew>(LocationReporterServiceUrl, new ServicePartitionKey(0));
        }

        public static IMember CreateMember()
        {
            return ServiceProxy.Create<IMember>(LocationReporterServiceUrl, new ServicePartitionKey(0));
        }

        public static IAssignment CreateAssignment()
        {
            return ServiceProxy.Create<IAssignment>(LocationReporterServiceUrl, new ServicePartitionKey(0));
        }
    }
}
