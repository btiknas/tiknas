namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class TiknasNavItemTagHelper : TiknasTagHelper<TiknasNavItemTagHelper, TiknasNavItemTagHelperService>
{
    public bool? Dropdown { get; set; }

    public TiknasNavItemTagHelper(TiknasNavItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
