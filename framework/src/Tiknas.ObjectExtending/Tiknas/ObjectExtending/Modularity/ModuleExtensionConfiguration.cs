﻿using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tiknas.ObjectExtending.Modularity;

public class ModuleExtensionConfiguration
{
    [NotNull]
    public EntityExtensionConfigurationDictionary Entities { get; }

    [NotNull]
    public Dictionary<string, object> Configuration { get; }

    public ModuleExtensionConfiguration()
    {
        Entities = new EntityExtensionConfigurationDictionary();
        Configuration = new Dictionary<string, object>();
    }
}
