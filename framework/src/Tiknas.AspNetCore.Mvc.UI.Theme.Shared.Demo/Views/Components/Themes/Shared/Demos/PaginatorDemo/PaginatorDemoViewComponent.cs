using Microsoft.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;
using Tiknas.AspNetCore.Mvc.UI.Packages.Prismjs;
using Tiknas.AspNetCore.Mvc.UI.Widgets;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Demo.Views.Components.Themes.Shared.Demos.PaginatorDemo;

[Widget(
    StyleTypes = new[] { typeof(PrismjsStyleBundleContributor) },
    ScriptTypes = new[] { typeof(PrismjsScriptBundleContributor) }
)]
public class PaginatorDemoViewComponent : TiknasViewComponent
{
    public const string ViewPath = "/Views/Components/Themes/Shared/Demos/PaginatorDemo/Default.cshtml";

    public PagerModel PagerModel { get; set; } = default!;

    public IViewComponentResult Invoke(PagerModel pagerModel)
    {
        PagerModel = pagerModel;

        return View(ViewPath);
    }
}
