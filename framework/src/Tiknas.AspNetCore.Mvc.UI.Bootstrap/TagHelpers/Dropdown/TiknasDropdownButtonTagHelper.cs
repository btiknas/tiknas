using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class TiknasDropdownButtonTagHelper : TiknasTagHelper<TiknasDropdownButtonTagHelper, TiknasDropdownButtonTagHelperService>
{
    public string? Text { get; set; }

    public TiknasButtonSize Size { get; set; } = TiknasButtonSize.Default;

    public DropdownStyle DropdownStyle { get; set; } = DropdownStyle.Single;

    public TiknasButtonType ButtonType { get; set; } = TiknasButtonType.Default;

    public string? Icon { get; set; }

    public FontIconType IconType { get; set; } = FontIconType.FontAwesome;

    public bool? Link { get; set; }

    public bool? NavLink { get; set; }

    public TiknasDropdownButtonTagHelper(TiknasDropdownButtonTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
