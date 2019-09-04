using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Geolocation;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace EmergencyButton.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {

        public MapPage()
        {
            InitializeComponent();


        }

        private async void test1_click(object sender, EventArgs e)
        {
            var location = await Singleton.GetService<IGeolocationService>().GetPositionAsync();
            var address = await Singleton.GetService<IGeolocationService>().GetAddressesForPositionAsync(location);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(location.Latitude, location.Longitude),
                Label = "мну тут",
                Address = address.ToString(),
                Id = "Xamarin",
            };

            myMap.Pins.Add(pin);
            var mapSpan = MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), new Distance(500));


            myMap.MoveToRegion(mapSpan);
        }
    }
}
