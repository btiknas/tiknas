using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tiknas.AspNetCore.Mvc.UI.Widgets.Components.WidgetStyles;

namespace Tiknas.AspNetCore.Mvc.UI.Widgets.Components.WidgetScripts;

public class WidgetScriptsViewComponent : TiknasViewComponent
{
    protected IPageWidgetManager PageWidgetManager { get; }
    protected TiknasWidgetOptions Options { get; }

    public WidgetScriptsViewComponent(
        IPageWidgetManager pageWidgetManager,
        IOptions<TiknasWidgetOptions> options)
    {
        PageWidgetManager = pageWidgetManager;
        Options = options.Value;
    }

    public virtual IViewComponentResult Invoke()
    {
        return View(
            "~/Tiknas/AspNetCore/Mvc/UI/Widgets/Components/WidgetScripts/Default.cshtml",
            new WidgetResourcesViewModel
            {
                Widgets = PageWidgetManager.GetAll()
            }
        );
    }
}
