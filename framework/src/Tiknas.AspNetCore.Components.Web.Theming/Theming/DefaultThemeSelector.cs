using System.Linq;
using Microsoft.Extensions.Options;
using Tiknas.DependencyInjection;

namespace Tiknas.AspNetCore.Components.Web.Theming.Theming;

public class DefaultThemeSelector : IThemeSelector, ITransientDependency
{
    protected TiknasThemingOptions Options { get; }

    public DefaultThemeSelector(IOptions<TiknasThemingOptions> options)
    {
        Options = options.Value;
    }

    public virtual ThemeInfo GetCurrentThemeInfo()
    {
        if (!Options.Themes.Any())
        {
            throw new TiknasException($"No theme registered! Use {nameof(TiknasThemingOptions)} to register themes.");
        }

        if (Options.DefaultThemeName == null)
        {
            return Options.Themes.Values.First();
        }

        var themeInfo = Options.Themes.Values.FirstOrDefault(t => t.Name == Options.DefaultThemeName);
        if (themeInfo == null)
        {
            throw new TiknasException("Default theme is configured but it's not found in the registered themes: " + Options.DefaultThemeName);
        }

        return themeInfo;
    }
}
