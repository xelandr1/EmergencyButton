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