namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardBodyTagHelper : TiknasTagHelper<TiknasCardBodyTagHelper, TiknasCardBodyTagHelperService>
{
    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public TiknasCardBodyTagHelper(TiknasCardBodyTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
