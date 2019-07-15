using System;
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
            TestClicked?.Invoke(this, new EventsArgs<int>(0));
        }

        private void StartService_OnClicked(object sender, EventArgs e)
        {
            TestClicked?.Invoke(this, new EventsArgs<int>(0));
        }



        public event EventsHandler<int> TestClicked;
    }
}