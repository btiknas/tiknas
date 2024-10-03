using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardTagHelperService : TiknasTagHelperService<TiknasCardTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("card");
        if (TagHelper.AddMarginBottomClass)
        {
            output.Attributes.AddClass("mb-3");
        }

        SetBorder(context, output);
    }
    protected virtual void SetBorder(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Border == TiknasCardBorderColorType.Default)
        {
            return;
        }

        output.Attributes.AddClass("border-" + TagHelper.Border.ToString().ToLowerInvariant());
    }
}
