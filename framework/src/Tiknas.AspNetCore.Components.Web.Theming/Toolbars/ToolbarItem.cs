﻿using System;
using JetBrains.Annotations;

namespace Tiknas.AspNetCore.Components.Web.Theming.Toolbars;

public class ToolbarItem
{
    public Type ComponentType {
        get => _componentType;
        set => _componentType = Check.NotNull(value, nameof(value));
    }
    private Type _componentType = default!;

    public int Order { get; set; }

    public ToolbarItem([NotNull] Type componentType, int order = 0)
    {
        Order = order;
        ComponentType = Check.NotNull(componentType, nameof(componentType));
    }
}
