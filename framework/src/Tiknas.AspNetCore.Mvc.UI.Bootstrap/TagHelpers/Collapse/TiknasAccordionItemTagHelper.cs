namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse;

public class TiknasAccordionItemTagHelper : TiknasTagHelper<TiknasAccordionItemTagHelper, TiknasAccordionItemTagHelperService>
{
    public string? Id { get; set; }

    public string Title { get; set; } = default!;

    public bool? Active { get; set; }

    public TiknasAccordionItemTagHelper(TiknasAccordionItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
