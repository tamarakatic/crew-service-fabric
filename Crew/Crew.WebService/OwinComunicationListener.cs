using System;
using System.Fabric;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Microsoft.ServiceFabric.Services.Communication.Runtime;

namespace Crew.WebService
{
    internal class OwinComunicationListener : ICommunicationListener
    {
        private readonly IOwinAppBuilder _company;
        private readonly string _appRoot;
        private readonly StatelessServiceContext _parametars;

        private string _listeningAdress;

        private IDisposable _serverHandle; 

        public OwinComunicationListener(string appRoot,
                                        IOwinAppBuilder company, 
                                        StatelessServiceContext parametars)
        {
            _company = company;
            _appRoot = appRoot;
            _parametars = parametars;
        }

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            var serviceEndpoint =
                _parametars
                    .CodePackageActivationContext
                    .GetEndpoint("ServiceEndpoint");

            var port = serviceEndpoint.Port;
            var root = String.IsNullOrWhiteSpace(_appRoot)
                ? String.Empty
                : _appRoot.TrimEnd('/') + '/';

            _listeningAdress = String.Format(CultureInfo.InvariantCulture,
                                            "http://+:{0}/{1}",
                                            port,
                                            root);

            _serverHandle = WebApp.Start(
                            _listeningAdress,
                            appBuilder => _company.Configuration(appBuilder));

            var publishAdress = _listeningAdress.Replace(
                                "+",
                                FabricRuntime.GetNodeContext().IPAddressOrFQDN);

            ServiceEventSource.Current.Message("Listening on {0}", publishAdress);
            return Task.FromResult(publishAdress);
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            StopWebServer();
            return Task.FromResult(true);
        }

        public void Abort()
        {
            StopWebServer();
        }

        private void StopWebServer()
        {
            if (_serverHandle == null)
                return;
            try
            {
                _serverHandle.Dispose();
            }
            catch (ObjectDisposedException) { }
        }
    }
}