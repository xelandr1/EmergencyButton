using EmergencyButton.Core.Instrumentation;
using EmergencyButton.Core.ComponentModel.Service;

namespace EmergencyButton.Core.ComponentModel
{
    public static class Singleton
    {
        static Singleton()
        {
            Services = new ServiceProvider(null);
        }

        public static IServiceProvider Services { get; }

        public static IInstrumentationService InstrumentationService => Services.GetService<IInstrumentationService>();

        public static TService GetService<TService>()
        {
            return Services.GetService<TService>();
        }
    }
}