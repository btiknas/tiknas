namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class TiknasDropdownTagHelper : TiknasTagHelper<TiknasDropdownTagHelper, TiknasDropdownTagHelperService>
{
    public DropdownDirection Direction { get; set; } = DropdownDirection.Down;

    public TiknasDropdownTagHelper(TiknasDropdownTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
