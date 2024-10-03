using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Figure;

public class TiknasFigureCaptionTagHelperService : TiknasTagHelperService<TiknasFigureCaptionTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "figcaption";
        output.Attributes.AddClass("figure-caption");
    }
}
