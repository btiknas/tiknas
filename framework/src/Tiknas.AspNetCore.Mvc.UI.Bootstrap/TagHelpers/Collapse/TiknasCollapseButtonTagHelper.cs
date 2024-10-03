using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse;

[HtmlTargetElement("tiknas-button", Attributes = "tiknas-collapse-id")]
[HtmlTargetElement("a", Attributes = "tiknas-collapse-id")]
public class TiknasCollapseButtonTagHelper : TiknasTagHelper<TiknasCollapseButtonTagHelper, TiknasCollapseButtonTagHelperService>
{
    [HtmlAttributeName("tiknas-collapse-id")]
    public string BodyId { get; set; } = default!;

    public TiknasCollapseButtonTagHelper(TiknasCollapseButtonTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
