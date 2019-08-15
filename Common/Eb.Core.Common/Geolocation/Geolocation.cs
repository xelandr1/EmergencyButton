using System;

namespace EmergencyButton.Core.Geolocation
{
    public class Geolocation
    {
        public Geolocation()
        {
        }

        public Geolocation(double latitude, double longitude)
        {

            Timestamp = DateTimeOffset.UtcNow;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Geolocation(Geolocation location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            Timestamp = location.Timestamp;
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            Altitude = location.Altitude;
            AltitudeAccuracy = location.AltitudeAccuracy;
            Accuracy = location.Accuracy;
            Heading = location.Heading;
            Speed = location.Speed;
            IsFromMockProvider = location.IsFromMockProvider;
        }

        /// <summary>
        /// Gets or sets the timestamp of the position
        /// </summary>
        public DateTimeOffset Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the altitude in meters relative to sea level.
        /// </summary>
        public double Altitude
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the potential position error radius in meters.
        /// </summary>
        public double Accuracy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the potential altitude error range in meters.
        /// </summary>
        /// <remarks>
        /// Not supported on Android, will always read 0.
        /// </remarks>
        public double AltitudeAccuracy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the heading in degrees relative to true North.
        /// </summary>
        public double Heading
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the speed in meters per second.
        /// </summary>
        public double Speed
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if from mock provider
        /// </summary>
        public bool IsFromMockProvider
        {
            get;
            set;
        }
    }
}