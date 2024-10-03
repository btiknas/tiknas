using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

[HtmlTargetElement("img", Attributes = "tiknas-card-image", TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement("tiknas-image", Attributes = "tiknas-card-image", TagStructure = TagStructure.WithoutEndTag)]
public class TiknasCardImageTagHelper : TiknasTagHelper<TiknasCardImageTagHelper, TiknasCardImageTagHelperService>
{
    [HtmlAttributeName("tiknas-card-image")]
    public TiknasCardImagePosition Position { get; set; } = TiknasCardImagePosition.Top;

    public TiknasCardImageTagHelper(TiknasCardImageTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
