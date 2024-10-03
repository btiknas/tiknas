namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Breadcrumb;

public class TiknasBreadcrumbItemTagHelper : TiknasTagHelper<TiknasBreadcrumbItemTagHelper, TiknasBreadcrumbItemTagHelperService>
{
    public string? Href { get; set; }

    public string Title { get; set; } = default!;

    public bool Active { get; set; }

    public TiknasBreadcrumbItemTagHelper(TiknasBreadcrumbItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
