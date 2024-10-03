using JetBrains.Annotations;
using Tiknas.Cli.ProjectBuilding.Building;

namespace Tiknas.Cli.ProjectBuilding.Templates.Wpf;

public class WpfTemplateBase : TemplateInfo
{
    protected WpfTemplateBase([NotNull] string name) :
        base(name)
    {
    }
}
