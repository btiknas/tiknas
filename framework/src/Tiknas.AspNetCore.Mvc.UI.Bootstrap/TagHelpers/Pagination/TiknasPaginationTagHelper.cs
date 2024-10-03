using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination;

[HtmlTargetElement("tiknas-paginator")]
public class TiknasPaginationTagHelper : TiknasTagHelper<TiknasPaginationTagHelper, TiknasPaginationTagHelperService>
{
    public PagerModel Model { get; set; } = default!;

    public bool? ShowInfo { get; set; }

    public TiknasPaginationTagHelper(TiknasPaginationTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
