namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

public static class TiknasButtonSizeExtensions
{
    public static string ToClassName(this TiknasButtonSize size)
    {
        switch (size)
        {
            case TiknasButtonSize.Small:
            case TiknasButtonSize.Block_Small:
                return "btn-sm";
            case TiknasButtonSize.Medium:
            case TiknasButtonSize.Block_Medium:
                return "btn-md";
            case TiknasButtonSize.Large:
            case TiknasButtonSize.Block_Large:
                return "btn-lg";
            case TiknasButtonSize.Block:
            case TiknasButtonSize.Default:
                return "";
            default:
                throw new TiknasException($"Unknown {nameof(TiknasButtonSize)}: {size}");
        }
    }
}
