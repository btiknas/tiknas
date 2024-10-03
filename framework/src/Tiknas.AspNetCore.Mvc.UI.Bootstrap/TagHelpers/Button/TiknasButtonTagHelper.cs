using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

[HtmlTargetElement("tiknas-button", TagStructure = TagStructure.NormalOrSelfClosing)]
public class TiknasButtonTagHelper : TiknasTagHelper<TiknasButtonTagHelper, TiknasButtonTagHelperService>, IButtonTagHelperBase
{
    public TiknasButtonType ButtonType { get; set; } = TiknasButtonType.Default;

    public TiknasButtonSize Size { get; set; } = TiknasButtonSize.Default;

    public string? BusyText { get; set; }

    public string? Text { get; set; }

    public string? Icon { get; set; }

    public bool? Disabled { get; set; }

    public FontIconType IconType { get; set; } = FontIconType.FontAwesome;

    public bool BusyTextIsHtml { get; set; }

    public TiknasButtonTagHelper(TiknasButtonTagHelperService service)
        : base(service)
    {

    }
}

