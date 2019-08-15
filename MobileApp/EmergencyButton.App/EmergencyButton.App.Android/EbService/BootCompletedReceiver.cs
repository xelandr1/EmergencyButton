using Android.App;
using Android.Content;
using Android.OS;
using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.App.Droid.EbService
{
    [BroadcastReceiver(Exported = false, DirectBootAware = true)]
    [IntentFilter(new[] { "android.intent.action.BOOT_COMPLETED", "android.intent.action.LOCKED_BOOT_COMPLETED" })]
    public class BootCompletedReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            bool bootCompleted;
            string action = intent.Action;


            if (Build.VERSION.SdkInt > BuildVersionCodes.M)
                bootCompleted = Intent.ActionLockedBootCompleted == action;
            else
                bootCompleted = Intent.ActionBootCompleted == action;

            //if (!bootCompleted)
            //    return;

            Logger.Information("OnReceive()", nameof(BootCompletedReceiver),$"bootCompleted{bootCompleted} action {action}");

            //try
            //{
            //    Application.Context.StartService(new Intent(context, typeof(EmergencyButtonService)));
            //}
            //catch
            //{
            //    // Do nothing, just try to start service on boot completed
            //}
        }
    }
}