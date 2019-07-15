using System;

namespace EmergencyButton.Core.ComponentModel.Service
{
    /// <summary>
    ///     Стандартное поведение сервиса
    /// </summary>
    public interface IService
    {
        /// <summary>
        ///     Текущее состояние сервиса
        /// </summary>
        ServiceState ServiceState { get; }

        /// <summary>
        ///     Активация, внутренняя инициализация
        /// </summary>
        void Activate();

        /// <summary>
        ///     Деактивация, Блокирование обращений, останов фоновых процессов, сохранение временных данных
        /// </summary>
        void Deactivate();

        /// <summary>
        ///     Изменилось состояние сервиса
        /// </summary>
        event EventHandler<EventArgs> ServiceStateChanged;

        ///// <summary>
        ///// Исключение ставшее причиной остановки сервиса
        ///// </summary>
        //CommonException BrokenException { get; }
    }
}