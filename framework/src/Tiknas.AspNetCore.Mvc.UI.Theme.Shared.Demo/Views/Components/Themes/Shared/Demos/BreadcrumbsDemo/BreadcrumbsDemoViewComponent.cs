using Microsoft.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.UI.Packages.Prismjs;
using Tiknas.AspNetCore.Mvc.UI.Widgets;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.BreadcrumbsDemo;

[Widget(
    StyleTypes = new[] { typeof(PrismjsStyleBundleContributor) },
    ScriptTypes = new[] { typeof(PrismjsScriptBundleContributor) }
)]
public class BreadcrumbsDemoViewComponent : TiknasViewComponent
{
    public const string ViewPath = "/Views/Components/Themes/Shared/Demos/BreadcrumbsDemo/Default.cshtml";

    public virtual IViewComponentResult Invoke()
    {
        return View(ViewPath);
    }
}
