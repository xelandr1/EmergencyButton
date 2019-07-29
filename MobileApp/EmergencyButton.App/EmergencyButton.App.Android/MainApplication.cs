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

            SingletonServicesInitializer.Initialize(this);
            var ttt = SubsystemIdentity.InstanceId;

            Logger.Information(nameof(MainApplication) + " OnCreate", nameof(MainApplication));

            RegisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            //if (activity is MainPage)
            //    ((MainPage)activity).TestClicked += MainApplication_TestClicked;

        }


        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {

        }

        public void OnActivityStopped(Activity activity)
        {

        }

        public void StartService()
        {
            throw new NotImplementedException();
        }

        public void ServiceInvokeTest()
        {
            throw new NotImplementedException();
        }
    }
}