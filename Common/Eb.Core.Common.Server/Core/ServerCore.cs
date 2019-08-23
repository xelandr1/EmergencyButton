using System;
using System.Diagnostics;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.ComponentModel.Service;
using EmergencyButton.Core.Instrumentation;
using EmergencyButton.Core.Server.Configuration;
using EmergencyButton.Core.Server.Instrumentation;
using EmergencyButton.Core.Server.Services;
using SimpleRemoteMethods.ServerSide;
using SimpleRemoteMethods.Utils.Windows;

namespace EmergencyButton.Core.Server.Core
{
   public class ServerCore:AbstractService,IServerCore
    {
        private Server<IServerCoreService> _remoteServer;

        public override void Activate()
        {
            var activateTimer = Stopwatch.StartNew();
            Singleton.Services.RegisterService<IServerCore>(this);
            Singleton.Services.RegisterService<IInstrumentationService>(new ServerInstrumentationService());
            Logger.Information("ServerCore Activate",nameof(ServerCore));

            var servConfig = new ServerConfiguration();

            _remoteServer = new SimpleRemoteMethods.ServerSide.Server<IServerCoreService>(new ServerCoreService(), true, servConfig.Port, servConfig.SecretKey)
            {
                MaxConcurrentCalls = (ushort)servConfig.MaxConcurrentCalls,
                MaxRequestLength = 500000,
                AuthenticationValidator = new LoginValidator()
            };

            ServerHelper.PrepareHttpsServer(_remoteServer, servConfig.CertificateHash, "UniqueId");
            _remoteServer.StartAsync();

            Logger.Information($"Start in {activateTimer.ElapsedMilliseconds} ms", nameof(ServerCore));


        }

        public override void Deactivate()
        {

        }
    }

   public class LoginValidator : IAuthenticationValidator
   {
       public bool Authenticate(string userName, string password)
       {
           try
           {
               return true;
           }
           catch (Exception e)
           {
               return false;
           }
       }
   }

}
