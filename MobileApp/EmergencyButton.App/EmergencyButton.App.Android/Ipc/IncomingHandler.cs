using System;
using Android.OS;

namespace EmergencyButton.App.Droid.Ipc
{
    public class IncomingHandler : Handler
    {
        public IncomingHandler()
        {
        }

        public override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);
            HasCome?.Invoke(this, msg);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event Action<object, Message> HasCome;
    }
}