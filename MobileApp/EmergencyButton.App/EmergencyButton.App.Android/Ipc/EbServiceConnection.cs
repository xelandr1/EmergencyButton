using Android.Content;
using Android.OS;

namespace EmergencyButton.App.Droid.Ipc
{
    public class EbServiceConnection : Java.Lang.Object, IServiceConnection
    {

        MainActivity mainActivity;
        public EbServiceConnection(MainActivity activity)
        {
            IsConnected = false;
            mainActivity = activity;
        }

        public bool IsConnected { get; private set; }
        public Messenger Messenger { get; private set; }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            //Log.Debug(TAG, $"OnServiceConnected {name.ClassName}");

            Messenger = new Messenger(service);

            IsConnected = this.Messenger != null;

            if (IsConnected)
            {
            }
            else
            {
            }

        }

        public void OnServiceDisconnected(ComponentName name)
        {
            //Log.Debug(TAG, $"OnServiceDisconnected {name.ClassName}");
            IsConnected = false;
            Messenger = null;
        }
    }
}