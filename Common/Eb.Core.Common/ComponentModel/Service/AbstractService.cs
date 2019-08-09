using System;
using EmergencyButton.Core.ComponentModel.Event;

namespace EmergencyButton.Core.ComponentModel.Service
{
    /// <summary>
    /// Базовый класс сервисов
    /// </summary>
    public abstract class AbstractService : IService
    {
        private ServiceState _serviceState;

        /// <summary>
        /// Активация, внутренняя инициализация
        /// </summary>
        public abstract void Activate();

        /// <summary>
        /// Деактивация, Блокирование обращений, останов фоновых процессов, сохранение временных данных
        /// </summary>
        public abstract void Deactivate();

        /// <summary>
        /// Текущее состояние сервиса
        /// </summary>
        public ServiceState ServiceState
        {
            get { return _serviceState; }
            protected set
            {
                if (_serviceState != value)
                {
                    _serviceState = value;
                    OnServiceStateChanged();
                }
            }
        }


        /// <summary>
        /// Изменилось состояние сервиса
        /// </summary>
        public event EventHandler<ServiceState> ServiceStateChanged;

        protected virtual void OnServiceStateChanged()
        {
            ServiceStateChanged?.Invoke(this, _serviceState);
        }
    }
}