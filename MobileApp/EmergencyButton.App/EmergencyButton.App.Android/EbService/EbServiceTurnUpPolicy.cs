using Android.Content;
using EmergencyButton.App.Droid.Instrumentation;
using EmergencyButton.App.Droid.Services;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.App.Droid.EbService
{
    public class EbServiceTurnUpPolicy
    {
        private readonly Context _context;

        public EbServiceTurnUpPolicy(Context context)
        {
            _context = context;
        }

        public void Start()
        {
            if (Singleton.InstrumentationService == null)
                Singleton.Services.RegisterService<IInstrumentationService>(new InstrumentationService());

            if(!Singleton.Services.ContainService<ICurrentContext>())
                Singleton.Services.RegisterService<ICurrentContext>(new CurrentContextService(_context));


        }

    }
}