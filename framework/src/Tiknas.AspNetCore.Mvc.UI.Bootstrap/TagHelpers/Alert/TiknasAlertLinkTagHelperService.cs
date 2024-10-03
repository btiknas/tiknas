using Microsoft.AspNetCore.Razor.TagHelpers;
using Tiknas.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Alert;

public class TiknasAlertLinkTagHelperService : TiknasTagHelperService<TiknasAlertLinkTagHelper>
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.AddClass("alert-link");
        output.Attributes.RemoveAll("tiknas-alert-link");
    }
}
