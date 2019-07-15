using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.App.Droid.Instrumentation
{
    public class InstrumentationService : InstrumentationServiceBase
    {
        public InstrumentationService()
        {
            LogStorages.Add(new FileLogStorage());
        }
    }
}