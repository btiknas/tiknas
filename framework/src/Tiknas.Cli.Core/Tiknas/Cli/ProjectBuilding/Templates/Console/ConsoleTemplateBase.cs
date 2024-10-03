using JetBrains.Annotations;
using Tiknas.Cli.ProjectBuilding.Building;

namespace Tiknas.Cli.ProjectBuilding.Templates.Console;

public abstract class ConsoleTemplateBase : TemplateInfo
{
    protected ConsoleTemplateBase([NotNull] string name) :
        base(name)
    {
    }
}
