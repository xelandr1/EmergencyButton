using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Instrumentation;
using EmergencyButton.Core.Server.Instrumentation;

namespace EmergencyButton.Core.Server.Core
{
    public static class SingletonInitializer
    {
        public static void Initialize()
        {
            if (Singleton.InstrumentationService == null)
                Singleton.Services.RegisterService<IInstrumentationService>(new ServerInstrumentationService());

        }

        public static void TearDown()
        {
        }
    }
}