﻿using EmergencyButton.Core.ComponentModel.Service;

namespace EmergencyButton.App.Droid.EbService
{
    public interface IEmergencyButtonService
    {
        string TestCall(string message);
    }
}