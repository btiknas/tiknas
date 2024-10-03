using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Popover;

[HtmlTargetElement("button", Attributes = "tiknas-popover")]
[HtmlTargetElement("button", Attributes = "tiknas-popover-right")]
[HtmlTargetElement("button", Attributes = "tiknas-popover-left")]
[HtmlTargetElement("button", Attributes = "tiknas-popover-top")]
[HtmlTargetElement("button", Attributes = "tiknas-popover-bottom")]
[HtmlTargetElement("tiknas-button", Attributes = "tiknas-popover")]
[HtmlTargetElement("tiknas-button", Attributes = "tiknas-popover-right")]
[HtmlTargetElement("tiknas-button", Attributes = "tiknas-popover-left")]
[HtmlTargetElement("tiknas-button", Attributes = "tiknas-popover-top")]
[HtmlTargetElement("tiknas-button", Attributes = "tiknas-popover-bottom")]
public class TiknasPopoverTagHelper : TiknasTagHelper<TiknasPopoverTagHelper, TiknasPopoverTagHelperService>
{
    public bool? Disabled { get; set; }

    public bool? Dismissible { get; set; }

    public bool? Hoverable { get; set; }

    public string? TiknasPopover { get; set; }

    public string? TiknasPopoverRight { get; set; }

    public string? TiknasPopoverLeft { get; set; }

    public string? TiknasPopoverTop { get; set; }

    public string? TiknasPopoverBottom { get; set; }

    public TiknasPopoverTagHelper(TiknasPopoverTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
