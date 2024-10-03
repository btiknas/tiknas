namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav;

public class TiknasNavTagHelper : TiknasTagHelper<TiknasNavTagHelper, TiknasNavTagHelperService>
{
    public TiknasNavAlign Align { get; set; } = TiknasNavAlign.Default;

    public NavStyle NavStyle { get; set; } = NavStyle.Default;

    public TiknasNavTagHelper(TiknasNavTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
