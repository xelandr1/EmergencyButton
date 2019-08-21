using SimpleRemoteMethods.Bases;

namespace EmergencyButton.Core.Server.Services
{
    public interface IServerCoreService
    {
        [Remote]
        string CoreVersion();
        [Remote]
        string Test1(string input);

        [Remote]
        void RestartServer();
    }
}