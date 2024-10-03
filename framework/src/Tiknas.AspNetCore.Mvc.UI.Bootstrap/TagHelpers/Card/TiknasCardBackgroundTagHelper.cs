using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

[HtmlTargetElement("tiknas-card", Attributes = "background")]
[HtmlTargetElement("tiknas-card-header", Attributes = "background")]
[HtmlTargetElement("tiknas-card-body", Attributes = "background")]
[HtmlTargetElement("tiknas-card-footer", Attributes = "background")]
public class TiknasCardBackgroundTagHelper : TiknasTagHelper<TiknasCardBackgroundTagHelper, TiknasCardBackgroundTagHelperService>
{
    public TiknasCardBackgroundType Background { get; set; } = TiknasCardBackgroundType.Default;

    public TiknasCardBackgroundTagHelper(TiknasCardBackgroundTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
