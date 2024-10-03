namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class TiknasNavBarTagHelper : TiknasTagHelper<TiknasNavBarTagHelper, TiknasNavBarTagHelperService>
{
    public TiknasNavbarSize Size { get; set; } = TiknasNavbarSize.Default;

    public TiknasNavbarStyle NavbarStyle { get; set; } = TiknasNavbarStyle.Default;

    public TiknasNavBarTagHelper(TiknasNavBarTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
