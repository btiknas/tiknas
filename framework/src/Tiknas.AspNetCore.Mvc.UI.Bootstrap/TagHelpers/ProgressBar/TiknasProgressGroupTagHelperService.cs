using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ProgressBar;

public class TiknasProgressGroupTagHelperService : TiknasTagHelperService<TiknasProgressGroupTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("progress");
        output.TagName = "div";
    }
}
