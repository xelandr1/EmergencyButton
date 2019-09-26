using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmergencyButton.App.ComponentModel;
using EmergencyButton.App.Remote;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Geolocation;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace EmergencyButton.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test1 : ContentPage
    {
        public Test1()
        {
            InitializeComponent();

            //var pin = new Pin
            //{
            //    Type = PinType.Place,
            //    Position = new Position(37.79752, -122.40183),
            //    Label = "Xamarin San Francisco Office",
            //    Address = "394 Pacific Ave, San Francisco CA",
            //    Id = "Xamarin",
            //};

            //myMap.Pins.Add(pin);
        }

        private async void test1_click(object sender, EventArgs e)
        {
            var remoteCommandManager = Singleton.GetService<IRemoteCommandManager>();

            var res = await remoteCommandManager.DoCommand<string>("CoreVersion");
            lblTest.Text = res;

        }

    }
}