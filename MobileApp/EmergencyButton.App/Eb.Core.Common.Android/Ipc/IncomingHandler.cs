using System;
using System.Diagnostics.CodeAnalysis;
using Android.OS;

namespace Eb.Core.Common.Droid.Ipc
{
    public class IncomingHandler : Handler
    {
        public override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);
            HasCome?.Invoke(this, msg);
        }

        [SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event Action<object, Message> HasCome;
    }
}