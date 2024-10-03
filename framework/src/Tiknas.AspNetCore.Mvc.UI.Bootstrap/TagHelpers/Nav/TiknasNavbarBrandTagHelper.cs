using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

[HtmlTargetElement(Attributes = "tiknas-navbar-brand")]
public class TiknasNavbarBrandTagHelper : TiknasTagHelper<TiknasNavbarBrandTagHelper, TiknasNavbarBrandTagHelperService>
{

    public TiknasNavbarBrandTagHelper(TiknasNavbarBrandTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
