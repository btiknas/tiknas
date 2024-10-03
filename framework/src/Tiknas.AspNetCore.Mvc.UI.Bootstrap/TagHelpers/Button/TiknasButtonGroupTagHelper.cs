namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public class TiknasButtonGroupTagHelper : TiknasTagHelper<TiknasButtonGroupTagHelper, TiknasButtonGroupTagHelperService>
{
    public TiknasButtonGroupDirection Direction { get; set; } = TiknasButtonGroupDirection.Horizontal;

    public TiknasButtonGroupSize Size { get; set; } = TiknasButtonGroupSize.Default;

    public TiknasButtonGroupTagHelper(TiknasButtonGroupTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
