namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public interface IButtonTagHelperBase
{
    TiknasButtonType ButtonType { get; }

    TiknasButtonSize Size { get; }

    string? Text { get; }

    string? Icon { get; }

    bool? Disabled { get; }

    FontIconType IconType { get; }
}
