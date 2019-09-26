using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmergencyButton.App.ComponentModel.Service;
using EmergencyButton.Core.Geolocation;
using EmergencyButton.Core.Instrumentation;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace EmergencyButton.App.Droid.Services
{
    public class GeolocationService : AbstractHostedService, IGeolocationService
    {
        protected override Task StartAsyncInternal(CancellationToken cancellationToken)
        {
            Logger.Information("Activate()", nameof(GeolocationService));

            ServiceState = ServiceState.Initiation;

            CrossGeolocator.Current.PositionChanged += CrossGeolocator_PositionChanged;

            ServiceState = ServiceState.Active;
            return Task.CompletedTask;

        }


        protected override Task StopAsyncInternal(CancellationToken cancellationToken)
        {
            Logger.Information("Deactivate()", nameof(DroidPowerManager));

            ServiceState = ServiceState.Termination;

            CrossGeolocator.Current.PositionChanged -= CrossGeolocator_PositionChanged;

            return Task.CompletedTask;

        }

        public event EventHandler<Geolocation> PositionChanged;
        public double DesiredAccuracy { get; set; }
        public bool IsListening { get; }
        public bool SupportsHeading { get; }
        public bool IsGeolocationAvailable { get; }
        public bool IsGeolocationEnabled { get; }

        public async Task<Geolocation> GetLastKnownLocationAsync()
        {
            var position = await CrossGeolocator.Current.GetLastKnownLocationAsync();
            var location = position.ToGeolocation();
            return location;
        }

        public async Task<Geolocation> GetPositionAsync(TimeSpan? timeout = null, CancellationToken? token = null, bool includeHeading = false)
        {
            var position = await CrossGeolocator.Current.GetPositionAsync(timeout, token, includeHeading);
            var location = position.ToGeolocation();
            return location;
        }

        public async Task<IEnumerable<CivicAddress>> GetAddressesForPositionAsync(Geolocation position)
        {
            var addressList = await CrossGeolocator.Current.GetAddressesForPositionAsync(position.ToPosition());
            return (from address in addressList select address.ToCivicAddress()).ToList();
        }

        private void CrossGeolocator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            OnPositionChanged(e.Position.ToGeolocation());
        }

        protected virtual void OnPositionChanged(Geolocation e)
        {
            PositionChanged?.Invoke(this, e);
        }
    }

    public static class GeolocationRoutines
    {
        public static Geolocation ToGeolocation(this Position position)
        {
            return new Geolocation()
            {
                Timestamp = position.Timestamp,
                Latitude = position.Latitude,
                Longitude = position.Longitude,
                Altitude = position.Altitude,
                AltitudeAccuracy = position.AltitudeAccuracy,
                Accuracy = position.Accuracy,
                Heading = position.Heading,
                Speed = position.Speed,
                //  IsFromMockProvider = position.
            };

        }
        public static Position ToPosition(this Geolocation location)
        {
            return new Position()
            {
                Timestamp = location.Timestamp,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Altitude = location.Altitude,
                AltitudeAccuracy = location.AltitudeAccuracy,
                Accuracy = location.Accuracy,
                Heading = location.Heading,
                Speed = location.Speed,
                //  IsFromMockProvider = position.
            };

        }


        public static CivicAddress ToCivicAddress(this Address address)
        {
            return new CivicAddress()
            {
                CountryCode = address.CountryCode,
                CountryName = address.CountryName,
                Latitude = address.Latitude,
                Longitude = address.Longitude,
                FeatureName = address.FeatureName,
                PostalCode = address.PostalCode,
                SubLocality = address.SubLocality,
                Thoroughfare = address.Thoroughfare,
                SubThoroughfare = address.SubThoroughfare,
                SubAdminArea = address.SubAdminArea,
                AdminArea = address.AdminArea

            };

        }

    }
}
