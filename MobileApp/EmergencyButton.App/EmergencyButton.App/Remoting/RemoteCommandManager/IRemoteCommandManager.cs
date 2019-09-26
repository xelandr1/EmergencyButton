using System.Threading.Tasks;

namespace EmergencyButton.App.Remote
{
    public interface IRemoteCommandManager
    {
        Task DoCommand(string command, object[] parameters = null);
        Task<TResult> DoCommand<TResult>(string command, object[] parameters = null);

        TransportLayerMode CurrentTransportMode { get; }

    }
}