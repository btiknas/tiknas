using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert;

[HtmlTargetElement("h1", ParentTag = "tiknas-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("h2", ParentTag = "tiknas-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("h3", ParentTag = "tiknas-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("h4", ParentTag = "tiknas-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("h5", ParentTag = "tiknas-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("h6", ParentTag = "tiknas-alert", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TiknasAlertHeaderTagHelper : TiknasTagHelper<TiknasAlertHeaderTagHelper, TiknasAlertHeaderTagHelperService>
{
    public TiknasAlertHeaderTagHelper(TiknasAlertHeaderTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
