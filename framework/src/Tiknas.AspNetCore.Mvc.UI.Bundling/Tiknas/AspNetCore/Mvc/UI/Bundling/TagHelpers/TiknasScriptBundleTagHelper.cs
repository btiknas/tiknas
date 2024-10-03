using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

[HtmlTargetElement("tiknas-script-bundle", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TiknasScriptBundleTagHelper : TiknasBundleTagHelper<TiknasScriptBundleTagHelper, TiknasScriptBundleTagHelperService>, IBundleTagHelper
{
    [HtmlAttributeName("defer")]
    public bool Defer { get; set; }

    public TiknasScriptBundleTagHelper(TiknasScriptBundleTagHelperService service)
        : base(service)
    {

    }
}
