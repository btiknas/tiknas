namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

public static class TiknasModalSizeExtensions
{
    public static string ToClassName(this TiknasModalSize size)
    {
        switch (size)
        {
            case TiknasModalSize.Small:
                return "modal-sm";
            case TiknasModalSize.Large:
                return "modal-lg";
            case TiknasModalSize.ExtraLarge:
                return "modal-xl";
            case TiknasModalSize.Default:
                return "";
            case TiknasModalSize.Fullscreen:
                return "modal-fullscreen";
            case TiknasModalSize.FullscreenSmDown:
                return "modal-fullscreen-sm-down";
            case TiknasModalSize.FullscreenMdDown:
                return "modal-fullscreen-md-down";
            case TiknasModalSize.FullscreenLgDown:
                return "modal-fullscreen-lg-down";
            case TiknasModalSize.FullscreenXlDown:
                return "modal-fullscreen-xl-down";
            case TiknasModalSize.FullscreenXxlDown:
                return "modal-fullscreen-xxl-down";
            default:
                throw new TiknasException($"Unknown {nameof(TiknasModalSize)}: {size}");
        }
    }
}
