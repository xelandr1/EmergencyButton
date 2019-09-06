using Android.App;
using Android.Content;
using Android.Widget;

namespace EmergencyButton.App.Droid.PanicWidget
{
    [Service(Exported = false, Permission = Android.Manifest.Permission.BindRemoteviews)]
    public class PanicButtonWidgetService : RemoteViewsService
    {
        override
            public IRemoteViewsFactory OnGetViewFactory(Intent intent)
        {
            return new StackRemoteViewsFactory(this.ApplicationContext, intent);
        }
    }

    class StackRemoteViewsFactory : Java.Lang.Object, RemoteViewsService.IRemoteViewsFactory
    {
        private Context context;

        public StackRemoteViewsFactory(Context context, Intent intent)
        {
            this.context = context;
        }

        public void OnCreate()
        {

        }

        public void OnDestroy()
        {

        }

        public int Count
        {
            get
            {
                return 10;
            }
        }

        public RemoteViews GetViewAt(int position)
        {


            RemoteViews rv = new RemoteViews(context.PackageName, Resource.Layout.panic_button);

            rv.SetTextViewText(Resource.Id.panic_button_counter, Count.ToString());

            //  int iconDrawable = message.IsRead ? Resource.Drawable.pink_button : Resource.Drawable.mark_unread;
            rv.SetImageViewResource(Resource.Id.panic_button_icon, Resource.Drawable.pink_button);

            // Add the message id to the intent
            //Intent fillInIntent = new Intent();
            //Bundle extras = new Bundle();
            //extras.PutString(RichPushApplication.MESSAGE_ID_RECEIVED_KEY, message.MessageId);
            //fillInIntent.PutExtras(extras);
            //rv.SetOnClickFillInIntent(Resource.Id.panic_button, fillInIntent);

            return rv;
        }

        public RemoteViews LoadingView
        {
            get
            {
                // We aren't going to return a default loading view in this sample
                return null;
            }
        }

        public int ViewTypeCount
        {
            get
            {
                // Technically, we have two types of views (the dark and light background views)
                return 2;
            }
        }

        public long GetItemId(int position)
        {
            return position;
        }

        public bool HasStableIds
        {
            get { return false; }
        }

        public void OnDataSetChanged()
        {

        }
    }

}