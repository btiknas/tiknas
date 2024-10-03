using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table;

[HtmlTargetElement("th")]
public class TiknasTableHeadScopeTagHelper : TiknasTagHelper<TiknasTableHeadScopeTagHelper, TiknasTableHeadScopeTagHelperService>
{
    public TiknasThScope Scope { get; set; } = TiknasThScope.Default;

    public TiknasTableHeadScopeTagHelper(TiknasTableHeadScopeTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
