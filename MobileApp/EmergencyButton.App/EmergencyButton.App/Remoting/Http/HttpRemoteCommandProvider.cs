using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using EmergencyButton.App.ComponentModel.Service;
using EmergencyButton.Core.ComponentModel.Event;
using EmergencyButton.Core.Remoting;
using SimpleRemoteMethods.ClientSide;

namespace EmergencyButton.App.Remote.Http
{
    public class HttpRemoteCommandProvider : AbstractHostedService, IRemoteCommandProvider
    {
        public Client _srmClient { get; private set; }
 
        protected override Task StartAsyncInternal(CancellationToken cancellationToken)
        {
            ServiceState = ServiceState.Initiation;
            var config = new ConnectionConfiguration();

            _srmClient = new Client(config.Host,config.Port,false,config.SecretKey,"login","password",TimeSpan.FromSeconds(config.ConnectionTimeout));

            _srmClient.ConnectionError += Client_ConnectionError;
            _srmClient.ConnectionNormal += Client_ConnectionNormal;
            ServiceState = ServiceState.Active;
            return Task.CompletedTask;

        }

        private void Client_ConnectionNormal(object sender, SimpleRemoteMethods.ClientSide.Client e)
        {
            SetCurrentTransportLayerMode(TransportLayerMode.Normal);

        }

        private void Client_ConnectionError(object sender, SimpleRemoteMethods.Bases.TaggedEventArgs<SimpleRemoteMethods.Bases.RemoteException> e)
        {
            SetCurrentTransportLayerMode(TransportLayerMode.Unavailable);
        }

        protected override Task StopAsyncInternal(CancellationToken cancellationToken)
        {
            ServiceState = ServiceState.Termination;
            if (_srmClient != null)
            {
                _srmClient.ConnectionError -= Client_ConnectionError;
                _srmClient.ConnectionNormal -= Client_ConnectionNormal;
                _srmClient = null;
            }

            ServiceState = ServiceState.Stoped;
            return Task.CompletedTask;

        }


        private void SetCurrentTransportLayerMode(TransportLayerMode newTransportLayerMode)
        {
            if (CurrentTransportLayerMode != newTransportLayerMode)
            {
                CurrentTransportLayerMode = newTransportLayerMode;
                TransportLayerModeChanged?.Invoke(this,EventArgs.Empty);
            }
        }

        public TransportLayerMode CurrentTransportLayerMode { get; protected set; }
        public event EventHandler TransportLayerModeChanged;
        public async void CheckAvailability()
        {
            await _srmClient.CallMethod(RemotingConstant.CheckAvailabilityMethodName, null);

        }

        public async Task DoCommand(string command, object[] parameters = null)
        {
            try
            {
                await _srmClient.CallMethod(command, parameters);
            }
            catch (Exception ex) {
                Debug.Print(ex.ToString());
            }
        }

        public async Task<TResult> DoCommand<TResult>(string command, object[] parameters = null)
        {
            return await _srmClient.CallMethod<TResult>(command, parameters);
        }

    }
}