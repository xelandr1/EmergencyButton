using System;
using System.Threading.Tasks;
using EmergencyButton.Core.ComponentModel.Event;

namespace EmergencyButton.App.Remote
{
    public interface IRemoteCommandProvider
    {
        TransportLayerMode CurrentTransportLayerMode { get; }
        event EventHandler TransportLayerModeChanged;
        void CheckAvailability();

        Task DoCommand(string command, object[] parameters = null);
        Task<TResult> DoCommand<TResult>(string command, object[] parameters = null);

    }

    public enum TransportLayerMode
    {
        Normal = 1,
        LowSpeed = 2,
        OnlyText = 3,
        Unavailable = 10
    }

}