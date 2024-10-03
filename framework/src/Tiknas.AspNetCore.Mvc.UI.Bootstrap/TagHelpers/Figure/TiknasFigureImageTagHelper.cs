using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Figure;

[HtmlTargetElement("tiknas-image", ParentTag = "tiknas-figure")]
public class TiknasFigureImageTagHelper : TiknasTagHelper<TiknasFigureImageTagHelper, TiknasFigureImageTagHelperService>
{
    public TiknasFigureImageTagHelper(TiknasFigureImageTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
