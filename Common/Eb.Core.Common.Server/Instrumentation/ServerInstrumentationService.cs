using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.Core.Server.Instrumentation
{
    public class ServerInstrumentationService : InstrumentationServiceBase
    {
        public ServerInstrumentationService()
        {
            RegisterLogStorage(new FileLogStorage());
        }
    }
}