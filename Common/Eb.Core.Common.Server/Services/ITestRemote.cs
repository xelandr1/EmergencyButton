using SimpleRemoteMethods.Bases;

namespace EmergencyButton.Core.Server.Services
{
    public interface ITestRemote
    {
        [Remote]
        string Test1(string input);
        [Remote]
        int Test2(string input);

    }
}