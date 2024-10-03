using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class TiknasDropdownItemTextTagHelperService : TiknasTagHelperService<TiknasDropdownItemTextTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("dropdown-item-text");
        output.TagName = "span";
    }
}
