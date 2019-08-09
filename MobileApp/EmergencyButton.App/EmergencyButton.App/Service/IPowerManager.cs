using System;
using EmergencyButton.Core.ComponentModel.Service;

namespace EmergencyButton.App.Service
{

    public interface IPowerManager : IService
    {
        DevicePowerMode CurrentPowerMode { get; set; }
        event EventHandler<DevicePowerMode> CurrentPowerModeChanged;

        double BatteryChargeLevel { get; }

        BatteryState BatteryState { get; }

    }

    public enum DevicePowerMode
    {
        Sleeping,
        PowerSave,
        Normal,
        MaxPerformance 
    }

    public enum BatteryState
    {
        Unknown,
        Charging,
        Full,
        NotCharging,
   }
}