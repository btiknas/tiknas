using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardFooterTagHelperService : TiknasTagHelperService<TiknasCardFooterTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("card-footer");
        output.TagName = "div";
    }
}
