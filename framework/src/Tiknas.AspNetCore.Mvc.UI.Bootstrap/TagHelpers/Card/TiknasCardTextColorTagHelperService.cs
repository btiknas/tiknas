using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardTextColorTagHelperService : TiknasTagHelperService<TiknasCardTextColorTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetTextColor(context, output);
    }

    protected virtual void SetTextColor(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.TextColor == TiknasCardTextColorType.Default)
        {
            return;
        }

        output.Attributes.AddClass("text-" + TagHelper.TextColor.ToString().ToLowerInvariant());
    }
}
