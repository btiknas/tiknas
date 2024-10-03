using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

[HtmlTargetElement("tiknas-script", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TiknasScriptTagHelper : TiknasBundleItemTagHelper<TiknasScriptTagHelper, TiknasScriptTagHelperService>, IBundleItemTagHelper
{
    [HtmlAttributeName("defer")]
    public bool Defer { get; set; }

    public TiknasScriptTagHelper(TiknasScriptTagHelperService service)
        : base(service)
    {

    }

    protected override string GetFileExtension()
    {
        return "js";
    }
}
