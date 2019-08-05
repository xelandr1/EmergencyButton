using EmergencyButton.Core.Instrumentation;
using System;
using System.Diagnostics;
using System.IO;

namespace EmergencyButton.App.Droid.Instrumentation
{
    public class FileLogStorage : ILogStorage
    {
        public static string _LogFileName;

        private readonly string _logFilesPath;
        private static string DefaultLogFilePath = Path.Combine(global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "EbLogs");


        public FileLogStorage() : this(string.Empty)
        {
        }

        public FileLogStorage(string logFilesPath)
        {
            _logFilesPath = string.IsNullOrWhiteSpace(logFilesPath)? DefaultLogFilePath:logFilesPath;
        }

        public string LogFileName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_LogFileName))
                {
                    if (!Directory.Exists(_logFilesPath))
                        Directory.CreateDirectory(_logFilesPath);

                    _LogFileName = Path.Combine(_logFilesPath, string.Format("{0:dd.MM.yy HH-mm}.log",
                        DateTime.Now));
                }

                return _LogFileName;
            }
        }


        public void StoreLogEntry(LogEntry logEntry)
        {
            try
            {
                var stream = File.AppendText(LogFileName);
                stream.WriteLine(InstrumentationConstants.DumpSeparateString);
                stream.WriteLine($"{logEntry.TimeStamp} {logEntry.Severity.ToString()} {logEntry.ComponentName}");
                if (!string.IsNullOrWhiteSpace(logEntry.Title)) stream.WriteLine(logEntry.Title);
                if (!string.IsNullOrWhiteSpace(logEntry.Description)) stream.WriteLine(logEntry.Description);
                if (!string.IsNullOrWhiteSpace(logEntry.Context)) stream.WriteLine(logEntry.Context);
                stream.WriteLine(InstrumentationConstants.DumpSeparateString);

                stream.Flush();
                stream.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }
    }
}