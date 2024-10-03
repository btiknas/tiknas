using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab;

[HtmlTargetElement("tiknas-tab-link", TagStructure = TagStructure.WithoutEndTag)]
public class TiknasTabLinkTagHelper : TiknasTagHelper<TiknasTabLinkTagHelper, TiknasTabLinkTagHelperService>
{
    public string? Name { get; set; }

    public string Title { get; set; } = default!;

    public string? ParentDropdownName { get; set; }

    public string Href { get; set; } = default!;

    public TiknasTabLinkTagHelper(TiknasTabLinkTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
