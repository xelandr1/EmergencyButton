﻿using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using EmergencyButton.App.Droid.Common;
using EmergencyButton.App.Droid.EbService;
using EmergencyButton.App.Droid.Ipc;
using EmergencyButton.App.Droid.Services;
using EmergencyButton.App.Service;
using EmergencyButton.Core.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace EmergencyButton.App.Droid
{
    [Activity(Label = "EmergencyButton", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity, IStub, IResumeSupportService
    {
        Intent startServiceIntent;
        private EbServiceConnection _ebServiceConnection;
        internal bool isStarting = false;

        private ResumeSupportState _currentState = ResumeSupportState.Closed;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                Singleton.Services.GetService<ICurrentContext>().Context = this;

                Singleton.Services.UnRegisterService<IResumeSupportService>();
                Singleton.Services.RegisterService<IResumeSupportService>(this);

                _ebServiceConnection = new EbServiceConnection(this);

                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                Forms.Init(this, savedInstanceState);

                base.OnCreate(savedInstanceState);
                LoadApplication(new App());

                Singleton.Services.RegisterService<IStub>(this);
                startServiceIntent = new Intent(this, typeof(EmergencyButtonService));
                startServiceIntent.SetAction(Constants.ACTION_START_SERVICE);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            var serviceToStart = new Intent(this, typeof(EmergencyButtonService));
            BindService(serviceToStart, _ebServiceConnection, Bind.AutoCreate);

            isStarting = true;
            //  Log.Debug(TAG, "BindService has been called.");



        }

        protected override void OnResume()
        {
            base.OnResume();
            RaiseStateChanged(ResumeSupportState.Active, _currentState);
            _currentState = ResumeSupportState.Active;

        }

        protected override void OnPause()
        {
            base.OnPause();
            var newState = IsFinishing ? ResumeSupportState.Stopped : ResumeSupportState.Paused;
            RaiseStateChanged(newState, _currentState);
            _currentState = newState;

        }


        public void StartService()
        {
            StartService(startServiceIntent);
        }

        public void ServiceInvokeTest()
        {
            if (_ebServiceConnection.Messenger != null)
            {
                Message msg = Message.Obtain(null, (int)ServiceCommand.test);
                try
                {
                    _ebServiceConnection.Messenger.Send(msg);
                }
                catch (RemoteException ex)
                {
                  //  Log.Error(TAG, ex, "There was a error trying to send the message.");
                }
            }
        }

        public ResumeSupportStateChanged StateChanged { get; set; }

        private void RaiseStateChanged(ResumeSupportState current, ResumeSupportState previous)
        {
            ((IResumeSupportService)this).StateChanged?.Invoke(this, current, previous);
        }
    }
}