using Android.Content;
using Eb.Core.Common.ComponentModel;
using Eb.Core.Common.Droid.Instrumentation;
using Eb.Core.Common.Instrumentation;
using EmergencyButton.Core.Common.Droid.Services;

namespace EmergencyButton.Service.Droid
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