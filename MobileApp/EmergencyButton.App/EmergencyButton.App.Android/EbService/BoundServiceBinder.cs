using Android.OS;

namespace EmergencyButton.App.Droid.EbService
{
    public class BoundServiceBinder : Binder
    {
        public IEmergencyButtonService Service { get; }

        public BoundServiceBinder(IEmergencyButtonService service)
        {
            Service = service;
        }
    }
}