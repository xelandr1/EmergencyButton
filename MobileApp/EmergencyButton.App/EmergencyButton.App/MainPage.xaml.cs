using System;
using System.Linq;
using EmergencyButton.App.Remote;
using EmergencyButton.App.Remote.Http;
using EmergencyButton.App.Service;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.ComponentModel.Event;
using EmergencyButton.Core.Geolocation;
using Xamarin.Forms;

namespace EmergencyButton.App
{
    public partial class MainPage 
    {

        public MainPage()
        {
            InitializeComponent();

            Detail=new NavigationPage(new Test1());
            IsPresented = true;

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
            //Singleton.GetService<IGeolocationService>().PositionChanged += IGeolocationService_PositionChanged;

        }
        private void BtnTest1_OnClicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Test1());
            IsPresented = false;
        }

        private void BtnMap_OnClicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new MapPage());
            IsPresented = false;
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
          //var version= await Singleton.GetService<RemoteClientManager>().Client.GetCoreVersion();

            // Navigation.PushModalAsync(new MapPage1());
            //Singleton.GetService<IStub>().ServiceInvokeTest();
        }

        //private void StartService_OnClicked(object sender, EventArgs e)
        //{
        //    Singleton.GetService<IStub>().StartService();
        //}


        //private async void StartGeo_OnClicked(object sender, EventArgs e)
        //{
        //    var location = await Singleton.GetService<IGeolocationService>().GetPositionAsync();

        //    lblGPS.Text = $"Time: {location.Timestamp} \nLat: {location.Latitude} \nLong: {location.Longitude}" +
        //                  $" \nAltitude: {location.Altitude} \nAltitude Accuracy: {location.Accuracy} " +
        //                  $"\nAltitudeAccuracy: {location.AltitudeAccuracy} \nHeading: {location.Heading} \nSpeed: {location.Speed}";

        //    var addressList = await Singleton.GetService<IGeolocationService>().GetAddressesForPositionAsync(location);
        //    var address = addressList.FirstOrDefault();

        //    if (address != null)
        //        lblAddress.Text = $"Address: Thoroughfare = {address.Thoroughfare}\nLocality = {address.Locality}\nCountryCode = {address.CountryCode}" +
        //                          $"\nCountryName = {address.CountryName}\nPostalCode = {address.PostalCode}\nSubLocality = {address.SubLocality}" +
        //                          $"\nSubThoroughfare = {address.SubThoroughfare}";


        //}

        //private void IGeolocationService_PositionChanged(object sender, Geolocation location)
        //{

        //    lblGPS.Text = $"Time: {location.Timestamp} \nLat: {location.Latitude} \nLong: {location.Longitude}" +
        //                  $" \nAltitude: {location.Altitude} \nAltitude Accuracy: {location.Accuracy} " +
        //                  $"\nAltitudeAccuracy: {location.AltitudeAccuracy} \nHeading: {location.Heading} \nSpeed: {location.Speed}";
        //}

    }
}