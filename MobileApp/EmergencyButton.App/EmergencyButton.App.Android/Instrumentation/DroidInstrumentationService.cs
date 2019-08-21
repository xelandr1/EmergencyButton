using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.App.Droid.Instrumentation
{
    public class DroidInstrumentationService : InstrumentationServiceBase
    {
        public DroidInstrumentationService()
        {
            RegisterLogStorage(new FileLogStorage());
        }
    }
}