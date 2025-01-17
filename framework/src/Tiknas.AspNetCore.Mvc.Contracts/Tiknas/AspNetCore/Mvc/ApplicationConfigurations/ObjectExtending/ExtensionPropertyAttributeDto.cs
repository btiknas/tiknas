﻿using System;
using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;

[Serializable]
public class ExtensionPropertyAttributeDto
{
    public string TypeSimple { get; set; } = default!;

    public Dictionary<string, object> Config { get; set; }

    public ExtensionPropertyAttributeDto()
    {
        Config = new Dictionary<string, object>();
    }
}
