using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class TiknasNavItemTagHelperService : TiknasTagHelperService<TiknasNavItemTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "li";
        output.Attributes.AddClass("nav-item");

        SetDropdownClass(context, output);
    }

    protected virtual void SetDropdownClass(TagHelperContext context, TagHelperOutput output)
    {
        if (TagHelper.Dropdown ?? false)
        {
            output.Attributes.AddClass("dropdown");
        }
    }
}
