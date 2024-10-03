namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public class TiknasCardSubtitleTagHelper : TiknasTagHelper<TiknasCardSubtitleTagHelper, TiknasCardSubtitleTagHelperService>
{
    public static HtmlHeadingType DefaultHeading { get; set; } = HtmlHeadingType.H6;

    public HtmlHeadingType Heading { get; set; } = DefaultHeading;

    public TiknasCardSubtitleTagHelper(TiknasCardSubtitleTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
