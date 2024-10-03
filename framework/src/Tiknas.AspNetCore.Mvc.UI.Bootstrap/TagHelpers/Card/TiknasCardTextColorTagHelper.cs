using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

[HtmlTargetElement("tiknas-card", Attributes = "text-color")]
[HtmlTargetElement("tiknas-card-header", Attributes = "text-color")]
[HtmlTargetElement("tiknas-card-body", Attributes = "text-color")]
[HtmlTargetElement("tiknas-card-footer", Attributes = "text-color")]
public class TiknasCardTextColorTagHelper : TiknasTagHelper<TiknasCardTextColorTagHelper, TiknasCardTextColorTagHelperService>
{
    public TiknasCardTextColorType TextColor { get; set; } = TiknasCardTextColorType.Default;

    public TiknasCardTextColorTagHelper(TiknasCardTextColorTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
