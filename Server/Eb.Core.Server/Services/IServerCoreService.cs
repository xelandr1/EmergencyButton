
using EmergencyButton.Core.Common;

namespace Eb.Core.Server.Services
{
    public interface IServerCoreService
    {
        string CheckAvailability();

        string CoreVersion { get; }
        string Test1(string input);

        void RestartServer();

        PairValue<string, string> GetString();
    }
}