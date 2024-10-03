using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Utils;

[HtmlTargetElement(Attributes = "tiknas-if")]
public class TiknasIfTagHelper : TiknasTagHelper
{
    [HtmlAttributeName("tiknas-if")]
    public bool Condition { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!Condition)
        {
            output.SuppressOutput();
        }
    }
}
