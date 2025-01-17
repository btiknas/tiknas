﻿using System;
using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;

[Serializable]
public class EntityExtensionDto
{
    public Dictionary<string, ExtensionPropertyDto> Properties { get; set; }

    public Dictionary<string, object> Configuration { get; set; }

    public EntityExtensionDto()
    {
        Properties = new Dictionary<string, ExtensionPropertyDto>();
        Configuration = new Dictionary<string, object>();
    }
}
