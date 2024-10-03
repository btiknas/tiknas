namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse;

public class TiknasAccordionTagHelper : TiknasTagHelper<TiknasAccordionTagHelper, TiknasAccordionTagHelperService>
{
    public string? Id { get; set; }

    public TiknasAccordionTagHelper(TiknasAccordionTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
