using System;
using System.Linq;
using EmergencyButton.App.Service;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.ComponentModel.Event;
using EmergencyButton.Core.Geolocation;
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
            Singleton.GetService<IGeolocationService>().PositionChanged += IGeolocationService_PositionChanged;

        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Singleton.GetService<IStub>().ServiceInvokeTest();
        }

        private void StartService_OnClicked(object sender, EventArgs e)
        {
            Singleton.GetService<IStub>().StartService();
        }


        private async void StartGeo_OnClicked(object sender, EventArgs e)
        {
            var location = await Singleton.GetService<IGeolocationService>().GetPositionAsync();

            lblGPS.Text = $"Time: {location.Timestamp} \nLat: {location.Latitude} \nLong: {location.Longitude}" +
                          $" \nAltitude: {location.Altitude} \nAltitude Accuracy: {location.Accuracy} " +
                          $"\nAltitudeAccuracy: {location.AltitudeAccuracy} \nHeading: {location.Heading} \nSpeed: {location.Speed}";

            var addressList = await Singleton.GetService<IGeolocationService>().GetAddressesForPositionAsync(location);
            var address = addressList.FirstOrDefault();

            if (address != null)
                lblAddress.Text = $"Address: Thoroughfare = {address.Thoroughfare}\nLocality = {address.Locality}\nCountryCode = {address.CountryCode}" +
                                  $"\nCountryName = {address.CountryName}\nPostalCode = {address.PostalCode}\nSubLocality = {address.SubLocality}" +
                                  $"\nSubThoroughfare = {address.SubThoroughfare}";


        }

        private void IGeolocationService_PositionChanged(object sender, Geolocation location)
        {

            lblGPS.Text = $"Time: {location.Timestamp} \nLat: {location.Latitude} \nLong: {location.Longitude}" +
                          $" \nAltitude: {location.Altitude} \nAltitude Accuracy: {location.Accuracy} " +
                          $"\nAltitudeAccuracy: {location.AltitudeAccuracy} \nHeading: {location.Heading} \nSpeed: {location.Speed}";
        }
    }
}