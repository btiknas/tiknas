using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Grid;

[HtmlTargetElement("tiknas-row")]
[HtmlTargetElement("tiknas-form-row")]
public class TiknasRowTagHelper : TiknasTagHelper<TiknasRowTagHelper, TiknasRowTagHelperService>
{
    public VerticalAlign VAlign { get; set; } = VerticalAlign.Default;

    public HorizontalAlign HAlign { get; set; } = HorizontalAlign.Default;

    public bool? Gutters { get; set; } = true;

    public TiknasRowTagHelper(TiknasRowTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
