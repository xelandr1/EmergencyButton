using System;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace EmergencyButton.App.Droid.PanicWidget
{
    class RemoteViewsFactory
    {
        internal static RemoteViews CreateLayout(Context context, int appWidgetId, Bundle options)
        {
            bool isLargeLayout = options.GetInt(AppWidgetManager.OptionAppwidgetMinHeight) >= 100;

            return CreateSmallLayout(context, appWidgetId);
        }

        private static RemoteViews CreateSmallLayout(Context context, int appWidgetId)
        {
            RemoteViews remoteViews = new RemoteViews(context.PackageName, Resource.Layout.panic_button);

            // Update the header for the current unread message count
            int count = 10;
            String header = context.GetString(Resource.String.panic_button_header);

            remoteViews.SetTextViewText(Resource.Id.panic_button_header, header);

            // Add a click pending intent to launch the inbox
            remoteViews.SetOnClickPendingIntent(Resource.Id.panic_button, CreateInboxActivityPendingIntent(context));

            return remoteViews;
        }

        private static PendingIntent CreateInboxActivityPendingIntent(Context context)
        {
            Intent intent = new Intent(context, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            return PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);
        }

        //private static RemoteViews CreateLargeLayout(Context context, int appWidgetId)
        //{
        //    RemoteViews remoteViews = new RemoteViews(context.PackageName, Resource.Layout.widget_layout);

        //    // Specify the service to provide data for the collection widget.  Note that we need to
        //    // embed the appWidgetId via the data otherwise it will be ignored.
        //    Intent intent = new Intent(context, typeof(RichPushWidgetService));
        //    intent.PutExtra(AppWidgetManager.ExtraAppwidgetId, appWidgetId);
        //    remoteViews.SetRemoteAdapter(appWidgetId, Resource.Id.message_list, intent);

        //    // Set the empty view to be displayed if the collection is empty.  It must be a sibling
        //    // view of the collection view.
        //    remoteViews.SetEmptyView(Resource.Id.message_list, Resource.Id.empty_view);

        //    // Bind a click listener template for the contents of the message list
        //    remoteViews.SetPendingIntentTemplate(Resource.Id.message_list, CreateMessageTemplateIntent(context, appWidgetId));

        //    // Add a click pending intent to launch the inbox
        //    remoteViews.SetOnClickPendingIntent(Resource.Id.widget_header, CreateInboxActivityPendingIntent(context));

        //    return remoteViews;
        //}

    }
}