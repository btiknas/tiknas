using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab;

[HtmlTargetElement("tiknas-tab")]
public class TiknasTabTagHelper : TiknasTagHelper<TiknasTabTagHelper, TiknasTabTagHelperService>
{
    public string? Name { get; set; }

    public string Title { get; set; } = default!;

    public bool? Active { get; set; }

    public string? ParentDropdownName { get; set; }

    public TiknasTabTagHelper(TiknasTabTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
