using Microsoft.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.UI.Packages.Prismjs;
using Tiknas.AspNetCore.Mvc.UI.Widgets;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.FormElementsDemo;

[Widget(
    StyleTypes = new[] { typeof(PrismjsStyleBundleContributor) },
    ScriptTypes = new[] { typeof(PrismjsScriptBundleContributor) }
)]
public class FormElementsDemoViewComponent : TiknasViewComponent
{
    public const string ViewPath = "/Views/Components/Themes/Shared/Demos/FormElementsDemo/Default.cshtml";

    public virtual IViewComponentResult Invoke()
    {
        var model = new FormElementsDemoModel();

        return View(ViewPath, model);
    }
}
