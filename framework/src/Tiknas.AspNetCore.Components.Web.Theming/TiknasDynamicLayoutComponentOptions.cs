using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tiknas.AspNetCore.Components.Web.Theming;

public class TiknasDynamicLayoutComponentOptions
{
    /// <summary>
    /// Used to define components that renders in the layout
    /// </summary>
    [NotNull]
    public Dictionary<Type, IDictionary<string,object>?> Components { get; set; }

    public TiknasDynamicLayoutComponentOptions()
    {
        Components = new Dictionary<Type, IDictionary<string, object>?>();
    }
}