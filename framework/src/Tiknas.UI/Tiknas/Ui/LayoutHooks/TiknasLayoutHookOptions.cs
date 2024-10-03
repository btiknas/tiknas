using System;
using System.Collections.Generic;

namespace Tiknas.Ui.LayoutHooks;

public class TiknasLayoutHookOptions
{
    public IDictionary<string, List<LayoutHookInfo>> Hooks { get; }

    public TiknasLayoutHookOptions()
    {
        Hooks = new Dictionary<string, List<LayoutHookInfo>>();
    }

    public TiknasLayoutHookOptions Add(string name, Type componentType, string? layout = null)
    {
        Hooks
            .GetOrAdd(name, () => new List<LayoutHookInfo>())
            .Add(new LayoutHookInfo(componentType, layout));

        return this;
    }
}
