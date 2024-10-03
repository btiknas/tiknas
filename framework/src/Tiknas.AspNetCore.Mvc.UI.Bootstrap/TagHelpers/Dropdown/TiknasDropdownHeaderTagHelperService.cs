using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class TiknasDropdownHeaderTagHelperService : TiknasTagHelperService<TiknasDropdownHeaderTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "h6";
        output.Attributes.AddClass("dropdown-header");
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}
