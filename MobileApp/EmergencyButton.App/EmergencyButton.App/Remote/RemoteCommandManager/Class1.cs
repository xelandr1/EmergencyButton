using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Android.Net.Wifi.P2p;
using EmergencyButton.Core.Common;
using EmergencyButton.Core.ComponentModel.Factory;
using EmergencyButton.Core.ComponentModel.Service;
using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.App.Remote
{
    class Class1
    {
    }

    public interface IRemoteCommandManager:IService
    {
        Task DoCommand(string command, object[] parameters = null);
        Task<TResult> DoCommand<TResult>(string command, object[] parameters = null);

        TransportLayerMode CurrentTransportMode { get; }

    }

    public class RemoteCommandManager : AbstractService, IRemoteCommandManager
    {
        private readonly ICommandProviderEnumerator _commandProviderEnumerator;
        private List<PairValue<TransportLayerMode, IRemoteCommandProvider>> _commandProviders=new List<PairValue<TransportLayerMode, IRemoteCommandProvider>>();

        public RemoteCommandManager(ICommandProviderEnumerator commandProviderEnumerator)
        {
            _commandProviderEnumerator = commandProviderEnumerator;
        }

        public void RegisterCommandProviders()
        {
            foreach (var rcp in _commandProviderEnumerator.CommandProviders)
            {
                try
                {
                    var provider = (IRemoteCommandProvider) DefaultFactory.Factory.CreateInstance(rcp.Second);

                    _commandProviders.Add(new PairValue<TransportLayerMode, IRemoteCommandProvider>(rcp.First,provider));
                }
                catch (Exception e)
                {
                    Logger.Error($"RemoteCommandManager.RegisterCommandProviders fail register {rcp.Second.Name}", nameof(RemoteCommandManager), e);
                }

            }
        }

        public override void Activate()
        {
        }

        public override void Deactivate()
        {
        }

        public async Task DoCommand(string command, object[] parameters = null)
        {
        }

        public async Task<TResult> DoCommand<TResult>(string command, object[] parameters = null)
        {
            return default;
        }

        public TransportLayerMode CurrentTransportMode { get; }
    }


    public interface IRemoteCommandProvider
    {
        TransportLayerMode CurrentTransportLayerMode { get; }

        TransportLayerMode CheckAvailability();

        Task DoCommand(string command, object[] parameters = null);
        Task<TResult> DoCommand<TResult>(string command, object[] parameters = null);

    }



    public enum TransportLayerMode
    {
        Normal=1,
        LowSpeed=2,
        OnlyText=3,
        Unavailable=10
    }
}
