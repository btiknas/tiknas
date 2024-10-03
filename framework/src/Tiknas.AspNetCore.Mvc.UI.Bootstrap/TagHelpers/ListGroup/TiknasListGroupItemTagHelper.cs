namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.ListGroup;

public class TiknasListGroupItemTagHelper : TiknasTagHelper<TiknasListGroupItemTagHelper, TiknasListGroupItemTagHelperService>
{
    public bool? Active { get; set; }

    public bool? Disabled { get; set; }

    public string? Href { get; set; }

    public TiknasListItemTagType TagType { get; set; } = TiknasListItemTagType.Default;

    public TiknasListItemType Type { get; set; } = TiknasListItemType.Default;

    public TiknasListGroupItemTagHelper(TiknasListGroupItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
