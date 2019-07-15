namespace EmergencyButton.Core.Instrumentation
{
    public enum InstrumentationLevel
    {
        /// <summary>Нормальный уровень инструментирования (уровень 1).</summary>
        Normal = 1,

        /// <summary>
        ///     Уровень инструментирования используемый во время расширенный поддержки (уровень 2).
        /// </summary>
        Support = 2,

        /// <summary>
        ///     Уровень инструментировани используемый во время поиска неисправностей (уровень 3).
        /// </summary>
        Troubleshooting = 3,

        /// <summary>
        ///     Уровень инструментирования используемый во время отладки (уровень 4).
        /// </summary>
        Debug = 4
    }
}