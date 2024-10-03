using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Tiknas.AspNetCore.Mvc.UI.Widgets.Components.WidgetStyles;

public class WidgetStylesViewComponent : TiknasViewComponent
{
    protected IPageWidgetManager PageWidgetManager { get; }
    protected TiknasWidgetOptions Options { get; }

    public WidgetStylesViewComponent(
        IPageWidgetManager pageWidgetManager,
        IOptions<TiknasWidgetOptions> options)
    {
        PageWidgetManager = pageWidgetManager;
        Options = options.Value;
    }

    public virtual IViewComponentResult Invoke()
    {
        return View(
            "~/Tiknas/AspNetCore/Mvc/UI/Widgets/Components/WidgetStyles/Default.cshtml",
            new WidgetResourcesViewModel
            {
                Widgets = PageWidgetManager.GetAll()
            }
        );
    }
}
