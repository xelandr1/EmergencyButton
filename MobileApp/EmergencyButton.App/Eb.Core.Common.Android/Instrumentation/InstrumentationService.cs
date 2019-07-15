using Eb.Core.Common.Instrumentation;

namespace Eb.Core.Common.Droid.Instrumentation
{
    public class InstrumentationService : InstrumentationServiceBase
    {
        public InstrumentationService()
        {
            LogStorages.Add(new FileLogStorage());
        }
    }
}