using EmergencyButton.Core.ComponentModel;
using EmergencyButton.Core.Instrumentation;
using System.Diagnostics;

namespace EmergencyButton.App.Droid.Instrumentation
{
    public static class Logger
    {
        public static string SubsystemId = "EmergencyButton.Service";

        public static LogEntry leInformationTemplate = new LogEntry
            {SubsystemId = SubsystemId, Severity = TraceEventType.Information};

        public static LogEntry leWarningTemplate = new LogEntry
            {SubsystemId = SubsystemId, Severity = TraceEventType.Warning};

        public static LogEntry leErrorTemplate = new LogEntry
            {SubsystemId = SubsystemId, Severity = TraceEventType.Error};

        public static void Information(string title, string componentName, string description = "")
        {
            var logEntry = new LogEntry(leInformationTemplate)
                {Title = title, ComponentName = componentName, Description = description};
            Singleton.InstrumentationService.WriteLogEntry(logEntry);
        }

        public static void Warning(string title, string componentName, string description = "")
        {
            var logEntry = new LogEntry(leWarningTemplate)
                {Title = title, ComponentName = componentName, Description = description};
            Singleton.InstrumentationService.WriteLogEntry(logEntry);
        }

        public static void Error(string title, string componentName, string description = "")
        {
            var logEntry = new LogEntry(leErrorTemplate)
                {Title = title, ComponentName = componentName, Description = description};
            Singleton.InstrumentationService.WriteLogEntry(logEntry);
        }
    }
}