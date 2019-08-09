using Android.App;
using Android.Content;
using Android.OS;
using EmergencyButton.App.Droid.Instrumentation;
using EmergencyButton.App.Droid.Ipc;
using EmergencyButton.App.Service;
using EmergencyButton.Core.Common.Droid.Ipc;
using EmergencyButton.Core.ComponentModel.Service;
using System;
using System.Threading;
using Android.Graphics;
using Android.Support.V4.App;
using EmergencyButton.Core.Common;
using EmergencyButton.Core.Instrumentation;
using Constants = EmergencyButton.App.Droid.Common.Constants;

namespace EmergencyButton.App.Droid.EbService
{
    [Service(Exported = false, Enabled = true)]
    public class EmergencyButtonService : global::Android.App.Service, IEmergencyButtonService, IService
    {
        private readonly IncomingHandler _inHandler = new IncomingHandler();
        private PowerManager.WakeLock _wakelock;
        private Notification _currentNotification;

        private static ScreenOnReciever ScreenOnReciever;
        private static ActionBootCompletedReceiver ActionBootCompletedReceiver;



        public EmergencyButtonService()
        {
            SubsystemIdentity.SubsystemId = Constants.EmergencyButtonApp_SubsystemName;
        }


        protected static ServiceState CurrentServiceState = ServiceState.None;
        private BoundServiceBinder _binder;

        public ServiceState ServiceState
        {
            get { return CurrentServiceState;}
            protected set { CurrentServiceState = value; }
        }



        private PowerManager PowerManager => GetSystemService(PowerService) as PowerManager;


        public override void OnCreate()
        {
            Logger.Information(nameof(EmergencyButtonService) + " OnCreate", nameof(EmergencyButtonService));

            SafeUnregisterReceiver(ScreenOnReciever);
            SafeUnregisterReceiver(ActionBootCompletedReceiver);
            RegisterReceiver(ScreenOnReciever = new ScreenOnReciever(), new IntentFilter(Intent.ActionScreenOn));
            RegisterReceiver(ActionBootCompletedReceiver = new ActionBootCompletedReceiver(), new IntentFilter(Intent.ActionBootCompleted));

            base.OnCreate();
            Activate();
        }

        private void SafeUnregisterReceiver(BroadcastReceiver receiver)
        {
            if (receiver == null)
            {
                return;
            }

            try
            {
                UnregisterReceiver(receiver);
            }
            catch
            {
                // Ignore, receiver already unregistered
            }
        }

        public override IBinder OnBind(Intent intent)
        {
            Logger.Information("OnBind()", nameof(EmergencyButtonService));
            _binder= new BoundServiceBinder(this);// _messenger.Binder;
            return _binder;
        }

        private void InitWakeLock()
        {
            _wakelock = PowerManager.NewWakeLock(WakeLockFlags.Partial, "lazurite::servicewakelock");
            _wakelock.SetReferenceCounted(false);
        }

        private void ReleaseWakeLock()
        {
            _wakelock?.Release();
            _wakelock = null;
        }

        private void InHandler_HasCome(object sender, Message msg)
        {
            try
            {


            //    _toActivityMessenger = IpcRoutines.GetAnswerMessenger(msg);
                //switch ((ServiceOperation) msg.What)
                //{
                //    case ServiceOperation.ExecuteScenario:
                //    {
                //        //_manager.ExecuteScenario(Utils.GetData<ExecuteScenarioArgs>(msg));
                //        break;
                //    }
            }
            catch (Exception e)
            {
                Logger.Error("Error in Handler_HasCome", nameof(EmergencyButtonService),e);
            }
        }

        public void Activate()
        {
            if(ServiceState!= ServiceState.None)
                return;

            ServiceState = ServiceState.Initiation;
            Logger.Information("Activate()", nameof(EmergencyButtonService));

            InitWakeLock();
            //   _messenger = new Messenger(_inHandler);
            //  _inHandler.HasCome += InHandler_HasCome;
            RegisterForegroundService();
              ServiceState = ServiceState.Active;

        }

        void RegisterForegroundService()
        {
            //var notification = new Notification.Builder(this)
            //    .SetContentTitle("title")
            //    .SetContentText("Connnnnnntent")
            //    .SetSmallIcon(Resource.Drawable.navigation_empty_icon)
            //    //   .SetContentIntent(BuildIntentToShowMainActivity())
            //    .SetOngoing(true)
            //    .AddAction(BuildTestAction())
            //    .Build();


            //// Enlist this instance of the service as a foreground service
            //StartForeground(15, notification);

            var activityIntent = new Intent(this, typeof(MainActivity));
            activityIntent.PutExtra(Constants.NeedOpenNotifications, -1);

            var showActivityIntent = PendingIntent.GetActivity(Application.Context, 0, activityIntent, PendingIntentFlags.UpdateCurrent);

            var channelId = Build.VERSION.SdkInt >= BuildVersionCodes.O ? CreateNotificationChannel() : string.Empty;

            var notificationBuilder = new NotificationCompat.Builder(this, channelId);
            notificationBuilder.SetOngoing(true);
            notificationBuilder.SetContentTitle("EmergencyButtonService запущен...");
            notificationBuilder.SetContentText("Нажмите, чтобы открыть список уведомлений");
            notificationBuilder.SetContentIntent(showActivityIntent);
            notificationBuilder.SetSmallIcon(Resource.Drawable.navigation_empty_icon);
            notificationBuilder.SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.navigation_empty_icon));
            notificationBuilder.SetVisibility((int)NotificationVisibility.Secret);
            notificationBuilder.SetPriority((int)NotificationPriority.Min);
            notificationBuilder.SetColor(Color.SteelBlue);
            notificationBuilder.SetOnlyAlertOnce(true);
            notificationBuilder.SetSound(null);
            notificationBuilder.SetGroupAlertBehavior((int)NotificationGroupAlertBehavior.Summary);
            notificationBuilder.SetGroup("eb_serv_group");
            notificationBuilder.SetGroupSummary(false);
            notificationBuilder.SetCategory(Notification.CategoryService);

