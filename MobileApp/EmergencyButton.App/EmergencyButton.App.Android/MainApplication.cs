using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using EmergencyButton.App.Droid.Common;
using EmergencyButton.App.Droid.Instrumentation;
using EmergencyButton.App.Droid.Services;
using EmergencyButton.App.Service;
using EmergencyButton.Core.Common;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Instrumentation;
using Constants = EmergencyButton.App.Droid.Common.Constants;

namespace EmergencyButton.App.Droid
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {

        public MainApplication(IntPtr handle, JniHandleOwnership transer)
            : base(handle, transer)
        {
            SubsystemIdentity.SubsystemId = Constants.EmergencyButtonApp_SubsystemName;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule")]
        public override void OnCreate()
        {
            base.OnCreate();

            SingletonInitializer.Initialize(this);
            Logger.Information("OnCreate()", nameof(MainApplication));

            RegisterActivityLifecycleCallbacks(this);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            Logger.Information("OnTerminate()", nameof(MainApplication));

            UnregisterActivityLifecycleCallbacks(this);
        }


        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            Logger.Information("OnActivityCreated()"+activity.GetType().Name, nameof(MainApplication));

        }


        public void OnActivityDestroyed(Activity activity)
        {
            Logger.Information("OnActivityDestroyed()" + activity.GetType().Name, nameof(MainApplication));

        }

        public void OnActivityPaused(Activity activity)
        {
            Logger.Information("OnActivityPaused()" + activity.GetType().Name, nameof(MainApplication));

        }

        public void OnActivityResumed(Activity activity)
        {
            Logger.Information("OnActivityResumed()" + activity.GetType().Name, nameof(MainApplication));

        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
            Logger.Information("OnActivitySaveInstanceState()" + activity.GetType().Name, nameof(MainApplication));

        }

        public void OnActivityStarted(Activity activity)
        {
            Logger.Information("OnActivityStarted()" + activity.GetType().Name, nameof(MainApplication));

        }

        public void OnActivityStopped(Activity activity)
        {
            Logger.Information("OnActivityStopped()" + activity.GetType().Name, nameof(MainApplication));

        }

    }
}