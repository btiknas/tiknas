using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tooltip;

[HtmlTargetElement("button", Attributes = "tiknas-tooltip")]
[HtmlTargetElement("button", Attributes = "tiknas-tooltip-right")]
[HtmlTargetElement("button", Attributes = "tiknas-tooltip-left")]
[HtmlTargetElement("button", Attributes = "tiknas-tooltip-top")]
[HtmlTargetElement("button", Attributes = "tiknas-tooltip-bottom")]
[HtmlTargetElement("tiknas-button", Attributes = "tiknas-tooltip")]
[HtmlTargetElement("tiknas-button", Attributes = "tiknas-tooltip-right")]
[HtmlTargetElement("tiknas-button", Attributes = "tiknas-tooltip-left")]
[HtmlTargetElement("tiknas-button", Attributes = "tiknas-tooltip-top")]
[HtmlTargetElement("tiknas-button", Attributes = "tiknas-tooltip-bottom")]
public class TiknasTooltipTagHelper : TiknasTagHelper<TiknasTooltipTagHelper, TiknasTooltipTagHelperService>
{
    public string? TiknasTooltip { get; set; }

    public string? TiknasTooltipRight { get; set; }

    public string? TiknasTooltipLeft { get; set; }

    public string? TiknasTooltipTop { get; set; }

    public string? TiknasTooltipBottom { get; set; }

    public string? Title { get; set; }

    public TiknasTooltipTagHelper(TiknasTooltipTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
