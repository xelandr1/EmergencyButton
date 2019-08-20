using System;
using System.Threading;
using EmergencyButton.Core.Instrumentation;
using EmergencyButton.Core.Server.ComponentModel;

namespace ServerConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Thread.CurrentThread.Name = "ConsoleHost.MainThread";
                SingletonInitializer.Initialize();

                HostStopWaitHandle.Wait();
            }
            catch (Exception e)
            {
                Logger.Error("Инициализация хостинга сервера", "ServerConsoleHost",e);

            }
        }
    }
}
