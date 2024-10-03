using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Grid;

public class TiknasColumnBreakerTagHelperService : TiknasTagHelperService<TiknasColumnBreakerTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("w-100");
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}
