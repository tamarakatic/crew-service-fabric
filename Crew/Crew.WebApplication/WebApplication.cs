using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Crew.WebApplication
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class WebApplication : StatelessService
    {
        public WebApplication(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return Context.CodePackageActivationContext.GetEndpoints()
                .Where(endpoint => endpoint.Protocol.Equals(EndpointProtocol.Http) ||
                                   endpoint.Protocol.Equals(EndpointProtocol.Https))
                .Select(endpoint => new ServiceInstanceListener(
                    serviceContext => new OwinComunicationListener("api",
                        new Startup(),
                        serviceContext)));
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {

        }
    }
}