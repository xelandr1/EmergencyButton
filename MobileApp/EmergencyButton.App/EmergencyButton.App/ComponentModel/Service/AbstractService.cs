using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmergencyButton.App.ComponentModel.Service
{

    /// <summary>
    /// Базовый класс сервисов
    /// </summary>
    public abstract class AbstractHostedService : IService
    {
    private readonly object _syncServiceState = new object();
        private ServiceState _serviceState;

        /// <summary>
        /// Текущее состояние сервиса
        /// </summary>
        public ServiceState ServiceState
        {
            get { 
                lock(_syncServiceState) return _serviceState; }
            protected set
            {
                if (_serviceState != value)
                {
                    lock (_syncServiceState)
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

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (ServiceState > ServiceState.None) throw new Exception("Already started");
            return StartAsyncInternal(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (ServiceState >= ServiceState.Termination) throw new Exception("Already stopped");
            return StopAsyncInternal(cancellationToken);
        }

        protected virtual Task StartAsyncInternal(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected virtual Task StopAsyncInternal(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}