using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace EmergencyButton.App.ComponentModel.Service
{
    /// <summary>
    ///     Боксинг сервисов песочницы для клиентской стороны
    /// </summary>
    public class ServiceProvider : IServiceProvider
    {
        private readonly IReadOnlyServiceProvider _parentServiceProvider;
        private readonly Dictionary<Type, object> _services;

        public ServiceProvider(IReadOnlyServiceProvider parentServiceProvider)
        {
            _parentServiceProvider = parentServiceProvider;
            _services = new Dictionary<Type, object>();
        }


        [SecuritySafeCritical]
        public TService GetService<TService>()
        {
            var serviceContract = typeof(TService);

            return (TService) GetService(serviceContract);
        }

        [SecuritySafeCritical]
        public object GetService(Type serviceContract)
        {
            var targetService =
                (from service in _services where serviceContract.IsAssignableFrom(service.Key) select service.Value)
                .FirstOrDefault();

            if (targetService != null) return targetService;

            if (_parentServiceProvider != null)
                targetService = _parentServiceProvider.GetService(serviceContract);

            return targetService;
        }


        [SecuritySafeCritical]
        public bool ContainService<TService>()
        {
            var targetType = typeof(TService);

            return _services.ContainsKey(targetType) ||
                   _parentServiceProvider != null && _parentServiceProvider.ContainService<TService>();
        }

        public TService GetService<TService>(string servicePath, bool permissionsRequired = true) where TService : class
        {
            //if (permissionsRequired)
            //{
            //    return GetRemoteService<TService>(servicePath, RemoteServicePermissions.FullAccess);
            //}
            //else
            //{
            //    return GetRemoteService<TService>(servicePath, RemoteServicePermissions.ReadonlyOperationState);

            //}
            throw new NotImplementedException();
        }


        //[SecuritySafeCritical]
        //private TService GetRemoteService<TService>(string servicePath, RemoteServicePermissions permissions) where TService : class
        //{
        //    return _environmentServices.Get<IOperationEnvironment>().Communications.GetRemoteCatalogService<TService>(servicePath, permissions);
        //}


        [SecuritySafeCritical]
        public void RegisterService<TService>(TService service)
        {
            _services.Add(typeof(TService), service);
        }

        [SecuritySafeCritical]
        public void UnRegisterService<TService>()
        {
            if (_services.ContainsKey(typeof(TService)))
            _services.Remove(typeof(TService));
        }

        [SecuritySafeCritical]
        public void RegisterService(Type serviceContract, object service)
        {
            _services.Add(serviceContract, service);
        }

        [SecuritySafeCritical]
        public void UnRegisterService(Type serviceContract)
        {
            _services.Remove(serviceContract);
        }
    }
}