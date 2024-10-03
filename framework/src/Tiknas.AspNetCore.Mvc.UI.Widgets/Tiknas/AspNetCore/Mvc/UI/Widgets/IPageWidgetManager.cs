using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.UI.Widgets;

public interface IPageWidgetManager
{
    bool TryAdd(WidgetDefinition widget);

    IReadOnlyList<WidgetDefinition> GetAll();
}
