using System;

namespace EmergencyButton.App.Service
{

    public interface IPowerManager
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