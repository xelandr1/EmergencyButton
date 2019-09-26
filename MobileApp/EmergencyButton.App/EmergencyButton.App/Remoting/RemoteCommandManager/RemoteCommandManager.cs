using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EmergencyButton.App.ComponentModel.Service;
using EmergencyButton.Core.ComponentModel.Factory;
using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.App.Remote
{
    public class RemoteCommandManager : AbstractHostedService, IRemoteCommandManager
    {
        private readonly ICommandProviderEnumerator _commandProviderEnumerator;
        private List< IRemoteCommandProvider> _commandProviders=new List<IRemoteCommandProvider>();
        private IRemoteCommandProvider _currentCommandProvider;

        public RemoteCommandManager(ICommandProviderEnumerator commandProviderEnumerator)
        {
            _commandProviderEnumerator = commandProviderEnumerator;
        }

        private void RegisterCommandProviders()
        {
            var logDescription = "";
            foreach (var rcpType in _commandProviderEnumerator.CommandProviders)
            {
                try
                {
                    var provider = (IRemoteCommandProvider) DefaultFactory.Factory.CreateInstance(rcpType);
                    _commandProviders.Add(provider);

                    logDescription += provider.GetType().ToString()+ System.Environment.NewLine;
                }
                catch (Exception e)
                {
                    Logger.Error($"RemoteCommandManager.RegisterCommandProviders fail register {rcpType.Name}", nameof(RemoteCommandManager), e);
                }
            }
            Logger.Information($"{_commandProviders.Count} RemoteCommandProviders registered",nameof(RemoteCommandManager), logDescription);
        }

        private void SelectBestCommandProvider()
        {
            if (_currentCommandProvider == null)
                _currentCommandProvider = _commandProviders[0];

            foreach (var provider in _commandProviders)
            {
                if (provider.CurrentTransportLayerMode < _currentCommandProvider.CurrentTransportLayerMode)
                    SetCurrentCommandProvider(provider);
            }
        }

        private void SetCurrentCommandProvider(IRemoteCommandProvider newCommandProvider)
        {
            if (_currentCommandProvider!= null)
            {
                _currentCommandProvider.TransportLayerModeChanged -= _currentCommandProvider_TransportLayerModeChanged;

            }
            _currentCommandProvider = newCommandProvider;
            _currentCommandProvider.TransportLayerModeChanged += _currentCommandProvider_TransportLayerModeChanged;
        }

        private void _currentCommandProvider_TransportLayerModeChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override Task StartAsyncInternal(CancellationToken cancellationToken)
        {
            ServiceState = ServiceState.Initiation;
            RegisterCommandProviders();
            SelectBestCommandProvider();
            ServiceState = ServiceState.Active;
            return Task.CompletedTask;
        }

        protected override Task StopAsyncInternal(CancellationToken cancellationToken)
        {
            ServiceState = ServiceState.Stoped;
            return Task.CompletedTask;
        }

        public async Task DoCommand(string command, object[] parameters = null)
        {
           await _currentCommandProvider.DoCommand(command, parameters);
        }

        public async Task<TResult> DoCommand<TResult>(string command, object[] parameters = null)
        {
            return await _currentCommandProvider.DoCommand<TResult>(command, parameters);
        }

        public TransportLayerMode CurrentTransportMode { get; }
    }
}