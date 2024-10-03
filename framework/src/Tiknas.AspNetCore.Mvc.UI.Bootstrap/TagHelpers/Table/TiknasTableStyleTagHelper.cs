using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

[HtmlTargetElement("tr")]
[HtmlTargetElement("td")]
public class TiknasTableStyleTagHelper : TiknasTagHelper<TiknasTableStyleTagHelper, TiknasTableStyleTagHelperService>
{
    public TiknasTableStyle TableStyle { get; set; } = TiknasTableStyle.Default;

    public TiknasTableStyleTagHelper(TiknasTableStyleTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
