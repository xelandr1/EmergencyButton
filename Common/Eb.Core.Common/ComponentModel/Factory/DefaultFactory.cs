using System;
using EmergencyButton.Core.Configuration;
using EmergencyButton.Core.Instrumentation;

namespace EmergencyButton.Core.ComponentModel.Factory
{
    public class DefaultFactory : IFactory
    {
        protected static IFactory _Factory;
        public static IFactory Factory
        {
            get
            {
                if (_Factory is null)
                    _Factory = new DefaultFactory();
                return _Factory;
            }
        }

        public object CreateInstance(Type targetType)
        {
            object targetObj = null;
            try
            {
                targetObj = Activator.CreateInstance(targetType);

                if (targetObj is IConfigurable)
                    Configure((IConfigurable)targetObj);

            }
            catch (Exception e)
            {
                Logger.Error("BaseFactory.CreateInstance fail",nameof(DefaultFactory),e);  Console.WriteLine(e);
            }

            return targetObj;
        }

        public T CreateInstance<T>()
        {
            var res = CreateInstance(typeof(T));
            return Equals(default(T),res) ? default(T) : (T)res;
        }

        private void Configure(IConfigurable target)
        {

        }
    }
}