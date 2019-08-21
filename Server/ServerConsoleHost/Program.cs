using System;
using System.Threading;
using EmergencyButton.Core.Instrumentation;
using EmergencyButton.Core.Server.Core;

namespace ServerConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerCore serverCore = null;
            try
            {
                Thread.CurrentThread.Name = "ConsoleHost.MainThread";
                serverCore = new ServerCore();
                serverCore.Activate();

                HostStopWaitHandle.Wait();
                serverCore.Deactivate();
            }
            catch (Exception e)
            {
                Logger.Error("Инициализация хостинга сервера", "ServerConsoleHost", e);

            }

            HostStopWaitHandle.Wait();

            try
            {
                serverCore.Deactivate();
            }
            catch (Exception e)
            {
                Logger.Error("Останов хостинга сервера", "ServerConsoleHost", e);

            }

        }
    }
}
