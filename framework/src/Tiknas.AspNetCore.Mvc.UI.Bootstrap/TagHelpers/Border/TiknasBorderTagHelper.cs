using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Border;

[HtmlTargetElement(Attributes = "tiknas-border")]
public class TiknasBorderTagHelper : TiknasTagHelper<TiknasBorderTagHelper, TiknasBorderTagHelperService>
{
    public TiknasBorderType TiknasBorder { get; set; } = TiknasBorderType.Default;

    public TiknasBorderTagHelper(TiknasBorderTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
