using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using EmergencyButton.App.Droid.Common;
using EmergencyButton.App.Droid.Instrumentation;
using EmergencyButton.App.Service;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.App.Droid
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {

        public MainApplication(IntPtr handle, JniHandleOwnership transer)
            : base(handle, transer)
        {
            // Do nothing
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2140:TransparentMethodsMustNotReferenceCriticalCodeFxCopRule")]
        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);


            if (Singleton.InstrumentationService == null)
                Singleton.Services.RegisterService<IInstrumentationService>(new InstrumentationService());



        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            //if (activity is MainPage)
            //    ((MainPage)activity).TestClicked += MainApplication_TestClicked;

        }

        private void MainApplication_TestClicked(object sender, Core.ComponentModel.Event.EventsArgs<int> args)
        {

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