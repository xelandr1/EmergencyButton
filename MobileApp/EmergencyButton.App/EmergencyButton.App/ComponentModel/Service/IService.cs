using System;
using Microsoft.Extensions.Hosting;

namespace EmergencyButton.App.ComponentModel.Service
{
    /// <summary>
    /// Стандартное поведение сервиса
    /// </summary>
    public interface IService:IHostedService
    {
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