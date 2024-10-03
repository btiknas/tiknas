namespace Tiknas.AspNetCore.Mvc.UI.Widgets;

public class TiknasWidgetOptions
{
    public WidgetDefinitionCollection Widgets { get; }

    public TiknasWidgetOptions()
    {
        Widgets = new WidgetDefinitionCollection();
    }
}
