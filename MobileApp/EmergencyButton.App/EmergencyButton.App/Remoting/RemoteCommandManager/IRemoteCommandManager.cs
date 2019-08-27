using System.Threading.Tasks;
using EmergencyButton.Core.ComponentModel.Service;

namespace EmergencyButton.App.Remote
{
    public interface IRemoteCommandManager:IService
    {
        Task DoCommand(string command, object[] parameters = null);
        Task<TResult> DoCommand<TResult>(string command, object[] parameters = null);

        TransportLayerMode CurrentTransportMode { get; }

    }
}