namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Carousel;

public class TiknasCarouselItemTagHelper : TiknasTagHelper<TiknasCarouselItemTagHelper, TiknasCarouselItemTagHelperService>
{
    public bool? Active { get; set; }

    public string Src { get; set; } = default!;

    public string Alt { get; set; } = default!;

    public string? CaptionTitle { get; set; }

    public string? Caption { get; set; }

    public TiknasCarouselItemTagHelper(TiknasCarouselItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
