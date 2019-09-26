using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmergencyButton.Core.Geolocation
{
    public interface IGeolocationService
    {
        /// <summary>
        /// Position changed event handler
        /// </summary>
        event EventHandler<Geolocation> PositionChanged;

        /// <summary>
        /// Desired accuracy in meters
        /// </summary>
        double DesiredAccuracy { get; set; }

        /// <summary>
        /// Gets if you are listening for location changes
        /// </summary>
        bool IsListening { get; }

        /// <summary>
        /// Gets if device supports heading
        /// </summary>
        bool SupportsHeading { get; }

        /// <summary>
        /// Gets if geolocation is available on device
        /// </summary>
        bool IsGeolocationAvailable { get; }

        /// <summary>
        /// Gets if geolocation is enabled on device
        /// </summary>
        bool IsGeolocationEnabled { get; }



        /// <summary>
        /// Gets the last known and most accurate location.
        /// This is usually cached and best to display first before querying for full position.
        /// </summary>
        /// <returns>Best and most recent location or null if none found</returns>
        Task<Geolocation> GetLastKnownLocationAsync();

        /// <summary>
        /// Gets position async with specified parameters
        /// </summary>
        /// <param name="timeout">Timeout to wait, Default Infinite</param>
        /// <param name="token">Cancelation token</param>
        /// <param name="includeHeading">If you would like to include heading</param>
        /// <returns>Position</returns>
        Task<Geolocation> GetPositionAsync(TimeSpan? timeout = null, CancellationToken? token = null, bool includeHeading = false);

        /// <summary>
        /// Retrieve addresses for position.
        /// </summary>
        /// <param name="location">Desired position (latitude and longitude)</param>
        /// <returns>Addresses of the desired position</returns>
        Task<IEnumerable<CivicAddress>> GetAddressesForPositionAsync(Geolocation location);

    }

    public class GeolocationServiceConfig
    {
        public TimeSpan MinimumUpdateTime { get; set; }
        public double MinimumUpdateDistance { get; set; }
        public bool IncludeHeading { get; set; }

    }
}