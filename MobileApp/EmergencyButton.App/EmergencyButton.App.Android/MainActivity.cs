using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using EmergencyButton.App.Droid.Common;
using EmergencyButton.App.Droid.EbService;
using EmergencyButton.App.Service;
using EmergencyButton.Core.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace EmergencyButton.App.Droid
{
    [Activity(Label = "EmergencyButton", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity, IStub
    {
        Intent startServiceIntent;
        private BoundServiceConnection _serviceConnection;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {

                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);
                Forms.Init(this, savedInstanceState);

                LoadApplication(new App());

                Singleton.Services.RegisterService<IStub>(this);
                if (_serviceConnection == null)
                {
                    _serviceConnection = new BoundServiceConnection();
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        public void StartService()
        {
                startServiceIntent = new Intent(this, typeof(EmergencyButtonService));
                startServiceIntent.SetAction(Constants.ACTION_START_SERVICE);
            //  StartService(startServiceIntent);
            BindService(startServiceIntent, _serviceConnection, Bind.AutoCreate);
            
        }

        private void Service_HasCome(object arg1, object arg2)
        {
            int a = -1;
        }

        public void ServiceInvokeTest()
        {
            if (_serviceConnection.IsConnected)
                _serviceConnection.Binder.Service.HasCome += Service_HasCome;

            if (_serviceConnection.IsConnected)
            {
               var aaa= _serviceConnection.Binder.Service.TestCall("caaaaaaaaaaaaaaal");
            }

        }
    }
}