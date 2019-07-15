using System;
using System.Diagnostics;

namespace EmergencyButton.Core.Instrumentation
{
    /// <summary>
    ///     Элемент лога
    /// </summary>
    [Serializable]
    public class LogEntry
    {
        public LogEntry()
        {
        }

        public LogEntry(LogEntry template)
        {
            Context = template.Context;
            EventId = template.EventId;
            Description = template.Description;
            Severity = template.Severity;
            Title = template.Title;
            ComponentName = template.ComponentName;
        }


        /// <param name="subsystemId">Идентификатор подсистемы.</param>
        /// <param name="eventId">Номер или идентификатор события.</param>
        /// <param name="severity">Тип события.</param>
        /// <param name="level">Уровень события.</param>
        /// <param name="title">Заголовок события.</param>
        /// <param name="description">Текст сообщения.</param>
        /// <param name="context">
        ///     Строка для дополнительной идентификации события.
        ///     Будет присутствовать в тексте события отдельной строкой вида Context: [Value].
        /// </param>
        /// <param name="priority">Приоритет.</param>
        public LogEntry(string subsystemId,
            int eventId,
            TraceEventType severity,
            string title,
            string description,
            string context)
        {
            SubsystemId = subsystemId;
            EventId = eventId;
            Severity = severity;
            Title = title;
            Description = description;
            Context = context;
        }

        public DateTime TimeStamp { get; set; } = DateTime.Now;

        /// <summary>
        ///     Наименование компонента
        /// </summary>
        public string ComponentName { get; set; } = string.Empty;

        /// <summary>
        ///     Строка для дополнительной идентификации события.
        ///     Будет присутствовать в тексте события отдельной строкой вида Context: [Value].
        /// </summary>
        public string Context { get; set; } = string.Empty;

        /// <summary>
        ///     Номер или идентификатор события.
        /// </summary>
        public int EventId { get; }

        /// <summary>
        ///     Текст сообщения.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     Тип события.
        /// </summary>
        public TraceEventType Severity { get; set; } = TraceEventType.Information;

        /// <summary>
        ///     Заголовок события.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        public string SubsystemId { get; set; }


        ///// <summary>
        ///// Дамп в текст
        ///// </summary>
        ///// <param name = "indentLevel" > Уровень отступа.</param>
        //public string ToString(int indentLevel)
        //{
        //    const char paggingChar = '\t';

        //    string indent = "".PadLeft(indentLevel, paggingChar);
        //    string indentForData = "".PadLeft(indentLevel + 1, paggingChar);

        //    var sb = new StringBuilder(Environment.NewLine);

        //    sb.AppendFormat("{0}Type: {1}" + Environment.NewLine, indent, (Type ?? String.Empty));
        //    sb.AppendFormat("{0}Description: {1}" + Environment.NewLine, indent, (Description ?? String.Empty));
        //    sb.AppendFormat("{0}StackTrace: {1}" + Environment.NewLine, indent, (StackTrace ?? String.Empty));

        //    sb.AppendFormat("{0}Data:", indent);

        //    if (Data == null)
        //        sb.Append("<NULL>" + Environment.NewLine);
        //    else
        //    {
        //        sb.Append(Environment.NewLine);

        //        foreach (string key in Data.Keys)
        //            sb.AppendFormat("{0}{1}: {2}" + Environment.NewLine, indentForData, key, (Data[key] ?? "<NULL>"));
        //    }

        //    if (InnerException != null)
        //    {
        //        sb.AppendFormat("{0}InnerException:", indent);
        //        sb.Append(InnerException.ToString(indentLevel + 1));
        //    }

        //    if (WrapperInfo != null)
        //    {
        //        sb.AppendFormat("{0}WrapperInfo:", indent);
        //        sb.Append(WrapperInfo.ToString(indentLevel + 1));
        //    }

        //    return sb.ToString();
        //}
    }
}