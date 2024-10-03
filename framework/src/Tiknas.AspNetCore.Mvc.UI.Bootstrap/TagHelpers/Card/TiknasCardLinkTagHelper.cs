using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

[HtmlTargetElement("a", Attributes = "tiknas-card-link")]
public class TiknasCardLinkTagHelper : TiknasTagHelper<TiknasCardLinkTagHelper, TiknasCardLinkTagHelperService>
{
    public TiknasCardLinkTagHelper(TiknasCardLinkTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
