using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Blockquote;

[HtmlTargetElement("footer", ParentTag = "blockquote")]
public class TiknasBlockquoteFooterTagHelper : TiknasTagHelper<TiknasBlockquoteFooterTagHelper, TiknasBlockquoteFooterTagHelperService>
{
    public TiknasBlockquoteFooterTagHelper(TiknasBlockquoteFooterTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
