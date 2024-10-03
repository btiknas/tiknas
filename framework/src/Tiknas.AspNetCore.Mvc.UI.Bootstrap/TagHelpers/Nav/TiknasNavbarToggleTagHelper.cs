namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class TiknasNavbarToggleTagHelper : TiknasTagHelper<TiknasNavbarToggleTagHelper, TiknasNavbarToggleTagHelperService>
{
    public string? Id { get; set; }

    public TiknasNavbarToggleTagHelper(TiknasNavbarToggleTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
