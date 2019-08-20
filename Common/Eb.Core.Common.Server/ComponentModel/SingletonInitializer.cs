using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Instrumentation;
using EmergencyButton.Core.Server.Instrumentation;

namespace EmergencyButton.Core.Server.ComponentModel
{
    public static class SingletonInitializer
    {
        public static void Initialize()
        {
            if (Singleton.InstrumentationService == null)
                Singleton.Services.RegisterService<IInstrumentationService>(new InstrumentationService());

        }

        public static void UnInitialize()
        {
        }
    }
}