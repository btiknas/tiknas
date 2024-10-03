namespace Tiknas.AspNetCore.Components.Web.Theming.Theming;

public class TiknasThemingOptions
{
    public ThemeDictionary Themes { get; }

    public string? DefaultThemeName { get; set; }

    public TiknasThemingOptions()
    {
        Themes = new ThemeDictionary();
    }
}
