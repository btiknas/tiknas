using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

[HtmlTargetElement(Attributes = "tiknas-nav-link")]
public class TiknasNavLinkTagHelper : TiknasTagHelper<TiknasNavLinkTagHelper, TiknasNavLinkTagHelperService>
{
    public bool? Active { get; set; }

    public bool? Disabled { get; set; }

    public TiknasNavLinkTagHelper(TiknasNavLinkTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
