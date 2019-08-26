using System.Reflection;

namespace Eb.Core.Server.Services
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