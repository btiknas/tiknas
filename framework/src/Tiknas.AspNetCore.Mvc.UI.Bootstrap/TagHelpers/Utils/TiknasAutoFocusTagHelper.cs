using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Utils;

[HtmlTargetElement(Attributes = "tiknas-auto-focus")]
public class TiknasAutoFocusTagHelper : TiknasTagHelper
{
    [HtmlAttributeName("tiknas-auto-focus")]
    public bool AutoFocus { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (AutoFocus)
        {
            output.Attributes.Add("data-auto-focus", "true");
        }
    }
}
