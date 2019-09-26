using System;
using System.Security;

namespace EmergencyButton.App.ComponentModel.Service
{
    /// <summary>
    ///     Контейнер локальных сервисов
    /// </summary>
    public interface IServiceProvider : IReadOnlyServiceProvider
    {
        /// <summary>
        /// </summary>
        /// <param name="service"></param>
        /// <typeparam name="TService"></typeparam>
        [SecuritySafeCritical]
        void RegisterService<TService>(TService service);

        /// <summary>
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        [SecuritySafeCritical]
        void UnRegisterService<TService>();

        /// <summary>
        /// </summary>
        /// <param name="serviceContract"></param>
        /// <param name="service"></param>
        [SecuritySafeCritical]
        void RegisterService(Type serviceContract, object service);

        /// <summary>
        /// </summary>
        /// <param name="serviceContract"></param>
        [SecuritySafeCritical]
        void UnRegisterService(Type serviceContract);
    }
}