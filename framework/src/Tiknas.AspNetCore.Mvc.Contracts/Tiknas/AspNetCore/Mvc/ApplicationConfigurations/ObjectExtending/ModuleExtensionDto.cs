﻿using System;
using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;

[Serializable]
public class ModuleExtensionDto
{
    public Dictionary<string, EntityExtensionDto> Entities { get; set; }

    public Dictionary<string, object> Configuration { get; set; }

    public ModuleExtensionDto()
    {
        Entities = new Dictionary<string, EntityExtensionDto>();
        Configuration = new Dictionary<string, object>();
    }
}
