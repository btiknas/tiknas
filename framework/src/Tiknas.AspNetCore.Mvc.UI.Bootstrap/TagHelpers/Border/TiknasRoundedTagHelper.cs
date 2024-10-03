using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Border;

[HtmlTargetElement(Attributes = "tiknas-rounded")]
public class TiknasRoundedTagHelper : TiknasTagHelper<TiknasRoundedTagHelper, TiknasRoundedTagHelperService>
{
    public TiknasRoundedType TiknasRounded { get; set; } = TiknasRoundedType.Default;

    public TiknasRoundedTagHelper(TiknasRoundedTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
