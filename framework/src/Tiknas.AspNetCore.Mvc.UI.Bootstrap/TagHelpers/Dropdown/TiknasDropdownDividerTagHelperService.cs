using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class TiknasDropdownDividerTagHelperService : TiknasTagHelperService<TiknasDropdownDividerTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.AddClass("dropdown-divider");
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}
