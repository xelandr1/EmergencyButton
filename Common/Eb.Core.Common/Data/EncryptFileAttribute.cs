﻿using System;

namespace EmergencyButton.Core.Data
{
    /// <summary>
    /// Attribute that allow to serialize and save encrypted file
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class EncryptFileAttribute: Attribute
    {
    }
}
