using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

[HtmlTargetElement("script")]
[HtmlTargetElement("body")]
public class ScriptNonceTagHelper : TiknasTagHelper
{
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = default!;
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext.HttpContext.Items.TryGetValue(TiknasAspNetCoreConsts.ScriptNonceKey, out var nonce) && nonce is string nonceString && !string.IsNullOrEmpty(nonceString))
        {
            output.Attributes.Add("nonce", nonceString);
        }
    }
}