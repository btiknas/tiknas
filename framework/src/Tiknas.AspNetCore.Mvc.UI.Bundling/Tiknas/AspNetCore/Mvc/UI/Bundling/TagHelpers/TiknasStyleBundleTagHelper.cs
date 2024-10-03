using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

[HtmlTargetElement("tiknas-style-bundle", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TiknasStyleBundleTagHelper : TiknasBundleTagHelper<TiknasStyleBundleTagHelper, TiknasStyleBundleTagHelperService>, IBundleTagHelper
{
    [HtmlAttributeName("preload")]
    public bool Preload { get; set; }

    public TiknasStyleBundleTagHelper(TiknasStyleBundleTagHelperService service)
        : base(service)
    {
    }
}
