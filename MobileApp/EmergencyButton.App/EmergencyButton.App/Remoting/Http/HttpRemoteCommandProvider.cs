using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EmergencyButton.Core.ComponentModel.Event;
using EmergencyButton.Core.ComponentModel.Service;
using EmergencyButton.Core.Remoting;
using SimpleRemoteMethods.ClientSide;

namespace EmergencyButton.App.Remote.Http
{
    public class HttpRemoteCommandProvider : AbstractService, IRemoteCommandProvider
    {
        public Client _srmClient { get; private set; }
        public HttpRemoteCommandProvider() { Activate();}

        public override void Activate()
        {
            if (ServiceState>=ServiceState.Initiation)return;
            ServiceState = ServiceState.Initiation;
            var config = new ConnectionConfiguration();

            _srmClient = new Client(config.Host,config.Port,true,config.SecretKey,"login","password",TimeSpan.FromSeconds(config.ConnectionTimeout));

            _srmClient.ConnectionError += Client_ConnectionError;
            _srmClient.ConnectionNormal += Client_ConnectionNormal;
            ServiceState = ServiceState.Active;
        }

        private void Client_ConnectionNormal(object sender, SimpleRemoteMethods.ClientSide.Client e)
        {
            SetCurrentTransportLayerMode(TransportLayerMode.Normal);

        }

        private void Client_ConnectionError(object sender, SimpleRemoteMethods.Bases.TaggedEventArgs<SimpleRemoteMethods.Bases.RemoteException> e)
        {
            SetCurrentTransportLayerMode(TransportLayerMode.Unavailable);
        }

        public override void Deactivate()
        {
            if (ServiceState>=ServiceState.Termination) return;
            ServiceState = ServiceState.Termination;
            if (_srmClient != null)
            {
                _srmClient.ConnectionError -= Client_ConnectionError;
                _srmClient.ConnectionNormal -= Client_ConnectionNormal;
                _srmClient = null;
            }

            ServiceState = ServiceState.Stoped;

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