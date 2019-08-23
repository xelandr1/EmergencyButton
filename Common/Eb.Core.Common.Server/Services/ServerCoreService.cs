using System.Reflection;

namespace EmergencyButton.Core.Server.Services
{
    public class ServerCoreService : IServerCoreService
    {
        public string CoreVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

       public string Test1(string input)
       {
           return input + " Result";
       }


        public void RestartServer()
        {
           
        }
    }
}