namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse;

public class TiknasCollapseBodyTagHelper : TiknasTagHelper<TiknasCollapseBodyTagHelper, TiknasCollapseBodyTagHelperService>
{
    public string? Id { get; set; }

    public bool? Multi { get; set; }

    public bool? Show { get; set; }

    public TiknasCollapseBodyTagHelper(TiknasCollapseBodyTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
