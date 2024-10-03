using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class TiknasNavbarBrandTagHelperService : TiknasTagHelperService<TiknasNavbarBrandTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll("tiknas-navbar-brand");
        output.Attributes.AddClass("navbar-brand");
    }
}
