using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Badge;

[HtmlTargetElement("a", Attributes = "tiknas-badge")]
[HtmlTargetElement("span", Attributes = "tiknas-badge")]
[HtmlTargetElement("a", Attributes = "tiknas-badge-pill")]
[HtmlTargetElement("span", Attributes = "tiknas-badge-pill")]
public class TiknasBadgeTagHelper : TiknasTagHelper<TiknasBadgeTagHelper, TiknasBadgeTagHelperService>
{
    [HtmlAttributeName("tiknas-badge")]
    public TiknasBadgeType BadgeType { get; set; } = TiknasBadgeType._;

    [HtmlAttributeName("tiknas-badge-pill")]
    public TiknasBadgeType BadgePillType { get; set; } = TiknasBadgeType._;

    public TiknasBadgeTagHelper(TiknasBadgeTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
