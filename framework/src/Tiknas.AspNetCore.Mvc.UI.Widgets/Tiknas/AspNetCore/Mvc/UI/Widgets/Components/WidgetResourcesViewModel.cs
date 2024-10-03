using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.UI.Widgets.Components;

public class WidgetResourcesViewModel
{
    public IReadOnlyList<WidgetDefinition> Widgets { get; set; } = default!;

}
