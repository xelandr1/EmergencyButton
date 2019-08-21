using System;
using System.Threading.Tasks;
using SimpleRemoteMethods.ClientSide;

namespace EmergencyButton.App.Remote
{
    public class RemoteClient
    {
        public Client Client { get; }

        public RemoteClient(string host, ushort port, bool ssl, string secretKey, string login, string password, TimeSpan connectionTimeout = default(TimeSpan), TimeSpan leaseTimeout = default(TimeSpan))
        {
            Client = new Client(host, port, ssl, secretKey, login, password, connectionTimeout, leaseTimeout);
        }

        public async Task<String> GetCoreVersion()
        {
            return await Client.CallMethod<String>("GetCoreVersion");
        }

        public async Task<String> GetScenarioInfo(String input)
        {
            return await Client.CallMethod<String>("Test1", new object[] { input });
        }


    }
}