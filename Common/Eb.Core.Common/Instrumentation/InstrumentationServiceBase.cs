using System;
using System.Collections.Generic;

namespace EmergencyButton.Core.Instrumentation
{
    public abstract class InstrumentationServiceBase : IInstrumentationService
    {
        protected IList<ILogStorage> LogStorages = new List<ILogStorage>();

        public void WriteLogEntry(LogEntry logEntry)
        {
            foreach (var logStrage in LogStorages)
                try
                {
                    logStrage.StoreLogEntry(logEntry);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
        }

        public void Dispose()
        {
        }
    }
}