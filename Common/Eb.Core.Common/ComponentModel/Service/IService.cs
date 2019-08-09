using System;
using EmergencyButton.Core.ComponentModel.Event;

namespace EmergencyButton.Core.ComponentModel.Service
{
    /// <summary>
    /// Стандартное поведение сервиса
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Активация, внутренняя инициализация
        /// </summary>
        void Activate();

        /// <summary>
        /// Деактивация, Блокирование обращений, останов фоновых процессов, сохранение временных данных
        /// </summary>
        void Deactivate();

        /// <summary>
        /// Текущее состояние сервиса
        /// </summary>
        ServiceState ServiceState { get; }

        /// <summary>
        /// Изменилось состояние сервиса
        /// </summary>
        event EventHandler<ServiceState> ServiceStateChanged;

    }
}