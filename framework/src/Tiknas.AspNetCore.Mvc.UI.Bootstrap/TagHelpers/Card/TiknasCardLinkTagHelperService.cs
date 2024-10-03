using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardLinkTagHelperService : TiknasTagHelperService<TiknasCardLinkTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("card-link");
        output.Attributes.RemoveAll("tiknas-card-link");
    }
}
