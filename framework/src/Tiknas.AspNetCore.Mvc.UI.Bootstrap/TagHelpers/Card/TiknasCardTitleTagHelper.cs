namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardTitleTagHelper : TiknasTagHelper<TiknasCardTitleTagHelper, TiknasCardTitleTagHelperService>
{
    public static HtmlHeadingType DefaultHeading { get; set; } = HtmlHeadingType.H5;

    public HtmlHeadingType Heading { get; set; } = DefaultHeading;

    public TiknasCardTitleTagHelper(TiknasCardTitleTagHelperService tagHelperService)
        : base(tagHelperService)
    {
    }
}
