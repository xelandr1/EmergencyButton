using System;
using Android.Content;
using Android.OS;

namespace EmergencyButton.App.Droid.EbService
{
    public class BoundServiceConnection : Java.Lang.Object, IServiceConnection
    {

        public bool IsConnected { get; private set; }
        public BoundServiceBinder Binder { get; private set; }


        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Binder = service as BoundServiceBinder;
            IsConnected = this.Binder != null;
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            throw new NotImplementedException();
        }
    }
}