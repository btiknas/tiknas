using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

[HtmlTargetElement("a", Attributes = "tiknas-button", TagStructure = TagStructure.NormalOrSelfClosing)]
[HtmlTargetElement("input", Attributes = "tiknas-button", TagStructure = TagStructure.WithoutEndTag)]
public class TiknasLinkButtonTagHelper : TiknasTagHelper<TiknasLinkButtonTagHelper, TiknasLinkButtonTagHelperService>, IButtonTagHelperBase
{
    [HtmlAttributeName("tiknas-button")]
    public TiknasButtonType ButtonType { get; set; }

    public TiknasButtonSize Size { get; set; } = TiknasButtonSize.Default;

    public string? Text { get; set; }

    public string? Icon { get; set; }

    public bool? Disabled { get; set; }

    public FontIconType IconType { get; } = FontIconType.FontAwesome;

    public TiknasLinkButtonTagHelper(TiknasLinkButtonTagHelperService service)
        : base(service)
    {

    }
}
