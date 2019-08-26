using Eb.Core.Server.Instrumentation;
using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Instrumentation;

namespace Eb.Core.Server.Core
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