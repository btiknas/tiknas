﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Tiknas.AspNetCore.Components.Web.Theming.PageToolbars;

public class PageToolbarItem
{
    [NotNull]
    public Type ComponentType { get; }

    public Dictionary<string, object?>? Arguments { get; set; }

    public int Order { get; set; }

    public PageToolbarItem(
        [NotNull] Type componentType,
        Dictionary<string, object?>? arguments = null,
        int order = 0)
    {
        ComponentType = Check.NotNull(componentType, nameof(componentType));
        Arguments = arguments;
        Order = order;
    }
}
