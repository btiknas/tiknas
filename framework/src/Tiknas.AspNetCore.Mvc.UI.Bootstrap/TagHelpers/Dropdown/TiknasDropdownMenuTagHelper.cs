namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown;

public class TiknasDropdownMenuTagHelper : TiknasTagHelper<TiknasDropdownMenuTagHelper, TiknasDropdownMenuTagHelperService>
{
    public DropdownAlign Align { get; set; } = DropdownAlign.Start;

    public TiknasDropdownMenuTagHelper(TiknasDropdownMenuTagHelperService tagHelperService)
        : base(tagHelperService)
    {

    }
}
