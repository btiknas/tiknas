﻿using System;
using JetBrains.Annotations;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public class PageToolbarItem
{
    [NotNull]
    public Type ComponentType { get; }

    public object? Arguments { get; set; }

    public int Order { get; set; }

    public PageToolbarItem(
        [NotNull] Type componentType,
        object? arguments = null,
        int order = 0)
    {
        ComponentType = Check.NotNull(componentType, nameof(componentType));
        Arguments = arguments;
        Order = order;
    }
}
