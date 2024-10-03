using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert;

[HtmlTargetElement("a", Attributes = "tiknas-alert-link", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TiknasAlertLinkTagHelper : TiknasTagHelper<TiknasAlertLinkTagHelper, TiknasAlertLinkTagHelperService>
{
    public TiknasAlertLinkTagHelper(TiknasAlertLinkTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
