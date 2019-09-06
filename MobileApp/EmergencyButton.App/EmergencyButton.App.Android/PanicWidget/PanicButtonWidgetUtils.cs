using Android.App;
using Android.Content;

namespace EmergencyButton.App.Droid.PanicWidget
{
    public class PanicButtonWidgetUtils
    {

        /**
     * Sends a request to the rich push message to refresh
     * @param context Application context
     */
        public static void RefreshWidget(Context context)
        {
            RefreshWidget(context, 0);
        }

        /**
     * Sends a request to the rich push message to refresh with a delay
     * @param context Application context
     * @param delayInMs Delay to wait in milliseconds before sending the request
     */
        public static void RefreshWidget(Context context, long delayInMs)
        {
            Intent refreshIntent = new Intent(context, typeof(PanicButtonWidgetProvider));
            refreshIntent.SetAction(PanicButtonWidgetProvider.REFRESH_ACTION);

            if (delayInMs > 0)
            {
                PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, refreshIntent, 0);
                AlarmManager am = (AlarmManager)context.GetSystemService(Context.AlarmService);
                am.Set(AlarmType.RtcWakeup, Java.Lang.JavaSystem.CurrentTimeMillis() + delayInMs, pendingIntent);
            }
            else
            {
                context.SendBroadcast(refreshIntent);
            }
        }
    }
}