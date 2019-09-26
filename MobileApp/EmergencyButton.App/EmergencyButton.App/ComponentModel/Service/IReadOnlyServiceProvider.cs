using System;
using System.Security;

namespace EmergencyButton.App.ComponentModel.Service
{
    /// <summary>
    /// </summary>
    public interface IReadOnlyServiceProvider : System.IServiceProvider
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        [SecuritySafeCritical]
        TService GetService<TService>();

        /// <summary>
        /// </summary>
        /// <param name="serviceContract"></param>
        /// <returns></returns>
        [SecuritySafeCritical]
        new object GetService(Type serviceContract);

        /// <summary>
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        [SecuritySafeCritical]
        bool ContainService<TService>();

        /// <summary>
        ///     получение ремоут сервиса
        /// </summary>
        /// <param name="servicePath"></param>
        /// <param name="permissionsRequired"> </param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        [SecuritySafeCritical]
        TService GetService<TService>(string servicePath, bool permissionsRequired = true) where TService : class;
    }
}