namespace Tiknas.AspNetCore.Mvc.UI.Theming;

public class TiknasThemingOptions
{
    public ThemeDictionary Themes { get; }

    public string? DefaultThemeName { get; set; }

    public TiknasThemingOptions()
    {
        Themes = new ThemeDictionary();
    }
}
