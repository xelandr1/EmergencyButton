using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.Core.Server.Instrumentation
{
    public class InstrumentationService : InstrumentationServiceBase
    {
        public InstrumentationService()
        {
            LogStorages.Add(new FileLogStorage());
        }
    }
}