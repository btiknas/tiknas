namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class TiknasDropdownItemTagHelper : TiknasTagHelper<TiknasDropdownItemTagHelper, TiknasDropdownItemTagHelperService>
{
    public bool? Active { get; set; }

    public bool? Disabled { get; set; }

    public TiknasDropdownItemTagHelper(TiknasDropdownItemTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
