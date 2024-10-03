using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

[HtmlTargetElement("span", Attributes = "tiknas-navbar-text")]
public class TiknasNavbarTextTagHelper : TiknasTagHelper<TiknasNavbarTextTagHelper, TiknasNavbarTextTagHelperService>
{
    public TiknasNavbarTextTagHelper(TiknasNavbarTextTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
