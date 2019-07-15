using Android.App;
using Android.Content;
using Android.OS;
using EmergencyButton.App.Droid.Instrumentation;
using EmergencyButton.App.Droid.Ipc;
using EmergencyButton.App.Service;
using EmergencyButton.Core.Common.Droid.Ipc;
using EmergencyButton.Core.ComponentModel.Service;
using System;
using Android.Graphics;
using Android.Support.V4.App;
using EmergencyButton.App.Droid.Common;

namespace EmergencyButton.App.Droid.EbService
{
    [Service(Name = "com.xelandr.emergencybuttonservice",
        Exported = true,
        Permission = "com.xelandr.emergencybuttonservice.REQUEST_TIMESTAMP",
        Process = "com.xelandr.emergencybuttonservice.timestampservice_process")]
    public class EmergencyButtonService : global::Android.App.Service, IEmergencyButtonService, IService
    {
        private readonly IncomingHandler _inHandler = new IncomingHandler();
        private Messenger _messenger;
        private Messenger _toActivityMessenger;
        private PowerManager.WakeLock _wakelock;
        private Notification _currentNotification;





        protected static ServiceState CurrentServiceState = ServiceState.None;
        public ServiceState ServiceState
        {
            get { return CurrentServiceState;}
            protected set { CurrentServiceState = value; }
        }



        private PowerManager PowerManager => GetSystemService(PowerService) as PowerManager;


        public override void OnCreate()
        {
            base.OnCreate();
            Activate();
        }

        public override IBinder OnBind(Intent intent)
        {
            Logger.Information("OnBind()", nameof(EmergencyButtonService));
            return _messenger.Binder;
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
                _toActivityMessenger = IpcRoutines.GetAnswerMessenger(msg);
                switch ((ServiceOperation) msg.What)
                {
                    case ServiceOperation.ExecuteScenario:
                    {
                        //_manager.ExecuteScenario(Utils.GetData<ExecuteScenarioArgs>(msg));
                        break;
                    }
                    //case ServiceOperation.GetClientSettings:
                    //    {
                    //        _manager.GetClientSettings((settings) =>
                    //        {
                    //            Handle((messenger) => Utils.SendData(settings, messenger, _messenger, ServiceOperation.GetClientSettings));
                    //        });
                    //        break;
                    //    }
                    //case ServiceOperation.GetIsConnected:
                    //    {
                    //        _manager.IsConnected((isConnected) =>
                    //        {
                    //            Handle((messenger) => Utils.SendData(isConnected, messenger, _messenger, ServiceOperation.GetIsConnected));
                    //        });
                    //        break;
                    //    }
                    //case ServiceOperation.GetScenarios:
                    //    {
                    //        _manager.GetScenarios((scenarios) =>
                    //        {
                    //            Handle((messenger) => Utils.SendData(scenarios, messenger, _messenger, ServiceOperation.GetScenarios));
                    //        });
                    //        break;
                    //    }
                    //case ServiceOperation.GetNotifications:
                    //    {
                    //        _manager.GetNotifications((notifications) =>
                    //            Handle((messenger) => Utils.SendData(notifications, messenger, _messenger, ServiceOperation.GetNotifications)));
                    //        break;
                    //    }
                    //case ServiceOperation.SetClientSettings:
                    //    {
                    //        _manager.SetClientSettings(Utils.GetData<ConnectionCredentials>(msg));
                    //        break;
                    //    }
                    //case ServiceOperation.ReConnect:
                    //    {
                    //        _manager.ReConnect();
                    //        break;
                    //    }
                    //case ServiceOperation.RefreshIteration:
                    //    {
                    //        _manager.RefreshIteration();
                    //        break;
                    //    }
                    //case ServiceOperation.ScreenOnActions:
                    //    {
                    //        _manager.ScreenOnActions();
                    //        ReInitTimer();
                    //        break;
                    //    }
                    //case ServiceOperation.GetListenerSettings:
                    //    {
                    //        _manager.GetListenerSettings((settings) =>
                    //        {
                    //            Handle((messenger) => Utils.SendData(settings, messenger, _messenger, ServiceOperation.GetListenerSettings));
                    //        });
                    //        break;
                    //    }
                    //case ServiceOperation.SetListenerSettings:
                    //    {
                    //        var settings = Utils.GetData<ListenerSettings>(msg);
                    //        _manager.SetListenerSettings(settings);
                    //        break;
                    //    }
                    //case ServiceOperation.GetGeolocationAccuracy:
                    //    {
                    //        _manager.GetGeolocationAccuracy((acc) =>
                    //            Handle((messenger) => Utils.SendData(acc, messenger, _messenger, ServiceOperation.GetGeolocationAccuracy)));
                    //        break;
                    //    }
                    //case ServiceOperation.SetGeolocationAccuracy:
                    //    {
                    //        _manager.SetGeolocationAccuracy(Utils.GetData<int>(msg));
                    //        break;
                    //    }
                    //case ServiceOperation.GetGeolocationListenerSettings:
                    //    {
                    //        _manager.GetGeolocationListenerSettings((listenerSettings) =>
                    //            Handle((messenger) => Utils.SendData(listenerSettings, messenger, _messenger, ServiceOperation.GetGeolocationListenerSettings)));
                    //        break;
                    //    }
                    //case ServiceOperation.SetGeolocationListenerSettings:
                    //    {
                    //        _manager.SetGeolocationListenerSettings(Utils.GetData<GeolocationListenerSettings>(msg));
                    //        break;
                    //    }
                    //case ServiceOperation.Close:
                    //    {
                    //        Stop();
                    //        break;
                    //    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Error in Handler_HasCome", nameof(EmergencyButtonService));
            }
        }

        public void Activate()
        {
            if(ServiceState!= ServiceState.None)
                return;

            ServiceState = ServiceState.Initiation;
            Logger.Information("Activate()", nameof(EmergencyButtonService));

            InitWakeLock();
            _messenger = new Messenger(_inHandler);
            _inHandler.HasCome += InHandler_HasCome;
            ServiceState = ServiceState.Active;

        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
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
            return "Processed message - " + message;
        }
    }
}