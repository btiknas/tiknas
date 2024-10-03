using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Badge;

public class TiknasBadgeTagHelperService : TiknasTagHelperService<TiknasBadgeTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetBadgeClass(context, output);
        SetBadgeStyle(context, output);
    }

    protected virtual void SetBadgeStyle(TagHelperContext context, TagHelperOutput output)
    {
        var badgeType = GetBadgeType(context, output);

        if (badgeType != TiknasBadgeType.Default && badgeType != TiknasBadgeType._)
        {
            output.Attributes.AddClass("bg-" + badgeType.ToString().ToLowerInvariant());
        }
    }

    protected virtual void SetBadgeClass(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("badge");

        if (TagHelper.BadgePillType != TiknasBadgeType._)
        {
            output.Attributes.AddClass("rounded-pill");
        }
    }

    protected virtual TiknasBadgeType GetBadgeType(TagHelperContext context, TagHelperOutput output)
    {
        return TagHelper.BadgeType != TiknasBadgeType._ ? TagHelper.BadgeType : TagHelper.BadgePillType;
    }
}
