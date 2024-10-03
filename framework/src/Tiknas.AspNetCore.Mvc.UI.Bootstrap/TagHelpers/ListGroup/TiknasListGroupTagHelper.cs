namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ListGroup;

public class TiknasListGroupTagHelper : TiknasTagHelper<TiknasListGroupTagHelper, TiknasListGroupTagHelperService>
{
    public bool? Flush { get; set; }

    public TiknasListGroupTagHelper(TiknasListGroupTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
