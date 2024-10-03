using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

[HtmlTargetElement("thead")]
public class TiknasTableHeaderTagHelper : TiknasTagHelper<TiknasTableHeaderTagHelper, TiknasTableHeaderTagHelperService>
{
    public TiknasTableHeaderTheme Theme { get; set; } = TiknasTableHeaderTheme.Default;

    public TiknasTableHeaderTagHelper(TiknasTableHeaderTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
