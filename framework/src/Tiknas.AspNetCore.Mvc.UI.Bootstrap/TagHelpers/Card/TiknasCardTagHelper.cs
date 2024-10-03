namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardTagHelper : TiknasTagHelper<TiknasCardTagHelper, TiknasCardTagHelperService>
{
    public TiknasCardBorderColorType Border { get; set; } = TiknasCardBorderColorType.Default;

    public bool AddMarginBottomClass  { get; set; } = true;

    public TiknasCardTagHelper(TiknasCardTagHelperService tagHelperService)
        : base(tagHelperService)
    {
    }
}
