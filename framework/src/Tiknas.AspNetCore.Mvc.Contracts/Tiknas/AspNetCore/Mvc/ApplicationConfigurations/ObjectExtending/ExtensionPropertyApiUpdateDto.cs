﻿using System;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;

[Serializable]
public class ExtensionPropertyApiUpdateDto
{
    /// <summary>
    /// Default: true.
    /// </summary>
    public bool IsAvailable { get; set; } = true;
}
