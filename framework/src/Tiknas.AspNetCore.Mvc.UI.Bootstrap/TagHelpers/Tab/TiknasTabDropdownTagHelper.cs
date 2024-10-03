using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab;

[HtmlTargetElement("tiknas-tab-dropdown")]
public class TiknasTabDropdownTagHelper : TiknasTagHelper<TiknasTabDropdownTagHelper, TiknasTabDropdownTagHelperService>
{
    public string? Name { get; set; }

    public string Title { get; set; } = default!;

    public TiknasTabDropdownTagHelper(TiknasTabDropdownTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
