using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardTitleTagHelperService : TiknasTagHelperService<TiknasCardTitleTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = TagHelper.Heading.ToHtmlTag();
        output.Attributes.AddClass("card-title");
    }
}
