using Android.App;
using Android.Content;
using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.App.Droid.EbService
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { Intent.ActionScreenOn })]
    public class ScreenOnReciever : BroadcastReceiver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule")]
        public override void OnReceive(Context context, Intent intent)
        {
            Logger.Information("OnReceive()", nameof(ScreenOnReciever));

            try
            {
                Application.Context.StartService(new Intent(context, typeof(EmergencyButtonService)));
            }
            catch
            {
                // Do nothing, just try to start service on boot completed
            }
        }
    }
}