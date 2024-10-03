using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tiknas.UI.Navigation;

public class TiknasNavigationOptions
{
    [NotNull]
    public List<IMenuContributor> MenuContributors { get; }

    /// <summary>
    /// Includes the <see cref="StandardMenus.Main"/> by default.
    /// </summary>
    public List<string> MainMenuNames { get; }

    public TiknasNavigationOptions()
    {
        MenuContributors = new List<IMenuContributor>();

        MainMenuNames = new List<string>
            {
                StandardMenus.Main
            };
    }
}
