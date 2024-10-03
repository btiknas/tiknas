using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

[HtmlTargetElement("tiknas-style", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TiknasStyleTagHelper : TiknasBundleItemTagHelper<TiknasStyleTagHelper, TiknasStyleTagHelperService>, IBundleItemTagHelper
{
    [HtmlAttributeName("preload")]
    public bool Preload { get; set; }

    public TiknasStyleTagHelper(TiknasStyleTagHelperService service)
        : base(service)
    {

    }

    protected override string GetFileExtension()
    {
        return "css";
    }
}
