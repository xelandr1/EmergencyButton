using SimpleRemoteMethods.Bases;

namespace Eb.Core.Server.Services
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