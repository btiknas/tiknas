namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

public class TiknasModalTagHelper : TiknasTagHelper<TiknasModalTagHelper, TiknasModalTagHelperService>
{
    public TiknasModalSize Size { get; set; } = TiknasModalSize.Default;

    public bool? Centered { get; set; } = false;

    public bool? Scrollable { get; set; } = false;

    public bool? Static { get; set; } = false;

    public TiknasModalTagHelper(TiknasModalTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
