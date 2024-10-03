namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Carousel;

public class TiknasCarouselTagHelper : TiknasTagHelper<TiknasCarouselTagHelper, TiknasCarouselTagHelperService>
{
    public string? Id { get; set; }

    public bool? Crossfade { get; set; }

    public bool? Controls { get; set; }

    public bool? Indicators { get; set; }

    public TiknasCarouselTagHelper(TiknasCarouselTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
