using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ListGroup;

public class TiknasListGroupTagHelperService : TiknasTagHelperService<TiknasListGroupTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";
        output.Attributes.AddClass("list-group");

        if (TagHelper.Flush ?? false)
        {
            output.Attributes.AddClass("list-group-flush");
        }
    }
}
