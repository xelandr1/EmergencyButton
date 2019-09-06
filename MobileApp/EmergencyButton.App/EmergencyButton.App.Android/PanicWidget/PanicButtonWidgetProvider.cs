using System;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace EmergencyButton.App.Droid.PanicWidget
{
    [BroadcastReceiver]
    [IntentFilter(new string[]{"android.appwidget.action.APPWIDGET_UPDATE"})]
    [MetaData("android.appwidget.provider", Resource = "@xml/panic_button_widget")]
    public class PanicButtonWidgetProvider : AppWidgetProvider
    {
        //public const string OPEN_MESSAGE_ACTION = "com.xamarin.samples.urbanairship.richpush.widget.OPEN_MESSAGE";
        public const string REFRESH_ACTION = "com.xelandr.emergencybutton.widget.REFRESH";

        private static HandlerThread workerThread;
        private static Handler workerQueue;

        public PanicButtonWidgetProvider()
        {
            // Start the worker thread
            workerThread = new HandlerThread("RichPushSampleInbox-Provider");
            workerThread.Start();
            workerQueue = new Handler(workerThread.Looper);
        }

        override public void OnReceive(Context context, Intent intent)
        {
            String action = intent.Action;

            if (action == REFRESH_ACTION)
            {
                ScheduleUpdate(context);
            }

            base.OnReceive(context, intent);
        }

        override public void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            // Update each of the widgets with the remote adapter
            foreach (int id in appWidgetIds)
            {
                RemoteViews layout = null;

                Bundle options = appWidgetManager.GetAppWidgetOptions(id);
                layout = RemoteViewsFactory.CreateLayout(context, id, options);


                appWidgetManager.UpdateAppWidget(id, layout);
            }

            base.OnUpdate(context, appWidgetManager, appWidgetIds);
        }

        override public void OnAppWidgetOptionsChanged(Context context,
                AppWidgetManager appWidgetManager,
                int appWidgetId,
                Bundle newOptions)
        {

            RemoteViews layout = RemoteViewsFactory.CreateLayout(context, appWidgetId, newOptions);
            appWidgetManager.UpdateAppWidget(appWidgetId, layout);
        }

        /**
     * Adds a runnable to update the widgets in the worker queue
     * @param context used for creating layouts
     */
        private void ScheduleUpdate(Context context)
        {
            workerQueue.RemoveMessages(0);
            workerQueue.Post(() =>
            {
                AppWidgetManager mgr = AppWidgetManager.GetInstance(context);
                ComponentName cn = new ComponentName(context, Java.Lang.Class.FromType(typeof(PanicButtonWidgetProvider)));
                OnUpdate(context, mgr, mgr.GetAppWidgetIds(cn));

                mgr.NotifyAppWidgetViewDataChanged(mgr.GetAppWidgetIds(cn), Resource.Id.panic_button);
            });
        }
    }
}