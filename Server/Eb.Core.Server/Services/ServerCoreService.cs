using System.Reflection;
using EmergencyButton.Core.Common;

namespace Eb.Core.Server.Services
{
    public class ServerCoreService : IServerCoreService
    {
        private string _started;

        public ServerCoreService() {
            _started = "true";
        }
        public string CheckAvailability()
        {
            return "OK";
        }

        public string CoreVersion
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
        }

       public string Test1(string input)
       {
           return input + " Result";
       }


        public void RestartServer()
        {
           
        }

        public PairValue<string, string> GetString()
        {
            return new PairValue<string, string>("first", "second");
        }
    }
}