            _currentNotification = notificationBuilder.Build();

            StartForeground(1, _currentNotification);


        }

        PendingIntent BuildIntentToShowMainActivity()
        {
            var notificationIntent = new Intent(this, typeof(MainActivity));
            notificationIntent.SetAction("ACTION_MAIN_ACTIVITY");
            notificationIntent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTask);
            notificationIntent.PutExtra("PutExtra", true);

            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);
            return pendingIntent;
        }


        Notification.Action BuildTestAction()
        {
            var restartTimerIntent = new Intent(this, GetType());
            restartTimerIntent.SetAction("ACTION_TEST");
            var restartTimerPendingIntent = PendingIntent.GetService(this, 0, restartTimerIntent, 0);

            var builder = new Notification.Action.Builder(Resource.Drawable.abc_btn_check_material,
                "Ttttest",
                restartTimerPendingIntent);

            return builder.Build();
        }



        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Logger.Information("OnStartCommand()", nameof(EmergencyButtonService));

            base.OnStartCommand(intent, flags, startId);

            if (ServiceState > ServiceState.None) return StartCommandResult.Sticky;

          //  ReInitTimer();


            var activityIntent = new Intent(this, typeof(MainActivity));
            activityIntent.PutExtra(Constants.NeedOpenNotifications, -1);

            var showActivityIntent = PendingIntent.GetActivity(Application.Context, 0, activityIntent, PendingIntentFlags.UpdateCurrent);

            var channelId = Build.VERSION.SdkInt >= BuildVersionCodes.O ? CreateNotificationChannel() : string.Empty;

            var notificationBuilder = new NotificationCompat.Builder(this, channelId);
            notificationBuilder.SetOngoing(true);
            notificationBuilder.SetContentTitle("Lazurite запущен...");
            notificationBuilder.SetContentText("Нажмите, чтобы открыть список уведомлений");
            notificationBuilder.SetContentIntent(showActivityIntent);
            notificationBuilder.SetSmallIcon(Resource.Drawable.navigation_empty_icon);
            notificationBuilder.SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.navigation_empty_icon));
            notificationBuilder.SetVisibility((int)NotificationVisibility.Secret);
            notificationBuilder.SetPriority((int)NotificationPriority.Min);
            notificationBuilder.SetColor(Color.SteelBlue);
            notificationBuilder.SetOnlyAlertOnce(true);
            notificationBuilder.SetSound(null);
            notificationBuilder.SetGroupAlertBehavior((int)NotificationGroupAlertBehavior.Summary);
            notificationBuilder.SetGroup("eb_serv_group");
            notificationBuilder.SetGroupSummary(false);
            notificationBuilder.SetCategory(Notification.CategoryService);

            _currentNotification = notificationBuilder.Build();

            StartForeground(1, _currentNotification);

            return StartCommandResult.Sticky;
        }

        private string CreateNotificationChannel()
        {
            var channel = new NotificationChannel(Constants.EmergencyButtonServiceNotification,
                Constants.EmergencyButtonServiceNotification, NotificationImportance.None);

            channel.LockscreenVisibility = NotificationVisibility.Secret;
            channel.EnableVibration(false);
            channel.EnableLights(false);
            channel.SetSound(null, null);
            channel.SetShowBadge(false);
            channel.Description = "Always visible EmergencyButton notification";
            var service = GetSystemService(NotificationService) as NotificationManager;
            service.CreateNotificationChannel(channel);
            return channel.Id;
        }


        public void Deactivate()
        {
            if(ServiceState!= ServiceState.Active)
                return;
            ServiceState = ServiceState.Termination;

            StopForeground(true);
            ReleaseWakeLock();

            _inHandler.HasCome -= InHandler_HasCome;
            ServiceState = ServiceState.Stoped;

        }

        public override void OnDestroy()
        {
            Deactivate();
            base.OnDestroy();
        }

        public event EventHandler<EventArgs> ServiceStateChanged;
        public string TestCall(string message)
        {
            var timer = new Timer(state => { OnHasCome(50, "blue"); },
                null, 0, 2000);

            return "Processed message - " + message;
        }

        public event Action<object, object> HasCome;

        protected virtual void OnHasCome(object arg1, object arg2)
        {
            HasCome?.Invoke(arg1, arg2);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}