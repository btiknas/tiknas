using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Figure;

[HtmlTargetElement("tiknas-figcaption")]
public class TiknasFigureCaptionTagHelper : TiknasTagHelper<TiknasFigureCaptionTagHelper, TiknasFigureCaptionTagHelperService>
{
    public TiknasFigureCaptionTagHelper(TiknasFigureCaptionTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
