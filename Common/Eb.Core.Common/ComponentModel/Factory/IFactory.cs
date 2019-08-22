using System;

namespace EmergencyButton.Core.ComponentModel.Factory
{
    public interface IFactory
    {
        object CreateInstance(Type targetType);
        T CreateInstance<T>();
    }
}