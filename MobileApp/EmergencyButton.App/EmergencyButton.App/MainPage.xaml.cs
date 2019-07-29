using System;
using EmergencyButton.App.Service;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.ComponentModel.Event;
using Xamarin.Forms;

namespace EmergencyButton.App
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            Singleton.GetService<IResumeSupportService>().StateChanged = (sender, currentState, previousState) =>
            {
                // Do not reinit when app was "home button pressed"
                if (currentState == ResumeSupportState.Paused)
                {
                  //  DialogView.CloseAllDialogs();
                }

                if (previousState == ResumeSupportState.Closed || previousState == ResumeSupportState.Stopped)
                {
                    Initialize();
                }
            };
        }

        private async void Initialize()
        {
            await Singleton.GetService<IRuntimePermissionsHandler>().TryGrantRequiredPermissions();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Singleton.GetService<IStub>().ServiceInvokeTest();
        }

        private void StartService_OnClicked(object sender, EventArgs e)
        {
            Singleton.GetService<IStub>().StartService();
        }


    }
}