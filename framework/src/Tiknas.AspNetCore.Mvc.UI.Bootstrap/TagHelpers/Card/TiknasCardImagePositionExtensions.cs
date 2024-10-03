namespace Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public static class TiknasCardImagePositionExtensions
{
    public static string ToClassName(this TiknasCardImagePosition position)
    {
        switch (position)
        {
            case TiknasCardImagePosition.None:
                return "card-img";
            case TiknasCardImagePosition.Top:
                return "card-img-top";
            case TiknasCardImagePosition.Bottom:
                return "card-img-bottom";
            default:
                throw new TiknasException("Unknown TiknasCardImagePosition: " + position);
        }
    }
}
