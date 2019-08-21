using System;
using EmergencyButton.Core.ComponentModel.Service;

namespace EmergencyButton.App.Remote
{
    public class RemoteClientManager:AbstractService
    {
        public RemoteClient Client { get; private set; }


        public override void Activate()
        {
            var config = new ConnectionConfiguration();

            Client =new RemoteClient(config.Host,config.Port,true,config.SecretKey,"login","password",TimeSpan.FromSeconds(config.ConnectionTimeout));

            Client.Client.ConnectionError += this.Client_ConnectionError;
            Client.Client.ConnectionNormal += Client_ConnectionNormal;

        }

        private void Client_ConnectionNormal(object sender, SimpleRemoteMethods.ClientSide.Client e)
        {

        }

        private void Client_ConnectionError(object sender, SimpleRemoteMethods.Bases.TaggedEventArgs<SimpleRemoteMethods.Bases.RemoteException> e)
        {
        }

        public override void Deactivate()
        {
        }
    }
}