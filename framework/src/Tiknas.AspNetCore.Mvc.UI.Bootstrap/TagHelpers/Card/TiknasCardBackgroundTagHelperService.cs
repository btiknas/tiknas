using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardBackgroundTagHelperService : TiknasTagHelperService<TiknasCardBackgroundTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        SetBackground(context, output);
    }

    protected virtual void SetBackground(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Background == TiknasCardBackgroundType.Default)
        {
            return;
        }

        output.Attributes.AddClass("bg-" + TagHelper.Background.ToString().ToLowerInvariant());
    }
}
