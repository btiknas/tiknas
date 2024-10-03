using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Blockquote;

[HtmlTargetElement("p", ParentTag = "blockquote")]
public class TiknasBlockquoteParagraphTagHelper : TiknasTagHelper<TiknasBlockquoteParagraphTagHelper, TiknasBlockquoteParagraphTagHelperService>
{
    public TiknasBlockquoteParagraphTagHelper(TiknasBlockquoteParagraphTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
